﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
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
    using Signum.Web.Extensions.Properties;
    using Signum.Entities.DynamicQuery;
    using Signum.Engine.DynamicQuery;
    using Signum.Entities.Reflection;
    using Signum.Entities.Chart;
    using Signum.Web.Chart;
    using Signum.Engine.Chart;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("MvcRazorClassGenerator", "1.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Chart/Views/ChartBuilder.cshtml")]
    public class _Page_Chart_Views_ChartBuilder_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {


        public _Page_Chart_Views_ChartBuilder_cshtml()
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












 using (var chart = Html.TypeContext<ChartRequest>())
{
    QueryDescription queryDescription = (QueryDescription)ViewData[ViewDataKeys.QueryDescription];
    if (queryDescription == null)
    {
        queryDescription = DynamicQueryManager.Current.QueryDescription(((UserChartDN)((TypeContext)chart.Parent).UntypedValue).QueryName);
    }


WriteLiteral("    <table id=\"");


          Write(chart.Compose("sfChartBuilder"));

WriteLiteral("\" class=\"sf-chart-builder\" data-url=\"");


                                                                                Write(Url.Action<ChartController>(cc => cc.UpdateChartBuilder(chart.Parent.ControlID)));

WriteLiteral("\">\r\n        <tr>\r\n            <td class=\"ui-widget ui-widget-content ui-corner-al" +
"l sf-chart-type\">\r\n                <div class=\"ui-widget-header\">\r\n             " +
"       ");


               Write(typeof(ChartScriptDN).NiceName());

WriteLiteral("\r\n");


                     using (var csc = chart.SubContext(c => c.ChartScript))
                    {
                        
                   Write(Html.Hidden(csc.Compose("RuntimeInfo"), csc.RuntimeInfo().ToString(), new { @class = "sf-chart-type-value" }));

                                                                                                                                      
                    }

WriteLiteral("                    ");


               Write(Html.Hidden(chart.Compose("GroupResults"), chart.Value.GroupResults, new { @class = "sf-chart-group-results" }));

WriteLiteral("\r\n                </div>\r\n");


                 foreach (var group in ChartScriptLogic.Scripts.Value.OrderBy(a => a.Id).GroupsOf(4))
                {
                    foreach (var script in group)
                    { 

WriteLiteral("                    <div class=\"");


                           Write(ChartClient.ChartTypeImgClass(chart.Value.Columns, chart.Value.ChartScript, script));

WriteLiteral("\" data-related=\"");


                                                                                                                               Write(script.Id.ToString());

WriteLiteral("\"  title=\"");


                                                                                                                                                              Write(script.ToString());

WriteLiteral("\">\r\n                        <img src=\" ");


                               Write(script.Icon == null ?
                        Url.Content("~/Chart/Images/unkwnown.png") :
                        Url.Action((Signum.Web.Files.FileController fc) => fc.DownloadFile(script.Icon.Id)));

WriteLiteral("\" />\r\n                    </div>\r\n");


                    }

WriteLiteral("                    <div class=\"clearall\">\r\n                    </div>\r\n");


                }

WriteLiteral("            </td>\r\n            <td class=\"ui-widget ui-widget-content ui-corner-a" +
"ll sf-chart-tokens\">\r\n                <div class=\"ui-widget-header\">");


                                         Write(Resources.Chart_ChartSettings);

WriteLiteral("</div>\r\n                <table>\r\n                    <tr>\r\n                      " +
"  <th class=\"sf-chart-token-narrow\">");


                                                     Write(Resources.Chart_Dimension);

WriteLiteral("\r\n                        </th>\r\n                        <th class=\"sf-chart-toke" +
"n-narrow\">");


                                                     Write(Resources.Chart_Group);

WriteLiteral("\r\n                        </th>\r\n                        <th class=\"sf-chart-toke" +
"n-wide\">\r\n                            Token\r\n                        </th>\r\n    " +
"                </tr>\r\n");


                     foreach (var column in chart.TypeElementContext(a => a.Columns))
                    {
                        
                   Write(Html.HiddenRuntimeInfo(column));

                                                       
                        
                   Write(Html.EmbeddedControl(column, c => c, ec => ec.ViewData[ViewDataKeys.QueryName] = queryDescription.QueryName));

                                                                                                                                     
                    }

WriteLiteral("                </table>\r\n            </td>\r\n        </tr>\r\n    </table>\r\n");


}

        }
    }
}
