﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Signum.Web.Extensions.Processes.Views
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 2 "..\..\Processes\Views\ProcessPanel.cshtml"
    using Signum.Engine.Processes;
    
    #line default
    #line hidden
    using Signum.Entities;
    using Signum.Utilities;
    
    #line 1 "..\..\Processes\Views\ProcessPanel.cshtml"
    using Signum.Utilities.ExpressionTrees;
    
    #line default
    #line hidden
    using Signum.Web;
    
    #line 3 "..\..\Processes\Views\ProcessPanel.cshtml"
    using Signum.Web.Processes;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Processes/Views/ProcessPanel.cshtml")]
    public partial class ProcessPanel : System.Web.Mvc.WebViewPage<ProcessLogicState>
    {
        public ProcessPanel()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" id=\"processMainDiv\"");

WriteLiteral(">\r\n    <h2>ProcessLogic state</h2>\r\n    <div>\r\n        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 209), Tuple.Create("\"", 264)
            
            #line 8 "..\..\Processes\Views\ProcessPanel.cshtml"
, Tuple.Create(Tuple.Create("", 216), Tuple.Create<System.Object, System.Int32>(Url.Action((ProcessController pc) => pc.Stop())
            
            #line default
            #line hidden
, 216), false)
);

WriteLiteral(" class=\"sf-button btn btn-default active\"");

WriteAttribute("style", Tuple.Create(" style=\"", 306), Tuple.Create("\"", 362)
            
            #line 8 "..\..\Processes\Views\ProcessPanel.cshtml"
                                     , Tuple.Create(Tuple.Create("", 314), Tuple.Create<System.Object, System.Int32>(Model.Running ? "" : "display:none"
            
            #line default
            #line hidden
, 314), false)
, Tuple.Create(Tuple.Create("", 352), Tuple.Create(";color:red", 352), true)
);

WriteLiteral(" id=\"sfProcessDisable\"");

WriteLiteral(">Stop </a>\r\n        <a");

WriteAttribute("href", Tuple.Create(" href=\"", 407), Tuple.Create("\"", 463)
            
            #line 9 "..\..\Processes\Views\ProcessPanel.cshtml"
, Tuple.Create(Tuple.Create("", 414), Tuple.Create<System.Object, System.Int32>(Url.Action((ProcessController pc) => pc.Start())
            
            #line default
            #line hidden
, 414), false)
);

WriteLiteral(" class=\"sf-button btn btn-default\"");

WriteAttribute("style", Tuple.Create("  style=\"", 498), Tuple.Create("\"", 546)
            
            #line 9 "..\..\Processes\Views\ProcessPanel.cshtml"
                                , Tuple.Create(Tuple.Create("", 507), Tuple.Create<System.Object, System.Int32>(!Model.Running ? "" : "display:none"
            
            #line default
            #line hidden
, 507), false)
);

WriteLiteral(" id=\"sfProcessEnable\"");

WriteLiteral(">Start </a>\r\n    </div>\r\n\r\n");

WriteLiteral("    ");

            
            #line 12 "..\..\Processes\Views\ProcessPanel.cshtml"
Write(Html.Partial(ProcessesClient.ViewPrefix.Formato("ProcessPanelTable")));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <script>\r\n        $(function () {\r\n");

WriteLiteral("            ");

            
            #line 15 "..\..\Processes\Views\ProcessPanel.cshtml"
        Write(new JsFunction(ProcessesClient.Module, "initControlPanel", Url.Action((ProcessController p) => p.View())));

            
            #line default
            #line hidden
WriteLiteral("\r\n        });\r\n    </script>\r\n</div>\r\n");

        }
    }
}
#pragma warning restore 1591
