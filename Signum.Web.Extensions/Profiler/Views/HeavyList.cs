﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using Signum.Utilities;
    using Signum.Entities;
    using Signum.Web;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web.Caching;
    using System.Web.DynamicData;
    using System.Web.SessionState;
    using System.Web.Profile;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Xml.Linq;
    using Signum.Utilities.ExpressionTrees;
    using Signum.Web.Profiler;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("MvcRazorClassGenerator", "1.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Profiler/Views/HeavyList.cshtml")]
    public class _Page_Profiler_Views_HeavyList_cshtml : System.Web.Mvc.WebViewPage<List<HeavyProfilerEntry>>
    {


        public _Page_Profiler_Views_HeavyList_cshtml()
        {
        }
        protected System.Web.HttpApplication ApplicationInstance
        {
            get
            {
                return ((System.Web.HttpApplication)(Context.ApplicationInstance));
            }
        }
        public override void Execute()
        {



WriteLiteral("\r\n<h2>");


Write(ViewData[ViewDataKeys.Title]);

WriteLiteral("</h2>\r\n<div>\r\n    ");


Write(Html.Partial(ProfilerClient.ViewPrefix.Formato("ProfilerButtons")));

WriteLiteral("\r\n    ");


Write(Html.ActionLink("Slowest SQLs", (ProfilerController pc) => pc.Statistics(SqlProfileResumeOrder.Sum)));

WriteLiteral("\r\n</div>\r\n\r\n");


 if (Model != null)
{

WriteLiteral("    <br />\r\n");



WriteLiteral("    <h3>Entries</h3>\r\n");



WriteLiteral("    <div class=\"sf-profiler-chart\" data-detail-url=\"");


                                               Write(Url.Action("HeavyRoute", "Profiler"));

WriteLiteral("\">\r\n    </div>\r\n");



WriteLiteral("    <br />\r\n");


}

WriteLiteral("\r\n");


Write(Html.ScriptCss("~/Profiler/Content/SF_Profiler.css"));

WriteLiteral("\r\n");


Write(Html.ScriptsJs("~/scripts/d3.v2.min.js",
                "~/Profiler/Scripts/SF_Profiler.js"));

WriteLiteral("\r\n\r\n<script language=\"javascript\">\r\n    $(function () {\r\n        SF.Profiler.init" +
"(function () {\r\n            $.ajax({\r\n                url: \"");


                 Write(Url.Action((ProfilerController p) => p.Heavy()));

WriteLiteral("\",\r\n                success: function (data) {\r\n                    $(\"table.sf-p" +
"rofiler-table\").replaceWith(data);\r\n                }\r\n            });\r\n        " +
"});\r\n\r\n        SF.Profiler.heavyListChart(");


                              Write(Html.Raw(Model.OrderBy(e => e.Start).HeavyDetailsToJson()));

WriteLiteral(");\r\n    });\r\n</script>\r\n");


        }
    }
}
