namespace Berzeker_1
{
    internal class Bag : IRandomProvider
    {

        private int _bagSize;
        private List<int> _items;
        private List<int> _deadItems;

        public Bag(int size)
        {
            _bagSize = size;
            _items = new List<int>();
            _deadItems = new List<int>();
            PopulateBag();
        }
        public int GetAndRemoveRandomItemFromBag()
        {
            if(_items.Count == 0)
                _items = _deadItems;
            int temp = _items[GetRandomInt(0, _items.Count)];
            _items.Remove(temp);
            return temp;
        }

        public int GetRandomInt(int minValue, int maxValue)
        {
            return Random.Shared.Next(minValue, maxValue);
        }


        private void PopulateBag()
        {
            for (int i = 0; i < _bagSize; i++)
            {
                _items.Add(GetRandomInt(0, _bagSize));
            }
            _deadItems = _items;
        }
    }
}
