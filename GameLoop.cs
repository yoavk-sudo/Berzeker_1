using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Berzeker_1
{
    internal static class GameLoop
    {
        static float _chanceOfWeatherChange = 0.1f;
        static uint _armySize;
        static Unit.Weather _currentWeather;
        static int _weatherSteps = 5;
        static int _currentWeatherStep;
        static List<Unit> _units = new List<Unit>();

        internal static List<Unit> Units { get => _units; private set => _units = value; }
        public static uint ArmySize { get => _armySize; private set => _armySize = value; }
        public static float ChanceOfWeatherChange { get => Math.Clamp(_chanceOfWeatherChange, 0 , 1); set => _chanceOfWeatherChange = value; }
        public static int CurrentWeatherStep { get => (int)Math.Clamp(_currentWeatherStep, 0, _weatherSteps); set => _currentWeatherStep = value; }

        public static void AddToUnitList(Unit unit)
        {
            Units.Add(unit);
        }

        public static void StartGame()
        {
            Console.WriteLine("How many units would you like to be in each army?");
            bool validArmySize = uint.TryParse(Console.ReadLine(), out _armySize);
            if(!validArmySize || ArmySize == 0)
            {
                Console.WriteLine("Number of units must be an interger greater than 0, bozo");
                return;
            }
            Console.WriteLine("Type first general's name");
            string nameOfFirstGeneral = Console.ReadLine();
            Console.WriteLine("Type second general's name");
            string nameOfSecondGeneral = Console.ReadLine();
            int race1 = Random.Shared.Next(0, 3);
            int race2 = Random.Shared.Next(0, 3);
            int numberOfResources1 = Random.Shared.Next(10, 100);
            int numberOfResources2 = Random.Shared.Next(10, 100);
            ArmyGeneral general1 = new(nameOfFirstGeneral, numberOfResources1, (Races.Race)Enum.ToObject(typeof(Races.Race), race1));
            ArmyGeneral general2 = new(nameOfSecondGeneral, numberOfResources2, (Races.Race)Enum.ToObject(typeof(Races.Race), race2));
            if (general1.Army.Count == ArmySize)
                Console.WriteLine($"{general1}'s {general1.Race} army is ready for battle!\n" +
                    $"they hold {general1.Resources} resources!");
            else
            {
                Console.WriteLine("Could not generate armies, terminating program");
                return;
            }
            if (general2.Army.Count == ArmySize)
                Console.WriteLine($"{general2}'s {general2.Race} army is ready for battle!\n" +
                    $"they hold {general2.Resources} resources!");
            else
            {
                Console.WriteLine("Could not generate armies, terminating program");
                return;
            }
            ChangeWeather();
            Console.WriteLine("Do you want the simulation to wait for your input between steps? (y/n)");
            
            char choice = Console.ReadKey().KeyChar;
            if(choice == 'y' || choice == 'Y')
            {
                MainLoopLineStops(general1, general2);
                return;
            }
            MainLoop(general1, general2);
        }

        //each step declare winner if an army dies. If not proceed with combat, then change weather.
        private static void MainLoop(ArmyGeneral general1, ArmyGeneral general2)
        {
            while (true)
            {
                general1.ApplyWeatherEffect(_currentWeather);
                general2.ApplyWeatherEffect(_currentWeather);
                Combat(general1, general2);
                if (AreArmiesAlive(general1, general2))
                    break;
                if (IsWeatherChanging())
                    ChangeWeather();
                Combat(general2, general1);
                if(AreArmiesAlive(general2, general1))
                    break;
                if (IsWeatherChanging())
                    ChangeWeather();
            }
        }
        private static void MainLoopLineStops(ArmyGeneral general1, ArmyGeneral general2)
        {
            while (true)
            {
                general1.ApplyWeatherEffect(_currentWeather);
                general2.ApplyWeatherEffect(_currentWeather);
                Console.ReadKey(true);
                Combat(general1, general2);
                Console.ReadKey(true);
                if (AreArmiesAlive(general1, general2))
                    break;
                if (IsWeatherChanging())
                {
                    ChangeWeather();
                    Console.ReadKey(true);
                }
                Combat(general2, general1);
                Console.ReadKey(true);
                if(AreArmiesAlive(general2, general1))
                    break;
                if (IsWeatherChanging())
                {
                    ChangeWeather();
                    Console.ReadKey(true);
                }
            }
        }

        private static void Combat(ArmyGeneral general1, ArmyGeneral general2)
        {
            Unit firstUnit = general1.GetRandomUnit();
            Unit secondUnit = general2.GetRandomUnit();
            if(firstUnit == null || secondUnit == null)
            {
                AreArmiesAlive(general1, general2);
                return;
            }
            firstUnit.Attack(secondUnit);
            return;
        }

        private static bool AreArmiesAlive(ArmyGeneral general1, ArmyGeneral general2)
        {   
            if (general1.AliveUnits.Count == 0 || general2.AliveUnits.Count == 0)
            {
                DeclareWinner(general1.AliveUnits.Count == 0 ? general2 : general1);
                return true;
            }
            return false;
        }

        private static void DeclareWinner(ArmyGeneral general)
        {
            Console.WriteLine($"{general} won! They now have {general.Resources} Ferrari Dino 246 GT!");
            Environment.Exit(0);
        }

        private static Unit.Weather ChangeWeather()
        {
            Unit.Weather[] weathers = (Unit.Weather[])Enum.GetValues(typeof(Unit.Weather));
            _currentWeather = weathers[Random.Shared.Next(0, weathers.Length)];
            Console.WriteLine($"Weather has changed to {_currentWeather}!");
            return _currentWeather;
        }

        private static Unit.Weather ChangeWeather(Unit.Weather weather)
        {
            _currentWeather = weather;
            Console.WriteLine($"Weather has changed to {_currentWeather}!");
            return _currentWeather;
        }

        private static bool IsWeatherChanging()
        {
            CurrentWeatherStep++;
            if (Random.Shared.NextDouble() < ChanceOfWeatherChange)
            {
                CurrentWeatherStep = 0;
                ChanceOfWeatherChange = 0.1f;
                return true;
            }
            if(CurrentWeatherStep > _weatherSteps)
            {
                ChangeWeather(Unit.Weather.ClearSkies);
                CurrentWeatherStep = 0;
            }
            ChanceOfWeatherChange += 0.1f;
            return false;
        }
    }
}