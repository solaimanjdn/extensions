﻿using Signum.Entities.UserAssets;
using Signum.React.Json;
using Signum.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Signum.Engine.DynamicQuery;
using Signum.Engine.Basics;
using Signum.React.UserAssets;
using Signum.Entities;
using Signum.React.ApiControllers;
using Signum.Entities.DynamicQuery;
using Signum.React.Maps;
using Signum.React.Facades;
using Signum.Engine.Cache;
using Signum.Entities.Cache;
using Signum.Engine.Authorization;
using Signum.Engine.Maps;
using Signum.Entities.Mailing;
using Signum.Entities.Templating;

namespace Signum.React.Mailing
{
    public static class MailingServer
    {
        public static void Start(HttpConfiguration config)
        {
            SignumControllerFactory.RegisterArea(MethodInfo.GetCurrentMethod());

            ReflectionServer.RegisterLike(typeof(TemplateTokenMessage));

            EntityJsonConverter.AfterDeserilization.Register((EmailTemplateEntity ue) =>
            {
                var qd = DynamicQueryManager.Current.QueryDescription(ue.Query.ToQueryName());
                ue.ParseData(qd);
            });
        }
    }
}