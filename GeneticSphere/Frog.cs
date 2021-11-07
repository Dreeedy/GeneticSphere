using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSphere
{
    enum FrogActions : int
    {
        LookAt315 = 0,
        LookAt0 = 1,
        LookAt45 = 2,
        LookAt90 = 3,
        LookAt135 = 4,
        LookAt180 = 5,
        LookAt225 = 6,
        LookAt270 = 7,

        TurnOn315 = 8,
        TurnOn0 = 9,
        TurnOn45 = 10,
        TurnOn90 = 11,
        TurnOn135 = 12,
        TurnOn180 = 13,
        TurnOn225 = 14,
        TurnOn270 = 15,

        MoveOn315 = 16,
        MoveOn0 = 17,
        MoveOn45 = 18,
        MoveOn90 = 19,
        MoveOn135 = 20,
        MoveOn180 = 21,
        MoveOn225 = 22,
        MoveOn270 = 23,

        Eat315 = 24,
        Eat0 = 25,
        Eat45 = 26,
        Eat90 = 27,
        Eat135 = 28,
        Eat180 = 29,
        Eat225 = 30,
        Eat270 = 31,

        UnconditionalJump32 = 32,
        UnconditionalJump33 = 33,
        UnconditionalJump34 = 34,
        UnconditionalJump35 = 35,
        UnconditionalJump36 = 36,
        UnconditionalJump37 = 37,
        UnconditionalJump38 = 38,
        UnconditionalJump39 = 39,

        UnconditionalJump40 = 40,
        UnconditionalJump41 = 41,
        UnconditionalJump42 = 42,
        UnconditionalJump43 = 43,
        UnconditionalJump44 = 44,
        UnconditionalJump45 = 45,
        UnconditionalJump46 = 46,
        UnconditionalJump47 = 47,

        UnconditionalJump48 = 48,
        UnconditionalJump49 = 49,
        UnconditionalJump50 = 50,
        UnconditionalJump51 = 51,
        UnconditionalJump52 = 52,
        UnconditionalJump53 = 53,
        UnconditionalJump54 = 54,
        UnconditionalJump55 = 55,

        UnconditionalJump56 = 56,
        UnconditionalJump57 = 57,
        UnconditionalJump58 = 58,
        UnconditionalJump59 = 59,
        UnconditionalJump60 = 60,
        UnconditionalJump61 = 61,
        UnconditionalJump62 = 62,
        UnconditionalJump63 = 63,
    }

    class Frog
    {
        // команды на перемещение и питание являются завершающими, ход переходит к другому боту
        // если перейти, съеть, посмотреть в другую ячейку будет происходить смешение указателя в зависемости от обнаруженного
        // яд 1, стена 2, бот 3, еда 4, пусто 5
        // если наступить на яд - бот умрет, если съесть яд, получить кучу еды

        private FrogActions[] _genome = new FrogActions[64];
        public int _genePointer = 0;

        public int PosX { get; set; }
        public int PosY { get; set; }

        private FrogActions _whereIsLooking;

        public FieldCellStatuses FrogType { get; set; }

        public int HelfPoint { get; set; }
        private static int maxHelfPoints = 99;
        private static int minHelfPoints = 1;
        private static int foodPoint = 10;
        private static int poisonPoint = 30;
        public bool IsAlive { get; set; }

        public Frog(int posX, int posY, FieldCellStatuses frogType = FieldCellStatuses.Frog)
        {
            //CreateGenome();
            GenerateGenome();

            IsAlive = true;
            _genePointer = 0;
            _whereIsLooking = FrogActions.LookAt0;// возможно устанавливать случайно
            FrogType = frogType;
            HelfPoint = 99;
            PosX = posX;
            PosY = posY;
            // установить posX posY
        }
        
        public static void TakeAction(ref FieldCellStatuses[,] field, Frog frog)
        {
            for (int i = 0; i < 10; i++)
            {
                if (frog.IsAlive == false)
                {
                    return;
                }

                // крутиться и осматриваться можно не больше 10 раз
                var action = frog.GetNextAction();

                if (action <= FrogActions.LookAt270)
                {
                    // LookAt
                    TakeLookAt(ref field, action, frog);
                }
                else if (action <= FrogActions.TurnOn270)
                {
                    // TurnOn
                    TakeTurnOn(action, frog);
                }
                else if (action <= FrogActions.MoveOn270)
                {
                    // MoveOn
                    TakeMoveOn(ref field, action, frog);
                    break;
                }
                else if (action <= FrogActions.Eat270)
                {
                    // Eat
                    TakeEat(ref field, action, frog);
                    break;
                }
                else
                {
                    // UnconditionalJump
                    TakeUnconditionalJump(((int)action), frog);
                }

                ReduceHelfPoints(frog, 1);
            }           
        }

        private static void ReduceHelfPoints(Frog frog, int points)
        {
            if (frog.HelfPoint - points < minHelfPoints)
            {
                frog.HelfPoint = 0;
                frog.IsAlive = false;
            }
            frog.HelfPoint -= points;
        }
        
        private static void AddHelfPoints(Frog frog, int points)
        {
            if (frog.HelfPoint + points > maxHelfPoints)
            {
                frog.HelfPoint = maxHelfPoints;
            }
            frog.HelfPoint += points;
        }

        private static void TakeEat(ref FieldCellStatuses[,] field, FrogActions action, Frog frog)
        {
            if (action == FrogActions.Eat315)
            {
                var targetCell = field[frog.PosX - 1, frog.PosY + 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    field[frog.PosX - 1, frog.PosY + 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, foodPoint);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    field[frog.PosX - 1, frog.PosY + 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, poisonPoint);
                }                
            }
            if (action == FrogActions.Eat0)
            {
                var targetCell = field[frog.PosX, frog.PosY + 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    field[frog.PosX, frog.PosY + 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, foodPoint);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    field[frog.PosX, frog.PosY + 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, poisonPoint);
                }
            }
            if (action == FrogActions.Eat45)
            {
                var targetCell = field[frog.PosX + 1, frog.PosY + 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    field[frog.PosX + 1, frog.PosY + 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, foodPoint);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    field[frog.PosX + 1, frog.PosY + 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, poisonPoint);
                }
            }
            if (action == FrogActions.Eat90)
            {
                var targetCell = field[frog.PosX + 1, frog.PosY];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    field[frog.PosX + 1, frog.PosY] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, foodPoint);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    field[frog.PosX + 1, frog.PosY] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, poisonPoint);
                }
            }
            if (action == FrogActions.Eat135)
            {
                var targetCell = field[frog.PosX + 1, frog.PosY - 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    field[frog.PosX + 1, frog.PosY - 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, foodPoint);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    field[frog.PosX + 1, frog.PosY - 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, poisonPoint);
                }
            }
            if (action == FrogActions.Eat180)
            {
                var targetCell = field[frog.PosX, frog.PosY - 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    field[frog.PosX, frog.PosY - 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, foodPoint);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    field[frog.PosX, frog.PosY - 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, poisonPoint);
                }
            }
            if (action == FrogActions.Eat225)
            {
                var targetCell = field[frog.PosX - 1, frog.PosY - 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    field[frog.PosX - 1, frog.PosY - 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, foodPoint);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    field[frog.PosX - 1, frog.PosY - 1] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, poisonPoint);
                }
            }
            if (action == FrogActions.Eat270)
            {
                var targetCell = field[frog.PosX - 1, frog.PosY];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                if (targetCell == FieldCellStatuses.Food)
                {
                    field[frog.PosX - 1, frog.PosY] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, foodPoint);
                }
                else if (targetCell == FieldCellStatuses.Poison)
                {
                    field[frog.PosX - 1, frog.PosY] = FieldCellStatuses.Empty;
                    AddHelfPoints(frog, poisonPoint);
                }
            }
        }

        private static void TakeMoveOn(ref FieldCellStatuses[,] field, FrogActions action, Frog frog)
        {
            if (action == FrogActions.MoveOn315 && frog._whereIsLooking == FrogActions.TurnOn315)
            {
                var targetCell = field[frog.PosX - 1, frog.PosY + 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                field[frog.PosX - 1, frog.PosY + 1] = frog.FrogType;
                frog.PosX -= 1;
                frog.PosY += 1;
            }
            else if (action == FrogActions.MoveOn0 && frog._whereIsLooking == FrogActions.TurnOn0)
            {
                var targetCell = field[frog.PosX, frog.PosY + 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                field[frog.PosX, frog.PosY + 1] = frog.FrogType;

                frog.PosY += 1;
            }
            else if (action == FrogActions.MoveOn45 && frog._whereIsLooking == FrogActions.TurnOn45)
            {
                var targetCell = field[frog.PosX + 1, frog.PosY + 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                field[frog.PosX + 1, frog.PosY + 1] = frog.FrogType;

                frog.PosX += 1;
                frog.PosY += 1;
            }
            else if(action == FrogActions.MoveOn90 && frog._whereIsLooking == FrogActions.TurnOn90)
            {
                var targetCell = field[frog.PosX + 1, frog.PosY];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                field[frog.PosX + 1, frog.PosY] = frog.FrogType;

                frog.PosX += 1;
            }
            else if(action == FrogActions.MoveOn135 && frog._whereIsLooking == FrogActions.TurnOn135)
            {
                var targetCell = field[frog.PosX + 1, frog.PosY - 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                field[frog.PosX + 1, frog.PosY - 1] = frog.FrogType;

                frog.PosX += 1;
                frog.PosY -= 1;
            }
            else if(action == FrogActions.MoveOn180 && frog._whereIsLooking == FrogActions.TurnOn180)
            {
                var targetCell = field[frog.PosX, frog.PosY - 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                field[frog.PosX, frog.PosY - 1] = frog.FrogType;

                frog.PosY -= 1;
            }
            else if(action == FrogActions.MoveOn225 && frog._whereIsLooking == FrogActions.TurnOn225)
            {
                var targetCell = field[frog.PosX - 1, frog.PosY - 1];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }

                field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                field[frog.PosX - 1, frog.PosY - 1] = frog.FrogType;

                frog.PosX -= 1;
                frog.PosY -= 1;
            }
            else if(action == FrogActions.MoveOn270 && frog._whereIsLooking == FrogActions.TurnOn270)
            {
                var targetCell = field[frog.PosX - 1, frog.PosY];
                ReactionTodiscovered(targetCell, frog);

                if (targetCell == FieldCellStatuses.Wall)
                {
                    return;
                }                

                field[frog.PosX, frog.PosY] = FieldCellStatuses.Empty;
                field[frog.PosX - 1, frog.PosY] = frog.FrogType;

                frog.PosX -= 1;
            }
            else
            {
                TakeUnconditionalJump(1, frog);
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
        private static void TakeTurnOn(FrogActions action, Frog frog)
        {
            if (action == FrogActions.TurnOn315)
            {
                frog._whereIsLooking = FrogActions.TurnOn315;
                TakeUnconditionalJump(1, frog);
            }
            if (action == FrogActions.TurnOn0)
            {
                frog._whereIsLooking = FrogActions.TurnOn0;
                TakeUnconditionalJump(1, frog);
            }
            if (action == FrogActions.TurnOn45)
            {
                frog._whereIsLooking = FrogActions.TurnOn45;
                TakeUnconditionalJump(1, frog);
            }
            if (action == FrogActions.TurnOn90)
            {
                frog._whereIsLooking = FrogActions.TurnOn90;
                TakeUnconditionalJump(1, frog);
            }
            if (action == FrogActions.TurnOn135)
            {
                frog._whereIsLooking = FrogActions.TurnOn135;
                TakeUnconditionalJump(1, frog);
            }
            if (action == FrogActions.TurnOn180)
            {
                frog._whereIsLooking = FrogActions.TurnOn180;
                TakeUnconditionalJump(1, frog);
            }
            if (action == FrogActions.TurnOn225)
            {
                frog._whereIsLooking = FrogActions.TurnOn225;
                TakeUnconditionalJump(1, frog);
            }
            if (action == FrogActions.TurnOn270)
            {
                frog._whereIsLooking = FrogActions.TurnOn270;
                TakeUnconditionalJump(1, frog);
            }
        }

        /// <summary>
        /// Бот остается на месте, а указаль смещается в зависимости от того,
        /// что обнаружил бот
        /// </summary>
        /// <param name="field"></param>
        /// <param name="action"></param>
        /// <param name="frog"></param>
        private static void TakeLookAt(ref FieldCellStatuses[,] field, FrogActions action, Frog frog)
        {
            if (action == FrogActions.LookAt315)
            {
                var targetCell = field[frog.PosX - 1, frog.PosY + 1];
                ReactionTodiscovered(targetCell, frog);
            }
            if (action == FrogActions.LookAt0)
            {
                var targetCell = field[frog.PosX, frog.PosY + 1];
                ReactionTodiscovered(targetCell, frog);
            }
            if (action == FrogActions.LookAt45)
            {
                var targetCell = field[frog.PosX + 1, frog.PosY + 1];
                ReactionTodiscovered(targetCell, frog);
            }
            if (action == FrogActions.LookAt90)
            {
                var targetCell = field[frog.PosX + 1, frog.PosY];
                ReactionTodiscovered(targetCell, frog);
            }
            if (action == FrogActions.LookAt135)
            {
                var targetCell = field[frog.PosX + 1, frog.PosY - 1];
                ReactionTodiscovered(targetCell, frog);
            }
            if (action == FrogActions.LookAt180)
            {
                var targetCell = field[frog.PosX, frog.PosY - 1];
                ReactionTodiscovered(targetCell, frog);
            }
            if (action == FrogActions.LookAt225)
            {
                var targetCell = field[frog.PosX - 1, frog.PosY - 1];
                ReactionTodiscovered(targetCell, frog);
            }
            if (action == FrogActions.LookAt270)
            {
                var targetCell = field[frog.PosX - 1, frog.PosY];
                ReactionTodiscovered(targetCell, frog);
            }
        }

        private static void ReactionTodiscovered(FieldCellStatuses targetCell, Frog frog)
        {
            if (targetCell == FieldCellStatuses.Poison)
            {
                TakeUnconditionalJump(1, frog);
            }
            if (targetCell == FieldCellStatuses.Wall)
            {
                TakeUnconditionalJump(2, frog);
            }
            if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
            {
                TakeUnconditionalJump(3, frog);
            }
            if (targetCell == FieldCellStatuses.Food)
            {
                TakeUnconditionalJump(4, frog);
            }
            if (targetCell == FieldCellStatuses.Empty)
            {
                TakeUnconditionalJump(5, frog);
            }
        }

        private static void TakeUnconditionalJump(int action, Frog frog)
        {
            if (frog._genePointer + ((int)action) > frog._genome.Length - 1)
            {
                frog._genePointer = frog._genePointer + ((int)action) - frog._genome.Length;
            }
            else
            {
                frog._genePointer += ((int)action);
            }            
        }

        private FrogActions GetNextAction()
        {
            return this._genome[this._genePointer];
        }

        private void GenerateGenome()
        {
            for (int i = 0; i < _genome.Length; i++)
            {
                Random rnd = new Random();
                int value = rnd.Next(0, 64);

                _genome[i] = ((FrogActions)value);
            }
        }

        private void CreateGenome()
        {
            _genome[0] = FrogActions.LookAt315;
            _genome[1] = FrogActions.LookAt0;
            _genome[2] = FrogActions.LookAt45;
            _genome[3] = FrogActions.LookAt90;
            _genome[4] = FrogActions.LookAt135;
            _genome[5] = FrogActions.LookAt180;
            _genome[6] = FrogActions.LookAt225;
            _genome[7] = FrogActions.LookAt270;

            _genome[8] = FrogActions.TurnOn315;
            _genome[9] = FrogActions.TurnOn0;
            _genome[10] = FrogActions.TurnOn45;
            _genome[11] = FrogActions.TurnOn90;
            _genome[12] = FrogActions.TurnOn135;
            _genome[13] = FrogActions.TurnOn180;
            _genome[14] = FrogActions.TurnOn225;
            _genome[15] = FrogActions.TurnOn270;

            _genome[16] = FrogActions.MoveOn315;
            _genome[17] = FrogActions.MoveOn0;
            _genome[18] = FrogActions.MoveOn45;
            _genome[19] = FrogActions.MoveOn90;
            _genome[20] = FrogActions.MoveOn135;
            _genome[21] = FrogActions.MoveOn180;
            _genome[22] = FrogActions.MoveOn225;
            _genome[23] = FrogActions.MoveOn270;

            _genome[24] = FrogActions.Eat315;
            _genome[25] = FrogActions.Eat0;
            _genome[26] = FrogActions.Eat45;
            _genome[27] = FrogActions.Eat90;
            _genome[28] = FrogActions.Eat135;
            _genome[29] = FrogActions.Eat180;
            _genome[30] = FrogActions.Eat225;
            _genome[31] = FrogActions.Eat270;

            _genome[32] = FrogActions.UnconditionalJump32;
            _genome[33] = FrogActions.UnconditionalJump33;
            _genome[34] = FrogActions.UnconditionalJump34;
            _genome[35] = FrogActions.UnconditionalJump35;
            _genome[36] = FrogActions.UnconditionalJump36;
            _genome[37] = FrogActions.UnconditionalJump37;
            _genome[38] = FrogActions.UnconditionalJump38;
            _genome[39] = FrogActions.UnconditionalJump39;

            _genome[40] = FrogActions.UnconditionalJump40;
            _genome[41] = FrogActions.UnconditionalJump41;
            _genome[42] = FrogActions.UnconditionalJump42;
            _genome[43] = FrogActions.UnconditionalJump43;
            _genome[44] = FrogActions.UnconditionalJump44;
            _genome[45] = FrogActions.UnconditionalJump45;
            _genome[46] = FrogActions.UnconditionalJump46;
            _genome[47] = FrogActions.UnconditionalJump47;

            _genome[48] = FrogActions.UnconditionalJump48;
            _genome[49] = FrogActions.UnconditionalJump49;
            _genome[50] = FrogActions.UnconditionalJump50;
            _genome[51] = FrogActions.UnconditionalJump51;
            _genome[52] = FrogActions.UnconditionalJump52;
            _genome[53] = FrogActions.UnconditionalJump53;
            _genome[54] = FrogActions.UnconditionalJump54;
            _genome[55] = FrogActions.UnconditionalJump55;

            _genome[56] = FrogActions.UnconditionalJump56;
            _genome[57] = FrogActions.UnconditionalJump57;
            _genome[58] = FrogActions.UnconditionalJump58;
            _genome[59] = FrogActions.UnconditionalJump59;
            _genome[60] = FrogActions.UnconditionalJump60;
            _genome[61] = FrogActions.UnconditionalJump61;
            _genome[62] = FrogActions.UnconditionalJump62;
            _genome[63] = FrogActions.UnconditionalJump63;
        }
    }
}
