using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal class RandomFighter<T> where T : struct, IComparable<T>
    {
        int _deckWins, _diceWins, _ties = 0;
        int result = 0;

        public void Match(Dice<T> dice, Deck<T> deck)
        {
            while (deck.Remaining > 0)
            {
                result = dice.Roll().CompareTo(deck.TryDraw());
                switch (result)
                {
                    case -1: _deckWins++; break;
                    case 0: _ties++; break;
                    case 1: _diceWins++; break;
                }
            }
            Console.WriteLine($"Deck wins: {_deckWins}\nDice Wins: {_diceWins}\nTies: {_ties}");
        }
    }
}
