// ---- C# II (Dor Ben Dor) ----
//         Yoav Kendler
// -----------------------------
namespace Berzeker_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dice dice1 = new Dice(2,6,2);
            Dice dice2 = new Dice(2,6,2);
            Barbarian barbarian = new(dice1, 20);
            Zombie zombie = new(dice2, 2);
            barbarian.Attack(zombie);
            //GameLoop.StartGame();
        }
    }

}
