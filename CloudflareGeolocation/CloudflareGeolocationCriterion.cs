using System.Security.Principal;
using System.Web;
using EPiServer.Personalization.VisitorGroups;
using Mediachase.Commerce.Security;

namespace Web.Business.VisitorGroups
{
    [VisitorGroupCriterion(
        Category = "Visitor Groups",
        DisplayName = "Cloudflare Geolocation",
        Description = "Geolocation based on Cloudflare HTTP header")]
    public class CloudflareGeolocationCriterion : CriterionBase<CloudflareGeolocationModel>
    {
        public override bool IsMatch(IPrincipal principal, HttpContextBase httpContext)
        {
            // uncomment if you want this Criterion to apply only to Authenticated users
            //if (principal == null || !principal.Identity.IsAuthenticated) return false;

            var geolocationHeader = httpContext.Request?.Headers["CF-IPCountry"];

            return Model.CountryCode.ToLower() == geolocationHeader.ToLower();
        }
    }
}
