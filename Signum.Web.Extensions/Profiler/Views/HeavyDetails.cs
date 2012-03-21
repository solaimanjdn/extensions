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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Profiler/Views/HeavyDetails.cshtml")]
    public class _Page_Profiler_Views_HeavyDetails_cshtml : System.Web.Mvc.WebViewPage<HeavyProfilerEntry>
    {


        public _Page_Profiler_Views_HeavyDetails_cshtml()
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



WriteLiteral("<h2>\r\n    Profiler Entry (\r\n");


     foreach (var e in Model.FollowC(a => a.Parent).Skip(1).Reverse())
    {
        
   Write(Html.ProfilerEntry(e.Index.ToString(), e.FullIndex()));


WriteLiteral(".\r\n");


        }

WriteLiteral("    ");


Write(Model.Index.ToString());

WriteLiteral(")\r\n</h2>\r\n");


Write(Html.ActionLink("(View all)", (ProfilerController pc) => pc.Heavy()));

WriteLiteral("\r\n<br />\r\n<h3>\r\n    Breakdown</h3>\r\n<div class=\"sf-profiler-chart\" data-detail-ur" +
"l=\"");


                                           Write(Url.Action("HeavyRoute", "Profiler"));

WriteLiteral("\">\r\n</div>\r\n<br />\r\n<table class=\"sf-search-results\">\r\n    <tr>\r\n        <th>\r\n  " +
"          Role\r\n        </th>\r\n        <td>\r\n            ");


       Write(Model.Role);

WriteLiteral("\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        <th>\r\n            Time\r\n        </t" +
"h>\r\n        <td>\r\n            ");


       Write(Model.Elapsed.NiceToString());

WriteLiteral("\r\n        </td>\r\n    </tr>\r\n</table>\r\n<br />\r\n<h3>\r\n    Aditional Data</h3>\r\n<div" +
">\r\n    <pre>\r\n    <code>\r\n        ");


   Write(Model.AditionalData);

WriteLiteral("\r\n    </code>\r\n    </pre>\r\n</div>\r\n<br />\r\n<h3>\r\n    StackTrace</h3>\r\n");


 if (Model.StackTrace == null)
{

WriteLiteral("    <span>No StackTrace</span>\r\n");


}
else
{

WriteLiteral(@"    <table class=""sf-search-results"">
        <thead>
            <tr>
                <th>
                    Type
                </th>
                <th>
                    Method
                </th>
                <th>
                    FileLine
                </th>
            </tr>
        </thead>
        <tbody>
");


             for (int i = 0; i < Model.StackTrace.FrameCount; i++)
            {
                var frame = Model.StackTrace.GetFrame(i);
                var type = frame.GetMethod().DeclaringType;

WriteLiteral("                <tr>\r\n                    <td>\r\n");


                         if (type != null)
                        {
                            var color = ColorExtensions.ToHtmlColor(type.Assembly.FullName.GetHashCode());
                    

WriteLiteral("                            <span style=\"color:");


                                          Write(color);

WriteLiteral("\">");


                                                  Write(frame.GetMethod().DeclaringType.TryCC(t => t.TypeName()));

WriteLiteral("</span>\r\n");


                        }

WriteLiteral("                    </td>\r\n                    <td>\r\n                        ");


                   Write(frame.GetMethod().Name);

WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");


                   Write(frame.GetFileLineAndNumber());

WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");


            }

WriteLiteral("        </tbody>\r\n    </table>\r\n");


}

WriteLiteral("<br />\r\n");


Write(Html.ScriptCss("~/Profiler/Content/SF_Profiler.css"));

WriteLiteral("\r\n");


Write(Html.ScriptsJs("~/scripts/d3.v2.min.js",
                "~/Profiler/Scripts/SF_Profiler.js"));

WriteLiteral("\r\n");


   
    var fullTree = Model.FollowC(e => e.Parent).ToList();
    fullTree.AddRange(Model.DescendantsAndSelf()); 


WriteLiteral("<script type=\"text/javascript\">\r\n    $(function() {\r\n        SF.Profiler.heavyDet" +
"ailsChart(");


                                 Write(Html.Raw(fullTree.Distinct().HeavyDetailsToJson()));

WriteLiteral(", ");


                                                                                      Write(Model.Depth);

WriteLiteral(");\r\n    });\r\n</script>\r\n");


        }
    }
}
