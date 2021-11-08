using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSphere
{
    public enum FieldCellStatuses : byte
    {
        Empty = 0,// в ячейке ничего нет
        Wall = 1,// в ячейке есть стена
        Poison = 2,// в ячейке есть яд
        Food = 3,// в ячейке есть еда
        Frog = 4,// в ячейке есть лягушка
        FrogMutant = 5// в ячейке есть лягушка мутант
    }

    public enum FrogActions : int
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

    static class SpecialDataTypes
    {
    }
}
