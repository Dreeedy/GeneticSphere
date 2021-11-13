using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticSphere
{
    internal class NotebookLoad
    {
        private static string _baseDirectory;

        private static OpenFileDialog _openFileDialog;

        public NotebookLoad(string folderName)
        {
            _openFileDialog = new OpenFileDialog();
            _openFileDialog.Filter = "Геном(*.txt)|*.txt";
            _openFileDialog.InitialDirectory = GetBaseDirectory() + folderName;
            _baseDirectory = GetBaseDirectory();
        }

        public string Load()
        {           
            if (_openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                string fileText = "";

                // получаем выбранный файл
                string filename = _openFileDialog.FileName;
                // читаем файл в строку
                fileText = System.IO.File.ReadAllText(filename);

                return fileText;
            }
            else
            {
                return "";
            }
        }

        private string GetBaseDirectory()
        {
            string path;
            path = AppDomain.CurrentDomain.BaseDirectory;
            return path;
        }
    }
}
