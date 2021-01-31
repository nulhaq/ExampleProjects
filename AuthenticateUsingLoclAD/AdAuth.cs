using System.Diagnostics;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApi.Business
{
    public class AdAuth : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var user = UserManager.GetUserFromContext();
            var isValidUser = UserManager.IsUserValid(user.UserName, user.Password);
            return isValidUser;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            Debug.WriteLine("user is not authorized.");
            base.HandleUnauthorizedRequest(actionContext);
        }
    }
}
