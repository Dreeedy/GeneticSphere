using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private bool[,] _field;
        private int _rows;
        private int _cols;

        public Form1()
        {
            InitializeComponent();
        }

        private void StartGame()
        {
            if (timer1.Enabled)
            {
                return;
            }

            numResolution.Enabled = false;
            numDensity.Enabled = false;

            _resolution = (int)numResolution.Value;

            _rows = pictureBox1.Height / _resolution;
            _cols = pictureBox1.Width / _resolution;
            _field = new bool[_cols, _rows];

            var _random = new Random();
            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    _field[x, y] = _random.Next((int)numDensity.Value) == 0;
                }
            }

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _graphics = Graphics.FromImage(pictureBox1.Image);

            timer1.Start();
        }

        private void NextGeneration()
        {
            _graphics.Clear(Color.Black);

            var newField = new bool[_cols, _rows];

            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    // всякая логика

                    if (_field[x,y])
                    {
                        _graphics.FillRectangle(Brushes.Crimson, x * _resolution, y * _resolution, _resolution, _resolution);
                    }
                }
            }
            pictureBox1.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }

        private void startBut_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StopGame()
        {
            if (!timer1.Enabled)
            {
                return;
            }
            timer1.Stop();

            numResolution.Enabled = true;
            numDensity.Enabled = true;
        }

        private void stopBut_Click(object sender, EventArgs e)
        {
            StopGame();
        }
    }
}
