using EPiServer.Data.Dynamic;
using EPiServer.Personalization.VisitorGroups;

namespace Web.Business.VisitorGroups
{
    [EPiServerDataStore(AutomaticallyCreateStore = true, AutomaticallyRemapStore = true)]
    public class CloudflareGeolocationModel : CriterionModelBase
    {
        
        public string CountryCode { get; set; }

        public override ICriterionModel Copy()
        {
            return ShallowCopy();
        }
    }

}
