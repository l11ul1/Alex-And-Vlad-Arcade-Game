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


    public sealed partial class SellerMain : Page
    {


        //Keep on passing the same instance.
        PassAccount passedAccount;
        GameAccountManager manager;
        Account currentPlayer;

        //Other classes
        Planets planetPics = new Planets();

        //Specific for this game
        Items venItems = new Items();
        Vendor vendor;
        PurchaseSell system;
     

        public SellerMain()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //Get the data from the other page
            passedAccount = e.Parameter as PassAccount;
            currentPlayer = passedAccount.GameUser;
            manager = passedAccount.AccountManager;

            int vendorMoney = new Random().Next(3000, 7000);

            vendor = new Vendor(currentPlayer.CurrentPlanet, venItems.GetItems(currentPlayer.CurrentPlanet), vendorMoney); //needs an ID of a planet which is the name of the planet
                 
            system = new PurchaseSell(currentPlayer, vendor);


            //Set the planet image
            foreach (KeyValuePair<string, BitmapImage> planetCur in planetPics.PlanetLandDict)
            {
                if (planetCur.Key == currentPlayer.CurrentPlanet)
                {
                    planet.Source = planetCur.Value;                  
                }

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e) 
        {
            if (sender == purchase)
            {
                if (list1.Items.Contains(list1.SelectedItem))
                {
                    string text = list1.SelectedItem.ToString();
                    int val = vendor.VendorItems[text];
                    if (system.MakeApurchase(text, val) != 0)
                    {
                        list2.Items.Add(text);
                        list1.Items.Remove(list1.SelectedItem);
                    }
                    
                }
                if (list2.Items.Contains(list2.SelectedItem))
                {
                    string text = list2.SelectedItem.ToString();
                    int val = currentPlayer.ResourceStats[text];
                   
                    if (system.SellAnItem(text, val) != 0)
                    {
                        list2.Items.Add(text);
                        list1.Items.Remove(list1.SelectedItem);


                        list1.Items.Add(text);
                        list2.Items.Remove(list2.SelectedItem);
                    }
                    
                }
                price1.Text = vendor.Credits.ToString();
                price2.Text = currentPlayer.TotalCredits.ToString();
            }
            if (sender == upgrade)
            {

                Frame.Navigate(typeof(Upgrade), passedAccount);

            }
            if (sender == back)
            {
                Frame.Navigate(typeof(GameMenu), passedAccount);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
            foreach (KeyValuePair<string, int> item in vendor.VendorItems)
            {
                list1.Items.Add(item.Key);
            }
            foreach (string item in currentPlayer.ResourceStats.Keys)
            {
                list2.Items.Add(item);
            }
            price1.Text = vendor.Credits.ToString();
            price2.Text = currentPlayer.TotalCredits.ToString();

  
        }
    }
}
