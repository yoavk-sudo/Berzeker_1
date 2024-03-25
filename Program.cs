// ---- C# II (Dor Ben Dor) ----
//         Yoav Kendler
// -----------------------------
using System.Reflection;
using static Berzeker_1.Races;

namespace Berzeker_1
{
    internal class Program
    {
        static int[] _randomNumbers = (int[])Enumerable.Range(0, 40).ToArray();
        static void Main(string[] args)
        {
            Deck<int> d = new Deck<int>(40);
            Dice<int> dice = new Dice<int>(1, 20, 0, _randomNumbers);
            TextWriter originalConsoleOut = Console.Out;
            Console.SetOut(TextWriter.Null);
            while (d.TryAddCard(dice.Roll())) ; //fill deck
            Console.SetOut(originalConsoleOut);
            RandomFighter<int> rf = new RandomFighter<int>();
            rf.Match(dice, d);
        }
    }

}
