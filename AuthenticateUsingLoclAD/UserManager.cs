using System;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Web;

namespace WebApi.Helpers
{
    public static class UserManager
    {
        public static bool IsUserValid(string userName, string password)
        {
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "-your-ad-domain"))
                {
                    var valid = pc.ValidateCredentials(userName, password);
                    return valid;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static User GetUserFromContext()
        {
            var user = new User();
            HttpContext httpContext = HttpContext.Current;
            string authHeader = httpContext.Request.Headers["Authorization"];

            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(UserToken()));

                int seperatorIndex = usernamePassword.IndexOf(':');

                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);

                user.UserName = username;
                user.Password = password;
            }

            return user;
        }

        public static string UserToken()
        {
            HttpContext httpContext = HttpContext.Current;
            string authHeader = httpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                return encodedUsernamePassword;
            }

            return string.Empty;
        }

       
    }
}
