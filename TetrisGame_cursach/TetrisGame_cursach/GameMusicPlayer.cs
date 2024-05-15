using System;
using System.IO;
using System.Media;
using System.Windows.Data;
using System.Windows.Media;

namespace TetrisGame_cursach
{
    public class MusicList
    {
        private List<string> musicFiles;    // cборник музыки
        private int ID;                     // Идентификатор трека

        public MusicList()
        {
            ID = 0;
            musicFiles = new List<string>
            {
                "Music/Tetris-ElectroSwing.wav",
                "Music/Tetris-metal.wav",
                "Music/Tetris-miatriss_remix_.wav",
                "Music/Tetris-original.wav",
                "Music/Tetris-phonk.wav"
            };
        }

        /// <summary>
        /// Текущий трек
        /// </summary>
        public string GetCurrentFile()
        {
            return musicFiles[ID];
        }

        /// <summary>
        /// Следующий трек
        /// </summary>
        public string GetNextFile()
        {
            ID = (ID + 1) % musicFiles.Count;
            return musicFiles[ID];
        }

        /// <summary>
        /// Предыдущий трек
        /// </summary>
        public string GetPreviousFile()
        {
            ID = (ID - 1 + musicFiles.Count) % musicFiles.Count;
            return musicFiles[ID];
        }

        /// <summary>
        /// Получить случайный трек из очереди
        /// </summary>
        public string GetRandomFile()
        {
            Random random = new Random();
            ID = random.Next(0, musicFiles.Count);
            return musicFiles[ID];
        }

        public int Count()
        {
            return musicFiles.Count;
        }
    }

    public class GameMusicPlayer
    {
        private MediaPlayer player;         // проигрыватель
        private MusicList musicList;        // cборник музыки
        private MediaTimeline musicTimeline;

        public GameMusicPlayer()
        {
            player = new MediaPlayer();
            musicList = new MusicList();
            player.Volume = 0.2f;
            player.MediaEnded += (sender, e) =>
            {
                PlayTrak(musicList.GetNextFile());
            };

        }
        
        /// <summary>
        /// Воспроизведение аудио файла
        /// </summary>
        /// <param name="filePath"></param>
        private void PlayTrak(string filePath) 
        {
            player.Open(new Uri(filePath, UriKind.Relative));
            player.Play();
        }

        /// <summary>
        /// Случайное воспроизведение аудиофайла
        /// </summary>
        public void GmRandomPlay()
        {
            PlayTrak(musicList.GetRandomFile());
        }

        /// <summary>
        /// Поставить воспроизведение на паузу
        /// </summary>
        public void GmPause()
        {
            player.Pause();
        }

        /// <summary>
        /// Продолжить воспроизведение
        /// </summary>
        public void GmContinue(string filePath)
        {
            player.Play();
        }
    }
}
