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

namespace Signum.Web.Extensions.Translation.Views
{
    using System;
    using System.Collections.Generic;
    
    #line 2 "..\..\Translation\Views\ViewInstance.cshtml"
    using System.Globalization;
    
    #line default
    #line hidden
    using System.IO;
    using System.Linq;
    using System.Net;
    
    #line 5 "..\..\Translation\Views\ViewInstance.cshtml"
    using System.Reflection;
    
    #line default
    #line hidden
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
    
    #line 3 "..\..\Translation\Views\ViewInstance.cshtml"
    using Signum.Engine.Translation;
    
    #line default
    #line hidden
    using Signum.Entities;
    
    #line 4 "..\..\Translation\Views\ViewInstance.cshtml"
    using Signum.Entities.Translation;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Translation\Views\ViewInstance.cshtml"
    using Signum.Utilities;
    
    #line default
    #line hidden
    using Signum.Web;
    
    #line 7 "..\..\Translation\Views\ViewInstance.cshtml"
    using Signum.Web.Translation.Controllers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Translation/Views/ViewInstance.cshtml")]
    public partial class ViewInstance : System.Web.Mvc.WebViewPage<Dictionary<CultureInfo, Dictionary<LocalizedInstanceKey, TranslatedInstanceDN>>>
    {
        public ViewInstance()
        {
        }
        public override void Execute()
        {
            
            #line 8 "..\..\Translation\Views\ViewInstance.cshtml"
  
    CultureInfo culture = ViewBag.Culture;
    Type type = ViewBag.Type;

    ViewBag.Title = TranslationMessage.View0In1.NiceToString().Formato(type.NiceName(), culture == null ? TranslationMessage.AllLanguages.NiceToString() : culture.DisplayName);

    Dictionary<LocalizedInstanceKey, string> master = ViewBag.Master;

    var cultures = TranslationLogic.CurrentCultureInfos(TranslatedInstanceLogic.DefaultCulture);

    Func<CultureInfo, bool> editCulture = c => culture == null || culture.Name == c.Name;

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 20 "..\..\Translation\Views\ViewInstance.cshtml"
Write(Html.ScriptCss("~/Translation/Content/Translation.css"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 22 "..\..\Translation\Views\ViewInstance.cshtml"
 if (master.Keys.IsEmpty())
{

            
            #line default
            #line hidden
WriteLiteral("   <h2>");

            
            #line 24 "..\..\Translation\Views\ViewInstance.cshtml"
  Write(TranslationMessage.NothingToTranslateIn0.NiceToString().Formato(type.NiceName()));

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n");

            
            #line 25 "..\..\Translation\Views\ViewInstance.cshtml"
}
else
{

            
            #line default
            #line hidden
WriteLiteral("   <h2>");

            
            #line 28 "..\..\Translation\Views\ViewInstance.cshtml"
  Write(ViewBag.Title);

            
            #line default
            #line hidden
WriteLiteral("</h2>\r\n");

            
            #line 29 "..\..\Translation\Views\ViewInstance.cshtml"

    using (Html.BeginForm())
    {

            
            #line default
            #line hidden
WriteLiteral("        <table");

WriteLiteral(" id=\"results\"");

WriteLiteral(" style=\"width: 100%; margin: 0px\"");

WriteLiteral(" class=\"st\"");

WriteLiteral(">\r\n");

            
            #line 33 "..\..\Translation\Views\ViewInstance.cshtml"
            
            
            #line default
            #line hidden
            
            #line 33 "..\..\Translation\Views\ViewInstance.cshtml"
             foreach (var instance in master.Keys.GroupBy(a => a.Instance).OrderBy(a => a.Key.Id))
            { 

            
            #line default
            #line hidden
WriteLiteral("                <thead>\r\n                    <tr>\r\n                        <th");

WriteLiteral(" class=\"leftCell\"");

WriteLiteral(">");

            
            #line 37 "..\..\Translation\Views\ViewInstance.cshtml"
                                        Write(TranslationMessage.Instance.NiceToString());

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n                        <th");

WriteLiteral(" class=\"titleCell\"");

WriteLiteral(">");

            
            #line 38 "..\..\Translation\Views\ViewInstance.cshtml"
                                         Write(Html.Href(Navigator.NavigateRoute(instance.Key), instance.Key.ToString()));

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n                    </tr>\r\n                </thead>\r\n");

            
            #line 41 "..\..\Translation\Views\ViewInstance.cshtml"

                foreach (LocalizedInstanceKey key in instance.OrderBy(a => a.Route.ToString()))
                {     

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <th");

WriteLiteral(" class=\"leftCell\"");

WriteLiteral(">");

            
            #line 45 "..\..\Translation\Views\ViewInstance.cshtml"
                                    Write(TranslationMessage.Property.NiceToString());

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </th>\r\n                    <th>");

            
            #line 47 "..\..\Translation\Views\ViewInstance.cshtml"
                   Write(key.Route.PropertyString());

            
            #line default
            #line hidden
WriteLiteral("</th>\r\n                </tr>\r\n");

            
            #line 49 "..\..\Translation\Views\ViewInstance.cshtml"
            
                    foreach (var ci in cultures)
                    {
                        if (ci.Name == TranslatedInstanceLogic.DefaultCulture)
                        {

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td");

WriteLiteral(" class=\"leftCell\"");

WriteLiteral(">");

            
            #line 55 "..\..\Translation\Views\ViewInstance.cshtml"
                                    Write(TranslatedInstanceLogic.DefaultCulture);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td");

WriteLiteral(" class=\"monospaceCell\"");

WriteLiteral(">\r\n                        <em>");

            
            #line 57 "..\..\Translation\Views\ViewInstance.cshtml"
                       Write(master[key]);

            
            #line default
            #line hidden
WriteLiteral("</em>\r\n                    </td>\r\n                </tr>\r\n");

            
            #line 60 "..\..\Translation\Views\ViewInstance.cshtml"
                        }
                        else
                        {
                            TranslatedInstanceDN trans = Model.TryGetC(ci).TryGetC(key);

                            if (trans != null || editCulture(ci))
                            {

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td");

WriteLiteral(" class=\"leftCell\"");

WriteLiteral(">");

            
            #line 68 "..\..\Translation\Views\ViewInstance.cshtml"
                                    Write(ci.Name);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td");

WriteLiteral(" class=\"monospaceCell\"");

WriteLiteral(">\r\n");

            
            #line 70 "..\..\Translation\Views\ViewInstance.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 70 "..\..\Translation\Views\ViewInstance.cshtml"
                         if (editCulture(ci))
                        {
                            
            
            #line default
            #line hidden
            
            #line 72 "..\..\Translation\Views\ViewInstance.cshtml"
                       Write(Html.TextArea(ci.Name + "#" + key.Instance.Key() + "#" + key.Route.PropertyString(), trans.Try(t => t.TranslatedText), new { style = "width:90%;height:16px" }));

            
            #line default
            #line hidden
            
            #line 72 "..\..\Translation\Views\ViewInstance.cshtml"
                                                                                                                                                                                              
                        }
                        else
                        {
                            
            
            #line default
            #line hidden
            
            #line 76 "..\..\Translation\Views\ViewInstance.cshtml"
                       Write(trans.TranslatedText);

            
            #line default
            #line hidden
            
            #line 76 "..\..\Translation\Views\ViewInstance.cshtml"
                                                 
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </td>\r\n                </tr>\r\n");

            
            #line 80 "..\..\Translation\Views\ViewInstance.cshtml"
                            }
                        }

                    }
                }
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n");

WriteLiteral("    <input");

WriteLiteral(" type=\"submit\"");

WriteAttribute("value", Tuple.Create(" value=\"", 3415), Tuple.Create("\"", 3462)
            
            #line 87 "..\..\Translation\Views\ViewInstance.cshtml"
, Tuple.Create(Tuple.Create("", 3423), Tuple.Create<System.Object, System.Int32>(TranslationMessage.Save.NiceToString()
            
            #line default
            #line hidden
, 3423), false)
);

WriteLiteral(" />\r\n");

            
            #line 88 "..\..\Translation\Views\ViewInstance.cshtml"
    }
}

            
            #line default
            #line hidden
        }
    }
}
#pragma warning restore 1591
