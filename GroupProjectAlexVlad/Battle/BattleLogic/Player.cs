using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

using GroupProjectAlexVlad.MenuLogic;

namespace GroupProjectAlexVlad.Battle.BattleLogic
{
    class Player
    {
        Account currentPlay;
        // Variables
        private Image playerSprite;
        private Image playerBullet;

        private BitmapImage playerBitmapImage = new BitmapImage(new Uri("ms-appx:///Battle/Assets/Sprites/player.png"));
        private BitmapImage invaderKilledImage = new BitmapImage(new Uri("ms-appx:///Battle/Assets/Sprites/explosion.png"));
        private BitmapImage alien1A = new BitmapImage(new Uri("ms-appx:///Battle/Assets/sprites/alien-1-1.png"));
        private BitmapImage alien1B = new BitmapImage(new Uri("ms-appx:///Battle/Assets/sprites/alien-1-2.png"));
        private BitmapImage alien2A = new BitmapImage(new Uri("ms-appx:///Battle/Assets/sprites/alien-2-1.png"));
        private BitmapImage alien2B = new BitmapImage(new Uri("ms-appx:///Battle/Assets/sprites/alien-2-2.png"));
        private BitmapImage alien3A = new BitmapImage(new Uri("ms-appx:///Battle/Assets/sprites/alien-3-1.png"));
        private BitmapImage alien3B = new BitmapImage(new Uri("ms-appx:///Battle/Assets/sprites/alien-3-2.png"));

        private bool isMovingLeft;
        private bool isMovingRight;
        private bool isShooting;

        private int playerScore;

        //Default constuctor
        public Player(Canvas canvas, int playerGameScore, Account currentPlayer)
        {
            currentPlay = currentPlayer;

            //Handle keyboard presses
            Window.Current.CoreWindow.KeyDown += OnKeyDown;
            Window.Current.CoreWindow.KeyUp += OnKeyUp;

            playerScore = playerGameScore;
            isMovingLeft = isMovingRight = isShooting = false;

            //Initialize player image
            playerSprite = new Image
            {
                Width = 52 * SizeModifier(),
                Height = 32 * SizeModifier()
            };

            //Set player location (x / y) on the canvas
            Canvas.SetLeft(playerSprite, Window.Current.Bounds.Width / 2);
            Canvas.SetTop(playerSprite, Window.Current.Bounds.Height - (playerSprite.Height * 2));

            playerSprite.Source = playerBitmapImage;
            canvas.Children.Add(playerSprite);
        }

        //Draw and update all object for every game frame
        public void Draw(Canvas canvas, Image[,] invaderGrid, SoundsManager sounds)
        {
            Canvas.SetTop(playerSprite, Window.Current.Bounds.Height - (playerSprite.Height * 2));

            //If player is moving left and is not hitting the left screen, keep moving left.
            if (isMovingLeft && Canvas.GetLeft(playerSprite) >= 0) Canvas.SetLeft(playerSprite, Canvas.GetLeft(playerSprite) - 6);
            //If player is moving right and is not hitting the right screen, keep moving right
            if (isMovingRight && Canvas.GetLeft(playerSprite) <= Window.Current.Bounds.Width - playerSprite.Width) Canvas.SetLeft(playerSprite, Canvas.GetLeft(playerSprite) + 6);
            //if the player is shooting
            if (isShooting)
            {
                //If no bullet is found, create a new one.
                if (!canvas.Children.Contains(playerBullet))
                {
                    playerBullet = new Image
                    {
                        Width = 3 * SizeModifier(),
                        Height = 8 * SizeModifier(),
                        Source = new BitmapImage(new Uri("ms-appx:///Battle/Assets/Sprites/player-bullet.png"))
                    };

                    Canvas.SetTop(playerBullet, Canvas.GetTop(playerSprite));
                    Canvas.SetLeft(playerBullet, Canvas.GetLeft(playerSprite) + (playerSprite.Width / 2 - 1));

                    sounds.PlayPlayerShootSound();
                    canvas.Children.Add(playerBullet);
                }
                //Move bullet upward and check for alienGrid collision.
                for (int r = 0; r < invaderGrid.GetLength(0); r++)
                {
                    for (int c = 0; c < invaderGrid.GetLength(1); c++)
                    {
                        //If the player bullet is in between the x coords of an invader, continue
                        if (Canvas.GetLeft(invaderGrid[r, c]) <= Canvas.GetLeft(playerBullet) && Canvas.GetLeft(invaderGrid[r, c]) + invaderGrid[r, c].Width >= Canvas.GetLeft(playerBullet))
                        {
                            //If the player bullet is in between the y coords of an invader, continue
                            if (Canvas.GetTop(invaderGrid[r, c]) + invaderGrid[r, c].Height >= Canvas.GetTop(playerBullet) && Canvas.GetTop(invaderGrid[r, c]) <= Canvas.GetTop(playerBullet))
                            {
                                //If the invader is not already dead, continue
                                if (invaderGrid[r, c].Tag != null)
                                {
                                    //Kill invader / update player score
                                    playerScore = playerScore + (10 * (invaderGrid.GetLength(1) - c));

                                    if (invaderGrid[r, c].Source == alien1A || invaderGrid[r, c].Source == alien1B) playerScore = playerScore + 7;
                                    else if (invaderGrid[r, c].Source == alien2A || invaderGrid[r, c].Source == alien2B) playerScore = playerScore + 3;
                                    else playerScore = playerScore + 1;

                                    isShooting = false;
                                    invaderGrid[r, c].Tag = null;
                                    sounds.PlayinvaderKilledSound();
                                    canvas.Children.Remove(playerBullet);
                                    RemoveKilled(canvas, invaderGrid[r, c]);

                                }
                            }
                        }
                        //If the bullet collides with the top of the screen, remove bullet
                        else if (Canvas.GetTop(playerBullet) <= 0)
                        {
                            isShooting = false;
                            canvas.Children.Remove(playerBullet);
                        }
                    }
                }

                //Move bullet upward
                Canvas.SetTop(playerBullet, Canvas.GetTop(playerBullet) - currentPlay.SpaceShipStats["ShootSpeed"]);
            }
        }

        //If the user presses down on the keyboard
        private void OnKeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            //Move the player
            if (args.VirtualKey == Windows.System.VirtualKey.Left) isMovingLeft = true;
            if (args.VirtualKey == Windows.System.VirtualKey.Right) isMovingRight = true;

            //Shoot new bullet
            if (args.VirtualKey == Windows.System.VirtualKey.Up) isShooting = true;
            if (args.VirtualKey == Windows.System.VirtualKey.Space) isShooting = true;
        }

        //If the user releases the keyboard press
        private void OnKeyUp(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            //Stop moving the player
            if (args.VirtualKey == Windows.System.VirtualKey.Left) isMovingLeft = false;
            if (args.VirtualKey == Windows.System.VirtualKey.Right) isMovingRight = false;
        }

        //Control sprite image size (to adapt to various screen sizes)
        private double SizeModifier()
        {
            if (Window.Current.Bounds.Width <= 300) return 0.5;
            else if (Window.Current.Bounds.Width <= 500) return 0.8;
            else return 1;
        }

        //Remove the killed invader from the canvas
        private async void RemoveKilled(Canvas canvas, Image invader)
        {
            invader.Source = invaderKilledImage;
            await Task.Delay(300);
            canvas.Children.Remove(invader);
        }

        //get the player image
        public Image GetPlayer() => playerSprite;

        //get the player score
        public int GetScore() => playerScore;
    }
}
