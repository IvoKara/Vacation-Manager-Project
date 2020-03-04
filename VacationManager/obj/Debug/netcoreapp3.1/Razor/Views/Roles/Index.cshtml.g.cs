#pragma checksum "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "35b457bfed3e729df77a2aee2bbc5313af93ecfd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Roles_Index), @"mvc.1.0.view", @"/Views/Roles/Index.cshtml")]
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
#line 1 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\_ViewImports.cshtml"
using VacationManager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\_ViewImports.cshtml"
using VacationManager.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml"
using Data.Entitiy;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"35b457bfed3e729df77a2aee2bbc5313af93ecfd", @"/Views/Roles/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3b593b3f0e5abb58450caa2dbe88a3b293178197", @"/Views/_ViewImports.cshtml")]
    public class Views_Roles_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml"
  
    ViewData["Title"] = "Projects";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>");
#nullable restore
#line 10 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n\r\n");
#nullable restore
#line 12 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml"
  
    string[] role = { "" };
    try
    {
        var user = await _signInManager.UserManager.FindByNameAsync(User.Identity.Name);
        var tempRole = await _signInManager.UserManager.GetRolesAsync(user);
        role[0] = tempRole[0];
    }
    catch
    {

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<table class=\"table\">\r\n    <thead>\r\n");
#nullable restore
#line 29 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml"
         foreach (var item in _roleManager.Roles)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <th>\r\n                    ");
#nullable restore
#line 33 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml"
               Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    ");
#nullable restore
#line 34 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml"
               Write(item.UsersInRole.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </th>\r\n            </tr>\r\n");
#nullable restore
#line 37 "C:\Users\Elena\Desktop\VacationManager\VacationManager\Views\Roles\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </thead>\r\n</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public RoleManager<Role> _roleManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<User> _signInManager { get; private set; }
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
