using System;

namespace DivideBySheepSolver
{
    public struct AnimalAmount : IEquatable<AnimalAmount>
    {
        public static bool operator ==(AnimalAmount a, AnimalAmount b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(AnimalAmount a, AnimalAmount b)
        {
            return !a.Equals(b);
        }

        public AnimalAmount(int sheep = 0, int sheepHalf = 0, int wolf = 0, int wolfFull = 0)
        {
            Sheep = sheep;
            SheepHalf = sheepHalf;
            Wolf = wolf;
            WolfFull = wolfFull;
        }

        /// <summary>
        /// 羊
        /// </summary>
        public int Sheep { get; }

        /// <summary>
        /// 切半的羊
        /// </summary>
        public int SheepHalf { get; }

        /// <summary>
        /// 狼
        /// </summary>
        public int Wolf { get; }

        /// <summary>
        /// 吃飽的狼
        /// </summary>
        public int WolfFull { get; }

        /// <summary>
        /// 傳回異動後的新實例
        /// </summary>
        /// <param name="sheep"></param>
        /// <param name="sheepHalf"></param>
        /// <param name="wolf"></param>
        /// <param name="wolfFull"></param>
        /// <returns></returns>
        public AnimalAmount Change(int sheep, int sheepHalf, int wolf, int wolfFull)
        {
            return new AnimalAmount(Sheep + sheep, SheepHalf + sheepHalf, Wolf + wolf, WolfFull + wolfFull);
        }

        public override bool Equals(object obj)
        {
            return obj is AnimalAmount amount && Equals(amount);
        }

        public bool Equals(AnimalAmount other)
        {
            return Sheep == other.Sheep &&
                   SheepHalf == other.SheepHalf &&
                   Wolf == other.Wolf &&
                   WolfFull == other.WolfFull;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Sheep, SheepHalf, Wolf, WolfFull);
        }

        public bool Vaild => (Sheep + SheepHalf) == 0 || Wolf == 0;

        /// <summary>
        /// 以半個計算羊數量
        /// </summary>
        public int SheepCountInHalf => Sheep * 2 + SheepHalf;

        /// <summary>
        /// 以半個計算非狼數量（用以處理移入羊、半羊）
        /// </summary>
        public int NotWolfCountInHalf => Sheep * 2 + SheepHalf + WolfFull;

        /// <summary>
        /// 以半個計算總量
        /// </summary>
        public int CountInHalf => Sheep * 2 + SheepHalf + Wolf * 2 + WolfFull * 2;

        /// <summary>
        /// 有可移動動物
        /// </summary>
        public bool HasMovableAnimal => Sheep > 0 || SheepHalf > 0 || Wolf > 0;

        public override string ToString()
        {
            string format(int number)
            {
                return number == 0 ? "-" : number.ToString();
            }
            return $"({format(Sheep)},{format(SheepHalf)},{format(Wolf)},{format(WolfFull)})";
        }
    }
}