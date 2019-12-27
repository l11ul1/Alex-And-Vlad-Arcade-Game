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

//Connect to other games and logic
using GroupProjectAlexVlad.MenuLogic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProjectAlexVlad.Battle
{

    public sealed partial class BattleMain : Page
    {
        //Keep on passing the same instance.
        PassAccount passedAccount;
        GameAccountManager manager;
        Account currentPlayer;

        Pictures pictures = new Pictures();

        public BattleMain() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            passedAccount = e.Parameter as PassAccount;
            currentPlayer = passedAccount.GameUser;
            manager = passedAccount.AccountManager;

            Droid.Source = pictures.ViewMenu1["Menu4"];
        }

        private void NavigatePlanet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            switch ((sender as Border).Name)
            {

                case "GoBack":
                    Frame.Navigate(typeof(GameMenu), passedAccount);
                    break;

                case "playBtn":                 
                    Frame.Navigate(typeof(BattleGamePage), passedAccount);
                    break;

                default: break;
            }
        }
    }
}
