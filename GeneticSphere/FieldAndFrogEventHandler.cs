using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSphere
{
    internal class FieldAndFrogEventHandler
    {
        private FieldCellStatuses[,] _field;
        private List<Frog> _frogsList;

        public FieldAndFrogEventHandler(FieldCellStatuses[,] field, List<Frog> frogsList)
        {
            _field = field;
            _frogsList = frogsList;
        }

        public void NextGeneration()
        {
            int countIsAliveFrogs = 0;

            foreach (var frog in _frogsList)
            {
                GameRules.CountAliveFrogs = _frogsList.Where(f => f.FrogType == FieldCellStatuses.Frog && f.IsAlive == true).Count();
                GameRules.CountAliveMutants = _frogsList.Where(f => f.FrogType == FieldCellStatuses.FrogMutant && f.IsAlive == true).Count();

                if (frog.IsAlive == false)
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;                   
                    continue;
                }
                else
                {
                    countIsAliveFrogs = _frogsList.Where(f => f.IsAlive).Count();
                    if (countIsAliveFrogs == GameRules.NumberSurvivors)
                    {
                        List<Frog> mutantFrogs = new List<Frog>();
                        for (int i = 0; i < GameRules.MaxCoutnFrogs; i += GameRules.NumberSurvivors)
                        {
                            mutantFrogs.AddRange(_frogsList.Where(f => f.IsAlive == true));
                        }
                        _frogsList = mutantFrogs;// сформировал новое поколение
                        GameEngine.Generation++;
                        GameEngine.NewGenerationIsReady = true;
                        break;
                    }
                }
                TakeAction(frog);
                GameRules.CountTurns++;
            }
        }

        public List<Frog> GetFrogs()
        {
            List<Frog> mutanFrogs = new List<Frog>();
            foreach (var frog in _frogsList)
            {
                mutanFrogs.Add(frog);
            }
            return mutanFrogs;
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



        private void TakeAction(Frog frog)
        {
            for (int i = 0; i < GameRules.MaxNoDamageTurns; i++)
            {
                if (frog.IsAlive == false)
                {
                    return;
                }

                var action = frog.GetNextAction();

                if (action <= FrogActions.LookAt270)
                {
                    TakeLookAt(action, frog);
                }
                else if (action <= FrogActions.TurnOn270)
                {
                    TakeTurnOn(action, frog);
                }
                else if (action <= FrogActions.MoveOn270)
                {
                    TakeMoveOn(action, frog);
                    break;
                }
                else if (action <= FrogActions.Eat270)
                {
                    TakeEat(action, frog);
                    break;
                }
                else
                {
                    frog.TakeUnconditionalJump(((int)action));
                }

                frog.ReduceHelfPoints(GameRules.EveryTurnDamage);
            }
        }
        
        private void TakeEat(FrogActions action, Frog frog)
        {
            if (action == FrogActions.Eat315)
            {
                var targetCell = _field[frog.PosX - 1, frog.PosY + 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    _field[frog.PosX - 1, frog.PosY + 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.FoodPoints);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    _field[frog.PosX - 1, frog.PosY + 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.PoisonPoints);
                }
            }
            if (action == FrogActions.Eat0)
            {
                var targetCell = _field[frog.PosX, frog.PosY + 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    _field[frog.PosX, frog.PosY + 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.FoodPoints);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    _field[frog.PosX, frog.PosY + 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.PoisonPoints);
                }
            }
            if (action == FrogActions.Eat45)
            {
                var targetCell = _field[frog.PosX + 1, frog.PosY + 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    _field[frog.PosX + 1, frog.PosY + 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.FoodPoints);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    _field[frog.PosX + 1, frog.PosY + 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.PoisonPoints);
                }
            }
            if (action == FrogActions.Eat90)
            {
                var targetCell = _field[frog.PosX + 1, frog.PosY];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    _field[frog.PosX + 1, frog.PosY] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.FoodPoints);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    _field[frog.PosX + 1, frog.PosY] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.PoisonPoints);
                }
            }
            if (action == FrogActions.Eat135)
            {
                var targetCell = _field[frog.PosX + 1, frog.PosY - 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    _field[frog.PosX + 1, frog.PosY - 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.FoodPoints);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    _field[frog.PosX + 1, frog.PosY - 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.PoisonPoints);
                }
            }
            if (action == FrogActions.Eat180)
            {
                var targetCell = _field[frog.PosX, frog.PosY - 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    _field[frog.PosX, frog.PosY - 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.FoodPoints);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    _field[frog.PosX, frog.PosY - 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.PoisonPoints);
                }
            }
            if (action == FrogActions.Eat225)
            {
                var targetCell = _field[frog.PosX - 1, frog.PosY - 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    _field[frog.PosX - 1, frog.PosY - 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.FoodPoints);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    _field[frog.PosX - 1, frog.PosY - 1] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.PoisonPoints);
                }
            }
            if (action == FrogActions.Eat270)
            {
                var targetCell = _field[frog.PosX - 1, frog.PosY];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    _field[frog.PosX - 1, frog.PosY] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.FoodPoints);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    _field[frog.PosX - 1, frog.PosY] = FieldCellStatuses.Empty;
                    frog.AddHelfPoints(GameRules.PoisonPoints);
                }
            }
        }

        private void TakeMoveOn(FrogActions action, Frog frog)
        {
            if (action == FrogActions.MoveOn315 && frog.WhereIsTurned == FrogActions.TurnOn315)
            {
                var targetCell = _field[frog.PosX - 1, frog.PosY + 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    frog.ReduceHelfPoints(GameRules.PoisonDamage);
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                }
                else if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
                {
                    return;
                }
                else
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                    _field[frog.PosX - 1, frog.PosY + 1] = frog.FrogType;
                    frog.PosX -= 1;
                    frog.PosY += 1;
                }                
            }
            else if (action == FrogActions.MoveOn0 && frog.WhereIsTurned == FrogActions.TurnOn0)
            {
                var targetCell = _field[frog.PosX, frog.PosY + 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    frog.ReduceHelfPoints(GameRules.PoisonDamage);
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                }
                else if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
                {
                    return;
                }
                else
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                    _field[frog.PosX, frog.PosY + 1] = frog.FrogType;

                    frog.PosY += 1;
                }                
            }
            else if (action == FrogActions.MoveOn45 && frog.WhereIsTurned == FrogActions.TurnOn45)
            {
                var targetCell = _field[frog.PosX + 1, frog.PosY + 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    frog.ReduceHelfPoints(GameRules.PoisonDamage);
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                }
                else if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
                {
                    return;
                }
                else
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                    _field[frog.PosX + 1, frog.PosY + 1] = frog.FrogType;

                    frog.PosX += 1;
                    frog.PosY += 1;
                }                
            }
            else if (action == FrogActions.MoveOn90 && frog.WhereIsTurned == FrogActions.TurnOn90)
            {
                var targetCell = _field[frog.PosX + 1, frog.PosY];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    frog.ReduceHelfPoints(GameRules.PoisonDamage);
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                }
                else if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
                {
                    return;
                }
                else
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                    _field[frog.PosX + 1, frog.PosY] = frog.FrogType;

                    frog.PosX += 1;
                }                
            }
            else if (action == FrogActions.MoveOn135 && frog.WhereIsTurned == FrogActions.TurnOn135)
            {
                var targetCell = _field[frog.PosX + 1, frog.PosY - 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    frog.ReduceHelfPoints(GameRules.PoisonDamage);
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                }
                else if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
                {
                    return;
                }
                else
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                    _field[frog.PosX + 1, frog.PosY - 1] = frog.FrogType;

                    frog.PosX += 1;
                    frog.PosY -= 1;
                }                
            }
            else if (action == FrogActions.MoveOn180 && frog.WhereIsTurned == FrogActions.TurnOn180)
            {
                var targetCell = _field[frog.PosX, frog.PosY - 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    frog.ReduceHelfPoints(GameRules.PoisonDamage);
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                }
                else if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
                {
                    return;
                }
                else
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                    _field[frog.PosX, frog.PosY - 1] = frog.FrogType;

                    frog.PosY -= 1;
                }                
            }
            else if (action == FrogActions.MoveOn225 && frog.WhereIsTurned == FrogActions.TurnOn225)
            {
                var targetCell = _field[frog.PosX - 1, frog.PosY - 1];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    frog.ReduceHelfPoints(GameRules.PoisonDamage);
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                }
                else if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
                {
                    return;
                }
                else
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                    _field[frog.PosX - 1, frog.PosY - 1] = frog.FrogType;

                    frog.PosX -= 1;
                    frog.PosY -= 1;
                }
                
            }
            else if (action == FrogActions.MoveOn270 && frog.WhereIsTurned == FrogActions.TurnOn270)
            {
                var targetCell = _field[frog.PosX - 1, frog.PosY];
                frog.ReactionTodiscovered(targetCell);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    frog.ReduceHelfPoints(GameRules.PoisonDamage);
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                }
                else if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
                {
                    return;
                }
                else
                {
                    _field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                    _field[frog.PosX - 1, frog.PosY] = frog.FrogType;

                    frog.PosX -= 1;
                }                
            }
            else
            {
                frog.TakeUnconditionalJump(1);
            }

            // передать управление следующему боту
            return;
        }

        /// <summary>
        /// Бот поворачивается в нужную сторону, а указатель команды переходит на
        /// следующую ячейку
        /// </summary>
        /// <param name="action"></param>
        /// <param name="frog"></param>
        private void TakeTurnOn(FrogActions action, Frog frog)
        {
            if (action == FrogActions.TurnOn315)
            {
                frog.WhereIsTurned = FrogActions.TurnOn315;
                frog.TakeUnconditionalJump(1);
            }
            if (action == FrogActions.TurnOn0)
            {
                frog.WhereIsTurned = FrogActions.TurnOn0;
                frog.TakeUnconditionalJump(1);
            }
            if (action == FrogActions.TurnOn45)
            {
                frog.WhereIsTurned = FrogActions.TurnOn45;
                frog.TakeUnconditionalJump(1);
            }
            if (action == FrogActions.TurnOn90)
            {
                frog.WhereIsTurned = FrogActions.TurnOn90;
                frog.TakeUnconditionalJump(1);
            }
            if (action == FrogActions.TurnOn135)
            {
                frog.WhereIsTurned = FrogActions.TurnOn135;
                frog.TakeUnconditionalJump(1);
            }
            if (action == FrogActions.TurnOn180)
            {
                frog.WhereIsTurned = FrogActions.TurnOn180;
                frog.TakeUnconditionalJump(1);
            }
            if (action == FrogActions.TurnOn225)
            {
                frog.WhereIsTurned = FrogActions.TurnOn225;
                frog.TakeUnconditionalJump(1);
            }
            if (action == FrogActions.TurnOn270)
            {
                frog.WhereIsTurned = FrogActions.TurnOn270;
                frog.TakeUnconditionalJump(1);
            }
        }

        /// <summary>
        /// Бот остается на месте, а указаль смещается в зависимости от того,
        /// что обнаружил бот
        /// </summary>
        /// <param name="_field"></param>
        /// <param name="action"></param>
        /// <param name="frog"></param>
        private void TakeLookAt(FrogActions action, Frog frog)
        {
            if (action == FrogActions.LookAt315)
            {
                var targetCell = _field[frog.PosX - 1, frog.PosY + 1];
                frog.ReactionTodiscovered(targetCell);
            }
            if (action == FrogActions.LookAt0)
            {
                var targetCell = _field[frog.PosX, frog.PosY + 1];
                frog.ReactionTodiscovered(targetCell);
            }
            if (action == FrogActions.LookAt45)
            {
                var targetCell = _field[frog.PosX + 1, frog.PosY + 1];
                frog.ReactionTodiscovered(targetCell);
            }
            if (action == FrogActions.LookAt90)
            {
                var targetCell = _field[frog.PosX + 1, frog.PosY];
                frog.ReactionTodiscovered(targetCell);
            }
            if (action == FrogActions.LookAt135)
            {
                var targetCell = _field[frog.PosX + 1, frog.PosY - 1];
                frog.ReactionTodiscovered(targetCell);
            }
            if (action == FrogActions.LookAt180)
            {
                var targetCell = _field[frog.PosX, frog.PosY - 1];
                frog.ReactionTodiscovered(targetCell);
            }
            if (action == FrogActions.LookAt225)
            {
                var targetCell = _field[frog.PosX - 1, frog.PosY - 1];
                frog.ReactionTodiscovered(targetCell);
            }
            if (action == FrogActions.LookAt270)
            {
                var targetCell = _field[frog.PosX - 1, frog.PosY];
                frog.ReactionTodiscovered(targetCell);
            }
        }   
        
    }
}
