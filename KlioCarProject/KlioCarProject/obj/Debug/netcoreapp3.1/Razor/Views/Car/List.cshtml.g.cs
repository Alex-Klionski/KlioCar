#pragma checksum "E:\!Study\4sem\OOP\KlioCarProject\KlioCarProject\Views\Car\List.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95e50a737bef9ceceb211913f167b040921c0caa"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Car_List), @"mvc.1.0.view", @"/Views/Car/List.cshtml")]
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
#nullable restore
#line 1 "E:\!Study\4sem\OOP\KlioCarProject\KlioCarProject\Views\_ViewImports.cshtml"
using KlioCarProject.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95e50a737bef9ceceb211913f167b040921c0caa", @"/Views/Car/List.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7e0701cdaeb151162bb163940cbc143d09f2d885", @"/Views/_ViewImports.cshtml")]
    public class Views_Car_List : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Car>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "E:\!Study\4sem\OOP\KlioCarProject\KlioCarProject\Views\Car\List.cshtml"
 foreach(var p in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        <h3>");
#nullable restore
#line 6 "E:\!Study\4sem\OOP\KlioCarProject\KlioCarProject\Views\Car\List.cshtml"
       Write(p.Model);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n        <h4>");
#nullable restore
#line 7 "E:\!Study\4sem\OOP\KlioCarProject\KlioCarProject\Views\Car\List.cshtml"
       Write(p.Price.ToString("c"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n   </div>\r\n");
#nullable restore
#line 9 "E:\!Study\4sem\OOP\KlioCarProject\KlioCarProject\Views\Car\List.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Car>> Html { get; private set; }
    }
}
#pragma warning restore 1591
