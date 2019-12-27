
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

//Connect to other games and logic
using GroupProjectAlexVlad.MenuLogic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProjectAlexVlad.ShipDeck
{

    public sealed partial class SpaceShipDeckMain : Page
    {
        //Keep on passing the same instance.
        PassAccount passedAccount;
        GameAccountManager manager;
        Account currentPlayer;

        //Other classes
        Pictures pictures = new Pictures();

        //Specific for this game
        List<Image> gameMap;    
        public Dictionary<Image, string> maps = new Dictionary<Image, string>();

        public SpaceShipDeckMain()
        {
            InitializeComponent();

            gameMap = new List<Image>(new Image[] { map1, map2, map3, map4, map5, map6 });

            for (int x = 0; x < 6; x++)
            {
                maps[gameMap[x]] = pictures.MapsImage[x];

                gameMap[x].Source = pictures.MapsLogo[x];
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            passedAccount = e.Parameter as PassAccount;
            currentPlayer = passedAccount.GameUser;
            manager = passedAccount.AccountManager;
        }

        //Play a map
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            passedAccount.ParameterSTR = maps[sender as Image];           
            Frame.Navigate(typeof(SpaceShipDeckGamePage), passedAccount);
        }

        //Go back
        private void GameClick(object sender, RoutedEventArgs e)
        {                     
            if (sender == playerBack) { Frame.Navigate(typeof(GameMenu), passedAccount); }
        }


    }
}

