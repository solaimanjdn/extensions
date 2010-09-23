﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Signum.Entities.Basics;
using Signum.Entities;
using Signum.Utilities;
using Signum.Engine.Maps;
using System.Linq.Expressions;
using Signum.Entities.Reflection;
using Signum.Utilities.ExpressionTrees;
using Signum.Utilities.Reflection;
using Signum.Utilities.DataStructures;
using Signum.Engine.Linq;

namespace Signum.Engine.Basics
{
    public static class EntityGroupLogic
    {
        internal interface IEntityGroupInfo
        {
            bool IsInGroup(IdentifiableEntity entity);
            LambdaExpression IsInGroupUntypedExpression { get; }

            bool IsApplicable(IdentifiableEntity entity);
            LambdaExpression IsApplicableUntypedExpression { get; }
        }

        internal class EntityGroupInfo<T> : IEntityGroupInfo
            where T : IdentifiableEntity
        {
            public EntityGroupInfo(Expression<Func<T, bool>> isInGroup, Expression<Func<T, bool>> isApplicable)
            {
                if (isInGroup != null)
                {
                    IsInGroupExpression = (Expression<Func<T, bool>>)DataBaseTransformer.ToDatabase(isInGroup);
                    IsInGroupFuncExpression = (Expression<Func<T, bool>>)MemoryTransformer.ToMemory(isInGroup);
                    IsInGroupFunc = IsInGroupFuncExpression.Compile();
                }

                if (isApplicable != null)
                {
                    IsApplicableExpression = (Expression<Func<T, bool>>)DataBaseTransformer.ToDatabase(isApplicable);
                    IsApplicableFuncExpression = (Expression<Func<T, bool>>)MemoryTransformer.ToMemory(isApplicable);
                    IsApplicableFunc = IsApplicableFuncExpression.Compile();
                }
            }

            public readonly Expression<Func<T, bool>> IsInGroupExpression;
            readonly Expression<Func<T, bool>> IsInGroupFuncExpression; //debugging purposes only
            readonly Func<T, bool> IsInGroupFunc;

            public readonly Expression<Func<T, bool>> IsApplicableExpression;
            readonly Expression<Func<T, bool>> IsApplicableFuncExpression; //debugging purposes only
            readonly Func<T, bool> IsApplicableFunc;

            public bool IsInGroup(IdentifiableEntity entity)
            {
                if (IsInGroupFunc == null)
                    return true;

                return IsInGroupFunc((T)entity);
            }

            public bool IsApplicable(IdentifiableEntity entity)
            {
                if (IsApplicableFunc == null)
                    return true;

                return IsApplicableFunc((T)entity);
            }

            public LambdaExpression IsInGroupUntypedExpression
            {
                get { return IsInGroupExpression; }
            }

            public LambdaExpression IsApplicableUntypedExpression
            {
                get { return IsInGroupExpression; }
            }
        }

        static Dictionary<Type, Dictionary<Enum, IEntityGroupInfo>> infos = new Dictionary<Type, Dictionary<Enum, IEntityGroupInfo>>();

        public static HashSet<Enum> groups;
        public static HashSet<Enum> Groups
        {
            get { return groups ?? (groups = infos.SelectMany(a => a.Value.Keys).ToHashSet()); }
        }

        public static IEnumerable<Type> Types
        {
            get { return infos.Keys; }
        }

        public static void Start(SchemaBuilder sb)
        {
            if (sb.NotDefined(MethodInfo.GetCurrentMethod()))
            {
                EnumLogic<EntityGroupDN>.Start(sb, () => Groups);
            }
        }

        public static void Register<T>(Enum entityGroupKey, Expression<Func<T, bool>> isInGroup)
            where T : IdentifiableEntity
        {
            infos.GetOrCreate(typeof(T))[entityGroupKey] = new EntityGroupInfo<T>(isInGroup, null);
        }

        public static void Register<T>(Enum entityGroupKey, Expression<Func<T, bool>> isInGroup, Expression<Func<T, bool>> isApplicable)
            where T : IdentifiableEntity
        {
            infos.GetOrCreate(typeof(T))[entityGroupKey] = new EntityGroupInfo<T>(isInGroup, isApplicable);
        }

        [MethodExpander(typeof(IsInGroupExpander))]
        public static bool IsInGroup(this IdentifiableEntity entity, Enum entityGroupKey)
        {
            IEntityGroupInfo info = GetEntityGroupInfo(entityGroupKey, entity.GetType());

            return info.IsInGroup(entity);
        }


        class IsInGroupExpander : IMethodExpander
        {
            public Expression Expand(Expression instance, Expression[] arguments, Type[] typeArguments)
            {
                Expression entity = arguments[0];
                Enum eg = (Enum)ExpressionEvaluator.Eval(arguments[1]);

                return Expression.Invoke(GetEntityGroupInfo(eg, entity.Type).IsInGroupUntypedExpression, entity);
            }
        }


        [MethodExpander(typeof(IsApplicableExpander))]
        public static bool IsApplicable(this IdentifiableEntity entity, Enum entityGroupKey)
        {
            IEntityGroupInfo info = GetEntityGroupInfo(entityGroupKey, entity.GetType());

            return info.IsApplicable(entity);
        }


        class IsApplicableExpander : IMethodExpander
        {
            public Expression Expand(Expression instance, Expression[] arguments, Type[] typeArguments)
            {
                Expression entity = arguments[0];
                Enum eg = (Enum)ExpressionEvaluator.Eval(arguments[1]);

                return Expression.Invoke(GetEntityGroupInfo(eg, entity.Type).IsApplicableUntypedExpression, entity);
            }
        }

        [MethodExpander(typeof(WhereInGroupExpander))]
        public static IQueryable<T> WhereInGroup<T>(this IQueryable<T> query, Enum entityGroupKey)
            where T : IdentifiableEntity
        {
            EntityGroupInfo<T> info = (EntityGroupInfo<T>)GetEntityGroupInfo(entityGroupKey, typeof(T));

            return query.Where(info.IsInGroupExpression);
        }

        class WhereInGroupExpander : IMethodExpander
        {
            static MethodInfo miWhere = ReflectionTools.GetMethodInfo(() => Queryable.Where<int>(null, i => i == 0)).GetGenericMethodDefinition();

            public Expression Expand(Expression instance, Expression[] arguments, Type[] typeArguments)
            {
                Expression group = arguments[1];
                if (group.NodeType == ExpressionType.Convert)
                    group = ((UnaryExpression)group).Operand;
 
                Type type = typeArguments[0];

                IEntityGroupInfo info = GetEntityGroupInfo((Enum)((ConstantExpression)group).Value, type);

                return Expression.Call(null, miWhere.MakeGenericMethod(type), arguments[0], info.IsInGroupUntypedExpression);
            }
        }

        public static IEnumerable<Enum> GroupsFor(Type type)
        {
            var dic = infos.TryGetC(type);
            if (dic != null)
                return dic.Keys;
            return NoGroups;
        }

        static readonly Enum[] NoGroups = new Enum[0];

        internal static IEntityGroupInfo GetEntityGroupInfo(Enum entityGroupKey, Type type)
        {
            IEntityGroupInfo info = infos
               .GetOrThrow(type, "There's no EntityGroup expression registered for type {0}")
               .GetOrThrow(entityGroupKey, "There's no EntityGroup expression registered for type {0} with key {{0}}".Formato(type));
            return info;
        }

        internal static EntityGroupInfo<T> GetEntityGroupInfo<T>(Enum entityGroupKey)
            where T : IdentifiableEntity
        {
            return (EntityGroupInfo<T>)GetEntityGroupInfo(entityGroupKey, typeof(T));
        }

        static MethodInfo miSmartRetrieve = ReflectionTools.GetMethodInfo(() => SmartRetrieve<IdentifiableEntity>(null)).GetGenericMethodDefinition();

        public static T SmartRetrieve<T>(this Lite<T> lite) where T : class, IIdentifiable
        {
            throw new InvalidOperationException("This methid is ment to be used only in declaration of entity groups"); 
        }

        static MethodInfo miSmartTypeIs = ReflectionTools.GetMethodInfo(() => SmartTypeIs<IdentifiableEntity>(null)).GetGenericMethodDefinition();

        public static bool SmartTypeIs<T>(this Lite lite)
        {
            throw new InvalidOperationException("This methid is ment to be used only in declaration of entity groups");
        }

        internal class DataBaseTransformer : SimpleExpressionVisitor
        {
            public static Expression ToDatabase(Expression exp)
            {
                DataBaseTransformer dbt = new DataBaseTransformer();
                return dbt.Visit(exp);
            }

            protected override Expression VisitMethodCall(MethodCallExpression m)
            {
                if (m.Method.IsInstantiationOf(miSmartRetrieve))
                {
                    return Expression.Property(base.Visit(m.Arguments[0]), "Entity");
                }
                else if (m.Method.IsInstantiationOf(miSmartTypeIs))
                {
                    return Expression.TypeIs(Expression.Property(base.Visit(m.Arguments[0]), "Entity"), m.Method.GetGenericArguments()[0]);
                }

                return base.VisitMethodCall(m);
            }
        }

        class SrChain
        {
            public SrChain(MethodCallExpression expression, SrChain parent)
            {
                this.Expression = expression;
                this.Parent = parent; 
            }

            public readonly MethodCallExpression Expression;
            public readonly SrChain Parent; 
        }

        class MemoryNominator : SimpleExpressionVisitor
        {
            class NominatorResult
            {
                public List<SrChain> FreeSRs = new List<SrChain>();
                public Dictionary<Expression, List<SrChain>> MINs = new Dictionary<Expression, List<SrChain>>();
            }

            NominatorResult data = new NominatorResult();

            public static Dictionary<Expression, List<SrChain>> Nominate(Expression exp)
            {
                MemoryNominator mn = new MemoryNominator();
                mn.Visit(exp);

                if (mn.data.FreeSRs.Any())
                    throw new InvalidOperationException("Impossible to transform SmartRetrieves in expression: {0}".Formato(exp.NiceToString()));
                    
                return mn.data.MINs;
            }

            protected override Expression Visit(Expression exp)
            {
                NominatorResult old = data;
                data = new NominatorResult();

                base.Visit(exp);

                if (data.MINs.Any())
                {
                    if (data.FreeSRs.Any())
                        throw new InvalidOperationException("Impossible to transform SmartRetrieves in expression: {0}".Formato(exp.NiceToString()));
                    else
                    {
                        old.MINs.AddRange(data.MINs);
                    }
                }
                else
                {
                    if (data.FreeSRs.Any())
                    {
                        if (exp.Type == typeof(bool))
                        {
                            old.MINs.Add(exp, data.FreeSRs);
                        }
                        else
                        {
                            if (old.FreeSRs.Count > 1) //Se nos mezclan cadenas
                                throw new InvalidOperationException("Impossible to transform SmartRetrieves in expression: {0}".Formato(exp.NiceToString()));

                            old.FreeSRs.AddRange(data.FreeSRs);
                        }
                    }
                    else
                    {
                        //nothing to do
                    }
                }
                
                data = old;

                return exp; 
            }

            protected override Expression VisitMethodCall(MethodCallExpression m)
            {
                base.VisitMethodCall(m);

                if (m.Method.IsInstantiationOf(miSmartRetrieve))
                {
                    if (data.FreeSRs.Count == 0)
                        data.FreeSRs.Add(new SrChain(m, null));
                    else //there should be only one
                        data.FreeSRs[0] = new SrChain(m, data.FreeSRs[0]);
                }

                return m; 
            }

        }

        internal class MemoryOnlyTransformer : SimpleExpressionVisitor
        {
            public static Expression ToMemory(Expression exp)
            {
                MemoryOnlyTransformer dbt = new MemoryOnlyTransformer();
                return dbt.Visit(exp);
            }

            protected override Expression VisitMethodCall(MethodCallExpression m)
            {
                if (m.Method.IsInstantiationOf(miSmartRetrieve))
                {
                    return Expression.Property(base.Visit(m.Arguments[0]), "Entity");
                }
                else if (m.Method.IsInstantiationOf(miSmartTypeIs))
                {
                    return Expression.Equal(Expression.Property(base.Visit(m.Arguments[0]), "RuntimeType"), Expression.Constant(m.Method.GetGenericArguments()[0], typeof(Type)));
                }

                return base.VisitMethodCall(m);
            }
        }

        internal class MemoryTransformer : SimpleExpressionVisitor
        {
            Dictionary<Expression, List<SrChain>> nominations;

            public static Expression ToMemory(Expression exp)
            {
                MemoryTransformer tr = new MemoryTransformer(){ nominations =  MemoryNominator.Nominate(exp)};

                return tr.Visit(exp);
            }

            protected override Expression Visit(Expression exp)
            {
                if (exp == null)
                    return null;

                List<SrChain> chains = nominations.TryGetC(exp);
                if (chains != null)
                    return Dispatch(exp, ImmutableStack<MethodCallExpression>.Empty, chains);

                return base.Visit(exp);
            }

            protected override Expression VisitMethodCall(MethodCallExpression m)
            {
                if (m.Method.IsInstantiationOf(miSmartTypeIs))
                {
                    return Expression.Equal(Expression.Property(base.Visit(m.Arguments[0]), "RuntimeType"), Expression.Constant(m.Method.GetGenericArguments()[0], typeof(Type)));
                }

                return base.VisitMethodCall(m);
            }

            static Expression Dispatch(Expression exp, ImmutableStack<MethodCallExpression> liteAssumptions,  IEnumerable<SrChain> chains)
            {
                if (chains.Empty())
                    return CaseBody(exp, liteAssumptions);


                return chains.First().FollowC(c => c.Parent).Select(c => c.Expression).Aggregate(Dispatch(exp, liteAssumptions, chains.Skip(1)),
                        (ac, sr) => Expression.Condition(LiteIsThin(DataBaseTransformer.ToDatabase(sr.Arguments[0])), Dispatch(exp, liteAssumptions.Push(sr), chains.Skip(1)), ac));

                //3 chains joinng in a Min not considered
            }

            static Expression LiteIsThin(Expression liteExpression)
            {
                return Expression.Equal(Expression.Property(liteExpression, "EntityOrNull"), 
                    Expression.Constant(null)); 
            }

            static MethodInfo miInDB = ReflectionTools.GetMethodInfo(() => Database.InDB<IdentifiableEntity>((Lite<IdentifiableEntity>)null)).GetGenericMethodDefinition();
            static MethodInfo miAny = ReflectionTools.GetMethodInfo(() => Queryable.Any<IdentifiableEntity>(null, null)).GetGenericMethodDefinition();

            static Expression CaseBody(Expression exp, ImmutableStack<MethodCallExpression> liteAsumptions)
            {
                if (liteAsumptions.Empty())
                    return MemoryOnlyTransformer.ToMemory(exp);

                AliasGenerator ag = new AliasGenerator();

                var dict = liteAsumptions.ToDictionary(sr => sr, sr => Expression.Parameter(sr.Type, ag.GetNextTableAlias(sr.Type)));

                var body = DataBaseTransformer.ToDatabase(SrReplacer.Replace(exp, dict)); // The only difference is the way SmartTypeIs works

                return dict.Aggregate(body, (acum, kvp)=>
                    Expression.Call(miAny.MakeGenericMethod(kvp.Key.Type),
                        Expression.Call(miInDB.MakeGenericMethod(kvp.Key.Type),
                         DataBaseTransformer.ToDatabase(kvp.Key.Arguments[0])),
                         Expression.Lambda(acum , kvp.Value)));
            }
        }

        public class SrReplacer : SimpleExpressionVisitor
        {
            Dictionary<MethodCallExpression, ParameterExpression> replacements = new Dictionary<MethodCallExpression, ParameterExpression>();

            public static Expression Replace(Expression expression, Dictionary<MethodCallExpression, ParameterExpression> replacements)
            {
                var replacer = new SrReplacer()
                {
                    replacements = replacements
                };

                return replacer.Visit(expression);
            }

            protected override Expression VisitMethodCall(MethodCallExpression m)
            {
                return replacements.TryGetC(m) ?? base.VisitMethodCall(m);
            }
        }      
    }
}
