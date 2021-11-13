using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneticSphere
{ 
    public partial class Form1 : Form
    {
        private Graphics _graphics;

        private GameEngine _gameEngine;

        private NotebookSave _notebookSave;

        public Form1()
        {
            InitializeComponent();

            _notebookSave = new NotebookSave("Genomes");           

            button_StartNewWorld.Enabled = true;
            button_pause.Enabled = false;
            button_resume.Enabled = false;
            button_saveGenome.Enabled = false;
            button_loadGenome.Enabled = false;
        }

        private void StartGame()
        {           
            _gameEngine = new GameEngine();
            _gameEngine.StartGame();

            GameRules.Resolution = (int)numResolution.Value;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _graphics = Graphics.FromImage(pictureBox1.Image);

            GameRules.RenderIsOn = cb_RenderToggle.Checked;
            GameRules.Generation = 0;

            timer1.Start();
        }

        private void DrawNextGeneration()            
        {
            FieldCellStatuses[,] field = _gameEngine.GetField();

            if (GameRules.RenderIsOn && GameRules.Generation % GameRules.DroppedFrames == 0) // GameEngine.Generation > 0 && GameEngine.Generation % 10 == 0
            {
                int offsetX = (pictureBox1.Width - GameRules.Cols * GameRules.Resolution) / 2 - 1;
                int offsetY = (pictureBox1.Height - GameRules.Rows * GameRules.Resolution) / 2 - 1;

                _graphics.Clear(Color.Black);
                for (int x = 0; x < GameRules.Cols; x++)
                {
                    for (int y = 0; y < GameRules.Rows; y++)
                    {
                        Brush brush = ChooseColoredBrush(field[x, y]);
                        _graphics.FillRectangle(brush, x * GameRules.Resolution + offsetX, y * GameRules.Resolution + offsetY, GameRules.Resolution - 1, GameRules.Resolution - 1);
                    }
                }
                pictureBox1.Refresh();
            }            

            DrawFrogsHelfPoints();
            label_GenerationNumber.Text = $"Поколение: {GameRules.Generation}";
            GameRules.RenderIsOn = cb_RenderToggle.Checked;
            label_Frogs.Text = $"Жаб: {GameRules.CountAliveFrogs} Мутантов: {GameRules.CountAliveMutants}" + "\r\n" + $"Живых: {GameRules.CountAliveFrogs + GameRules.CountAliveMutants} Мертвых: {GameRules.MaxCoutnFrogs - (GameRules.CountAliveFrogs + GameRules.CountAliveMutants)}";
            label_CountTurns.Text = $"Ход: {GameRules.CountTurns}";
            GameRules.Resolution = (int)numResolution.Value;

            _gameEngine.NextGeneration();
        }

        private Brush ChooseColoredBrush(FieldCellStatuses cellStatus)
        {
            Brush brush = Brushes.Black;

            if (cellStatus == FieldCellStatuses.Empty)
            {
                brush = Brushes.Black;
            }
            if (cellStatus == FieldCellStatuses.Wall)
            {
                brush = Brushes.Blue;
            }
            if (cellStatus == FieldCellStatuses.Poison)
            {
                brush = Brushes.MidnightBlue;
            }
            if (cellStatus == FieldCellStatuses.Food)
            {
                brush = Brushes.DarkSlateBlue;
            }
            if (cellStatus == FieldCellStatuses.Frog)
            {
                brush = Brushes.Aquamarine;
            }
            if (cellStatus == FieldCellStatuses.FrogMutant)
            {
                brush = Brushes.Red;
            }

            return brush;
        }

        private void DrawFrogsHelfPoints()
        {
            string str = "";
            int index = 0;
            int sep = 8;
            foreach (var item in _gameEngine.GetFrogsHelfPoints())
            {
                if (index == sep)
                {
                    str += "\r\n";
                    index = 0;
                }
                str += item + " | ";
                index++;
            }
            label_FrogsHelfPoints.Text = str;
        }

        private void PauseGame()
        {
            if (!timer1.Enabled)
            {
                return;
            }
            timer1.Stop();            
        }

        private void button_StartNewWorld_Click(object sender, EventArgs e)
        {
            button_StartNewWorld.Enabled = true;
            button_pause.Enabled = true;
            button_resume.Enabled = false;
            button_saveGenome.Enabled = false;
            button_loadGenome.Enabled = false;

            StartGame();
        }

        private void button_pause_Click(object sender, EventArgs e)
        {
            button_pause.Enabled = false;
            button_resume.Enabled = true;
            button_saveGenome.Enabled = true;
            button_loadGenome.Enabled = true;

            PauseGame();
        }

        private void button_resume_Click(object sender, EventArgs e)
        {
            button_pause.Enabled = true;
            button_resume.Enabled = false;
            button_saveGenome.Enabled = false;
            button_loadGenome.Enabled = false;

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextGeneration();
        }

        private void button_saveGenome_Click(object sender, EventArgs e)
        {
            button_saveGenome.Enabled = false;

            string genomeFileName = $"F{GameRules.MaxCountFood}_P{GameRules.MaxCountPoison}_W{GameRules.MaxCounWalls}_MCF{GameRules.MaxCoutnFrogs}_G{GameRules.Generation.ToString()}";
            _notebookSave.Save(GameRules.GenomeToSave, genomeFileName);
        }

        private void button_loadGenome_Click(object sender, EventArgs e)
        {           
            GameRules.StartWithLoadedGenome = true;
            StartGame();

            button_pause.Enabled = true;
            button_resume.Enabled = false;
            button_saveGenome.Enabled = false;
            button_loadGenome.Enabled = false;
        }
    }
}
