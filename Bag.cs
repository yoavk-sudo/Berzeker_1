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
        }
        public int GetAndRemoveRandomItemFromBag()
        {
            if(_items.Count == 0)
                _items = _deadItems;
            int temp = _items[GetRandomInt()];
            _items.Remove(temp);
            return temp;
        }

        public int GetRandomInt()
        {
            return Random.Shared.Next(0, _items.Count);
        }

        private void PopulateBag()
        {
            for (int i = 0; i < _bagSize; i++)
            {
                _items.Add(GetRandomInt());
            }
            _deadItems = _items;
        }
    }
}
