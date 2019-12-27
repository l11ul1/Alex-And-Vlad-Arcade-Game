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
using GroupProjectAlexVlad.Seller.SellerLogic;
using GroupProjectAlexVlad.MenuLogic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProjectAlexVlad.Seller
{
    public sealed partial class Upgrade : Page
    {
        //Keep on passing the same instance.
        PassAccount passedAccount;
        GameAccountManager manager;
        Account currentPlayer;

        //Other classes
        Pictures pictures = new Pictures();

        //Specific for this game
        int baseMoney = 4000;

        public Upgrade()
        {       
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            passedAccount = e.Parameter as PassAccount;
            currentPlayer = passedAccount.GameUser;
            manager = passedAccount.AccountManager;

            for (int interval = 0; interval < 10; interval++)
            {
                if (currentPlayer.CurrentLevel == interval)
                {
                    ship.Source = pictures.SpaceShips[interval];
                }
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string item in passedAccount.GameUser.ResourceStats.Keys)
            {
                listOfMyItems.Items.Add(item);
            }
            coins.Text = passedAccount.GameUser.TotalCredits.ToString();
        }

        //Clicking the button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == goback)
            {                
                this.Frame.Navigate(typeof(SellerMain), passedAccount);
            }

            if (sender == upgrade)
            {
                if (passedAccount.GameUser.TotalCredits >= baseMoney && passedAccount.GameUser.ResourceStats.ContainsKey("Black Diamonds") && passedAccount.GameUser.CurrentLevel == 1)
                {
                    GameDialog();
                   
                    coins.Text = passedAccount.GameUser.TotalCredits.ToString();
                    passedAccount.GameUser.ResourceStats.Remove("Black Diamonds");
                    listOfMyItems.Items.Remove("Black Diamonds");
                    ship.Source = pictures.SpaceShips[1];

                    //Changes to player
                    PlayerChanges(1);

                }
                if (passedAccount.GameUser.TotalCredits >= baseMoney * 2 && passedAccount.GameUser.ResourceStats.ContainsKey("Sand magma") && passedAccount.GameUser.CurrentLevel == 2)
                {
                    GameDialog();
             
                    coins.Text = passedAccount.GameUser.TotalCredits.ToString();
                    passedAccount.GameUser.ResourceStats.Remove("Sand magma");
                    listOfMyItems.Items.Remove("Sand magma");
                    ship.Source = pictures.SpaceShips[2];

                    //Changes to player
                    PlayerChanges(2);
                }
                if (passedAccount.GameUser.TotalCredits >= baseMoney * 3 && passedAccount.GameUser.ResourceStats.ContainsKey("Plastoid Platium") && passedAccount.GameUser.CurrentLevel == 3)
                {
                    GameDialog();

                    coins.Text = passedAccount.GameUser.TotalCredits.ToString();
                    passedAccount.GameUser.ResourceStats.Remove("Plastoid Platium");
                    listOfMyItems.Items.Remove("Plastoid Platium");
                    ship.Source = pictures.SpaceShips[3];

                    //Changes to player
                    PlayerChanges(3);

                 }
            }
        }

        public void PlayerChanges(int UpgradeLevel)
        {
            passedAccount.GameUser.CurrentLevel += 1;
            passedAccount.GameUser.SpaceShipStats["ShootSpeed"] += 5;
            passedAccount.GameUser.TotalCredits -= baseMoney * UpgradeLevel;
        }

        private async void GameDialog()
        {
            ContentDialog printSpecific = new ContentDialog()
            {
                Title = "Upgrade complete", CloseButtonText = "Ok"
            };
            await printSpecific.ShowAsync();
        }


    }
}
