using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectAlexVlad.Seller.SellerLogic
{
    class Vendor
    {
        public string PlanetId { set; get; }
        public int Credits { set; get; }
        public Dictionary<string, int> VendorItems { set; get; }
        public Vendor(string planetId, Dictionary<string, int> vendorItems, int credits)
        {
            PlanetId = planetId;
            VendorItems = vendorItems;
            Credits = credits;
        }
    }
}
