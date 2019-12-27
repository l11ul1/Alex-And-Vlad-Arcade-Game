using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace GroupProjectAlexVlad.MenuLogic
{

    //All the pictures/ bitmaps will be retrieved from here
    class Pictures
    {
        //menu
        public Dictionary<string, BitmapImage> ViewMenu1 { get; set; }

        //Spaceships and planets
        public List<BitmapImage> SpaceShips { get; set; }
        public List<BitmapImage> Planets { get; set; }
        public List<BitmapImage> PlanetLand { get; set; }

        //ship deck data
        public BitmapImage ShipDeckLogo { get; set; }

        //Memory Game main menu     
        public List<string> MapsImage { get; set; }
        public List<BitmapImage> MapsLogo { get; set; }

        //Image tile data (Memory game)
        public List<BitmapImage> ImageList { get; set; }
        public List<BitmapImage> ImageState { get; set; }
        public string BackgroundImage { get; set; }

        //Battle game
        public List<BitmapImage> BattleList { get; set; }

        //For the main menu
        public Pictures()
        {
            
            ViewMenu1 = new Dictionary<string, BitmapImage>();
            SpaceShips = new List<BitmapImage>();
            Planets = new List<BitmapImage>();
            PlanetLand = new List<BitmapImage>();
            BattleList = new List<BitmapImage>();
            MapsImage = new List<string>();
            MapsLogo = new List<BitmapImage>();


            //Put the backgrounds in a list
            for (int i = 1; i < 5; i++)
            {
                ViewMenu1["Menu" + i.ToString()] = new BitmapImage(new Uri("ms-appx:///Assets/MenuBackground/Menu" + i.ToString() + ".jpg"));
            }
            //Insert the gif background
            ViewMenu1["Menu2-2"] = new BitmapImage(new Uri("ms-appx:///Assets/MenuBackground/Menu2-2.gif"));
            ViewMenu1["Menu2-3"] = new BitmapImage(new Uri("ms-appx:///Assets/MenuBackground/Menu2-3.gif"));

            //Spaceships
            for (int i = 1; i < 9; i++)
            {
                SpaceShips.Add(new BitmapImage(new Uri("ms-appx:///Assets/SpaceShips/SpaceShip" + i.ToString() + ".png")));
            }

            //Get the shipdeck picture
            ShipDeckLogo = new BitmapImage(new Uri("ms-appx:///ShipDeck/Assets/BackgroundMenu.jpg"));
         

            //Get the planet pictures
            for (int i = 1; i < 6; i++)
            {
                Planets.Add(new BitmapImage(new Uri("ms-appx:///Assets/Planets/Planet" + i.ToString() + ".jpg")));
            }
            

            //For Memory game main menu
            for (int x = 0; x < 6; x++)
            {
                MapsImage.Add("ms-appx:///ShipDeck/Assets/Maps/map" + (x + 1).ToString());
                MapsLogo.Add(new BitmapImage(new Uri("ms-appx:///ShipDeck/Assets/Maps/map" + (x + 1).ToString() + "/BackGroundGame.jpg")));
            }
       
            //For seller game main menu
            for (int x=1; x<4; x++)
            {
                PlanetLand.Add(new BitmapImage(new Uri("ms-appx:///Assets/Planets/Planet" + x.ToString() + "Land.png")));

            }

            //Battle game
            for (int x = 1; x < 6; x++)
            {
                BattleList.Add(new BitmapImage(new Uri("ms-appx:///Assets/Planets/PlanetBattle" + x.ToString() + ".jpg")));

            }

        }

        //_______________________________________________________________________

        //For Memory game
        public void ImageTile(string mapPicker)
        {
            BackgroundImage = mapPicker + "/BackGroundGame.jpg";

            ImageList = new List<BitmapImage>();
            ImageState = new List<BitmapImage>();

            for (int i = 1; i < 5; i++)
            {
                ImageList.Add(new BitmapImage(new Uri(mapPicker + "/gameImage" + i.ToString() + ".png")));

                if (i < 3)
                {
                    ImageState.Add(new BitmapImage(new Uri(mapPicker + "/gameCheck" + i.ToString() + ".png")));
                }
            }

            //Randomize it
            ImageList = ImageList.OrderBy(a => Guid.NewGuid()).ToList();
                     
        }

        //_______________________________________________________________________

    }
}
