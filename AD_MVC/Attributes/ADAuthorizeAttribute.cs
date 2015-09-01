using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace AD_MVC.Attributes
{
    public class ADAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] _groupNames;

        public ADAuthorizeAttribute(params string[] groupNames)
        {
            _groupNames = groupNames;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // get the current user
            var currentUser = HttpContext.Current.User;

            // if not logged in, he/she is NOT authorized
            if (!currentUser.Identity.IsAuthenticated) return false;

            return IsUserInGroups(currentUser, _groupNames);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }

        private bool IsUserInGroups(IPrincipal principal, params string[] groupNames)
        {
            if (principal == null || groupNames == null || !groupNames.Any()) return true;

            using (var pc = new PrincipalContext(ContextType.Domain, 
                                                    ConfigurationManager.AppSettings["AD_Domain"]))
            {
                // find user
                var user = UserPrincipal
                            .FindByIdentity(pc, IdentityType.SamAccountName,
                                                principal.Identity.Name);

                // if no user found, NOT authorized
                if (user == null) return false;

                // get groups that the user is a member
                var groups = user.GetAuthorizationGroups().ToList();

                if (!groups.Any()) return false;

                return groups.Any(g => groupNames.Contains(g.Name));
            }
            
        }
    }
}