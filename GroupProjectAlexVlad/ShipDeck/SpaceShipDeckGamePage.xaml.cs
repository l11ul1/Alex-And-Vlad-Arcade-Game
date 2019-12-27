using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;

//Connect to other games and logic
using GroupProjectAlexVlad.ShipDeck.GameLogic;
using GroupProjectAlexVlad.MenuLogic;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProjectAlexVlad.ShipDeck
{

    public sealed partial class SpaceShipDeckGamePage : Page
    {
        //Keep on passing the same instance.
        PassAccount passedAccount;
        GameAccountManager manager;
        Account currentPlayer;

        //Specific for this game
        int totalClicks = 0;
        MatchingManager tileMatching;
        List<Image> correctImage;

        public SpaceShipDeckGamePage()
        {
            InitializeComponent();
            correctImage = new List<Image>(new Image[] { TileOneCorrect, TileTwoCorrect, TileThreeCorrect, TileFourCorrect });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //Get the data from the other page
            passedAccount = e.Parameter as PassAccount;
            currentPlayer = passedAccount.GameUser;
            manager = passedAccount.AccountManager;

            List<Image> imageTileList = new List<Image>(new Image[] { TileOne, TileTwo, TileThree, TileFour, TileFive, TileSix, TileSeven, TileEight });

            tileMatching = new MatchingManager(imageTileList, passedAccount.ParameterSTR);

            //Start changes
            GridData1.Background = new ImageBrush { ImageSource = new BitmapImage(new Uri(tileMatching.BackgroundImage)) };

            foreach (Image item in tileMatching.TileListAfter)
            {
                item.Source = tileMatching.ImageState[0];
            }

        }

        //This event handler is in charge of the image click
        private async void Tile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            (sender as Image).Source = tileMatching.ChangePicture(sender as Image);
            totalClicks++;

            switch (tileMatching.PlayGame(sender as Image))
            {

                case 2:

                    
                    await Task.Delay(TimeSpan.FromSeconds(0.35));
                    
                    foreach (Image item in tileMatching.UsedTile.Keys)
                    {
                        item.Source = tileMatching.ImageState[0];
                    }

                    tileMatching.ResetDictionary();

                    break;

                case 3:
                  
                    await Task.Delay(TimeSpan.FromSeconds(0.35));

                    correctImage[0].Source = (sender as Image).Source;
                    correctImage.RemoveAt(0);

                    foreach (Image item in tileMatching.UsedTile.Keys)
                    {
                        item.Source = tileMatching.ImageState[1];
                        item.Tapped -= Tile_Tapped;
                    }

                    gameCounterTxt.Text = $"{(correctImage.Count() - 4) * -1 } / 4";
                    tileMatching.ResetDictionary();

                    if (correctImage.Count() == 0)
                    {
                        TilesDialog("Game Complete", "Game won in " + totalClicks + " clicks.");
                        currentPlayer.SpaceShipStats["ShipStrength"] += 10;
                    }
                 
                    break;

                default: break;
            }

            //Change the game log text on the UI
            gameLogTxt.Text = tileMatching.GameLog;

        }

  
        private async void TilesDialog(string title, string info)
        {
            await new ContentDialog()
            {
                Title = title,
                Content = info,
                CloseButtonText = "Ok"
            }.ShowAsync();
        }

      
        private void GameClick(object sender, RoutedEventArgs e)
        {
            if (sender == playerBack)
            {
                Frame.Navigate(typeof(SpaceShipDeckMain), passedAccount);
            }
        }
    }
}
