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

        public FrogActions[] Load()
        {           
            if (_openFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                string filename = _openFileDialog.FileName;

                string fileText = System.IO.File.ReadAllText(filename);

                FrogActions[] loadedGenome = new FrogActions[GameRules.GenomeSize];

                int index = 0;
                foreach (string gen in fileText.Split(' '))
                {
                    if (index == GameRules.GenomeSize - 1)
                    {
                        break;
                    }
                    loadedGenome[index] = (FrogActions)int.Parse(gen);
                    index++;
                }

                return loadedGenome;
            }
            else
            {
                return new FrogActions[GameRules.GenomeSize];
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
