#pragma checksum "C:\Users\pashk\Documents\GitHub\gpo2\gp0\gp0\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c6f50563092871aeb8b2b8eaf385f86bc12cb7fd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Index.cshtml", typeof(AspNetCore.Views_Home_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c6f50563092871aeb8b2b8eaf385f86bc12cb7fd", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7d4f3d63bb95af09b98e12bce7c5fa931008cb7", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "C:\Users\pashk\Documents\GitHub\gpo2\gp0\gp0\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "Home Page";

#line default
#line hidden
            BeginContext(45, 2388, true);
            WriteLiteral(@"
<div id=""min-width"">
    <div id=""container"">
        <h3 class=""text-center"">Подпись и проверка XmlDSig</h3>
        <div class=""mainContent"">
            <div id=""info"">
                <div id=""info_msg"" style=""text-align:center;"">
                    <span id=""PlugInEnabledTxt"">Проверка</span>
                    <br>
                    <span id=""PlugInVersionTxt"" lang=""ru""> </span>
                    <span id=""CSPVersionTxt"" lang=""ru""> </span>
                    <br>
                    <span id=""CSPNameTxt"" lang=""ru""> </span>
                </div>
            </div>

           

            <div class=""tab-content"">

                <div id=""panel1"" class=""tab-pane active"">
                    <p id=""info_msg"" name=""CertificateTitle"">Сертификат:</p>
                    <div id=""item_border"" name=""CertListBoxToHide"">
                        <select size=""4"" name=""CertListBox"" id=""CertListBox"" style=""width:100%;resize:none;border:0;""></select>
                    </div>

  ");
            WriteLiteral(@"                  <div id=""boxdiv"" style=""display:none"">
                        <span id=""errorarea"">
                            У вас отсутствуют личные сертификаты. Вы можете получить сертификат от тестового УЦ, предварительно установив корневой сертификат тестового УЦ в доверенные.
                        </span>
                    </div>


                    <div id=""cert_info"">
                        <h3 id=""cert_txt"" style=""visibility:hidden"">Информация о сертификате</h3>
                        <p class=""info_field"" id=""subject""></p>
                        <p class=""info_field"" id=""issuer""></p>
                        <p class=""info_field"" id=""from""></p>
                        <p class=""info_field"" id=""till""></p>
                        <p class=""info_field"" id=""provname""></p>
                        <p class=""info_field"" id=""algorithm""></p>
                        <p class=""info_field"" id=""status""></p>
                    </div>
                    <br />

                   ");
            WriteLiteral(@"
                    

                      
                        <script language=""javascript"">
                                CheckForPlugIn('isPlugInEnabled');
                        </script>
                   
                </div>

                

            </div>
        </div>
    </div>
</div>




");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
