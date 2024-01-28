using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal struct Dice
    {
        private uint _scalar;
        private uint _baseDie;
        private int _modifier;

        public uint Scalar { get => _scalar; set => _scalar = value; }
        public uint BaseDie { get => _baseDie; set => _baseDie = value; }
        public int Modifier { get => _modifier; set => _modifier = value; }

        public Dice(uint numberOfDice, uint numberOfSidesPerDie, int modifier)
        {
            _scalar = numberOfDice;
            _baseDie = numberOfSidesPerDie;
            _modifier = modifier;
        }

        public int Roll()
        {
            int value = 0;
            for (int i = 0; i < Scalar; i++)
            {
                value += (Random.Shared.Next((int)BaseDie) + 1);
            }
            value += Modifier;
            Console.WriteLine($"Rolling {this}...\n{value}!");
            return value;
        }

        public override string ToString()
        {
            string plus = "+";
            if (Modifier < 0) plus = "";
            return $"{Scalar}d{BaseDie}{plus}{Modifier}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if((obj is not Dice enemyDie)) { return false; }
            return Scalar == enemyDie.Scalar &&
                BaseDie == enemyDie.BaseDie &&
                Modifier == enemyDie.Modifier;
        }

        /// <summary>
        /// ///////////////////////////
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ShiftAndWrap(Scalar,2) * ShiftAndWrap(BaseDie, 2) + Modifier.GetHashCode();
        }

        private int ShiftAndWrap(uint value, int positions)
        {
            positions = positions & 0x1F;

            // Save the existing bit pattern, but interpret it as an unsigned integer.
            uint number = BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
            // Preserve the bits to be discarded.
            uint wrapped = number >> (32 - positions);
            // Shift and wrap the discarded bits.
            return BitConverter.ToInt32(BitConverter.GetBytes((number << positions) | wrapped), 0);
        }

        /// <summary>
        /// Set the modifier to a new interger value
        /// </summary>
        /// <param name="modifier"></param>
        public void SetModifier(int modifier)
        {
            Modifier = modifier;
        }

        /// <summary>
        /// Increase or decrease modifier
        /// </summary>
        /// <param name="modifier"></param>
        public void ChangeModifier(int modifier)
        {
            Modifier += modifier;
        }

        public void AddDice(int amount)
        {
            Scalar += (uint)Math.Abs(amount);
        }
        
        public void RemoveDice(int amount)
        {
            Scalar -= (uint)Math.Abs(amount);
        }

        public static Dice GenerateRandomDice()
        {
            uint scalar = 4;
            uint baseDie = 41;
            int modifier = 3;
            uint randomScalar = (uint)Random.Shared.Next(1, (int)scalar);
            uint randomBaseDie = (uint)Random.Shared.Next(1, (int)baseDie);
            int randomModifier = Random.Shared.Next(0, modifier);
            if(Random.Shared.NextSingle() < 0.5f)
                randomModifier *= -1;
            return new Dice(randomScalar, randomBaseDie, randomModifier);
        }
    }
}
