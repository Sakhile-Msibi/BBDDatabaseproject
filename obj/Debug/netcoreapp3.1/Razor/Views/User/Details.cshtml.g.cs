#pragma checksum "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4921735d6053a0a4f23c5c3777c6ef7021744e06"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Details), @"mvc.1.0.view", @"/Views/User/Details.cshtml")]
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
#line 1 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\_ViewImports.cshtml"
using Website;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\_ViewImports.cshtml"
using Website.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4921735d6053a0a4f23c5c3777c6ef7021744e06", @"/Views/User/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"971d47fbe439df3910fb180c393f0b2f21208c79", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<Website.Models.UserModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
  
    ViewData["Title"] = "Person";
    int i = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 8 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
 foreach (UserModel item in Model)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
     if (item != null)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<h2>");
#nullable restore
#line 12 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n<hr />\r\n<div>\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
#nullable restore
#line 17 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
       Write(Html.DisplayNameFor(model => model[i].FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
#nullable restore
#line 20 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
       Write(Html.DisplayFor(model => model[i].FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
#nullable restore
#line 23 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
       Write(Html.DisplayNameFor(model => model[i].UserId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
#nullable restore
#line 26 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
       Write(Html.DisplayFor(model => model[i].UserId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n");
#nullable restore
#line 30 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
    i++;

#line default
#line hidden
#nullable disable
#nullable restore
#line 33 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
         
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 34 "C:\Users\bbdnet2232\Desktop\CSTeam_management\Views\User\Details.cshtml"
     
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<Website.Models.UserModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
