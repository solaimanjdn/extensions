﻿using DocumentFormat.OpenXml.Packaging;
using Signum.Engine.Basics;
using Signum.Engine.DynamicQuery;
using Signum.Engine.Maps;
using Signum.Engine.Operations;
using Signum.Entities;
using Signum.Entities.Basics;
using Signum.Entities.DynamicQuery;
using Signum.Entities.Word;
using Signum.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Signum.Engine.UserAssets;
using Signum.Engine.Templating;
using Signum.Entities.Files;
using Signum.Utilities.DataStructures;
using DocumentFormat.OpenXml;
using W = DocumentFormat.OpenXml.Wordprocessing;
using System.Text.RegularExpressions;
using Signum.Utilities.ExpressionTrees;
using Signum.Entities.UserQueries;
using System.Data;
using Signum.Entities.Chart;

namespace Signum.Engine.Word
{

    public interface IWordDataTableProvider
    {
        string Validate(string suffix, WordTemplateEntity template);
        DataTable GetDataTable(string suffix, WordTemplateLogic.WordContext context);
    }

    public static class WordTemplateLogic
    {
        public static bool AvoidSynchronize = false;

        public static ResetLazy<Dictionary<Lite<WordTemplateEntity>, WordTemplateEntity>> WordTemplatesLazy;

        public static ResetLazy<Dictionary<TypeEntity, List<Lite<WordTemplateEntity>>>> TemplatesByType;

        public static Dictionary<WordTransformerSymbol, Action<WordContext, OpenXmlPackage>> Transformers = new Dictionary<WordTransformerSymbol, Action<WordContext, OpenXmlPackage>>();
        public static Dictionary<WordConverterSymbol, Func<WordContext, byte[], byte[]>> Converters = new Dictionary<WordConverterSymbol, Func<WordContext, byte[], byte[]>>();

        public static Dictionary<string, IWordDataTableProvider> ToDataTableProviders = new Dictionary<string, IWordDataTableProvider>();

        static Expression<Func<SystemWordTemplateEntity, IQueryable<WordTemplateEntity>>> WordTemplatesExpression =
            e => Database.Query<WordTemplateEntity>().Where(a => a.SystemWordTemplate == e);
        [ExpressionField]
        public static IQueryable<WordTemplateEntity> WordTemplates(this SystemWordTemplateEntity e)
        {
            return WordTemplatesExpression.Evaluate(e);
        }

        public static void Start(SchemaBuilder sb, DynamicQueryManager dqm)
        {
            if (sb.NotDefined(MethodInfo.GetCurrentMethod()))
            {
                sb.Include<WordTemplateEntity>()
                    .WithSave(WordTemplateOperation.Save)
                    .WithDelete(WordTemplateOperation.Delete)
                    .WithQuery(dqm, e => new
                    {
                        Entity = e,
                        e.Id,
                        e.Query,
                        e.Template.Entity.FileName
                    });

                SystemWordTemplateLogic.Start(sb, dqm);

                SymbolLogic<WordTransformerSymbol>.Start(sb, dqm, () => Transformers.Keys.ToHashSet());
                SymbolLogic<WordConverterSymbol>.Start(sb, dqm, () => Converters.Keys.ToHashSet());

                sb.Include<WordTransformerSymbol>()
                .WithQuery(dqm, f => new
                {
                    Entity = f,
                    f.Key
                });

                sb.Include<WordConverterSymbol>()
                    .WithQuery(dqm, f => new
                    {
                        Entity = f,
                        f.Key
                    });


                ToDataTableProviders.Add("Model", new ModelDataTableProvider());
                ToDataTableProviders.Add("UserQuery", new UserQueryDataTableProvider());
                ToDataTableProviders.Add("UserChart", new UserChartDataTableProvider());

                dqm.RegisterExpression((SystemWordTemplateEntity e) => e.WordTemplates(), () => typeof(WordTemplateEntity).NiceName());

                
                new Graph<WordTemplateEntity>.Execute(WordTemplateOperation.CreateWordReport)
                {
                    CanExecute = et =>
                    {
                        if (et.SystemWordTemplate != null && SystemWordTemplateLogic.RequiresExtraParameters(et.SystemWordTemplate))
                            return WordTemplateMessage._01RequiresExtraParameters.NiceToString(typeof(SystemWordTemplateEntity).NiceName(), et.SystemWordTemplate);

                        return null;
                    },
                    Execute = (et, args) =>
                    {
                        throw new InvalidOperationException("UI-only operation");
                    }
                }.Register();

                TemplatesByType = sb.GlobalLazy(() =>
                {
                    var list = Database.Query<WordTemplateEntity>().Select(r => KVP.Create(r.Query, r.ToLite())).ToList();

                    return (from kvp in list
                            let imp = dqm.GetEntityImplementations(kvp.Key.ToQueryName())
                            where !imp.IsByAll
                            from t in imp.Types
                            group kvp.Value by t into g
                            select KVP.Create(g.Key.ToTypeEntity(), g.ToList())).ToDictionary();

                }, new InvalidateWith(typeof(WordTemplateEntity)));

                WordTemplatesLazy = sb.GlobalLazy(() => Database.Query<WordTemplateEntity>()
                   .ToDictionary(et => et.ToLite()), new InvalidateWith(typeof(WordTemplateEntity)));

                Schema.Current.Synchronizing += Schema_Synchronize_Tokens;

                Validator.PropertyValidator((WordTemplateEntity e) => e.Template).StaticPropertyValidation += ValidateTemplate;
            }
        }

        public static void RegisterTransformer(WordTransformerSymbol transformerSymbol, Action<WordContext, OpenXmlPackage> transformer)
        {
            if (transformerSymbol == null)
                throw AutoInitAttribute.ArgumentNullException(typeof(WordTransformerSymbol), nameof(transformerSymbol));

            Transformers.Add(transformerSymbol, transformer);
        }

        public class WordContext
        {
            public WordTemplateEntity Template;
            public Entity Entity;
            public ISystemWordTemplate SystemWordTemplate;
        }

        public static void RegisterConverter(WordConverterSymbol converterSymbol, Func<WordContext, byte[], byte[]> converter)
        {
            if (converterSymbol == null)
                throw new ArgumentNullException(nameof(converterSymbol));

            Converters.Add(converterSymbol, converter);
        }

        static string ValidateTemplate(WordTemplateEntity template, PropertyInfo pi)
        {
            if (template.Template == null)
                return null;

            using (template.DisableAuthorization ? ExecutionMode.Global() : null)
            {
                QueryDescription qd = DynamicQueryManager.Current.QueryDescription(template.Query.ToQueryName());

                string error = null;
                template.ProcessOpenXmlPackage(document =>
                {
                    Dump(document, "0.Original.txt");

                    var parser = new TemplateParser(document, qd, template.SystemWordTemplate.ToType(), template);
                    parser.ParseDocument(); Dump(document, "1.Match.txt");
                    parser.CreateNodes(); Dump(document, "2.BaseNode.txt");
                    parser.AssertClean();

                    error = parser.Errors.IsEmpty() ? null :
                        parser.Errors.ToString(e => e.Message, "\r\n");
                });

                return error;
            }
        }

        public static string DumpFileFolder = null;

        public static byte[] CreateReport(this Lite<WordTemplateEntity> liteTemplate, Entity entity, ISystemWordTemplate systemWordTemplate = null, bool avoidConversion = false)
        {
            return liteTemplate.GetFromCache().CreateReport(entity, systemWordTemplate, avoidConversion);
        }

        public static WordTemplateEntity GetFromCache(this Lite<WordTemplateEntity> liteTemplate)
        {
            WordTemplateEntity template = WordTemplatesLazy.Value.GetOrThrow(liteTemplate, "Word report template {0} not in cache".FormatWith(liteTemplate));

            return template;
        }

        public static byte[] CreateReport(this WordTemplateEntity template, Entity entity, ISystemWordTemplate systemWordTemplate = null, bool avoidConversion = false)
        {
            if (systemWordTemplate != null && template.SystemWordTemplate.FullClassName != systemWordTemplate.GetType().FullName)
                throw new ArgumentException("systemWordTemplate should be a {0} instead of {1}".FormatWith(template.SystemWordTemplate.FullClassName, systemWordTemplate.GetType().FullName));

            using (template.DisableAuthorization ? ExecutionMode.Global() : null)
            using (CultureInfoUtils.ChangeBothCultures(template.Culture.ToCultureInfo()))
            {
                QueryDescription qd = DynamicQueryManager.Current.QueryDescription(template.Query.ToQueryName());

                var array = template.ProcessOpenXmlPackage(document =>
                {
                    Dump(document, "0.Original.txt");

                    var parser = new TemplateParser(document, qd, template.SystemWordTemplate.ToType(), template);
                    parser.ParseDocument(); Dump(document, "1.Match.txt");
                    parser.CreateNodes(); Dump(document, "2.BaseNode.txt");
                    parser.AssertClean();

                    if (parser.Errors.Any())
                        throw new InvalidOperationException("Error in template {0}:\r\n".FormatWith(template) + parser.Errors.ToString(e => e.Message, "\r\n"));

                    var renderer = new TemplateRenderer(document, qd, entity, template.Culture.ToCultureInfo(), systemWordTemplate, template);
                    renderer.MakeQuery();
                    renderer.RenderNodes(); Dump(document, "3.Replaced.txt");
                    renderer.AssertClean();

                    FixDocument(document); Dump(document, "4.Fixed.txt");

                    if (template.WordTransformer != null)
                        Transformers.GetOrThrow(template.WordTransformer)(new WordContext
                        {
                            Template = template,
                            Entity = entity,
                            SystemWordTemplate = systemWordTemplate
                        }, document);
                });

                if (!avoidConversion && template.WordConverter != null)
                    array = Converters.GetOrThrow(template.WordConverter)(new WordContext
                    {
                        Template = template,
                        Entity = entity,
                        SystemWordTemplate = systemWordTemplate
                    }, array);

                return array;
            }
        }

        private static void FixDocument(OpenXmlPackage document)
        {
            foreach (var root in document.AllRootElements())
            {
                foreach (var cell in root.Descendants<W.TableCell>().ToList())
                {
                    if (!cell.ChildElements.Any(c => !(c is W.TableCellProperties)))
                        cell.AppendChild(new W.Paragraph());
                }
            }
        }

        private static void Dump(OpenXmlPackage document, string fileName)
        {
            if (DumpFileFolder == null)
                return;

            if (!Directory.Exists(DumpFileFolder))
                Directory.CreateDirectory(DumpFileFolder);

            foreach (var part in AllParts(document).Where(p => p.RootElement != null))
            {
                string fullFileName = Path.Combine(DumpFileFolder, part.Uri.ToString().Replace("/", "_") + "." + fileName);

                File.WriteAllText(fullFileName, part.RootElement.NiceToString());
            }
        }

        static SqlPreCommand Schema_Synchronize_Tokens(Replacements replacements)
        {
            if (AvoidSynchronize)
                return null;

            StringDistance sd = new StringDistance();

            var emailTemplates = Database.Query<WordTemplateEntity>().ToList();

            SqlPreCommand cmd = emailTemplates.Select(uq => SynchronizeWordTemplate(replacements, uq, sd)).Combine(Spacing.Double);

            return cmd;
        }

        internal static SqlPreCommand SynchronizeWordTemplate(Replacements replacements, WordTemplateEntity template, StringDistance sd)
        {
            try
            {
                if (template.Template == null)
                    return null;

                var queryName = QueryLogic.ToQueryName(template.Query.Key);

                QueryDescription qd = DynamicQueryManager.Current.QueryDescription(queryName);

                Console.Clear();

                SafeConsole.WriteLineColor(ConsoleColor.White, "WordTemplate: " + template.Name);
                Console.WriteLine(" Query: " + template.Query.Key);

                var file = template.Template.Retrieve();

                try
                {
                    SyncronizationContext sc = new SyncronizationContext
                    {
                        ModelType = template.SystemWordTemplate.ToType(),
                        QueryDescription = qd,
                        Replacements = replacements,
                        StringDistance = sd,
                        HasChanges = false,
                        Variables = new ScopedDictionary<string, ValueProviderBase>(null),
                    };

                    var bytes = template.ProcessOpenXmlPackage(document =>
                    {
                        Dump(document, "0.Original.txt");

                        var parser = new TemplateParser(document, qd, template.SystemWordTemplate.ToType(), template);
                        parser.ParseDocument(); Dump(document, "1.Match.txt");
                        parser.CreateNodes(); Dump(document, "2.BaseNode.txt");
                        parser.AssertClean();

                        foreach (var root in document.AllRootElements())
                        {
                            foreach (var node in root.Descendants<BaseNode>().ToList())
                            {
                                node.Synchronize(sc);
                            }
                        }

                        if (sc.HasChanges)
                        {
                            Dump(document, "3.Synchronized.txt");
                            var variables = new ScopedDictionary<string, ValueProviderBase>(null);
                            foreach (var root in document.AllRootElements())
                            {
                                foreach (var node in root.Descendants<BaseNode>().ToList())
                                {
                                    node.RenderTemplate(variables);
                                }
                            }

                            Dump(document, "4.Rendered.txt");
                        }
                    });

                    if (!sc.HasChanges)
                        return null;

                    file.AllowChange = true;
                    file.BinaryFile = bytes;

                    using (replacements.WithReplacedDatabaseName())
                        return Schema.Current.Table<FileEntity>().UpdateSqlSync(file, comment: "WordTemplate: " + template.Name);
                }
                catch (TemplateSyncException ex)
                {
                    if (ex.Result == FixTokenResult.SkipEntity)
                        return null;

                    if (ex.Result == FixTokenResult.DeleteEntity)
                        return SqlPreCommandConcat.Combine(Spacing.Simple,
                            Schema.Current.Table<WordTemplateEntity>().DeleteSqlSync(template),
                            Schema.Current.Table<FileEntity>().DeleteSqlSync(file));

                    if (ex.Result == FixTokenResult.ReGenerateEntity)
                        return Regenerate(template, replacements);

                    throw new InvalidOperationException("Unexcpected {0}".FormatWith(ex.Result));
                }
                finally
                {
                    Console.Clear();
                }
            }
            catch (Exception e)
            {
                return new SqlPreCommandSimple("-- Exception in {0}: {1}".FormatWith(template.BaseToString(), e.Message));
            }
        }

        public static byte[] ProcessOpenXmlPackage(this WordTemplateEntity template, Action<OpenXmlPackage> processPackage)
        {
            var file = template.Template.Retrieve();

            using (var memory = new MemoryStream())
            {
                memory.WriteAllBytes(file.BinaryFile);

                var ext = Path.GetExtension(file.FileName).ToLower();

                var document = 
                    ext == ".docx" ? WordprocessingDocument.Open(memory, true) :
                    ext == ".pptx" ? PresentationDocument.Open(memory, true) :
                    new InvalidOperationException("Extension '{0}' not supported".FormatWith(ext)).Throw<OpenXmlPackage>();

                using (document)
                {
                    processPackage(document);
                }

                return memory.ToArray();
            }
        }
        
        public static bool Regenerate(WordTemplateEntity template)
        {
            var result = Regenerate(template, null);
            if (result == null)
                return false;
            
            result.ExecuteLeaves();
            return true;
        }

        private static SqlPreCommand Regenerate(WordTemplateEntity template, Replacements replacements)
        {
            var newTemplate = SystemWordTemplateLogic.CreateDefaultTemplate(template.SystemWordTemplate);

            var file = template.Template.Retrieve();

            using (file.AllowChanges())
            {
                file.BinaryFile = newTemplate.Template.Entity.BinaryFile;
                file.FileName = newTemplate.Template.Entity.FileName;

                return Schema.Current.Table<FileEntity>().UpdateSqlSync(file, comment: "WordTemplate Regenerated: " + template.Name);
            }
        }

        public static IEnumerable<OpenXmlPartRootElement> AllRootElements(this OpenXmlPartContainer document)
        {
            return AllParts(document).Select(p => p.RootElement).NotNull();
        }

        public static IEnumerable<OpenXmlPart> AllParts(this OpenXmlPartContainer container)
        {
            var roots = container.Parts.Select(a => a.OpenXmlPart);
            var result = DirectedGraph<OpenXmlPart>.Generate(roots, c => c.Parts.Select(a => a.OpenXmlPart));
            return result;
        }

        public static void GenerateDefaultTemplates()
        {
            var systemWordTemplates = Database.Query<SystemWordTemplateEntity>().Where(se => !se.WordTemplates().Any(a => a.Active)).ToList();

            List<string> exceptions = new List<string>();

            foreach (var se in systemWordTemplates)
            {
                try
                {
                    SystemWordTemplateLogic.CreateDefaultTemplate(se).Save();
                }
                catch (Exception ex)
                {
                    exceptions.Add("{0} in {1}:\r\n{2}".FormatWith(ex.GetType().Name, se.FullClassName, ex.Message.Indent(4)));
                }
            }

            if (exceptions.Any())
                throw new Exception(exceptions.ToString("\r\n\r\n"));
        }
    }
}
