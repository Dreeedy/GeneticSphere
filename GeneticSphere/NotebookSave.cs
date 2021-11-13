using System;
using System.Diagnostics;
using System.IO;

namespace GeneticSphere
{
    /// <summary>
    /// Класс отвечает за сохранение данных в формате JSON
    /// </summary>
    internal class NotebookSave // : SaveHandler
    {
        private static string _baseDirectory;
        private static string _pathToJsonFolder;

        /// <summary>
        /// Конструктор принимает название папки для файлов в формате JSON
        /// </summary>
        /// <param name="folderName"></param>
        public NotebookSave(string folderName)
        {
            _baseDirectory = GetBaseDirectory();

            CreateFolder(folderName);
        }        

        /// <summary>
        /// Метод сохраняет данные в базовой папке в формате JSON
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        public void Save(string data, string fileName)
        {
            string path = _pathToJsonFolder + "/" + fileName + ".txt";

            try
            {
                // Создание файла
                FileStream fileStream = new FileStream(path, FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream);

                // Запись в файл
                streamWriter.Write(data);

                // Закрытие потоков для работы с файлом
                streamWriter.Close();
                fileStream.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

        }

        /// <summary>
        /// Метод создает директории в "базовой директории"
        /// </summary>
        /// <param name="folderName"></param>
        private void CreateFolder(string folderName)
        {
            string path = _baseDirectory + folderName;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            _pathToJsonFolder = path;
        }

        /// <summary>
        /// Метод возвращает путь до базовой директории
        /// </summary>
        /// <returns></returns>
        private string GetBaseDirectory()
        {
            string path;
            path = AppDomain.CurrentDomain.BaseDirectory;
            return path;
        }
    }
}
