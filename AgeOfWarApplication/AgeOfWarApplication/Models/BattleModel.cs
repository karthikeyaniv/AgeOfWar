namespace AgeOfWarApplication.Models
{
    public class BattleInput
    {
        public string MyPlatoons { get; set; }
        public string OpponentPlatoons { get; set; }
    }

    public class Platoon
    {
        public string Class { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Class}#{Count}";
        }
    }

    public class BattleWinningPlatoon
    {
        public int wins { get; set; }
        public string platoon { get; set; }
    }

    public class BattlePlatoonAdvantage
    {
        public static readonly Dictionary<string, List<string>> classAdvantages = new Dictionary<string, List<string>>()
        {
            { "Militia", new List<string>{ "Spearmen", "LightCavalry" } },
            { "Spearmen", new List<string>{ "LightCavalry", "HeavyCavalry" } },
            { "LightCavalry", new List<string>{ "FootArcher", "CavalryArcher" } },
            { "HeavyCavalry", new List<string>{ "Militia", "FootArcher", "LightCavalry" } },
            { "CavalryArcher", new List<string>{ "Spearmen", "HeavyCavalry" } },
            { "FootArcher", new List<string>{ "Militia", "CavalryArcher" } }
        };
    }
}
