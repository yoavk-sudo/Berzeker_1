namespace Berzeker_1
{
    internal interface IRandomProvider
    {
        int GetRandomInt();
        int GetAverageRandom();
        void ChangeRandomWeights(int weight);
    }
}
