#pragma checksum "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\GetUsers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2158d368d01ce9141824adcfd7f202e30c748841"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_GetUsers), @"mvc.1.0.view", @"/Views/Home/GetUsers.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/GetUsers.cshtml", typeof(AspNetCore.Views_Home_GetUsers))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\_ViewImports.cshtml"
using gp0;

#line default
#line hidden
#line 2 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\_ViewImports.cshtml"
using gp0.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2158d368d01ce9141824adcfd7f202e30c748841", @"/Views/Home/GetUsers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7d4f3d63bb95af09b98e12bce7c5fa931008cb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_GetUsers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<gp0.ViewModels.UserView>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\GetUsers.cshtml"
    ViewData["Title"] = "GetUser";

#line default
#line hidden
            BeginContext(94, 9, true);
            WriteLiteral("\r\n<div>\r\n");
            EndContext();
#line 8 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\GetUsers.cshtml"
     foreach (var user in Model)
    {

#line default
#line hidden
            BeginContext(144, 39, true);
            WriteLiteral("        <p>Фамилия Имя</p>\r\n        <p>");
            EndContext();
            BeginContext(184, 10, false);
#line 11 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\GetUsers.cshtml"
      Write(user.email);

#line default
#line hidden
            EndContext();
            BeginContext(194, 22, true);
            WriteLiteral("</p>\r\n        <hr />\r\n");
            EndContext();
#line 13 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\GetUsers.cshtml"

    }

#line default
#line hidden
            BeginContext(225, 10, true);
            WriteLiteral("</div>\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<gp0.ViewModels.UserView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
