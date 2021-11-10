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
        public static int Generation { get; set; } = 0;
        public static bool NewGenerationIsReady { get; set; } = false;        
        
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
            _field = new FieldCellStatuses[GameRules.Cols, GameRules.Rows];
            // заполнения поля пустыми ячейками
            for (int x = 1; x < GameRules.Cols; x++)
            {
                for (int y = 1; y < GameRules.Rows; y++)
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

            FieldAndFrogEventHandler handler = new FieldAndFrogEventHandler(GetField(), _frogsList);
            handler.NextGeneration();
            _field = handler.GetField();
            _frogsList = handler.GetFrogs();
        }
        public FieldCellStatuses[,] GetField()
        {
            FieldCellStatuses[,] newField = new FieldCellStatuses[GameRules.Cols, GameRules.Rows];
            for (int x = 0; x < GameRules.Cols; x++)
            {
                for (int y = 0; y < GameRules.Rows; y++)
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
            for (int x = 0; x < GameRules.Cols; x++)
            {
                for (int y = 0; y < GameRules.Rows; y++)
                {
                    if ((x == 0 || x == GameRules.Cols-1 && y >= 0) || (y == 0 || y == GameRules.Rows-1 && x >= 0))
                    {
                        _field[x, y] = FieldCellStatuses.Wall;
                    }
                }
            }
        }
        private void AddInternalWalls()
        {
            _currentCountWalls = 0;
            while (_currentCountWalls < GameRules.MaxCounWalls)
            {
                Random rand = new Random();

                int xCenter = rand.Next(0, GameRules.Cols);
                int yCenter = rand.Next(0, GameRules.Rows);

                for (int x = xCenter - 1; x < xCenter + 1; x++)
                {
                    for (int y = yCenter - 1; y < yCenter + 1; y++)
                    {
                        if (x != xCenter && y != yCenter && x < GameRules.Cols-2 && y < GameRules.Rows-2 && x > 0 && y > 0)
                        {
                            if (_field[x, y] == FieldCellStatuses.Wall && _currentCountWalls + 2 < GameRules.MaxCounWalls)
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
            while (_currenCountFood < GameRules.MaxCountFood)
            {
                Random rand = new Random();
                int x = rand.Next(0, GameRules.Cols);
                int y = rand.Next(0, GameRules.Rows);
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
            while (_currenCountPoison < GameRules.MaxCountPoison)
            {
                Random rand = new Random();
                int x = rand.Next(0, GameRules.Cols);
                int y = rand.Next(0, GameRules.Rows);
                if (_field[x, y] == FieldCellStatuses.Empty)
                {
                    _field[x, y] = FieldCellStatuses.Poison;
                    _currenCountPoison++;
                }
            }
        }
        private void AddFrogs()
        {
            while (_frogsList.Count < GameRules.MaxCoutnFrogs)
            {
                Random rand = new Random();
                int posX = rand.Next(0, GameRules.Cols);
                int posY = rand.Next(0, GameRules.Rows);
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
            int countMutantFrogs = 0;
            foreach (var frog in _frogsList)
            {                
                bool frogPlaced = false;
                while (frogPlaced == false)
                {
                    Random rand = new Random();
                    int posX = rand.Next(0, GameRules.Cols);
                    int posY = rand.Next(0, GameRules.Rows);
                    if (_field[posX, posY] == FieldCellStatuses.Empty)
                    {
                        FieldCellStatuses newCellStatus;
                        if (countMutantFrogs < GameRules.MaxCoutnMutants)// типо было каждая 8
                        {
                            mutantFrogsList.Add( new Frog( posX, posY, PerformMutation(frog.Genome), FieldCellStatuses.FrogMutant) );
                            newCellStatus = FieldCellStatuses.FrogMutant;
                        }
                        else
                        {
                            mutantFrogsList.Add( new Frog(posX, posY, frog.Genome) );
                            newCellStatus = FieldCellStatuses.Frog;
                        }

                        _field[posX, posY] = newCellStatus;

                        frogPlaced = true;
                    }
                }
                countMutantFrogs++;
            }
            _frogsList.Clear();
            foreach (var mutantFrog in mutantFrogsList)
            {
                _frogsList.Add(mutantFrog);
            }
        }
        private FrogActions[] PerformMutation(FrogActions[] oldGenome)
        {
            FrogActions[] newGenome = new FrogActions[GameRules.GenomeSize];
            int countMutantGen = 0;

            for (int i = 0; i < oldGenome.Length; i++)
            {
                newGenome[i] = oldGenome[i];
            }

            while (countMutantGen < GameRules.MaxMutantGens)
            {
                Random rand1 = new Random();
                int genToMutationIndex = rand1.Next(0, GameRules.GenomeSize);
                Random rand2 = new Random();
                int newActionId = rand2.Next(0, 64);
                FrogActions newAction = (FrogActions)newActionId;

                while (newAction == oldGenome[genToMutationIndex])
                {
                    newActionId = rand2.Next(0, 64);
                    newAction = (FrogActions)newActionId;
                }
                newGenome[genToMutationIndex] = newAction;
                countMutantGen++;
            }
            return newGenome;            
        }        
    }
}
