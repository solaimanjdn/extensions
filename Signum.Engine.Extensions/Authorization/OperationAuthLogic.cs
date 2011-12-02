﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Signum.Engine.Maps;
using Signum.Entities.Authorization;
using Signum.Entities.Basics;
using Signum.Engine.DynamicQuery;
using Signum.Engine.Basics;
using Signum.Utilities;
using Signum.Utilities.DataStructures;
using System.Threading;
using Signum.Entities;
using Signum.Entities.Operations;
using Signum.Engine.Operations;
using System.Reflection;
using Signum.Engine.Extensions.Properties;

namespace Signum.Engine.Authorization
{
    public static class OperationAuthLogic
    {
        static AuthCache<RuleOperationDN, OperationAllowedRule, OperationDN, Enum, bool> cache;

        public static IManualAuth<Enum, bool> Manual { get { return cache; } }

        public static bool IsStarted { get { return cache != null; } }

        public static void Start(SchemaBuilder sb)
        {
            if (sb.NotDefined(MethodInfo.GetCurrentMethod()))
            {
                AuthLogic.AssertStarted(sb);
                OperationLogic.AssertStarted(sb);

                OperationLogic.AllowOperation += new AllowOperationHandler(OperationLogic_AllowOperation);

                cache = new AuthCache<RuleOperationDN, OperationAllowedRule, OperationDN, Enum, bool>(sb,
                     EnumLogic<OperationDN>.ToEnum,
                     EnumLogic<OperationDN>.ToEntity,
                     AuthUtils.MaxBool,
                     AuthUtils.MinBool);

                AuthLogic.ExportToXml += () => cache.ExportXml("Operations", "Operation", p => p.Key, b => b.ToString());
                AuthLogic.ImportFromXml += (x, roles) => cache.ImportXml(x, "Operations", "Operation", roles, EnumLogic<OperationDN>.ToEntity, bool.Parse);
            }
        }

        static bool OperationLogic_AllowOperation(Enum operationKey)
        {
            return GetOperationAllowed(operationKey);
        }

        public static List<OperationInfo> Filter(List<OperationInfo> operationInfos)
        {
            return operationInfos.Where(ai => GetOperationAllowed(ai.Key)).ToList(); 
        }

        public static OperationRulePack GetOperationRules(Lite<RoleDN> roleLite, TypeDN typeDN)
        {
            var resources = OperationLogic.GetAllOperationInfos(TypeLogic.DnToType[typeDN]).Select(a => EnumLogic<OperationDN>.ToEntity(a.Key));
            var result = new OperationRulePack { Role = roleLite, Type = typeDN, };
            cache.GetRules(result, resources);
            return result;
        }

        public static void SetOperationRules(OperationRulePack rules)
        {
            var keys = OperationLogic.GetAllOperationInfos(TypeLogic.DnToType[rules.Type])
                .Select(a => OperationDN.UniqueKey(a.Key)).ToArray();

            cache.SetRules(rules, r => keys.Contains(r.Key));
        }

        public static bool GetOperationAllowed(Lite<RoleDN> role, Enum operationKey)
        {
            return cache.GetAllowed(role, operationKey);
        }

        public static bool GetOperationAllowed(Enum operationKey)
        {
            if (!AuthLogic.IsEnabled || Schema.Current.InGlobalMode)
                return true;

            if (GetTemporallyAllowed(operationKey))
                return true;

            return cache.GetAllowed(RoleDN.Current.ToLite(), operationKey);
        }

        public static AuthThumbnail? GetAllowedThumbnail(Lite<RoleDN> role, Type entityType)
        {
            return OperationLogic.GetAllOperationInfos(entityType).Select(oi => cache.GetAllowed(role, oi.Key)).Collapse();
        }

        public static DefaultDictionary<Enum, bool> OperationRules()
        {
            return cache.GetDefaultDictionary();
        }

        static readonly Variable<ImmutableStack<Enum>> tempAllowed = Statics.ThreadVariable<ImmutableStack<Enum>>("authTempOperationsAllowed");

        public static IDisposable AllowTemporally(Enum operationKey)
        {
            tempAllowed.Value = (tempAllowed.Value ?? ImmutableStack<Enum>.Empty).Push(operationKey);

            return new Disposable(() => tempAllowed.Value = tempAllowed.Value.Pop());
        }

        internal static bool GetTemporallyAllowed(Enum operationKey)
        {
            var ta = tempAllowed.Value;
            if (ta == null || ta.IsEmpty)
                return false;

            return ta.Contains(operationKey);
        }
    }
}
