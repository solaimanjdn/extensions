﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
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
    using Signum.Entities.Authorization;
    using Signum.Web;
    using Signum.Web.Auth;
    using Signum.Web.Extensions.Properties;
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("MvcRazorClassGenerator", "1.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Auth/Views/Register.cshtml")]
    public class _Page_Auth_Views_Register_cshtml : System.Web.Mvc.WebViewPage<dynamic>
    {
#line hidden

        public _Page_Auth_Views_Register_cshtml()
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
WriteLiteral("<h2>\r\n    Account Creation</h2>\r\n<p>\r\n    Use the form below to create a new acco" +
"unt.\r\n</p>\r\n<p>\r\n    Passwords are required to be a minimum of ");


                                         Write(ViewData["PasswordLength"]);

WriteLiteral(" characters\r\n    in length.\r\n</p>\r\n");


Write(Html.ValidationSummary());

WriteLiteral("\r\n");


 using (Html.BeginForm())
{

WriteLiteral("    <div>\r\n        <table>\r\n            <tr>\r\n                <td>\r\n             " +
"       Username:\r\n                </td>\r\n                <td>\r\n                 " +
"   ");


               Write(Html.TextBox("username"));

WriteLiteral("\r\n                    ");


               Write(Html.ValidationMessage("username"));

WriteLiteral("\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td" +
">\r\n                    Email:\r\n                </td>\r\n                <td>\r\n    " +
"                ");


               Write(Html.TextBox("email"));

WriteLiteral("\r\n                    ");


               Write(Html.ValidationMessage("email"));

WriteLiteral("\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td" +
">\r\n                    Password:\r\n                </td>\r\n                <td>\r\n " +
"                   ");


               Write(Html.Password("password"));

WriteLiteral("\r\n                    ");


               Write(Html.ValidationMessage("password"));

WriteLiteral("\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td" +
">\r\n                    Confirm password:\r\n                </td>\r\n               " +
" <td>\r\n                    ");


               Write(Html.Password("confirmPassword"));

WriteLiteral("\r\n                    ");


               Write(Html.ValidationMessage("confirmPassword"));

WriteLiteral(@"
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type=""submit"" value=""Register"" />
                </td>
            </tr>
        </table>
    </div>
");


}

        }
    }
}