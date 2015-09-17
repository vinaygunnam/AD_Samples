using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADQuery
{
    class ActiveDirectoryQueryHandler : IActiveDirectoryQueryHandler
    {
        public ActiveDirectoryUser GetUser(string username)
        {
            string domain = null;
            string domainPrefix = null;
            using (var principalContext = new PrincipalContext(ContextType.Domain, domain))
            {
                return FetchUser(username, principalContext, domainPrefix);
            }
        }

        private static ActiveDirectoryUser FetchUser(string username, PrincipalContext principalContext, string domainPrefix)
        {
            var user = UserPrincipal
                .FindByIdentity(principalContext,
                    IdentityType.SamAccountName,
                    String.Format("{0}\\{1}", domainPrefix, username));

            if (user == null) return null;

            return new ActiveDirectoryUser
            {
                Username = username,
                Groups = user.GetGroups().Select(g => new ActiveDirectoryGroup
                {
                    Name = g.Name
                })
            };
        }

        public ActiveDirectoryUser GetUser(string username, string password)
        {
            string domain = null;
            string domainPrefix = null;
            using (var principalContext = new PrincipalContext(ContextType.Domain, domain))
            {
                var isValid = principalContext.ValidateCredentials(username, password);
                if (isValid)
                {
                    return FetchUser(username, principalContext, domainPrefix);
                }
                else
                {
                    return null;
                }
            }
        }
    }

    interface IActiveDirectoryQueryHandler
    {
        ActiveDirectoryUser GetUser(string username);
        ActiveDirectoryUser GetUser(string username, string password);
    }

    class ActiveDirectoryUser
    {
        public string Username { get; set; }
        public IEnumerable<ActiveDirectoryGroup> Groups { get; set; }
    }

    class ActiveDirectoryGroup
    {
        public string Name { get; set; }
    }
}
