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
    using Signum.Entities;
    
    #line 2 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
    using Signum.Entities.DynamicQuery;
    
    #line default
    #line hidden
    
    #line 1 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
    using Signum.Entities.Mailing;
    
    #line default
    #line hidden
    using Signum.Utilities;
    using Signum.Web;
    
    #line 3 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
    using Signum.Web.UserQueries;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Mailing/Views/EmailTemplateRecipient.cshtml")]
    public partial class EmailTemplateRecipient : System.Web.Mvc.WebViewPage<dynamic>
    {
        public EmailTemplateRecipient()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
 using (var tc = Html.TypeContext<EmailTemplateRecipientDN>())
{
    using (var sc = tc.SubContext())
    {
        sc.LabelColumns = new BsColumn(4);

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"col-sm-2\"");

WriteLiteral(" style=\"text-align: right;\"");

WriteLiteral(">\r\n            <label>\r\n");

WriteLiteral("                ");

            
            #line 12 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
           Write(Html.ValueLine(tc, c => c.Kind, vl => vl.FormGroupStyle = FormGroupStyle.None));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </label>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-sm-5\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 16 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
       Write(Html.ValueLine(sc, c => c.EmailAddress));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"col-sm-5\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 19 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
       Write(Html.ValueLine(sc, c => c.DisplayName));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n");

            
            #line 22 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
    }

    using (var qtTc = tc.SubContext(etTc => etTc.Token))
    {
    
            
            #line default
            #line hidden
            
            #line 26 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
Write(Html.FormGroup(tc, null, "Email Owner", Html.QueryTokenDNBuilder(qtTc, SearchControlHelper.GetQueryTokenBuilderSettings(
            (QueryDescription)ViewData[ViewDataKeys.QueryDescription]))));

            
            #line default
            #line hidden
            
            #line 27 "..\..\Mailing\Views\EmailTemplateRecipient.cshtml"
                                                                        
    }
}
            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
