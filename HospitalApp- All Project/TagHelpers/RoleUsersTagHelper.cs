using HospitalApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace HospitalApp.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "asp-role-users")]
    public class RoleUsersTagHelper : TagHelper
    {
        private readonly RoleManager<AppRole> _RoleManager;
        private readonly UserManager<AppUser> _UserManager;
        public RoleUsersTagHelper(RoleManager<AppRole> RoleManager, UserManager<AppUser> UserManager)
        {
            _UserManager = UserManager;
            _RoleManager = RoleManager;
        }
        [HtmlAttributeName("asp-role-users")]
        public string? Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var userNames = new List<string>();
#pragma warning disable CS8604 // Possible null reference argument.
            var role = await _RoleManager.FindByIdAsync(Role);
#pragma warning restore CS8604 // Possible null reference argument.
            if (role != null && role.Name != null)
            {
                foreach (var user in _UserManager.Users)
                {
                    if (await _UserManager.IsInRoleAsync(user, role.Name))
                    {
#pragma warning disable CS8604 // Possible null reference argument.
                        userNames.Add(user.UserName ?? "");
#pragma warning restore CS8604 // Possible null reference argument.
                    }
                }
                output.Content.SetHtmlContent(userNames.Count == 0 ? "Kullanıcı Yok" : setHtml(userNames));
            }
        }

        private string setHtml(List<string> userNames)
        {
            var html = "<select>";
            foreach (var item in userNames)
            {
                html += "<option>" + item + "</option>";

            }
            html += "</select>";
            return html;
        }
    }
}