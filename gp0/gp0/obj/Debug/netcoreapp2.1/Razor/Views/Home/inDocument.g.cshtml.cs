#pragma checksum "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\inDocument.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dd1e612fbc35df7671e91cb2f930f6fe99b57bcd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_inDocument), @"mvc.1.0.view", @"/Views/Home/inDocument.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/inDocument.cshtml", typeof(AspNetCore.Views_Home_inDocument))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd1e612fbc35df7671e91cb2f930f6fe99b57bcd", @"/Views/Home/inDocument.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7d4f3d63bb95af09b98e12bce7c5fa931008cb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_inDocument : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<gp0.ViewModels.DocumentView>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\inDocument.cshtml"
  
    Layout = "HomeLayout";

#line default
#line hidden
            BeginContext(71, 1209, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3c9ddbc96d3d4e8cb6bfe9671a19138b", async() => {
                BeginContext(77, 164, true);
                WriteLiteral("\r\n<div>\r\n    <h4>Текст сообщения: </h4>\r\n    <textarea style=\"height: 300px; width: 100%; resize: none; border: 1px solid lightgray;\" readonly=\"readonly\" id=\"Text\">");
                EndContext();
                BeginContext(242, 10, false);
#line 8 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\inDocument.cshtml"
                                                                                                                      Write(Model.text);

#line default
#line hidden
                EndContext();
                BeginContext(252, 36, true);
                WriteLiteral("</textarea>\r\n    <h5>Дата отправки: ");
                EndContext();
                BeginContext(289, 10, false);
#line 9 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\inDocument.cshtml"
                  Write(Model.date);

#line default
#line hidden
                EndContext();
                BeginContext(299, 25, true);
                WriteLiteral("</h5>\r\n    <h5>Отправил: ");
                EndContext();
                BeginContext(325, 12, false);
#line 10 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\inDocument.cshtml"
             Write(Model.sender);

#line default
#line hidden
                EndContext();
                BeginContext(337, 163, true);
                WriteLiteral("</h5>\r\n    <div>\r\n        <button class=\"btn btn-secondary btn-block\" onclick=\"CheckDoc(\'Text\')\">Проверить подпись</button>\r\n        <input hidden=\"true\" name=\"id\"");
                EndContext();
                BeginWriteAttribute("value", " value=", 500, "", 516, 1);
#line 13 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\inDocument.cshtml"
WriteAttributeValue("", 507, Model.id, 507, 9, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(516, 121, true);
                WriteLiteral(" />\r\n        <input class=\"btn btn-secondary btn-block\" style=\"margin-top: 10px; background-color: darkred\" type=\"button\"");
                EndContext();
                BeginWriteAttribute("onclick", " onclick=\"", 637, "\"", 695, 3);
                WriteAttributeValue("", 647, "location.href=\'/Home/DeleteInDocument/", 647, 38, true);
#line 14 "C:\Users\Александра\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\inDocument.cshtml"
WriteAttributeValue("", 685, Model.id, 685, 9, false);

#line default
#line hidden
                WriteAttributeValue("", 694, "\'", 694, 1, true);
                EndWriteAttribute();
                BeginContext(696, 577, true);
                WriteLiteral(@"
               asp-controller=""Home"" asp-action=""DeleteInDocument"" value=""Удалить документ"" />
    </div>
    <div hidden=""true"" id=""result"">
        <div class=""divTableCellSmall""><h4 id=""Success1""></h4></div>
        <div class=""divTableCell"" hidden=""true"" id=""resultCertificate"">
            <h4>Информация о сертификате: </h4>
            <p id=""subject""></p>
            <p id=""issuer""></p>
            <p id=""dateto""></p>
            <p id=""algorithm""></p>
            <p id=""valid""></p>
            <p id=""message""></p>
        </div>
    </div>
</div>
");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1280, 4, true);
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<gp0.ViewModels.DocumentView> Html { get; private set; }
    }
}
#pragma warning restore 1591
