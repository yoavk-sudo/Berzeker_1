// ---- C# II (Dor Ben Dor) ----
//         Yoav Kendler
// -----------------------------
using System.Reflection;
using static Berzeker_1.Races;

namespace Berzeker_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bag bag = new Bag(20);
            Dice damage = new(2,2,2);
            Dice hit = new(2,2,2);
            int hp = 3;

            MPoppins maid = new(damage, hit, hp, bag);
            maid.Attack(maid);
            maid.Attack(maid);
            maid.Attack(maid);
            maid.Attack(maid);
            maid.Attack(maid);
            maid.Attack(maid);
            maid.Attack(maid);
            maid.Attack(maid);
            maid.Attack(maid);
            //GameLoop.StartGame();
        }
    }

}
