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
using Windows.Media.Core;
using Windows.Media.Playback;

//Connect to other games and logic
using GroupProjectAlexVlad.MenuLogic;
using GroupProjectAlexVlad.ShipDeck;
using GroupProjectAlexVlad.Battle;
using GroupProjectAlexVlad.Seller;



// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProjectAlexVlad
{

    public sealed partial class GameMenu : Page
    {
        //Keep on passing the same instance.
        PassAccount passedAccount;
        GameAccountManager manager;
        Account currentPlayer;

        //Other classes
        Pictures pictures = new Pictures();
        Planets planet = new Planets();

        //Specific for this game
        bool NavigateBattleUsable = true;

        //An instance of media player and A list for our music (All music was made by Alex Canales)
        MediaPlayer musicPlayer = new MediaPlayer();
        SoundsManager sounds = new SoundsManager();



        public GameMenu()
        {
            InitializeComponent();

            map1.Source = pictures.ShipDeckLogo;
            musicPlayer.Source = MediaSource.CreateFromUri(new Uri(sounds.MusicList[0]));
            NavigationCacheMode = NavigationCacheMode.Enabled;

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            passedAccount = e.Parameter as PassAccount;
            currentPlayer = passedAccount.GameUser;
            manager = passedAccount.AccountManager;

            //Change the labels for user
            userIDlbl.Text = currentPlayer.UserID;
            gamesCompletedlbl.Text = $"Level: \n{currentPlayer.CurrentLevel.ToString() }";

            //Change the labels for ship data
            CrewMembers.Text = $"Credits: \n{currentPlayer.TotalCredits.ToString() }";
            ShipStrength.Text = $"Ship Strength: \n{currentPlayer.SpaceShipStats["ShipStrength"].ToString() }";
            ShipCapacity.Text = $"Current Planet: \n{currentPlayer.CurrentPlanet.ToString() }";

            
            //Set the spaceship
            for (int interval = 0; interval < 10; interval++)
            {
                if (currentPlayer.CurrentLevel == interval)
                {
                    SpaceShip.Source = pictures.SpaceShips[interval];
                }
            }

            //Set the planet image
            foreach (KeyValuePair<string, BitmapImage> planetCur in planet.PlanetDict)
            {
                if (planetCur.Key == currentPlayer.CurrentPlanet)
                {
                    Space.Visibility = Visibility.Collapsed;
                    BigImage.Source = planetCur.Value;           
                }                       
            }

            //Set usable (Battle)
            if (currentPlayer.SpaceShipStats["ShipStrength"] <= 0)
            {
                NavigateBattle.Background = new SolidColorBrush(Windows.UI.Colors.Red);
                NavigateBattleUsable = false;
            }
            else
            {
                NavigateBattle.Background = new SolidColorBrush(Windows.UI.Colors.White);
                NavigateBattleUsable = true;
            }

        }

        //Click event
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if ((sender as Image).Source == pictures.ShipDeckLogo)
            {
                Frame.Navigate(typeof(SpaceShipDeckMain), passedAccount);
            }            
        }

        //Music play event
        private void PlayerMusic1_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleSwitch toggleSwitch)
            {
                if (toggleSwitch.IsOn == true)
                {              
                    musicPlayer.Play();
                }
                else
                {
                    musicPlayer.Pause();
                }
            }
        }

      //Click event
        private void NavigatePlanet_Tapped(object sender, TappedRoutedEventArgs e)
        {  
            switch ((sender as Border).Name)
            {
                case "NavigatePlanet":
                    Frame.Navigate(typeof(NavigationMap), passedAccount);
                    break;

                case "LaunchDropShip":
                    PrintSpecific("Thanks for playing our game. We'll be updating it as time goes on, but currently, this is what we have.");
                    break;

                case "NavigateBattle":

                    if (NavigateBattleUsable == true)
                    {
                        musicPlayer.Pause();
                        Frame.Navigate(typeof(BattleMain), passedAccount);
                    }
                    else
                    {
                        PrintSpecific("Your hull is damaged!");
                    }
                    
                    break;

                case "Sell":                
                    Frame.Navigate(typeof(SellerMain), passedAccount);
                    break;

                case "LeaveGame":
                    manager.UpdateAccount();
                    Application.Current.Exit();
                    break;

                default: break;
            }
        }
 
        private async void PrintSpecific(string x)
        {
            await new ContentDialog()
            {
                Content = x,
                CloseButtonText = "Ok"
            }.ShowAsync();
        }
    }

   
}
