using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSphere
{
    internal class GameEngine
    {
        public int Rows { get; } = 257;
        public int Cols { get; } = 257;

        public static int EveryTurnDamage { get; } = 1;
        public static int FoodPoints { get; } = 59;
        public static int PoisonPoints { get; } = 99;

        public static int Generation { get; set; } = 0;
        public static bool NewGenerationIsReady { get; set; } = false;

        private const int MAXCOUNTFOOD = 20000;
        private const int MAXCOUNTPOISON = 960;
        private const int MAXCOUNTWALLS = 90;       
        
        private int _currenCountFood = 0;        
        private int _currenCountPoison = 0;        
        private int _currentCountWalls = 0;

        private FieldCellStatuses[,] _field;
        private List<Frog> _frogsList;

        public static int CountFrogs { get; set; } = 0;
        public static int CoutnMutants { get; set; } = 0;

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
            if (GameEngine.NewGenerationIsReady == true)
            {
                AddMutantFrogs();
                GameEngine.NewGenerationIsReady = false;
            }
            else
            {
                AddFrogs();
            }            
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
            if (GameEngine.NewGenerationIsReady == true)
            {
                StartGame();
                return;
            }

            FieldAndFrogEventHandler handler = new FieldAndFrogEventHandler(GetField(), Cols, Rows, _frogsList);
            handler.NextGeneration();
            _field = handler.GetField();
            _frogsList = handler.GetFrogs();
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
        public List<int> GetFrogsHelfPoints()
        {
            List<int> frogsHelfPoints = new List<int>();
            foreach (var frog in _frogsList)
            {
                frogsHelfPoints.Add(frog.HelfPoint);
            }
            return frogsHelfPoints;
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
                        if (x != xCenter && y != yCenter && x < 255 && y < 255 && x > 0 && y > 0)
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
            Random rand;
            while (_currenCountFood < MAXCOUNTFOOD)
            {
                rand = new Random();
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
            Random rand;
            while (_frogsList.Count < 64)
            {
                rand = new Random();
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
        private void AddMutantFrogs()
        {
            List<Frog> mutantFrogsList = new List<Frog>();
            int index = 0;
            foreach (var frog in _frogsList)
            {
                Random rand;
                bool frogPlaced = false;
                while (frogPlaced != true)
                {
                    rand = new Random();
                    int posX = rand.Next(0, 257);
                    int posY = rand.Next(0, 257);
                    if (_field[posX, posY] == FieldCellStatuses.Empty)
                    {
                        if (index % 8 == 0)
                        {
                            mutantFrogsList.Add( new Frog( posX, posY, PerformMutation(frog.Genome), FieldCellStatuses.FrogMutant) );
                        }
                        else
                        {
                            mutantFrogsList.Add(new Frog(posX, posY, frog.Genome));
                        }

                        _field[posX, posY] = frog.FrogType;

                        frogPlaced = true;
                    }
                }
                index++;
            }
            _frogsList.Clear();
            foreach (var mutantFrog in mutantFrogsList)
            {
                _frogsList.Add(mutantFrog);
            }
        }
        private FrogActions[] PerformMutation(FrogActions[] genome)
        {
            Random rand = new Random();
            int mutantGenIndex = rand.Next(0, 64);

            Random rand2 = new Random();
            int mutantGenId = rand2.Next(0, 64);

            FrogActions[] newGenome = new FrogActions[genome.Length];
            for (int i = 0; i < genome.Length; i++)
            {
                if (mutantGenIndex == i)
                {
                    FrogActions newAction = (FrogActions)mutantGenId;
                    while (newGenome[i] == newAction)
                    {
                        rand = new Random();
                        mutantGenId = rand2.Next(0, 64);
                        newAction = (FrogActions)mutantGenId;                        
                    }
                    newGenome[i] = newAction;
                }
                else
                {
                    newGenome[i] = genome[i];
                }
                
            }          
            return newGenome;
        }
    }
}
