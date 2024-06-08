namespace TetrisGame_cursach
{
    public class MusicList
    {
        /// <summary>
        /// Списко путей к музыкальным файлам
        /// </summary>
        private List<string> musicFiles;

        /// <summary>
        /// Идентификационный номер трека
        /// </summary>
        private int ID;

        /// <summary>
        /// Плейлист фоновой музыки
        /// </summary>
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

        /// <summary>
        /// Полдсчет кол-ва треков в списке
        /// </summary>
        /// <returns></returns>
        private int Count()
        {
            return musicFiles.Count;
        }
    }
}
