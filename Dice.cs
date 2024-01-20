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
        private int _lastRollValue;

        public uint Scalar { get => _scalar; set => _scalar = value; }
        public uint BaseDie { get => _baseDie; set => _baseDie = value; }
        public int Modifier { get => _modifier; set => _modifier = value; }
        public int LastRollValue { get => _lastRollValue; set => _lastRollValue = value; }

        public Dice(uint numberOfDice, uint numberOfSidesPerDie, int modifier)
        {
            _scalar = numberOfDice;
            _baseDie = numberOfSidesPerDie;
            _modifier = modifier;
            _lastRollValue = 0;
        }

        public int Roll()
        {
            LastRollValue = 0;
            for (int i = 0; i < Scalar; i++)
            {
                LastRollValue += (Random.Shared.Next((int)BaseDie) + 1) + Modifier;
            }
            Console.WriteLine("Rolling...\n" + LastRollValue + "!");
            return LastRollValue;
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
            if((obj is not Dice)) { return false; }
            Dice enemyDie = (Dice)obj;
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
            return (int)(Math.Pow(Scalar, Math.PI) * Math.Pow(BaseDie, 3)) + Modifier;
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
    }
}
