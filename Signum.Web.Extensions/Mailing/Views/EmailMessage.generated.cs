﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
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
    
    #line 1 "..\..\Mailing\Views\EmailMessage.cshtml"
    using Signum.Engine;
    
    #line default
    #line hidden
    using Signum.Entities;
    
    #line 2 "..\..\Mailing\Views\EmailMessage.cshtml"
    using Signum.Entities.Mailing;
    
    #line default
    #line hidden
    using Signum.Utilities;
    using Signum.Web;
    
    #line 3 "..\..\Mailing\Views\EmailMessage.cshtml"
    using Signum.Web.Files;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Mailing/Views/EmailMessage.cshtml")]
    public partial class EmailMessage : System.Web.Mvc.WebViewPage<dynamic>
    {
        public EmailMessage()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n<style");

WriteLiteral(" type=\"text/css\"");

WriteLiteral(">\r\n    .sf-email-message .sf-repeater-element\r\n    {\r\n        padding: 2px 10px;\r" +
"\n    }\r\n\r\n        .sf-email-message .sf-repeater-element legend\r\n        {\r\n    " +
"        float: left;\r\n            margin-right: 10px;\r\n        }\r\n</style>\r\n\r\n");

            
            #line 18 "..\..\Mailing\Views\EmailMessage.cshtml"
Write(Html.ScriptCss("~/Mailing/Content/SF_Mailing.css"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n\r\n");

            
            #line 21 "..\..\Mailing\Views\EmailMessage.cshtml"
 using (var e = Html.TypeContext<EmailMessageDN>())
{

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"sf-email-message\"");

WriteLiteral(">\r\n\r\n        <div");

WriteLiteral(" class=\"sf-tabs\"");

WriteLiteral(">\r\n            <fieldset");

WriteAttribute("id", Tuple.Create("  id=\"", 545), Tuple.Create("\"", 579)
            
            #line 26 "..\..\Mailing\Views\EmailMessage.cshtml"
, Tuple.Create(Tuple.Create("", 551), Tuple.Create<System.Object, System.Int32>(e.Compose("sfEmailMessage")
            
            #line default
            #line hidden
, 551), false)
);

WriteLiteral(">\r\n                <legend>");

            
            #line 27 "..\..\Mailing\Views\EmailMessage.cshtml"
                   Write(typeof(EmailMessageDN).NiceName());

            
            #line default
            #line hidden
WriteLiteral("</legend>\r\n\r\n");

            
            #line 29 "..\..\Mailing\Views\EmailMessage.cshtml"
                
            
            #line default
            #line hidden
            
            #line 29 "..\..\Mailing\Views\EmailMessage.cshtml"
                 if (e.Value.State != EmailMessageState.Created)
                {
                    e.ReadOnly = true;
                }

            
            #line default
            #line hidden
WriteLiteral("\r\n                <div");

WriteLiteral(" style=\"float: left; margin: 10px\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 35 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.EntityLineDetail(e, f => f.From));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 36 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.EntityRepeater(e, f => f.Recipients));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 37 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.EntityRepeater(e, f => f.Attachments));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n\r\n                <fieldset");

WriteLiteral(" style=\"float: left\"");

WriteLiteral(">\r\n                    <legend>Properties</legend>\r\n");

WriteLiteral("                    ");

            
            #line 42 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.ValueLine(e, f => f.State));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 43 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.ValueLine(e, f => f.Sent));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 44 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.EntityLine(e, f => f.Exception));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 45 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.EntityLine(e, f => f.Template));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 46 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.EntityLine(e, f => f.Package));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 47 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.ValueLine(e, f => f.IsBodyHtml));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </fieldset>\r\n\r\n                <div");

WriteLiteral(" class=\"clearall\"");

WriteLiteral(" />\r\n");

WriteLiteral("                ");

            
            #line 51 "..\..\Mailing\Views\EmailMessage.cshtml"
           Write(Html.EntityLine(e, f => f.Target));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 52 "..\..\Mailing\Views\EmailMessage.cshtml"
           Write(Html.ValueLine(e, f => f.Subject));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 53 "..\..\Mailing\Views\EmailMessage.cshtml"
                
            
            #line default
            #line hidden
            
            #line 53 "..\..\Mailing\Views\EmailMessage.cshtml"
                 if (e.Value.State == EmailMessageState.Created)
                {
                    
            
            #line default
            #line hidden
            
            #line 55 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.ValueLine(e, f => f.Body, vl =>
        {
            vl.ValueLineType = ValueLineType.TextArea;
            vl.ValueHtmlProps["style"] = "width:100%; height:180px;";
        }));

            
            #line default
            #line hidden
            
            #line 59 "..\..\Mailing\Views\EmailMessage.cshtml"
          

                    
            
            #line default
            #line hidden
            
            #line 61 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.ScriptsJs("~/Scripts/ckeditor/ckeditor.js"));

            
            #line default
            #line hidden
            
            #line 61 "..\..\Mailing\Views\EmailMessage.cshtml"
                                                                     
                    
            
            #line default
            #line hidden
            
            #line 62 "..\..\Mailing\Views\EmailMessage.cshtml"
               Write(Html.ScriptsJs("~/Mailing/Scripts/SF_Mailing.js"));

            
            #line default
            #line hidden
            
            #line 62 "..\..\Mailing\Views\EmailMessage.cshtml"
                                                                      


            
            #line default
            #line hidden
WriteLiteral("                    <script>\r\n                        $(function () {\r\n          " +
"                  SF.Mailing.initHtmlEditor(\'");

            
            #line 66 "..\..\Mailing\Views\EmailMessage.cshtml"
                                                  Write(e.Compose("Body"));

            
            #line default
            #line hidden
WriteLiteral("\');\r\n                        });\r\n                    </script>\r\n");

            
            #line 69 "..\..\Mailing\Views\EmailMessage.cshtml"
                }
                else
                {

            
            #line default
            #line hidden
WriteLiteral("                    <h3>");

            
            #line 72 "..\..\Mailing\Views\EmailMessage.cshtml"
                   Write(EmailMessageMessage.Message.NiceToString());

            
            #line default
            #line hidden
WriteLiteral(":</h3>\r\n");

WriteLiteral("                    <div>\r\n");

            
            #line 74 "..\..\Mailing\Views\EmailMessage.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 74 "..\..\Mailing\Views\EmailMessage.cshtml"
                          
                    var body = Signum.Web.Mailing.MailingClient.GetWebMailBody(e.Value.Body, e.Value.Attachments);
                        
            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 77 "..\..\Mailing\Views\EmailMessage.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 77 "..\..\Mailing\Views\EmailMessage.cshtml"
                         if (e.Value.IsBodyHtml)
                        {
                            
            
            #line default
            #line hidden
            
            #line 79 "..\..\Mailing\Views\EmailMessage.cshtml"
                       Write(Html.ScriptsJs("~/Mailing/Scripts/SF_Mailing.js"));

            
            #line default
            #line hidden
            
            #line 79 "..\..\Mailing\Views\EmailMessage.cshtml"
                                                                              
            

            
            #line default
            #line hidden
WriteLiteral("                            <iframe");

WriteAttribute("id", Tuple.Create(" id=\"", 2875), Tuple.Create("\"", 2900)
            
            #line 81 "..\..\Mailing\Views\EmailMessage.cshtml"
, Tuple.Create(Tuple.Create("", 2880), Tuple.Create<System.Object, System.Int32>(e.Compose("iframe")
            
            #line default
            #line hidden
, 2880), false)
);

WriteLiteral(" style=\"width:90%\"");

WriteLiteral(">\r\n");

WriteLiteral("                                ");

            
            #line 82 "..\..\Mailing\Views\EmailMessage.cshtml"
                           Write(Html.Raw(body));

            
            #line default
            #line hidden
WriteLiteral("\r\n                            </iframe>\r\n");

WriteLiteral("                            <script>\r\n                                $(function " +
"() {\r\n                                    var iframe = $(\"");

            
            #line 86 "..\..\Mailing\Views\EmailMessage.cshtml"
                                               Write(e.Compose("iframe"));

            
            #line default
            #line hidden
WriteLiteral("\");\r\n                                    SF.Mailing.activateIFrame(iframe);\r\n    " +
"                            });\r\n                            </script>\r\n");

            
            #line 90 "..\..\Mailing\Views\EmailMessage.cshtml"
                        }
                        else
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <pre>\r\n");

WriteLiteral("            ");

            
            #line 94 "..\..\Mailing\Views\EmailMessage.cshtml"
       Write(Html.Raw(HttpUtility.HtmlEncode(body)));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </pre>\r\n");

            
            #line 96 "..\..\Mailing\Views\EmailMessage.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </div>\r\n");

            
            #line 98 "..\..\Mailing\Views\EmailMessage.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n            </fieldset>\r\n");

            
            #line 102 "..\..\Mailing\Views\EmailMessage.cshtml"
            
            
            #line default
            #line hidden
            
            #line 102 "..\..\Mailing\Views\EmailMessage.cshtml"
             if (e.Value.Mixins.OfType<EmailReceptionMixin>().Any() && e.Value.Mixin<EmailReceptionMixin>().ReceptionInfo != null)
            {
                using (var ri = e.SubContext(f => f.Mixin<EmailReceptionMixin>().ReceptionInfo))
                {

            
            #line default
            #line hidden
WriteLiteral("                <fieldset");

WriteAttribute("id", Tuple.Create("  id=\"", 3906), Tuple.Create("\"", 3946)
            
            #line 106 "..\..\Mailing\Views\EmailMessage.cshtml"
, Tuple.Create(Tuple.Create("", 3912), Tuple.Create<System.Object, System.Int32>(e.Compose("sfEmailReceptionInfo")
            
            #line default
            #line hidden
, 3912), false)
);

WriteLiteral(">\r\n                    <legend>");

            
            #line 107 "..\..\Mailing\Views\EmailMessage.cshtml"
                       Write(typeof(EmailReceptionInfoDN).NiceName());

            
            #line default
            #line hidden
WriteLiteral("</legend>\r\n\r\n                    <fieldset>\r\n                        <legend>Prop" +
"erties</legend>\r\n\r\n");

WriteLiteral("                        ");

            
            #line 112 "..\..\Mailing\Views\EmailMessage.cshtml"
                   Write(Html.EntityLine(ri, f => f.Reception));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 113 "..\..\Mailing\Views\EmailMessage.cshtml"
                   Write(Html.ValueLine(ri, f => f.UniqueId));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 114 "..\..\Mailing\Views\EmailMessage.cshtml"
                   Write(Html.ValueLine(ri, f => f.SentDate));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 115 "..\..\Mailing\Views\EmailMessage.cshtml"
                   Write(Html.ValueLine(ri, f => f.ReceivedDate));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 116 "..\..\Mailing\Views\EmailMessage.cshtml"
                   Write(Html.ValueLine(ri, f => f.DeletionDate));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n                    </fieldset>\r\n\r\n                    <pre>\r\n");

WriteLiteral("                        ");

            
            #line 121 "..\..\Mailing\Views\EmailMessage.cshtml"
                   Write(ri.Value.RawContent);

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </pre>\r\n                </fieldset>\r\n");

            
            #line 124 "..\..\Mailing\Views\EmailMessage.cshtml"
                }
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n\r\n\r\n\r\n    </div>\r\n");

            
            #line 131 "..\..\Mailing\Views\EmailMessage.cshtml"
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
