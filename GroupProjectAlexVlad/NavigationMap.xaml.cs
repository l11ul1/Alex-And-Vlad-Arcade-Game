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
using GroupProjectAlexVlad.MenuLogic;
using Windows.UI.Xaml.Media.Imaging;


using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProjectAlexVlad
{

    public sealed partial class NavigationMap : Page
    {

        //Keep on passing the same instance.
        PassAccount passedAccount;
        GameAccountManager manager;
        Account currentPlayer;

        //Other classes
        Planets planet = new Planets();

        //Specific to this game
        List<Image> planetSpot;
        string pickedPlanet = "";

        public NavigationMap()
        {
            InitializeComponent();

            planetSpot = new List<Image>(new Image[] { Planet1, Planet2, Planet3, Planet4, Planet5 });

            int i = 0;
            foreach (KeyValuePair<string, BitmapImage> planetPic in planet.PlanetDict)
            {
                planetSpot[i].Source = planetPic.Value;
                i++;
            }     
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {        
            passedAccount = e.Parameter as PassAccount;
            currentPlayer = passedAccount.GameUser;
            manager = passedAccount.AccountManager;
        }

        //Click event
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            foreach (var planetCur in planet.PlanetDict)
            {
                if (planetCur.Value == (sender as Image).Source)
                {
                    map1.Source = (sender as Image).Source;
                    pickedPlanet = planetCur.Key;
                }
            }
        }

        //Click event
        private async void NavigatePlanet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            switch ((sender as Border).Name)
            {

                case "GoBack":
                    Frame.Navigate(typeof(GameMenu), passedAccount);
                    break;

                case "Launch":

                    if (map1.Source != null)
                    {
                        //Change planet
                        currentPlayer.CurrentPlanet = pickedPlanet;

                        int coloumn = 0;
                        int row = 0;

                        //Show planet location
                        switch (pickedPlanet)
                        {

                            case "Planet1":
                                coloumn = 5;
                                row = 5;
                                break;

                            case "Planet2":
                                coloumn = 7;
                                row = 4;
                                break;

                            case "Planet3":
                                coloumn = 4;
                                row = 8;
                                break;

                            case "Planet4":
                                coloumn = 10;
                                row = 11;
                                break;

                            case "Planet5":
                                coloumn = 7;
                                row = 9;
                                break;

                            default: break;
                        }

                        //Set coloumn
                        for (int pc = 0; pc < coloumn; pc++)
                        {
                            await Task.Delay(TimeSpan.FromSeconds(0.05));
                            Grid.SetColumn(dot, pc);                           
                        }

                        //Set row
                        for (int pr = 0; pr < row; pr++)
                        {
                            await Task.Delay(TimeSpan.FromSeconds(0.025));
                            Grid.SetRow(dot, pr);
                        }

                        await Task.Delay(TimeSpan.FromSeconds(1.00));

                        Frame.Navigate(typeof(GameMenu), passedAccount);
                    }
                    break;

                default: break;
            }

        }


    }
}
