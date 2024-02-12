namespace Berzeker_1
{
    internal class Bag : IRandomProvider
    {

        private int _bagSize;
        private List<int> _items;
        private List<int> _deadItems;

        public Bag(int size)
        {
            if(size <= 0)
            {
                throw new ArgumentException("Bag size must be a positive int larger than 0");
            }
            _bagSize = size;
            _items = new List<int>();
            _deadItems = new List<int>();
            PopulateBag();
            Console.WriteLine(_items.Count);
            Console.WriteLine(_deadItems.Count);
        }

        /// <summary>
        /// if weight is already present in bag, add another instance of it
        /// </summary>
        /// <param name="weight"></param>
        public void ChangeRandomWeights(int weight)
        {
            if(_deadItems.Contains(weight))
            {
                _items.Add(weight);
                _deadItems.Add(weight);
            }
        }

        public int GetAverageRandom()
        {
            if(_items.Count == 0)
                return 0;
            return (int)_items.Average();
        }

        /// <summary>
        /// returns item from bag, removes it. If bag is empty at the start, refills it.
        /// </summary>
        /// <returns></returns>
        public int GetRandomInt()
        {
            if (_items.Count == 0)
                _items = _deadItems;
            int temp = _items[Random.Shared.Next(0, _items.Count - 1)];
            _items.Remove(temp);
            return temp;
        }

        private void PopulateBag()
        {
            for (int i = 0; i < _bagSize; i++)
            {
                _items.Add(Random.Shared.Next(200));
                _deadItems.Add(_items[i]);
            }
        }
    }
}
