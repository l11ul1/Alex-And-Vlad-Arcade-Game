

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
using System;

namespace GroupProjectAlexVlad
{
 
    public sealed partial class MainPage : Page
    {
        GameAccountManager loginaccount = new GameAccountManager();
        
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;         
        }

        //Button click event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {

                case "LoginButton":
                    LoginEnter();
                    break;

                case "LoginCreate":
                    LoginCreateAccount();
                    break;

                default: break;
            }
        }

        //The data we are going to use
        private Tuple<string, string, bool> GetData()
        {
            bool usable = true;
      
            //Get the User ID (User Name)
            string userID = userIDTxt.Text;
            if (String.IsNullOrEmpty(userIDTxt.Text)) { usable = false; }

            //Get the password 
            string userPass = userPassTxt.Password;
            if (String.IsNullOrEmpty(userPassTxt.Password)) {  usable = false;  }

            return Tuple.Create(userID, userPass, usable);
        }
        
        //Create a new account
        private void LoginCreateAccount()
        {

            Tuple<string, string, bool> data = GetData();

            switch (data.Item3)
            {

                case true:
                    if (loginaccount.AddAccount(data.Item1, data.Item2) == 0)
                    {
                        GameDialog("That user ID is already in use.");
                    }
                    break;

                case false:
                    GameDialog("Missing inputs in the required fields.");
                    break;

                default: break;
            }
        }

        //Login and play
        private void LoginEnter()
        {
            Tuple<string, string, bool> data = GetData();

            if (data.Item3 == true)
            {
                if (loginaccount.UsersCheckContain(data.Item1) == 0 && loginaccount.UsersPasswordCheckContain(data.Item2) == 0 )
                {
                    Account currentPlayer = loginaccount.GetAccount(data.Item1, data.Item2);

                    //Put the data into payload and send it
                    PassAccount payload = new PassAccount { AccountManager = loginaccount,  GameUser = currentPlayer };

                    if (payload != null)
                    {
                        Frame.Navigate(typeof(GameMenu), payload);
                    }
                    else
                    {
                        GameDialog("Sorry, the account doesn't exist.");
                    }                  
                }
                else
                {
                    GameDialog("Sorry, the account doesn't exist.");
                }
            }
            else
            {
                GameDialog("Missing inputs in the required fields.");
            }
        }
 
        //Message
        private async void GameDialog(string message)
        {
            await new ContentDialog() { Title = message, CloseButtonText = "Ok" }.ShowAsync();
        }
    }
}
