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
            //Dice a = new Dice(2,6,2);
            //Barbarian b = new(a, 2);
            //Console.WriteLine(b);
            //List<Type> _elfUnitTypes = Assembly.GetExecutingAssembly()
            //.GetTypes()
            //.Where(type => type.IsSubclassOf(typeof(Unit)) && !type.IsAbstract)
            //.ToList();
            //foreach (Type type in _elfUnitTypes)
            //{
            //    Console.WriteLine(type.Name);
            //}
            //Dice dice1 = new Dice(2, 6, 2);
            //int roll = dice1.Roll();
            ////Console.WriteLine(dice1.LastRollValue);
            ////Console.WriteLine(roll);
            //Barbarian barbarian = new(dice1, 20);
            //Zombie zombie = new(dice1, 2);
            //barbarian.Attack(zombie);
            GameLoop.StartGame();
        }
    }

}
