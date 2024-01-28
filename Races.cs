using System.Reflection;

namespace Berzeker_1
{
    internal abstract class Races
    {
        static List<Type> _elfUnitTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Unit)) && !type.IsAbstract && GetUnitRace(type) == Race.elf)
            .ToList();
        static List<Type> _humanUnitTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Unit)) && !type.IsAbstract && GetUnitRace(type) == Race.human)
            .ToList();
        static List<Type> _undeadUnitTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Unit)) && !type.IsAbstract && GetUnitRace(type) == Race.undead)
            .ToList();

        public static Type? GetTypeFromRace(Race race)
        {
            Type selectedType;
            switch (race)
            {
                case Race.elf:
                    selectedType = _elfUnitTypes[new Random().Next(_undeadUnitTypes.Count)];
                    break;
                case Race.human:
                    selectedType = _humanUnitTypes[new Random().Next(_undeadUnitTypes.Count)];
                    break;
                case Race.undead:
                    selectedType = _undeadUnitTypes[new Random().Next(_undeadUnitTypes.Count)];
                    break;
                default:
                    return null;
            }
            return selectedType;
        }

        private static Race GetUnitRace(Type type)
        {
            switch (type.Name)
            {
                case nameof(Wizard):
                case nameof(ForestWarrior):
                case nameof(ElementalArcher):
                    return Race.elf;
                case nameof(Rogue):
                case nameof(Barbarian):
                case nameof(Paladin):
                    return Race.human;
                case nameof(Zombie):
                case nameof(Ghoul):
                case nameof(Vampire):
                    return Race.undead;
                default:
                    break;
            }
            throw new NotImplementedException();
        }

        public enum Race
        {
            elf,
            human,
            undead
        }
    }
}
