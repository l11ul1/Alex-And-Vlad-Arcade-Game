using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GroupProjectAlexVlad.MenuLogic;

namespace GroupProjectAlexVlad.Seller.SellerLogic
{
    class PurchaseSell
    {
        Account customer;
        Vendor vendor;

        public PurchaseSell(Account player, Vendor seller)
        {   
            customer = player;
            vendor = seller;
        }

        public int MakeApurchase(string key, int value)
        {
            if (vendor.VendorItems.ContainsKey(key))
            {
                if (customer.TotalCredits < value || customer.ResourceStats.ContainsKey(key) == true) { return 0; }
                else
                {
                    customer.ResourceStats.Add(key, value);
                    vendor.VendorItems.Remove(key);
                    vendor.Credits += value;
                    customer.TotalCredits -= value;
                    return 1;
                }
            }
            return 0;
        }

        public int SellAnItem(string key, int value)
        {
            if (customer.ResourceStats.ContainsKey(key))
            {
                if (vendor.VendorItems.ContainsKey(key) == false)
                {
                    vendor.VendorItems.Add(key, value);
                    customer.ResourceStats.Remove(key);               
                    vendor.Credits -= value;
                    customer.TotalCredits += value;
                    return 1;
                }
                return 0;
            }
            return 0;
        }

    }
}
