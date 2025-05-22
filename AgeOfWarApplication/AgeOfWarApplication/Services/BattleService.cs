#region usings
using AgeOfWarApplication.Models;
#endregion usings

namespace AgeOfWarApplication.Services
{
    public class BattleService
    {
        // This method is used to find out the winning platoon order to defeat the enemy platoon
        public string PlanBattle(BattleInput input)
        {
            var myPlatoons = ParseInput(input.MyPlatoons);
            var enemyPlatoons = ParseInput(input.OpponentPlatoons);

            if (myPlatoons.Count != enemyPlatoons.Count)
                return "Both sides must have equal number of platoons.";

            int totalPlatoonCount = enemyPlatoons.Count();

            // Number of minimum wins required to win the battle
            int thresholdWin = (int)Math.Ceiling(totalPlatoonCount / 2.0);

            if (totalPlatoonCount % 2 == 0)
            {
                thresholdWin += 1;
            }

            // Get all possible combination(s)
            var platoonCombination = GetPermutationsQueue(myPlatoons, myPlatoons.Count);

            List<BattleWinningPlatoon> objBattleWinningPlatoonList = new List<BattleWinningPlatoon>();
            BattleWinningPlatoon objBattleWinningPlatoon = new BattleWinningPlatoon();

            string winResult = "";

            foreach (var permutation in platoonCombination)
            {
                int wins = 0;
                for (int i = 0; i < permutation.Count; i++)
                {
                    if (BattleResult(permutation[i], enemyPlatoons[i]) == 1)
                        wins++;
                }

                // If the winning count matches or exceeds the threshold winning count, adding the winning combination into the list.
                if (wins >= thresholdWin)
                {
                    objBattleWinningPlatoon.wins = wins;
                    objBattleWinningPlatoon.platoon = string.Join(";", permutation.Select(p => p.ToString()));

                    objBattleWinningPlatoonList.Add(objBattleWinningPlatoon);
                }
            }

            // If there is no possible combination to win return the message stating no chance of winning else return the winning combination
            if (objBattleWinningPlatoonList.Count == 0)
            {
                winResult = "There is no chance of winning";
            }
            else
            {
                winResult = objBattleWinningPlatoonList.OrderByDescending(x => x.wins).FirstOrDefault().platoon;
            }

            return winResult;
        }

        // This method is used to convert the user input to a model with platoon class and the number of soldiers
        private static List<Platoon> ParseInput(string input)
        {
            return input.Split(';').Select(p =>
            {
                var parts = p.Split('#');
                return new Platoon { Class = parts[0], Count = int.Parse(parts[1]) };
            }).ToList();

        }

        // This method is used to find whether the platoon class has advantage over the opponent platoon 
        private static bool HasAdvantage(string attacker, string defender)
        {
            return BattlePlatoonAdvantage.classAdvantages.ContainsKey(attacker) && BattlePlatoonAdvantage.classAdvantages[attacker].Contains(defender);
        }

        // This method is used to find out whether the platoon is able to win, draw or lose the opponent platoon based on the platoon advantage and number of soldiers
        private static int BattleResult(Platoon my, Platoon enemy)
        {
            double myStrength = my.Count;
            double enemyStrength = enemy.Count;

            if (HasAdvantage(my.Class, enemy.Class))
                myStrength *= 2;
            if (HasAdvantage(enemy.Class, my.Class))
                enemyStrength *= 2;

            if (myStrength > enemyStrength) return 1;
            else if (myStrength == enemyStrength) return 0;
            else return -1;
        }

        // This method is used to get all possible combinations of a platoon depending upon the count of classes
        private static IEnumerable<List<T>> GetPermutationsQueue<T>(List<T> list, int length)
        {
            var result = new List<List<T>>();
            var queue = new Queue<(List<T> perm, List<T> remaining)>();

            queue.Enqueue((new List<T>(), new List<T>(list)));

            while (queue.Count > 0)
            {
                var (perm, remaining) = queue.Dequeue();

                if (perm.Count == length)
                {
                    result.Add(perm);
                }
                else
                {
                    for (int i = 0; i < remaining.Count; i++)
                    {
                        var nextPerm = new List<T>(perm) { remaining[i] };
                        var nextRemaining = new List<T>(remaining);
                        nextRemaining.RemoveAt(i);
                        queue.Enqueue((nextPerm, nextRemaining));
                    }
                }
            }

            return result;
        }
    }
}
