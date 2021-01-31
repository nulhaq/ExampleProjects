using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web.Http;

namespace WebApi.Controllers
{
    [AdAuth]
    public class UserGroupsController : ApiController
    {
        public IEnumerable<string> Get()
        {

            var userGrps = new List<string>();
            var user = UserManager.GetUserFromContext();
            var isValidUser = UserManager.IsUserValid(user.UserName, user.Password);

            if (!isValidUser) return userGrps;

            var userToken = UserManager.UserToken();
            userGrps.Add($"Token:{userToken}");
            UserPrincipal userPrinciple = UserPrincipal.FindByIdentity(
                new PrincipalContext(ContextType.Domain, "--your-ad-domain--"), IdentityType.SamAccountName, user.UserName);
            if (userPrinciple != null)
            {
                userGrps.AddRange(from GroupPrincipal @group in userPrinciple.GetGroups() select @group.Name);
            }

            return userGrps;
        }
    }
}
