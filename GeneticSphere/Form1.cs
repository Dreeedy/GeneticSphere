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
            _graphics.Clear(Color.Black);

            int offsetX = (pictureBox1.Width - _gameEngine.Cols * _resolution) / 2 - 1;
            int offsetY = (pictureBox1.Height - _gameEngine.Rows * _resolution) / 2 - 1;

            FieldCellStatuses[,] field = _gameEngine.GetField();

            for (int x = 0; x < _gameEngine.Cols; x++)
            {
                for (int y = 0; y < _gameEngine.Rows; y++)
                {
                    Brush brush = ChooseColoredBrush(field[x, y]);
                    _graphics.FillRectangle(brush, x * _resolution + offsetX, y * _resolution + offsetY, _resolution - 1, _resolution - 1);
                }
            }
            pictureBox1.Refresh();

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
