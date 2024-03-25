using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal class Deck<T> where T : struct, IComparable<T>
    {
        List<T> _deck = new List<T>();
        List<T> _discardPile = new List<T>();
        
        public int Size { get; }
        public int Remaining => _deck.Count;

        public Deck(int size)
        {
            if(size <= 0)
                throw new ArgumentOutOfRangeException("size must be greter than 0");
            this.Size = size;
        }

        public void Shuffle()
        {
            Random rng = new Random();
            _deck = _deck.OrderBy(card => rng.Next()).ToList();
        }

        public void ReShuffle()
        {
            _deck.AddRange(_discardPile);
            _discardPile.Clear();
            Shuffle();
        }

        public bool TryAddCard(T card)
        {
            if(_deck.Count == Size)
                return false;
            _deck.Add(card);
            return true;
        }
        
        public T TryDraw()
        {
            if(_deck.Count == 0) 
            {
                return default;
            }
            T card = _deck.First();
            _discardPile.Add(card);
            _deck.Remove(card);
            return card;
        }

        public T Peek()
        {
            if (_deck.Count == 0)
            {
                return default;
            }
            return _deck.First();
        }
    }
}
