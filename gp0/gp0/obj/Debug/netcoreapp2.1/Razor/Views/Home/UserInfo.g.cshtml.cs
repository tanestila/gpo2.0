#pragma checksum "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\UserInfo.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "af9c401e3a460ee79bb992bae9de8974101b8b77"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_UserInfo), @"mvc.1.0.view", @"/Views/Home/UserInfo.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/UserInfo.cshtml", typeof(AspNetCore.Views_Home_UserInfo))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"af9c401e3a460ee79bb992bae9de8974101b8b77", @"/Views/Home/UserInfo.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7d4f3d63bb95af09b98e12bce7c5fa931008cb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_UserInfo : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<gp0.ViewModels.UserView>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\UserInfo.cshtml"
  
    Layout = "HomeLayout";

#line default
#line hidden
            BeginContext(67, 24, true);
            WriteLiteral("\r\n<div>\r\n    <h3>Email: ");
            EndContext();
            BeginContext(92, 11, false);
#line 7 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\UserInfo.cshtml"
          Write(Model.email);

#line default
#line hidden
            EndContext();
            BeginContext(103, 23, true);
            WriteLiteral("</h3>]\r\n    <h3>Логин: ");
            EndContext();
            BeginContext(127, 11, false);
#line 8 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\UserInfo.cshtml"
          Write(Model.login);

#line default
#line hidden
            EndContext();
            BeginContext(138, 15, true);
            WriteLiteral("</h3>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<gp0.ViewModels.UserView> Html { get; private set; }
    }
}
#pragma warning restore 1591
