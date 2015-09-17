using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pc = new PrincipalContext(ContextType.Domain, "corp.local"))
            {
                var isValid = pc.ValidateCredentials("vgunnam", "L@mbda009");
                // find user
                var user = UserPrincipal
                            .FindByIdentity(pc, IdentityType.SamAccountName, 
                                                "CORP\\vgunnam");

                // get groups that the user is a member
                var groups = user.GetAuthorizationGroups();

                Console.WriteLine(user);
            }
        }
    }
}
