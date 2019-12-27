using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using GroupProjectAlexVlad.MenuLogic;
using Windows.UI.Xaml.Controls;

namespace GroupProjectAlexVlad.ShipDeck.GameLogic
{
    class MatchingManager
    {
        Pictures picture;

        public string GameLog { get; set; }

        //A list with our (game tiles) (image tiles) (image states) 
        public List<Image> TileListAfter { get; set; }
        public List<BitmapImage> ImageListAfter { get; set; }
        public List<BitmapImage> ImageState { get; set; }
        public string BackgroundImage { get; set; }
   
        public Dictionary<Image, BitmapImage> imageTiles = new Dictionary<Image, BitmapImage>();
        public Dictionary<Image, BitmapImage> UsedTile { get; set; }
     
        
        public MatchingManager(List<Image> imageTileList, string mapPicker)
        {

            UsedTile = new Dictionary<Image, BitmapImage>();

            //Put the images in a list and then shuffle them around
            TileListAfter = imageTileList;
            TileListAfter = TileListAfter.OrderBy(a => Guid.NewGuid()).ToList();

            //Get images
            picture = new Pictures();
            picture.ImageTile(mapPicker);
            ImageListAfter = picture.ImageList;
            ImageState = picture.ImageState;
            BackgroundImage = picture.BackgroundImage;
       
            //Assign a tile (key) to an image (value)
            int i = 0;
            for (int y=0; y<8; y++)
            {
                imageTiles.Add(TileListAfter[y], picture.ImageList[i]);
                if (y % 2 != 0) { i++; }
            }          
        }
        
        public int PlayGame(Image currentTile)
        {
            //Check if the tile already exists
            if (UsedTile.ContainsKey(currentTile) == false)
            {
                //Get the image of the currentTile and add it to the usedTile dictionary        
                UsedTile[currentTile] = imageTiles[currentTile];

                // Do the comparison between tiles
                if (UsedTile.Count() == 2 && UsedTile.Values.First() == UsedTile.Values.Last())
                {
                    AddToGameLog("Cards do match");
                    return 3;
                }
                else if (UsedTile.Count() == 2 && UsedTile.Values.First() != UsedTile.Values.Last())
                {
                    AddToGameLog("Cards don't match");
                    return 2;
                }

                GameLog += "Tile clicked\n";
                return 1;
            }
            else
            {
                GameLog += "Same Tile clicked\n";
                return 0;
            }
        }

        //Reset the dictionary so it can be used again
        public void ResetDictionary() => UsedTile.Clear();

        //Add the currentTile to the dictionary
        public BitmapImage ChangePicture(Image currentTile) => imageTiles[currentTile];

        //Game log
        private void AddToGameLog(string x)
        {
            GameLog += "2nd Tile clicked\n";
            GameLog += x+"\n";
        }
    }
}

