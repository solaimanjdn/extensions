using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Mvc;
using Signum.Utilities;
using System.Web.UI;
using System.Web.Routing;
using Signum.Entities;
using Signum.Entities.Reports;
using Signum.Entities.Basics;
using Signum.Entities.DynamicQuery;
using Signum.Engine.DynamicQuery;
using Signum.Engine.Reports;
using Signum.Engine;
using Signum.Engine.Basics;
using Signum.Entities.UserQueries;
using Signum.Engine.UserQueries;
using Signum.Engine.Authorization;
using Signum.Entities.Authorization;
using Signum.Web.Operations;
using Signum.Web.Basic;
using Signum.Web.Extensions.UserQueries;

namespace Signum.Web.UserQueries
{
    public static class UserQueriesClient
    {
        public const string QueryKey = "QueryKey";

        public static string ViewPrefix = "~/UserQueries/Views/{0}.cshtml";
        public static string Module = "Extensions/Signum.Web.Extensions/UserQueries/Scripts/UserQuery";

        public static Mapping<QueryTokenDN> QueryTokenMapping = ctx =>
        {
			string tokenStr = UserQueriesHelper.GetTokenString(ctx);

            string queryKey = ctx.Parent.Parent.Parent.Parent.Inputs[TypeContextUtilities.Compose("Query", "Key")];
            object queryName = QueryLogic.ToQueryName(queryKey);
            QueryDescription qd = DynamicQueryManager.Current.QueryDescription(queryName);
            return new QueryTokenDN(QueryUtils.Parse(tokenStr, qd, canAggregate: false));
        };

        public static void Start()
        {
            if (Navigator.Manager.NotDefined(MethodInfo.GetCurrentMethod()))
            {
                QueryClient.Start();

                Navigator.RegisterArea(typeof(UserQueriesClient));

                UserAssetsClient.Start();
                UserAssetsClient.RegisterExportAssertLink<UserQueryDN>();

                RouteTable.Routes.MapRoute(null, "UQ/{webQueryName}/{lite}",
                    new { controller = "UserQueries", action = "View" });

                Navigator.AddSettings(new List<EntitySettings>
                {
                    new EntitySettings<UserQueryDN> { PartialViewName = e => ViewPrefix.Formato("UserQuery") },
                    
                    new EmbeddedEntitySettings<QueryFilterDN>
                    { 
                        PartialViewName = e => ViewPrefix.Formato("QueryFilter"), 
                        MappingDefault = new EntityMapping<QueryFilterDN>(false)
                            .SetProperty(a=>a.Token, QueryTokenMapping)
                            .CreateProperty(a=>a.Operation)
                            .CreateProperty(a=>a.ValueString)
                    },

                    new EmbeddedEntitySettings<QueryColumnDN>
                    { 
                        PartialViewName = e => ViewPrefix.Formato("QueryColumn"), 
                        MappingDefault = new EntityMapping<QueryColumnDN>(false)
                            .SetProperty(a=>a.Token, QueryTokenMapping)
                            .CreateProperty(a=>a.DisplayName)
                    },

                    new EmbeddedEntitySettings<QueryOrderDN>
                    { 
                        PartialViewName = e => ViewPrefix.Formato("QueryOrder"), 
                        MappingDefault = new EntityMapping<QueryOrderDN>(false)
                            .SetProperty(a=>a.Token, QueryTokenMapping)
                            .CreateProperty(a=>a.OrderType)
                            
                    },
                });

                ButtonBarQueryHelper.RegisterGlobalButtons(ButtonBarQueryHelper_GetButtonBarForQueryName);

                OperationClient.AddSettings(new List<OperationSettings>
                {
                    new EntityOperationSettings(UserQueryOperation.Save)
                    {
                        OnClick = ctx => new JsOperationFunction(Module, "saveUserQuery", ctx.Url.Action((UserQueriesController uq)=>uq.Save()))
                    },
                    new EntityOperationSettings(UserQueryOperation.Delete)
                    {
                        OnClick = ctx => new JsOperationFunction(Module, "deleteUserQuery", Navigator.FindRoute( ((UserQueryDN)ctx.Entity).Query.ToQueryName()))
                    }
                });

                LinksClient.RegisterEntityLinks<IdentifiableEntity>((entity, ctrl) =>
                {
                    if (!UserQueryPermission.ViewUserQuery.IsAuthorized())
                        return null;

                    return UserQueryLogic.GetUserQueriesEntity(entity.EntityType)
                        .Select(cp => new UserQueryQuickLink(cp, entity)).ToArray();
                });
            }
        }

        class UserQueryQuickLink : QuickLink
        {
            Lite<UserQueryDN> userQuery;
            Lite<IdentifiableEntity> entity;

            public UserQueryQuickLink(Lite<UserQueryDN> userQuery, Lite<IdentifiableEntity> entity)
            {
                this.Text = userQuery.ToString();
                this.userQuery = userQuery;
                this.entity = entity;
                this.IsVisible = true;
            }

            public override MvcHtmlString Execute()
            {
                return new HtmlTag("a").Attr("href", RouteHelper.New().Action((UserQueriesController c) => c.View(userQuery, null, entity))).SetInnerText(Text);
            }
        }

        static ToolBarButton[] ButtonBarQueryHelper_GetButtonBarForQueryName(QueryButtonContext ctx)
        {
            if (ctx.Prefix.HasText())
                return null;

            if (!Navigator.IsNavigable(typeof(UserQueryDN), null, isSearchEntity: true))
                return null;

            var items = new List<IMenuItem>();

            Lite<UserQueryDN> currentUserQuery = null;
            string url = (ctx.ControllerContext.RouteData.Route as Route).Try(r => r.Url);
            if (url.HasText() && url.Contains("UQ"))
                currentUserQuery = Lite.Create<UserQueryDN>(int.Parse(ctx.ControllerContext.RouteData.Values["lite"].ToString()));

            foreach (var uq in UserQueryLogic.GetUserQueries(ctx.QueryName))
            {
                items.Add(new MenuItem
                {
                    Text = uq.ToString(),
                    Title = uq.ToString(),
                    Href = RouteHelper.New().Action<UserQueriesController>(uqc => uqc.View(uq, null, null)), 
                    CssClass = "sf-userquery" + (currentUserQuery.Is(uq) ? " sf-userquery-selected" : "")
                });
            }

            if (items.Count > 0)
                items.Add(new MenuItemSeparator());

            if (Navigator.IsCreable(typeof(UserQueryDN), isSearchEntity:true))
            {
                items.Add(new MenuItem
                {
                    Id = TypeContextUtilities.Compose(ctx.Prefix, "qbUserQueryNew"),
                    Title = UserQueryMessage.UserQueries_CreateNew.NiceToString(),
                    Text = UserQueryMessage.UserQueries_CreateNew.NiceToString(),
                    OnClick = new JsFunction(Module,  "createUserQuery", ctx.Prefix, ctx.Url.Action("Create", "UserQueries"))
                });
            }

            if (currentUserQuery != null && currentUserQuery.IsAllowedFor(TypeAllowedBasic.Modify, inUserInterface: true))
            {
                items.Add(new MenuItem
                {
                    Id = TypeContextUtilities.Compose(ctx.Prefix, "qbUserQueryEdit"),
                    Title = UserQueryMessage.UserQueries_Edit.NiceToString(),
                    Text = UserQueryMessage.UserQueries_Edit.NiceToString(),
                    Href = Navigator.NavigateRoute(currentUserQuery)
                });
            }

            string uqUserQueriesText = UserQueryMessage.UserQueries_UserQueries.NiceToString();
            return new ToolBarButton[]
            {
                new ToolBarDropDown
                { 
                    Id = TypeContextUtilities.Compose(ctx.Prefix, "tmUserQueries"),
                    Title = uqUserQueriesText,
                    Text = uqUserQueriesText,
                    Items = items
                }
            };
        }

        public static void ApplyUserQuery(this FindOptions findOptions, UserQueryDN userQuery)
        {
            if (!userQuery.WithoutFilters)
            {
                findOptions.FilterOptions.RemoveAll(fo => !fo.Frozen);

                findOptions.FilterOptions.AddRange(userQuery.Filters.Select(qf => new FilterOption
                {
                    Token = qf.Token.Token,
                    ColumnName = qf.Token.TokenString,
                    Operation = qf.Operation,
                    Value = qf.Value
                }));
            }

            findOptions.ColumnOptionsMode = userQuery.ColumnsMode;

            findOptions.ColumnOptions.Clear();
            findOptions.ColumnOptions.AddRange(userQuery.Columns.Select(qc => new ColumnOption
            {
                Token = qc.Token.Token,
                ColumnName = qc.Token.TokenString,                
                DisplayName = qc.DisplayName,
            }));

            findOptions.OrderOptions.Clear();
            findOptions.OrderOptions.AddRange(userQuery.Orders.Select(qo => new OrderOption
            {
                Token = qo.Token.Token,
                ColumnName = qo.Token.TokenString,
                OrderType = qo.OrderType
            }));

            findOptions.Pagination = userQuery.GetPagination();
        }

        public static FindOptions ToFindOptions(this UserQueryDN userQuery)
        {
            object queryName = QueryLogic.ToQueryName(userQuery.Query.Key);

            var result = new FindOptions(queryName);
            result.ApplyUserQuery(userQuery);
            return result;
        }
    }
}
