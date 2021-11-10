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
        public static int Rows { get; } = 257;
        public static int Cols { get; } = 257;
        // Сколько максимум может быть объектов на карте
        public static int MaxCountFood { get; } = 20000;
        public static int MaxCountPoison { get; } = 960;
        public static int MaxCounWalls { get; } = 90;
        // Урон за каждый ход
        public static int EveryTurnDamage { get; } = 1;
        // Урон яда
        public static int PoisonDamage { get; } = 100;
        // Сколько еда и яд, дают хп
        public static int FoodPoints { get; } = 59;
        public static int PoisonPoints { get; } = 99;
        #endregion

        #region FROGS
        // Количество жаб (ботов)
        public static int MaxCoutnFrogs { get; } = 64;
        // Кол-во выживщих, жаб, которые дадут новое поколение
        public static int NumberSurvivors { get; } = 8;
        // Кол-во мутантов
        public static int MaxCoutnMutants { get; } = 32;//
        // Кол-во мутированных генов
        public static int MaxMutantGens { get; } = 1;
        // Здоровье
        public static int MaxHelfPoints { get; } = 99;
        public static int MinHelfPoints { get; } = 1;
        // Размер генома 
        public static int GenomeSize { get; } = 64;
        #endregion

        



        public static int GetCountFieldCells()
        {
            return Rows * Cols;
        }
    }
}
