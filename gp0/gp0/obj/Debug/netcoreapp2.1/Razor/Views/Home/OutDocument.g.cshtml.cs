#pragma checksum "C:\Users\ДНС\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\OutDocument.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0c2f5aca6c307dfa9ed1f65b5ef7756e5585442b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_OutDocument), @"mvc.1.0.view", @"/Views/Home/OutDocument.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/OutDocument.cshtml", typeof(AspNetCore.Views_Home_OutDocument))]
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
#line 1 "C:\Users\ДНС\Documents\GitHub\gpo2.0\gp0\gp0\Views\_ViewImports.cshtml"
using gp0;

#line default
#line hidden
#line 2 "C:\Users\ДНС\Documents\GitHub\gpo2.0\gp0\gp0\Views\_ViewImports.cshtml"
using gp0.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0c2f5aca6c307dfa9ed1f65b5ef7756e5585442b", @"/Views/Home/OutDocument.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7d4f3d63bb95af09b98e12bce7c5fa931008cb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_OutDocument : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<gp0.ViewModels.DocumentView>
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
#line 2 "C:\Users\ДНС\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\OutDocument.cshtml"
  
    Layout = "HomeLayout";

#line default
#line hidden
            BeginContext(71, 612, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0007f974291b497aa3053a2d3226d9d9", async() => {
                BeginContext(77, 176, true);
                WriteLiteral("\r\n    <div>\r\n        <h4>Текст сообщения: </h4>\r\n        <textarea style=\"height: 300px; width: 100%; resize: none; border: 1px solid lightgray;\" readonly=\"readonly\" id=\"Text\">");
                EndContext();
                BeginContext(254, 10, false);
#line 8 "C:\Users\ДНС\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\OutDocument.cshtml"
                                                                                                                          Write(Model.text);

#line default
#line hidden
                EndContext();
                BeginContext(264, 40, true);
                WriteLiteral("</textarea>\r\n        <h4>Дата отправки: ");
                EndContext();
                BeginContext(305, 10, false);
#line 9 "C:\Users\ДНС\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\OutDocument.cshtml"
                      Write(Model.date);

#line default
#line hidden
                EndContext();
                BeginContext(315, 31, true);
                WriteLiteral("</h4>\r\n        <h4>Отправлено: ");
                EndContext();
                BeginContext(347, 14, false);
#line 10 "C:\Users\ДНС\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\OutDocument.cshtml"
                   Write(Model.receiver);

#line default
#line hidden
                EndContext();
                BeginContext(361, 175, true);
                WriteLiteral("</h4>\r\n        <div>\r\n            <button class=\"btn btn-secondary btn-block\" onclick=\"CheckDoc(\'Text\')\">Проверить подписи</button>\r\n            <input hidden=\"true\" name=\"id\"");
                EndContext();
                BeginWriteAttribute("value", " value=", 536, "", 552, 1);
#line 13 "C:\Users\ДНС\Documents\GitHub\gpo2.0\gp0\gp0\Views\Home\OutDocument.cshtml"
WriteAttributeValue("", 543, Model.id, 543, 9, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(552, 124, true);
                WriteLiteral("/>\r\n            <input type=\"button\" asp-action=\"DeleteOutDocument\" value=\"Удалить документ\"/>\r\n        </div>\r\n    </div>\r\n");
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
            BeginContext(683, 2, true);
            WriteLiteral("\r\n");
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
