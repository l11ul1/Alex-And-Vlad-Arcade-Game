using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using GroupProjectAlexVlad.Seller.SellerLogic;

namespace GroupProjectAlexVlad.MenuLogic
{
    [DataContract]
    class Account
    {

        //User details
        [DataMember]
        public string UserID { get; set; }
        [DataMember]
        public string UserPassword { get; set; }

        //Player stats
        [DataMember]
        public int CurrentLevel { get; set; }
        [DataMember]
        public int TotalScore { get; set; }
        [DataMember]
        public int TotalCredits { get; set; }

        //Spaceship stats     
        public SpaceShip Spacecraft { get; set; }

        [DataMember]
        public Dictionary<string, int> SpaceShipStats { get; set; }

        //Resource stats
        public Items Resources{get; set;}

        [DataMember]
        public Dictionary<string, int> ResourceStats { get; set; }

        //Planet
        [DataMember]
        public string CurrentPlanet { get; set; }
        
        public Account(string userID, string userPassword)
        {
            //Account 
            UserID = userID;
            UserPassword = userPassword;

            //Account Stats
            CurrentLevel = 1;
            TotalScore = 0;
            TotalCredits = 2000;
            
            //Spaceship
            Spacecraft = new SpaceShip();
            SpaceShipStats = Spacecraft.SpaceShipStats;

            //Resources
            Resources = new Items();
            ResourceStats = Resources.GetItems("Planet1");
            ResourceStats.Remove(ResourceStats.Keys.Last());

            //Planet         
            CurrentPlanet = "Unkown space";
                
        }       
    }
}
