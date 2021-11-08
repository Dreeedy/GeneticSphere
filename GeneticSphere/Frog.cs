using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSphere
{
    internal class Frog
    {
        // команды на перемещение и питание являются завершающими, ход переходит к другому боту
        // если перейти, съеть, посмотреть в другую ячейку будет происходить смешение указателя в зависемости от обнаруженного
        // яд 1, стена 2, бот 3, еда 4, пусто 5
        // если наступить на яд - бот умрет, если съесть яд, получить кучу еды

        public FrogActions[] Genome { get; private set; } = new FrogActions[64];
        public int GenePointer { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public FrogActions WhereIsLooking { get; set; }

        public FieldCellStatuses FrogType { get; set; }

        public int HelfPoint { get; private set; }
        private static int maxHelfPoints = 99;
        private static int minHelfPoints = 1;        
        public bool IsAlive { get; private set; }

        public Frog(int posX, int posY, FieldCellStatuses frogType = FieldCellStatuses.Frog)
        {
            //CreateGenome();
            GenerateGenome();

            IsAlive = true;
            GenePointer = 0;
            WhereIsLooking = FrogActions.LookAt0;// возможно устанавливать случайно
            FrogType = frogType;
            HelfPoint = 99;
            PosX = posX;
            PosY = posY;
            // установить posX posY
        }    
        public FrogActions GetNextAction()
        {
            return this.Genome[this.GenePointer];
        }
        public void ReduceHelfPoints(int value)
        {
            if (this.HelfPoint - value < minHelfPoints)
            {
                this.HelfPoint = 0;
                this.IsAlive = false;
            }
            this.HelfPoint -= value;
        }        
        public void AddHelfPoints(int value)
        {
            if (this.HelfPoint + value > maxHelfPoints)
            {
                this.HelfPoint = maxHelfPoints;
            }
            this.HelfPoint += value;
        }        
        public void ReactionTodiscovered(FieldCellStatuses targetCell)
        {
            if (targetCell == FieldCellStatuses.Poison)
            {
                this.TakeUnconditionalJump(1);
            }
            if (targetCell == FieldCellStatuses.Wall)
            {
                this.TakeUnconditionalJump(2);
            }
            if (targetCell == FieldCellStatuses.Frog || targetCell == FieldCellStatuses.FrogMutant)
            {
                this.TakeUnconditionalJump(3);
            }
            if (targetCell == FieldCellStatuses.Food)
            {
                this.TakeUnconditionalJump(4);
            }
            if (targetCell == FieldCellStatuses.Empty)
            {
                this.TakeUnconditionalJump(5);
            }
        }
        public void TakeUnconditionalJump(int action)
        {
            if (this.GenePointer + ((int)action) > this.Genome.Length - 1)
            {
                this.GenePointer = this.GenePointer + ((int)action) - this.Genome.Length;
            }
            else
            {
                this.GenePointer += ((int)action);
            }
        }



        private void GenerateGenome()
        {
            for (int i = 0; i < Genome.Length; i++)
            {
                Random rnd = new Random();
                int value = rnd.Next(0, 64);

                Genome[i] = ((FrogActions)value);
            }
        }
        private void CreateGenome()
        {
            Genome[0] = FrogActions.LookAt315;
            Genome[1] = FrogActions.LookAt0;
            Genome[2] = FrogActions.LookAt45;
            Genome[3] = FrogActions.LookAt90;
            Genome[4] = FrogActions.LookAt135;
            Genome[5] = FrogActions.LookAt180;
            Genome[6] = FrogActions.LookAt225;
            Genome[7] = FrogActions.LookAt270;

            Genome[8] = FrogActions.TurnOn315;
            Genome[9] = FrogActions.TurnOn0;
            Genome[10] = FrogActions.TurnOn45;
            Genome[11] = FrogActions.TurnOn90;
            Genome[12] = FrogActions.TurnOn135;
            Genome[13] = FrogActions.TurnOn180;
            Genome[14] = FrogActions.TurnOn225;
            Genome[15] = FrogActions.TurnOn270;

            Genome[16] = FrogActions.MoveOn315;
            Genome[17] = FrogActions.MoveOn0;
            Genome[18] = FrogActions.MoveOn45;
            Genome[19] = FrogActions.MoveOn90;
            Genome[20] = FrogActions.MoveOn135;
            Genome[21] = FrogActions.MoveOn180;
            Genome[22] = FrogActions.MoveOn225;
            Genome[23] = FrogActions.MoveOn270;

            Genome[24] = FrogActions.Eat315;
            Genome[25] = FrogActions.Eat0;
            Genome[26] = FrogActions.Eat45;
            Genome[27] = FrogActions.Eat90;
            Genome[28] = FrogActions.Eat135;
            Genome[29] = FrogActions.Eat180;
            Genome[30] = FrogActions.Eat225;
            Genome[31] = FrogActions.Eat270;

            Genome[32] = FrogActions.UnconditionalJump32;
            Genome[33] = FrogActions.UnconditionalJump33;
            Genome[34] = FrogActions.UnconditionalJump34;
            Genome[35] = FrogActions.UnconditionalJump35;
            Genome[36] = FrogActions.UnconditionalJump36;
            Genome[37] = FrogActions.UnconditionalJump37;
            Genome[38] = FrogActions.UnconditionalJump38;
            Genome[39] = FrogActions.UnconditionalJump39;

            Genome[40] = FrogActions.UnconditionalJump40;
            Genome[41] = FrogActions.UnconditionalJump41;
            Genome[42] = FrogActions.UnconditionalJump42;
            Genome[43] = FrogActions.UnconditionalJump43;
            Genome[44] = FrogActions.UnconditionalJump44;
            Genome[45] = FrogActions.UnconditionalJump45;
            Genome[46] = FrogActions.UnconditionalJump46;
            Genome[47] = FrogActions.UnconditionalJump47;

            Genome[48] = FrogActions.UnconditionalJump48;
            Genome[49] = FrogActions.UnconditionalJump49;
            Genome[50] = FrogActions.UnconditionalJump50;
            Genome[51] = FrogActions.UnconditionalJump51;
            Genome[52] = FrogActions.UnconditionalJump52;
            Genome[53] = FrogActions.UnconditionalJump53;
            Genome[54] = FrogActions.UnconditionalJump54;
            Genome[55] = FrogActions.UnconditionalJump55;

            Genome[56] = FrogActions.UnconditionalJump56;
            Genome[57] = FrogActions.UnconditionalJump57;
            Genome[58] = FrogActions.UnconditionalJump58;
            Genome[59] = FrogActions.UnconditionalJump59;
            Genome[60] = FrogActions.UnconditionalJump60;
            Genome[61] = FrogActions.UnconditionalJump61;
            Genome[62] = FrogActions.UnconditionalJump62;
            Genome[63] = FrogActions.UnconditionalJump63;
        }
    }
}
