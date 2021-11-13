using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSphere
{
    internal static class GameRules
    {
        #region GameEngine
        // Размеры поля
        public static int Rows { get; } = 65; // default 257
        public static int Cols { get; } = 65; // default 257
        // Сколько максимум может быть объектов на карте
        // Еда
        public static int MaxCountFood { get; } = 1250; // default 20000
        public static int CurrentCountFood { get; set; } = 0;
        public static int CountRegenFoodPerTurn { get; } = 1;
        // Яд
        public static int MaxCountPoison { get; } = 250; // default 960        
        public static int CurrentCountPoison { get; set; } = 0;
        public static int CountRegenPoisonPerTurn { get; } = 1;
        // Стены
        public static int MaxCounWalls { get; } = 90; // default 90
        public static int CurrentCountWalls { get; set; } = 0;
        // Номер поколения 
        public static int Generation { get; set; } = 0;
        // Урон за каждый ход
        public static int EveryTurnDamage { get; } = 1;
        // Сколько бесплатных ходов
        public static int MaxNoDamageTurns { get; } = 10;
        // Урон яда
        public static int PoisonDamage { get; } = 100;
        // Сколько еда и яд, дают хп
        public static int FoodPoints { get; } = 10;
        public static int PoisonPoints { get; } = 50;
        // Кол-во ходов
        public static int CountTurns { get; set; } = 0;
        // Разрешение (размер одной клетки в px) 
        public static int Resolution { get; set; } = 3;
        //
        public static bool StartWithLoadedGenome { get; set; } = false;
        #endregion

        #region FROGS
        // Максимальное кол-во жаб
        public static int MaxCoutnFrogs { get; } = 64; // default 64
        // Кол-во выживщих, жаб, которые дадут новое поколение
        public static int NumberSurvivors { get; } = 8; // default 8
        // Кол-во мутантов (жаб с измененнными генами)
        public static int MaxCoutnMutants { get; } = 32;// defalut 8
        // Кол-во мутированных генов
        public static int MaxMutantGens { get; } = 1; // default 1
        // Здоровье
        public static int MaxHelfPoints { get; } = 50;
        public static int MinHelfPoints { get; } = 1;
        // Размер генома 
        public static int GenomeSize { get; } = 64;
        // Кол-во живых обычных жаб
        public static int CountAliveFrogs { get; set; }
        // Кол-во живых мутантов
        public static int CountAliveMutants { get; set; }
        // Текущий геном, чтобы сохранить его
        public static string GenomeToSave { get; set; }
        public static string GenomeToLoad { get; set; }
        #endregion

        #region RENDERING
        // Отрисовывать ли кадр
        public static bool RenderIsOn { get; set; }
        // Сколь пропускать кадров
        public static int DroppedFrames { get; set; } = 1; // 1 default
        #endregion



        public static int GetCountFieldCells()
        {
            return Rows * Cols;
        }
    }
}
