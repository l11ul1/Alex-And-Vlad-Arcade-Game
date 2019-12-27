using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace GroupProjectAlexVlad.MenuLogic
{
    class Planets
    {
        public List<string> Planet { get; set; }
        public Dictionary<string, BitmapImage> PlanetDict { get; set; }
        public Dictionary<string, BitmapImage> PlanetLandDict { get; set; }
        readonly Pictures pictures = new Pictures();

        public Planets()
        {
            PlanetDict = new Dictionary<string, BitmapImage>();
            PlanetLandDict = new Dictionary<string, BitmapImage>();

            //Put the planets in a list
            Planet = new List<string>(new string[] { "Planet1", "Planet2", "Planet3", "Planet4", "Planet5" });

            //Put the planets in a dictionary
            for (int i = 0; i < Planet.Count; i++)
            {
                PlanetDict[Planet[i]] = pictures.Planets[i];
            }

            for (int i = 0; i < 3; i++)
            {
                PlanetLandDict[Planet[i]] = pictures.PlanetLand[i];
            }
        }
    }
}
