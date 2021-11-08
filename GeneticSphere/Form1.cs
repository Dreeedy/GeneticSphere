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

        private int maxFood = 640;
        private int currenFood;
        private int maxPoison = 160;
        private int currenPoison;
        private int maxWalls = 90;
        private int currentWalls;

        public Form1()
        {
            InitializeComponent();
            // TODO: переделать START GAME
        }

        private void StartGame()
        {
            if (timer1.Enabled)
            {
                return;
            }

            numResolution.Enabled = false;


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

            AddAroundWalls();// установка границы вокруг поля
            AddWalls();

            // создаю жаб
            while (frogs.Count < 64)
            {
                // сгенерить позицию
                Random rand = new Random();
                int posX = rand.Next(0, 257);
                int posY = rand.Next(0, 257);

                if (_field[posX, posY] == FieldCellStatuses.Empty)
                {
                    var frog = new Frog(posX, posY);
                    _field[posX, posY] = frog.FrogType;
                    frogs.Add(frog);
                }
            }

            
            AddFood();
            AddPoison();

            timer1.Start();
        }

        private void AddFood()
        {
            currenFood = 0;
            while (currenFood < maxFood)
            {
                // сгенерить позицию
                Random rand = new Random();
                int x = rand.Next(0, 257);
                int y = rand.Next(0, 257);

                if (_field[x, y] == FieldCellStatuses.Empty)
                {
                    _field[x, y] = FieldCellStatuses.Food;
                    currenFood++;
                }
            }
        }

        private void AddPoison()
        {
            currenPoison = 0;
            while (currenPoison < maxPoison)
            {
                // сгенерить позицию
                Random rand = new Random();
                int x = rand.Next(0, 257);
                int y = rand.Next(0, 257);

                if (_field[x, y] == FieldCellStatuses.Empty)
                {
                    _field[x, y] = FieldCellStatuses.Poison;
                    currenPoison++;
                }
            }
        }

        private void AddWalls()
        {
            currentWalls = 0;

            while (currentWalls < maxWalls)
            {
                Random rand = new Random();

                int xCenter = rand.Next(0, 257);
                int yCenter = rand.Next(0, 257);

                for (int x = xCenter - 1; x < xCenter + 1; x++)
                {
                    for (int y = yCenter - 1; y < yCenter + 1; y++)
                    {
                        if (x != xCenter && y != yCenter && x < 257 && y < 257)
                        {
                            if (_field[x,y] == FieldCellStatuses.Wall)
                            {
                                _field[xCenter, yCenter] = FieldCellStatuses.Wall;
                                _field[xCenter, yCenter - 1] = FieldCellStatuses.Wall;
                                currentWalls += 2;
                            }
                            else
                            { 
                                _field[xCenter, yCenter + 1] = FieldCellStatuses.Wall;
                                _field[xCenter, yCenter] = FieldCellStatuses.Wall;
                                _field[xCenter, yCenter - 1] = FieldCellStatuses.Wall;
                                currentWalls += 3;
                            }
                        }
                    }
                }
            }
        }

        private void AddAroundWalls()
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

        private void NextGeneration()
        {
            // отрисовка старого кадра
            _graphics.Clear(Color.Black);
            AddAroundWalls();// установка границы вокруг поля

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
        }

        private void stopBut_Click(object sender, EventArgs e)
        {
            StopGame();
        }
    }
}
