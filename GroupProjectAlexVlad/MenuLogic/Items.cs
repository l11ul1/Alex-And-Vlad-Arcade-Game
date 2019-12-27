using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectAlexVlad.Seller.SellerLogic
{
    class Items
    {
        public Dictionary<string, int> itemsPlanet1;

        public Items() => itemsPlanet1 = new Dictionary<string, int>();

        //Dictionary containg the planets
        public Dictionary<string, int> GetItems(string id)
        {
            if (id == "Planet1")
            {
                itemsPlanet1.Clear();
                itemsPlanet1.Add("Ruby", 100);
                itemsPlanet1.Add("Black Diamonds", 3000);
                itemsPlanet1.Add("Pevet Steel", 100);
            }
            if (id == "Planet2")
            {
                itemsPlanet1.Clear();
                itemsPlanet1.Add("Star dust", 800);
                itemsPlanet1.Add("Sand magma", 2000);
                itemsPlanet1.Add("Gold", 200);
            }
            if (id == "Planet3")
            {
                itemsPlanet1.Clear();
                itemsPlanet1.Add("Alkane", 600);
                itemsPlanet1.Add("Spinmetal", 300);
                itemsPlanet1.Add("Plastoid Platium", 1000);
            }
            return itemsPlanet1;
        }
    }
}
