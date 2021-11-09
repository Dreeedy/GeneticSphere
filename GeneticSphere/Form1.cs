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
        private int _resolution;

        private GameEngine _gameEngine;

        public Form1()
        {
            InitializeComponent();
            // TODO: переделать START GAME
        }
        private void StartGame()
        {
            _gameEngine = new GameEngine();
            _gameEngine.StartGame();

            _resolution = (int)numResolution.Value;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _graphics = Graphics.FromImage(pictureBox1.Image);

            timer1.Start();
        }
        private void DrawNextGeneration()            
        {
            FieldCellStatuses[,] field = _gameEngine.GetField();

            if (GameEngine.Generation > 0 && GameEngine.Generation % 10 == 0)
            {
                int offsetX = (pictureBox1.Width - GameRules.Cols * _resolution) / 2 - 1;
                int offsetY = (pictureBox1.Height - GameRules.Rows * _resolution) / 2 - 1;

                _graphics.Clear(Color.Black);
                for (int x = 0; x < GameRules.Cols; x++)
                {
                    for (int y = 0; y < GameRules.Rows; y++)
                    {
                        Brush brush = ChooseColoredBrush(field[x, y]);
                        _graphics.FillRectangle(brush, x * _resolution + offsetX, y * _resolution + offsetY, _resolution - 1, _resolution - 1);
                    }
                }
                pictureBox1.Refresh();
            }            

            DrawFrogsHelfPoints();
            lab_GenerationNumber.Text = GameEngine.Generation.ToString();

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
                brush = Brushes.Magenta;
            }
            if (cellStatus == FieldCellStatuses.Food)
            {
                brush = Brushes.Yellow;
            }
            if (cellStatus == FieldCellStatuses.Frog)
            {
                brush = Brushes.Aquamarine;
            }
            if (cellStatus == FieldCellStatuses.FrogMutant)
            {
                brush = Brushes.Aqua;
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
            lab_FrogsHelfPoints.Text = str;
        }
        private void StopGame()
        {
            if (!timer1.Enabled)
            {
                return;
            }
            timer1.Stop();

            numResolution.Enabled = true;
        }



        private void startBut_Click(object sender, EventArgs e)
        {
            StartGame();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextGeneration();
        }            
        private void stopBut_Click(object sender, EventArgs e)
        {
            StopGame();
        }
    }
}
