#pragma checksum "C:\Users\pashk\Documents\GitHub\gpo2\gp0\gp0\Views\Shared\Error.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d4d5fbbf598adea3fca4252d51bbd8c650a468f0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Error), @"mvc.1.0.view", @"/Views/Shared/Error.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Error.cshtml", typeof(AspNetCore.Views_Shared_Error))]
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
#line 1 "C:\Users\pashk\Documents\GitHub\gpo2\gp0\gp0\Views\_ViewImports.cshtml"
using gp0;

#line default
#line hidden
#line 2 "C:\Users\pashk\Documents\GitHub\gpo2\gp0\gp0\Views\_ViewImports.cshtml"
using gp0.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d4d5fbbf598adea3fca4252d51bbd8c650a468f0", @"/Views/Shared/Error.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7d4f3d63bb95af09b98e12bce7c5fa931008cb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Error : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<gp0.ViewModels.ErrorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\pashk\Documents\GitHub\gpo2\gp0\gp0\Views\Shared\Error.cshtml"
  
    ViewData["Title"] = "Error";
    Layout = "HomeLayout";

#line default
#line hidden
            BeginContext(107, 118, true);
            WriteLiteral("<h1 class=\"text-danger\">Error.</h1>\r\n<h2 class=\"text-danger\">An error occurred while processing your request.</h2>\r\n\r\n");
            EndContext();
#line 9 "C:\Users\pashk\Documents\GitHub\gpo2\gp0\gp0\Views\Shared\Error.cshtml"
 if (Model.ShowRequestId)
{

#line default
#line hidden
            BeginContext(255, 53, true);
            WriteLiteral("    <h2>\r\n        <strong>Request ID:</strong> <code>");
            EndContext();
            BeginContext(309, 15, false);
#line 12 "C:\Users\pashk\Documents\GitHub\gpo2\gp0\gp0\Views\Shared\Error.cshtml"
                                      Write(Model.RequestId);

#line default
#line hidden
            EndContext();
            BeginContext(324, 20, true);
            WriteLiteral("</code>\r\n    </h2>\r\n");
            EndContext();
#line 14 "C:\Users\pashk\Documents\GitHub\gpo2\gp0\gp0\Views\Shared\Error.cshtml"
}

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<gp0.ViewModels.ErrorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
