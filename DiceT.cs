using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal class Dice<T> where T : IComparable<T>
    {
        private uint _scalar;
        private uint _baseDie;
        private int _modifier;
        private T[] _diceValues;

        public uint Scalar { get => _scalar; set => _scalar = value; }
        public uint BaseDie { get => _baseDie; set => _baseDie = value; }
        public int Modifier { get => _modifier; set => _modifier = value; }

        public Dice(uint numberOfDice, uint numberOfSidesPerDie, int modifier, T[] diceValues)
        {
            _scalar = numberOfDice;
            _baseDie = numberOfSidesPerDie;
            _modifier = modifier;
            _diceValues = new T[DiceParameterAggregator()];
            SetDice(diceValues);
        }

        private int DiceParameterAggregator()
        {
            int value = 0;
            for (int i = 0; i < Scalar; i++)
            {
                value += (int)BaseDie;
            }
            value += Modifier;
            return value;
        }

        private void SetDice(T[] diceValues)
        {
            int length = Math.Min(_diceValues.Length, diceValues.Length);
            for (int i = 0; i < length; i++)
            {
                _diceValues[i] = diceValues[i];
            }
        }

        public T Roll()
        {
            int randomIndex = Random.Shared.Next(0, _diceValues.Length);
            return _diceValues[randomIndex];
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
            if ((obj is not Dice enemyDie)) { return false; }
            return Scalar == enemyDie.Scalar &&
                BaseDie == enemyDie.BaseDie &&
                Modifier == enemyDie.Modifier;
        }

        public override int GetHashCode()
        {
            return ShiftAndWrap(Scalar, 2) * ShiftAndWrap(BaseDie, 2) + Modifier.GetHashCode();
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

        private void SetModifier(int modifier)
        {
            Modifier = modifier;
        }

        private void ChangeModifier(int modifier)
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
            if (Random.Shared.NextSingle() < 0.5f)
                randomModifier *= -1;
            return new Dice(randomScalar, randomBaseDie, randomModifier);
        }

        public int AverageDiceRoll()
        {
            return (int)(Scalar * BaseDie + Modifier) / 2;
        }

        /// <summary>
        /// Rolls Dice, returns result as int
        /// </summary>
        /// <returns></returns>
        public T GetRandomT()
        {
            return Roll();
        }

        /// <summary>
        /// Change Dice modifier to be +weight
        /// </summary>
        /// <param name="weight"></param>
        public void ChangeRandomWeights(int weight)
        {
            ChangeModifier(weight);
        }

        /// <summary>
        /// return the average Dice roll as int
        /// </summary>
        /// <returns></returns>
        public int GetAverageRandom()
        {
            return AverageDiceRoll();
        }
    }
}
