﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Signum.Engine.Basics;
using Signum.Engine.Chart;
using Signum.Engine.Operations;
using Signum.Entities;
using Signum.Entities.Basics;
using Signum.Entities.Chart;
using Signum.Entities.ControlPanel;
using Signum.Entities.Reflection;
using Signum.Entities.UserQueries;
using Signum.Services;
using Signum.Utilities;
using Signum.Utilities.Reflection;
using Signum.Engine.DynamicQuery;
using Signum.Entities.DynamicQuery;

namespace Signum.Engine.UserQueries
{
    public static class UserAssetsExporter
    {
        class ToXmlContext : IToXmlContext
        {
            public Dictionary<Guid, XElement> elements = new Dictionary<Guid, XElement>();
            public Guid Include(IUserAssetEntity content)
            {
                elements.GetOrCreate(content.Guid, () => content.ToXml(this));

                return content.Guid;
            }

            public string TypeToName(Lite<TypeDN> type)
            {
                return TypeLogic.GetCleanName(TypeLogic.DnToType.GetOrThrow(type.Retrieve()));
            }


            public string QueryToName(Lite<QueryDN> query)
            {
                return query.Retrieve().Key;
            }
        }

        public static byte[] ToXml(params IUserAssetEntity[] entities)
        {
            ToXmlContext ctx = new ToXmlContext();

            foreach (var e in entities)
                ctx.Include(e);

            XDocument doc = new XDocument(
                new XDeclaration("1.0", "UTF8", "yes"),
                new XElement("Entities",
                    ctx.elements.Values));

            return new MemoryStream().Using(s => { doc.Save(s); return s.ToArray(); });
        }
    }

    public static class UserAssetsImporter
    {
        public static Dictionary<string, Type> UserAssetNames = new Dictionary<string, Type>();
        public static Dictionary<string, Type> PartNames = new Dictionary<string, Type>();

        class PreviewContext :IFromXmlContext
        {
            public Dictionary<Guid, IUserAssetEntity> entities = new Dictionary<Guid, IUserAssetEntity>();
            public Dictionary<Guid, XElement> elements;
            public Dictionary<Guid, UserAssetPreviewLine> previews = new Dictionary<Guid, UserAssetPreviewLine>();

            public PreviewContext(XDocument doc)
            {
                elements = doc.Element("Entities").Elements().ToDictionary(a => Guid.Parse(a.Attribute("Guid").Value));
            }

            QueryDN IFromXmlContext.GetQuery(string queryKey)
            {
                return QueryLogic.GetQuery(QueryLogic.ToQueryName(queryKey));
            }

            public IUserAssetEntity GetEntity(Guid guid)
            {
                return entities.GetOrCreate(guid, () =>
                {
                    var element = elements.GetOrThrow(guid);

                    Type type = UserAssetNames.GetOrThrow(element.Name.ToString());

                    var entity = giRetrieveOrCreate.GetInvoker(type)(guid);

                    entity.FromXml(element, this);

                    previews.Add(guid, new UserAssetPreviewLine
                    {
                        Text = entity.ToString(),
                        Type = entity.GetType(),
                        Guid = guid,
                        Action = entity.IsNew ? EntityAction.New :
                                 GraphExplorer.FromRoot((IdentifiableEntity)entity).Any(a => a.Modified != ModifiedState.Clean) ? EntityAction.Different :
                                 EntityAction.Identical,
                    });

                    return entity;
                });
            }

            public Lite<TypeDN> NameToType(string cleanName)
            {
                return TypeLogic.TypeToDN.GetOrThrow(TypeLogic.GetType(cleanName)).ToLite();
            }

            public IPartDN GetPart(IPartDN old, XElement element)
            {
                Type type = PartNames.GetOrThrow(element.Name.ToString());

                var part = old.GetType() == type ? old : (IPartDN)Activator.CreateInstance(type);

                part.FromXml(element, this);

                return part;
            }


            public Lite<TypeDN> GetType(string cleanName)
            {
                return TypeLogic.GetType(cleanName).ToTypeDN().ToLite();
            }

            public ChartScriptDN ChartScript(string chartScriptName)
            {
                return ChartScriptLogic.GetChartScript(chartScriptName);
            }

            public QueryDescription GetQueryDescription(QueryDN Query)
            {
                return DynamicQueryManager.Current.QueryDescription(QueryLogic.QueryNames.GetOrThrow(Query.Key));
            }
        }

        public static UserAssetPreviewModel Preview(byte[] doc)
        {
            XDocument document = new MemoryStream(doc).Using(XDocument.Load);

            PreviewContext ctx = new PreviewContext(document);

            foreach (var item in ctx.elements)
                ctx.GetEntity(item.Key);

            return new UserAssetPreviewModel { Lines = ctx.previews.Values.ToMList() };
        }

        class ImporterContext : IFromXmlContext
        {
            Dictionary<Guid, bool> overrideEntity;
            Dictionary<Guid, IUserAssetEntity> entities = new Dictionary<Guid, IUserAssetEntity>();
            public List<IPartDN> toRemove = new List<IPartDN>();
            public Dictionary<Guid, XElement> elements;

            public ImporterContext(XDocument doc, Dictionary<Guid, bool> overrideEntity)
            {
                this.overrideEntity = overrideEntity;
                elements = doc.Element("Entities").Elements().ToDictionary(a => Guid.Parse(a.Attribute("Guid").Value));
            }

            QueryDN IFromXmlContext.GetQuery(string queryKey)
            {
                return QueryLogic.GetQuery(QueryLogic.ToQueryName(queryKey));
            }

            public IUserAssetEntity GetEntity(Guid guid)
            {
                return entities.GetOrCreate(guid, () =>
                {
                    var element = elements.GetOrThrow(guid);

                    Type type = UserAssetNames.GetOrThrow(element.Name.ToString());

                    var entity = giRetrieveOrCreate.GetInvoker(type)(guid);

                    if (entity.IsNew || overrideEntity.ContainsKey(guid))
                    {
                        entity.FromXml(element, this);
                        using (OperationLogic.AllowSave(entity.GetType()))
                            entity.Save();
                    }

                    return entity;
                });
            }

            public Lite<TypeDN> NameToType(string cleanName)
            {
                return TypeLogic.TypeToDN.GetOrThrow(TypeLogic.GetType(cleanName)).ToLite();
            }

            public IPartDN GetPart(IPartDN old, XElement element)
            {
                Type type = PartNames.GetOrThrow(element.Name.ToString());

                var part = old.GetType() == type ? old : (IPartDN)Activator.CreateInstance(type);

                part.FromXml(element, this);

                if (part != old)
                    toRemove.Add(old);

                return part;
            }

            public Lite<TypeDN> GetType(string cleanName)
            {
                return TypeLogic.GetType(cleanName).ToTypeDN().ToLite();
            }

            public ChartScriptDN ChartScript(string chartScriptName)
            {
                return ChartScriptLogic.GetChartScript(chartScriptName); 
            }

            public QueryDescription GetQueryDescription(QueryDN Query)
            {
                return DynamicQueryManager.Current.QueryDescription(QueryLogic.QueryNames.GetOrThrow(Query.Key));
            }
        }

        public static void Import(byte[] document, UserAssetPreviewModel preview)
        {
            using (Transaction tr = new Transaction())
            {
                var doc = new MemoryStream(document).Using(XDocument.Load);

                ImporterContext importer = new ImporterContext(doc,
                    preview.Lines
                    .Where(a => a.Action == EntityAction.Different)
                    .ToDictionary(a => a.Guid, a => a.OverrideEntity));

                foreach (var item in importer.elements)
                    importer.GetEntity(item.Key);

                Database.DeleteList(importer.toRemove);

                tr.Commit();
            }

        }

        static readonly GenericInvoker<Func<Guid, IUserAssetEntity>> giRetrieveOrCreate = new GenericInvoker<Func<Guid, IUserAssetEntity>>(
            guid => RetrieveOrCreate<UserQueryDN>(guid));
        static T RetrieveOrCreate<T>(Guid guid) where T : IdentifiableEntity, IUserAssetEntity, new()
        {
            var result = Database.Query<T>().SingleOrDefaultEx(a => a.Guid == guid);

            if (result != null)
                return result;

            return new T { Guid = guid };
        }
    }
}
