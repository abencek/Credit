using System.Collections.Generic;

namespace Credit.Web.Models
{
    public class AdminIndexViewModel
    {
        public IEnumerable<IdentityUserViewModel> Users;

        public IEnumerable<IdentityRoleViewModel> Roles;

        public string DropdownsData;
    }
}
