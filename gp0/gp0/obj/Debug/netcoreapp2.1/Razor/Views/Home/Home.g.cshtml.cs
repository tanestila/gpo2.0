#pragma checksum "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\Home.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b0ce7084d0afdcb3c1ea46a46fb92247621f37c9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Home), @"mvc.1.0.view", @"/Views/Home/Home.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Home.cshtml", typeof(AspNetCore.Views_Home_Home))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b0ce7084d0afdcb3c1ea46a46fb92247621f37c9", @"/Views/Home/Home.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7d4f3d63bb95af09b98e12bce7c5fa931008cb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Home : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Document>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\Home.cshtml"
  
    ViewData["Title"] = "Home Page";
    Layout = "HomeLayout";

#line default
#line hidden
            BeginContext(103, 221, true);
            WriteLiteral("<table class=\"table\">\r\n    <thead class=\"thead-light\">\r\n    <tr>\r\n        <th scope=\"col\">Отправитель</th>\r\n        <th scope=\"col\">Дата</th>\r\n        <th scope=\"col\">Сообщение</th>\r\n    </tr>\r\n    </thead>\r\n    <tbody>\r\n");
            EndContext();
#line 15 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\Home.cshtml"
     foreach (var doc in Model)
    {

#line default
#line hidden
            BeginContext(364, 65, true);
            WriteLiteral("        <tr>\r\n            <th scope=\"row\"></th>\r\n            <td>");
            EndContext();
            BeginContext(430, 10, false);
#line 19 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\Home.cshtml"
           Write(doc.sender);

#line default
#line hidden
            EndContext();
            BeginContext(440, 23, true);
            WriteLiteral("</td>\r\n            <td>");
            EndContext();
            BeginContext(464, 8, false);
#line 20 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\Home.cshtml"
           Write(doc.date);

#line default
#line hidden
            EndContext();
            BeginContext(472, 23, true);
            WriteLiteral("</td>\r\n            <td>");
            EndContext();
            BeginContext(496, 8, false);
#line 21 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\Home.cshtml"
           Write(doc.text);

#line default
#line hidden
            EndContext();
            BeginContext(504, 22, true);
            WriteLiteral("</td>\r\n        </tr>\r\n");
            EndContext();
#line 23 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\Home.cshtml"
    }

#line default
#line hidden
            BeginContext(533, 30, true);
            WriteLiteral("    </tbody>\r\n</table>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Document>> Html { get; private set; }
    }
}
#pragma warning restore 1591
