using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSphere
{
    public class GameEngine
    {
        public int Rows { get; } = 257;
        public int Cols { get; } = 257;

        private const int MAXCOUNTFOOD = 640;
        private const int MAXCOUNTPOISON = 160;
        private const int MAXCOUNTWALLS = 90;       
        
        private int _currenCountFood = 0;        
        private int _currenCountPoison = 0;        
        private int _currentCountWalls = 0;

        private FieldCellStatuses[,] _field;
        private List<Frog> _frogsList;

        public GameEngine()
        {
            _frogsList = new List<Frog>();
        }
        public void StartGame()
        {
            // создание игрового поля
            _field = new FieldCellStatuses[Cols, Rows];
            // заполнения поля пустыми ячейками
            for (int x = 1; x < Cols - 1; x++)
            {
                for (int y = 1; y < Rows - 1; y++)
                {
                    _field[x, y] = FieldCellStatuses.Empty;
                }
            }
            // добавление внешних стен
            AddExternalWalls();
            // добавление внутренних стен
            AddInternalWalls();
            // добавление еды
            AddFood();
            // добавление яда
            AddPoison();
            // добавление жаб
            AddFrogs();
        }
        public void StopGame()
        {

        }
        public void ContinueGame()
        {

        }
        public void PauseGame()
        {

        }
        public void NextGeneration()
        {        
            foreach (var frog in _frogsList)
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
        public FieldCellStatuses[,] GetField()
        {
            FieldCellStatuses[,] newField = new FieldCellStatuses[Rows, Cols];

            for (int x = 0; x < Cols; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    newField[x, y] = _field[x, y];
                }
            }

            return newField;
        }



        private void AddExternalWalls()
        {
            for (int x = 0; x < Cols; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    if ((x == 0 || x == 256 && y >= 0) || (y == 0 || y == 256 && x >= 0))
                    {
                        _field[x, y] = FieldCellStatuses.Wall;
                    }
                }
            }
        }
        private void AddInternalWalls()
        {
            _currentCountWalls = 0;

            while (_currentCountWalls < MAXCOUNTWALLS)
            {
                Random rand = new Random();

                int xCenter = rand.Next(0, 257);
                int yCenter = rand.Next(0, 257);

                for (int x = xCenter - 1; x < xCenter + 1; x++)
                {
                    for (int y = yCenter - 1; y < yCenter + 1; y++)
                    {
                        if (x != xCenter && y != yCenter && x < 257 && y < 257 && x > 0 && y > 0)
                        {
                            if (_field[x, y] == FieldCellStatuses.Wall)
                            {
                                _field[xCenter, yCenter] = FieldCellStatuses.Wall;
                                _field[xCenter, yCenter - 1] = FieldCellStatuses.Wall;
                                _currentCountWalls += 2;
                            }
                            else
                            {
                                _field[xCenter, yCenter + 1] = FieldCellStatuses.Wall;
                                _field[xCenter, yCenter] = FieldCellStatuses.Wall;
                                _field[xCenter, yCenter - 1] = FieldCellStatuses.Wall;
                                _currentCountWalls += 3;
                            }
                        }
                    }
                }
            }
        }
        private void AddFood()
        {
            _currenCountFood = 0;
            while (_currenCountFood < MAXCOUNTFOOD)
            {
                // сгенерить позицию
                Random rand = new Random();
                int x = rand.Next(0, 257);
                int y = rand.Next(0, 257);

                if (_field[x, y] == FieldCellStatuses.Empty)
                {
                    _field[x, y] = FieldCellStatuses.Food;
                    _currenCountFood++;
                }
            }
        }
        private void AddPoison()
        {
            _currenCountPoison = 0;
            while (_currenCountPoison < MAXCOUNTPOISON)
            {
                // сгенерить позицию
                Random rand = new Random();
                int x = rand.Next(0, 257);
                int y = rand.Next(0, 257);

                if (_field[x, y] == FieldCellStatuses.Empty)
                {
                    _field[x, y] = FieldCellStatuses.Poison;
                    _currenCountPoison++;
                }
            }
        }
        private void AddFrogs()
        {
            // создаю жаб
            while (_frogsList.Count < 64)
            {
                // сгенерить позицию
                Random rand = new Random();
                int posX = rand.Next(0, 257);
                int posY = rand.Next(0, 257);

                if (_field[posX, posY] == FieldCellStatuses.Empty)
                {
                    var frog = new Frog(posX, posY);
                    _field[posX, posY] = frog.FrogType;
                    _frogsList.Add(frog);
                }
            }
        }     
    }
}
