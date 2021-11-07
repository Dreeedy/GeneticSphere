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
    enum FieldCellStatuses : byte
    {
        Empty = 0,// в ячейке ничего нет
        Wall = 1,// в ячейке есть стена
        Poison = 2,// в ячейке есть яд
        Food = 3,// в ячейке есть еда
        Frog = 4,// в ячейке есть лягушка
        FrogMutant = 5// в ячейке есть лягушка мутант
    }

    public partial class Form1 : Form
    {
        private Graphics _graphics;
        private int _resolution;
        private FieldCellStatuses[ , ] _field;
        private int _rows;
        private int _cols;

        private List<Frog> frogs = new List<Frog>();

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

            _rows = 257;
            _cols = 257;
            _field = new FieldCellStatuses[_cols, _rows];            

            for (int x = 1; x < _cols - 1; x++)
            {
                for (int y = 1; y < _rows - 1; y++)
                {
                    _field[x, y] = FieldCellStatuses.Empty;
                }
            }

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            _graphics = Graphics.FromImage(pictureBox1.Image);

            SetFieldBoundaries();// установка границы вокруг поля
            // создаю жаб
            while (frogs.Count < 64)
            {
                // сгенерить позицию
                Random rand = new Random();
                int posX = rand.Next(50, 257);
                int posY = rand.Next(50, 257);

                if (_field[posX, posY] == FieldCellStatuses.Empty)
                {
                    var frog = new Frog(posX, posY);
                    _field[posX, posY] = frog.FrogType;
                    frogs.Add(frog);
                }
            }

            timer1.Start();
        }

        private void SetFieldBoundaries()
        {
            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    if ( (x == 0 || x == 256 && y >= 0) || (y == 0 || y == 256 && x >= 0) )
                    {
                        _field[x, y] = FieldCellStatuses.Wall;
                    }   
                }
            }            
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
                brush = Brushes.Red;
            }
            if (cellStatus == FieldCellStatuses.Food)
            {
                brush = Brushes.Black;
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

        private void NextGeneration()
        {
            // отрисовка старого кадра
            _graphics.Clear(Color.Black);
            SetFieldBoundaries();// установка границы вокруг поля            

            int offsetX = (pictureBox1.Width - _cols * _resolution) / 2 - 1;
            int offsetY = (pictureBox1.Height - _rows * _resolution) / 2 - 1;

            for (int x = 0; x < _cols; x++)
            {
                for (int y = 0; y < _rows; y++)
                {
                    Brush brush = ChooseColoredBrush(_field[x, y]);

                    _graphics.FillRectangle(brush, x * _resolution + offsetX, y * _resolution + offsetY, _resolution - 1, _resolution - 1);
                }
            }
            pictureBox1.Refresh();

            // логика нового кадра
            foreach (var frog in frogs)
            {
                if (frog.IsAlive == false)
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                    continue;
                }
                Debug.WriteLine($"HP:{frog.HelfPoint} P:{frog._genePointer}");
                Frog.TakeAction(ref _field, frog);                
            }

            Debug.WriteLine("Все жабы");
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
