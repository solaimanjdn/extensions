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

namespace Signum.Web.Extensions.Mailing.Views
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
    
    #line 5 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
    using Signum.Engine;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
    using Signum.Engine.Translation;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
    using Signum.Entities;
    
    #line default
    #line hidden
    
    #line 7 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
    using Signum.Entities.DynamicQuery;
    
    #line default
    #line hidden
    
    #line 1 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
    using Signum.Entities.Mailing;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
    using Signum.Entities.Translation;
    
    #line default
    #line hidden
    using Signum.Utilities;
    
    #line 6 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
    using Signum.Web;
    
    #line default
    #line hidden
    
    #line 8 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
    using Signum.Web.Mailing;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Mailing/Views/EmailMasterTemplateMessage.cshtml")]
    public partial class EmailMasterTemplateMessage : System.Web.Mvc.WebViewPage<dynamic>
    {
        public EmailMasterTemplateMessage()
        {
        }
        public override void Execute()
        {
            
            #line 9 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
 using (var ec = Html.TypeContext<EmailMasterTemplateMessageDN>())
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"sf-email-template-message\"");

WriteLiteral(">\r\n        <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" class=\"sf-tab-title\"");

WriteAttribute("value", Tuple.Create(" value=\"", 396), Tuple.Create("\"", 441)
            
            #line 12 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
, Tuple.Create(Tuple.Create("", 404), Tuple.Create<System.Object, System.Int32>(ec.Value.CultureInfo.TryToString()
            
            #line default
            #line hidden
, 404), false)
);

WriteLiteral(" />\r\n        \r\n");

WriteLiteral("        ");

            
            #line 14 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
   Write(Html.EntityCombo(ec, e => e.CultureInfo, vl =>
        {
            vl.LabelText = EmailTemplateViewMessage.Language.NiceToString();
        }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        \r\n        <div");

WriteLiteral(" class=\"sf-template-message-insert-container\"");

WriteLiteral(">\r\n            <input");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"sf-button sf-master-template-insert-content\"");

WriteAttribute("value", Tuple.Create(" value=\"", 769), Tuple.Create("\"", 840)
            
            #line 20 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
              , Tuple.Create(Tuple.Create("", 777), Tuple.Create<System.Object, System.Int32>(EmailTemplateViewMessage.InsertMessageContent.NiceToString()
            
            #line default
            #line hidden
, 777), false)
);

WriteLiteral(" />\r\n        </div>\r\n\r\n");

WriteLiteral("        ");

            
            #line 23 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
   Write(Html.ValueLine(ec, e => e.Text, vl =>
        {
            vl.FormGroupStyle = FormGroupStyle.None;
            vl.ValueLineType = ValueLineType.TextArea;
            vl.ValueHtmlProps["style"] = "width:100%; height:180px;";
            vl.ValueHtmlProps["class"] = "sf-rich-text-editor sf-email-template-message-text";
        }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        \r\n        <script>\r\n            $(function () {\r\n");

WriteLiteral("               ");

            
            #line 33 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
           Write(new JsFunction(MailingClient.Module, "initHtmlEditorMasterTemplate", ec.SubContext(e => e.Text).Prefix, UICulture));

            
            #line default
            #line hidden
WriteLiteral("\r\n            });\r\n        </script>\r\n    </div>\r\n");

            
            #line 37 "..\..\Mailing\Views\EmailMasterTemplateMessage.cshtml"
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
