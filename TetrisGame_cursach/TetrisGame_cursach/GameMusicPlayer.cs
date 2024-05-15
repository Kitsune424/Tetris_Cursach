using System;
using System.IO;
using System.Media;

namespace TetrisGame_cursach
{
    public class GameMusicPlayer
    {
        private string[] audioFiles;
        private int ID;
        private SoundPlayer player;

        public GameMusicPlayer(string[] files)
        {
            audioFiles = files;
            ID = 0;
            player = new SoundPlayer();
            player.Stream = new MemoryStream(File.ReadAllBytes(audioFiles[ID]));
        }

        public void PlayRandom()
        {
            Random rnd = new Random();
            ID = rnd.Next(0, audioFiles.Length);
            PlayCurrentFile();
        }

        public void PlayNext()
        {
            ID = (ID + 1) % audioFiles.Length;
            PlayCurrentFile();
        }

        public void PlayPrevious()
        {
            ID = (ID - 1 + audioFiles.Length) % audioFiles.Length;
            PlayCurrentFile();
        }

        public void Stop()
        {
            player.Stop();
        }
        
        public string MusicName()
        {
            string name = audioFiles[ID];
            return name;
        }

        private void PlayCurrentFile()
        {
            player.Stop();
            player.SoundLocation = audioFiles[ID];
            player.Play();
        }
    }
}
