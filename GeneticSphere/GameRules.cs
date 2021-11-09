using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSphere
{
    internal static class GameRules
    {
        // Размеры поля
        public static int Rows { get; } = 257;
        public static int Cols { get; } = 257;

        // Количество жаб (ботов)

        // Размер генома 
        public static int GenomeSize { get; } = 128;

        // Урон за каждый ход
        public static int EveryTurnDamage { get; } = 1;

        // Урон яда

        // Сколько еда и яд, дают хп
        public static int FoodPoints { get; } = 59;
        public static int PoisonPoints { get; } = 99;

        // Сколько максимум может быть объектов на карте
        public static int MaxCountFood { get; } = 20000;
        public static int MaxCountPoison { get; } = 960;
        public static int MaxCounWalls { get; } = 90;



        public static int GetCountFieldCells()
        {
            return Rows * Cols;
        }
    }
}
