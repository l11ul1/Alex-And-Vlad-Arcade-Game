using System;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

//Connect to other games and logic
using GroupProjectAlexVlad.MenuLogic;
using GroupProjectAlexVlad.Battle.BattleLogic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GroupProjectAlexVlad.Battle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BattleGamePage : Page
    {
        //Keep on passing the same instance.
        PassAccount passedAccount;
        GameAccountManager manager;
        Account currentPlayer;

        //Get pictures
        Pictures pictures = new Pictures();

        //Sounds
        private SoundsManager sound = new SoundsManager();

        //Use of dispatch timer
        private DispatcherTimer dispatcherTimer;

        //Specific to this game
        private Player player;
        private Invaders invaders;
        
        private int playerLives;
        private int playerGameScore;
        private int level;

        public BattleGamePage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //Get the data from the other page
            passedAccount = e.Parameter as PassAccount;
            currentPlayer = passedAccount.GameUser;
            manager = passedAccount.AccountManager;

            //Planet background
            switch (currentPlayer.CurrentPlanet)
            {
                case "Planet1":
                    BigImage.Source = pictures.BattleList[0];
                    break;

                case "Planet2":
                    BigImage.Source = pictures.BattleList[1];
                    break;

                case "Planet3":
                    BigImage.Source = pictures.BattleList[2];
                    break;

                case "Planet4":
                    BigImage.Source = pictures.BattleList[3];
                    break;

                case "Planet5":
                    BigImage.Source = pictures.BattleList[4];
                    break;

                default: break;
            }


            //set preffered window size
            ApplicationView.PreferredLaunchViewSize = new Size(700, 500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            //initialize variable for gameplay
            playerLives = passedAccount.GameUser.CurrentLevel;
            playerGameScore = 0;
            level = 1;

            //initiate classes
            player = new Player(canvas, playerGameScore, currentPlayer);
            invaders = new Invaders(canvas, level);
            sound.Sounds(grid);
            
            //start the game dispatch timer
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += Game;
            dispatcherTimer.Interval = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 30);
            dispatcherTimer.Start();

        }


        //game method that fires in dispatch timer
        private void Game(object sender, object e)
        {
            //check to see if player has died
            if (!invaders.PlayerAlive())
            {
                //get the player score
                playerGameScore = player.GetScore();

                //handle how many lives player has left
                switch (playerLives)
                {
                    case 8:
                        invaders.SetPlayerAlive(true);
                        break;

                    case 7:
                        invaders.SetPlayerAlive(true);
                        break;

                    case 6:
                        invaders.SetPlayerAlive(true);
                        break;

                    case 5:
                        invaders.SetPlayerAlive(true);
                        break;

                    case 4:
                        invaders.SetPlayerAlive(true);
                        break;

                    case 3:
                        invaders.SetPlayerAlive(true);
                        break;

                    case 2:

                        invaders.SetPlayerAlive(true);
                        break;
                    case 1:
                        dispatcherTimer.Stop();
                        finalScoreBlock.Text = playerGameScore.ToString();
                        gameOverPanel.Visibility = Visibility.Visible;
                        sound.PlayGameOverSound();
                        
                        return;
                    default:
                        break;
                }

                //loss of a life and reset player and alien grid
                playerLives--;
                canvas.Children.Clear();
                invaders.RebuildInvaders(canvas);
                player = new Player(canvas, playerGameScore, currentPlayer);
            }

            //new level upon killing of all aliens
            if (!invaders.InvadersAreAlive())
            {
                level++;
                invaders = new Invaders(canvas, level);
            }

            //call the draw methods of each player and alien classes passing in requirements
            invaders.Draw(canvas, player.GetPlayer(), sound);
            player.Draw(canvas, invaders.GetInvaderGrid(), sound);

            //update score textblock
            scoreBlock.Text = player.GetScore().ToString();
        }

        //submit score function 
        private void SubmitScoreBtn_Click(object sender, RoutedEventArgs e)
        {

            currentPlayer.SpaceShipStats["ShipStrength"] -= 10;
            currentPlayer.TotalCredits += player.GetScore();

            Frame.Navigate(typeof(GameMenu), passedAccount);

        }
    }
}

