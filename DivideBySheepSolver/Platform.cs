using System;
using System.Collections.Generic;

namespace DivideBySheepSolver
{
    /// <summary>
    /// 平台（島或救生艇）
    /// </summary>
    public class Platform : IEquatable<Platform>
    {
        public Platform(Coordinate coordinate, int capacity, AnimalAmount animalAmount = default, int? boatOrder = null, int? boatSheep = 0, int? boatWolf = 0)
        {
            Coordinate = coordinate;
            Capacity = capacity;
            AnimalAmount = animalAmount;
            BoatOrder = boatOrder;
            BoatSheep = boatSheep;
            BoatWolf = boatWolf;

            if (!animalAmount.Vaild) throw new ArgumentException();
            if (animalAmount.CountInHalf > CapacityInHalf) throw new ArgumentException();
        }

        /// <summary>
        /// 深度複製
        /// </summary>
        /// <returns></returns>
        public Platform Clone()
        {
            return new Platform(Coordinate, Capacity, AnimalAmount, BoatOrder, BoatSheep, BoatWolf);
        }

        /// <summary>
        /// 坐標
        /// </summary>
        public Coordinate Coordinate { get; private set; }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity { get; private set; }

        /// <summary>
        /// 以半個計算容量
        /// </summary>
        public int CapacityInHalf => Capacity * 2;

        /// <summary>
        /// 動物數量
        /// </summary>
        public AnimalAmount AnimalAmount { get; private set; }

        /// <summary>
        /// 第幾個救生艇
        /// </summary>
        public int? BoatOrder { get; private set; }

        /// <summary>
        /// 達成救生艇條件
        /// </summary>
        public bool FulfillBoat => AnimalAmount.SheepCountInHalf == BoatSheep * 2 &&
            AnimalAmount.Wolf == BoatWolf;

        /// <summary>
        /// 達成救生艇條件的羊數量
        /// </summary>
        public int? BoatSheep { get; }

        /// <summary>
        /// 達成救生艇條件的狼數量
        /// </summary>
        public int? BoatWolf { get; }

        /// <summary>
        /// 移動動物到另一個平台
        /// </summary>
        /// <param name="other"></param>
        public bool MoveTo(Platform other,bool razor)
        {
            if (BoatOrder.HasValue) return false;
            if (other == null) return false;
            var distence = Coordinate.Distence(other.Coordinate);
            if (distence != 1) return false;
            if (!AnimalAmount.HasMovableAnimal) return false;

            var sheep = razor ? 0 : AnimalAmount.Sheep;
            var sheepHalf = razor ? AnimalAmount.SheepHalf + AnimalAmount.Sheep * 2 : AnimalAmount.SheepHalf;
            other.MoveIn(sheep, sheepHalf, AnimalAmount.Wolf);
            AnimalAmount = new AnimalAmount(0, 0, 0, AnimalAmount.WolfFull);
            return true;
        }

        /// <summary>
        /// 移入動物
        /// </summary>
        /// <param name="sheep"></param>
        /// <param name="sheepHalf"></param>
        /// <param name="wolf"></param>
        public void MoveIn(int sheep,int sheepHalf,int wolf)
        {
            if ((sheep + sheepHalf) > 0 && wolf > 0) throw new ArgumentException();
            sheep += AnimalAmount.Sheep;
            sheepHalf += AnimalAmount.SheepHalf;
            wolf += AnimalAmount.Wolf;
            var wolfFull = AnimalAmount.WolfFull;
            //TODO:確認狼吃羊或半羊的順序
            if(wolf > 0)
            {
                var sheepAte = Math.Min(sheep, wolf);
                sheep -= sheepAte;
                wolf -= sheepAte;
                wolfFull += sheepAte;
                var sheepHalfAte = Math.Min(sheepHalf, wolf);
                sheepHalf -= sheepHalf;
                wolf -= sheepHalfAte;
                wolfFull += sheepHalfAte;
            }
            //TODO:確認移入羊與半羊超過島容量時的丟棄做法
            //TODO:救生艇的狀況
            var capacity = Capacity - wolfFull;
            if(wolf == 0)
            {
                sheep = Math.Min(sheep, capacity);
                capacity -= sheep;
                sheepHalf = Math.Min(sheepHalf, BoatOrder.HasValue ? capacity * 2 : capacity);
                AnimalAmount = new AnimalAmount(sheep, sheepHalf, 0, wolfFull);
                return;
            }
            wolf = Math.Min(wolf, capacity);
            AnimalAmount = new AnimalAmount(0, 0, wolf, wolfFull);
        }




        public override bool Equals(object obj)
        {
            return Equals(obj as Platform);
        }

        public bool Equals(Platform other)
        {
            return other != null &&
                   Coordinate == other.Coordinate &&
                   Capacity == other.Capacity &&
                   AnimalAmount == other.AnimalAmount &&
                   EqualityComparer<int?>.Default.Equals(BoatOrder, other.BoatOrder) &&
                   FulfillBoat == other.FulfillBoat &&
                   EqualityComparer<int?>.Default.Equals(BoatSheep, other.BoatSheep) &&
                   EqualityComparer<int?>.Default.Equals(BoatWolf, other.BoatWolf);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Coordinate);
            hash.Add(Capacity);
            hash.Add(AnimalAmount);
            hash.Add(BoatOrder);
            hash.Add(FulfillBoat);
            hash.Add(BoatSheep);
            hash.Add(BoatWolf);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"{Coordinate},{Capacity},{AnimalAmount},{BoatOrder},{BoatSheep},{BoatWolf}";
        }

        public string Visual()
        {
            var bo = BoatOrder?.ToString() ?? "I";
            return $"{Capacity}{AnimalAmount}{bo}";
        }
    }
}