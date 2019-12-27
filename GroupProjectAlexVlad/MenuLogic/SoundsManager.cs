using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.UI.Xaml;

namespace GroupProjectAlexVlad.MenuLogic
{
    class SoundsManager
    {

        //Variables for Battle Game
        MediaElement BackgroundMusic;
        MediaElement playerShootSound;
        MediaElement invaderKilledSound;
        MediaElement playerKilledSound;
        MediaElement gameOverSound;

        double volume;

        public List<string> MusicList { get; set; }

        public SoundsManager()
        {

            //Put the music into the list
            string[] input = { "ms-appx:///Assets/MenuMusic/MenuMusic1.mp3" };
            MusicList = new List<string>(input);
        }

        //_______________________________________________________________________
        //Default for Battle game
        public void Sounds(Grid grid)
        {
            //If the volume is set in the localStorage, use this volume level
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("volumeLevel"))
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                Object volumeLevel = localSettings.Values["volumeLevel"];
                volume = (Convert.ToDouble(volumeLevel) / 100);
            }
            else
            {
                volume = 0.5;
            }

            //If the mute option is enabled in the localStorage, mute the volume
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey("musicOn"))
            {
                ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
                Object musicOn = localSettings.Values["musicOn"];
                if (!Convert.ToBoolean(musicOn))
                {
                    volume = 0;
                }
            }

            //Create the background music
            BackgroundMusic = new MediaElement
            {
                Source = new Uri("ms-appx:///Battle/Assets/Audio/01-opening-theme.mp3"),
                AutoPlay = true
            };
            BackgroundMusic.MediaEnded += new RoutedEventHandler(MediaEnded);
            grid.Children.Add(BackgroundMusic);
            BackgroundMusic.Volume = volume;
            BackgroundMusic.Play();

            //Create the player shooting sound FX
            playerShootSound = new MediaElement
            {
                Source = new Uri("ms-appx:///Battle/Assets/Audio/shoot.wav"),
                AutoPlay = false
            };
            grid.Children.Add(playerShootSound);
            playerShootSound.Volume = volume;

            //Create the invader killed sound FX
            invaderKilledSound = new MediaElement
            {
                Source = new Uri("ms-appx:///Battle/Assets/Audio/invaderkilled.wav"),
                AutoPlay = false
            };
            grid.Children.Add(invaderKilledSound);
            invaderKilledSound.Volume = volume;

            //Create the player killed sound FX
            playerKilledSound = new MediaElement
            {
                Source = new Uri("ms-appx:///Battle/Assets/Audio/explosion.wav"),
                AutoPlay = false
            };
            grid.Children.Add(playerKilledSound);
            playerKilledSound.Volume = volume;

            //Create the gameover sound FX
            gameOverSound = new MediaElement
            {
                Source = new Uri("ms-appx:///Battle/Assets/Audio/gameover.mp3"),
                AutoPlay = false
            };
            grid.Children.Add(gameOverSound);
            gameOverSound.Volume = volume;
        }


        //Play sound on method call
        void MediaEnded(object sender, RoutedEventArgs e)
        {
            BackgroundMusic.Position = TimeSpan.FromSeconds(0);
            BackgroundMusic.Play();
        }

        //Play sound on method call
        public void PlayPlayerShootSound() => playerShootSound.Play();

        //Play sound on method call
        public void PlayinvaderKilledSound() => invaderKilledSound.Play();

        //Play sound on method call
        public void PlayPlayerKilledSound() => playerKilledSound.Play();

        //Play sound on method call
        public void PlayGameOverSound() => gameOverSound.Play();

        //_______________________________________________________________________



    }
}
