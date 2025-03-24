using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Football_LR1
{
    public class Player
    {
        public string Name { get; set; }
        public int Goals { get; set; }

        public Player(string name, int goals)
        {
            Name = name;
            Goals = goals;
        }
    }

    public class Team
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public List<Player> Players { get; set; }

        public Team(string name, int points, List<Player> players)
        {
            Name = name;
            Points = points;
            Players = players;
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            List<Team> teams = GetTeams(); ///получаем список команд

            Console.WriteLine("Введите название команды:");
            string teamName = Console.ReadLine();

            Team selectedTeam = FindTeam(teams, teamName);

            if (selectedTeam != null)
            {
                List<Player> topScorers = GetTopScorers(selectedTeam.Players); ///определяем топ 3 бомбардира
                Console.WriteLine($"Топ 3 бомбардира команды {selectedTeam.Name}:");
                PrintTopScorers(topScorers);
                int maxPoints = GetMaxPoints(teams);  /// находим максимальную суммы очков среди всех команд
                int pointDifference = maxPoints - selectedTeam.Points;  /// вычисляем разницу в очках между командой и лидером
                Console.WriteLine($"Команда {selectedTeam.Name} отстает от первого места на {pointDifference} баллов.");
            }
            else
            {
                Console.WriteLine("Команда не найдена.");
            }
        }

        private static List<Team> GetTeams()
        {
            return new List<Team>
            {
            new Team("Торжок FC", 15, new List<Player> { new Player("Игрок1", 10), new Player("Игрок2", 5), new Player("Игрок3", 2) }),
            new Team("Спартак", 20, new List<Player> { new Player("Игрок4", 8), new Player("Игрок5", 7), new Player("Игрок6", 3) }),
            new Team("Локомотив", 18, new List<Player> { new Player("Игрок7", 12), new Player("Игрок8", 5), new Player("Игрок9", 1) }),
            new Team("Зенит", 22, new List<Player> { new Player("Игрок10", 9), new Player("Игрок11", 6), new Player("Игрок12", 4) }),
            new Team("Динамо", 10, new List<Player> { new Player("Игрок13", 1), new Player("Игрок14", 0), new Player("Игрок15", 0) }),
            new Team("Кубань", 12, new List<Player> { new Player("Игрок16", 3), new Player("Игрок17", 5), new Player("Игрок18", 3) })
            };
        }

        private static Team FindTeam(List<Team> teams, string teamName)
        {
            foreach (var team in teams)
            {
                if (team.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                {
                    return team;
                }
            }
            return null;
        }

        private static List<Player> GetTopScorers(List<Player> players)
        {
            var topScorers = new List<Player>();

            foreach (var player in players)
            {
                if (topScorers.Count < 3)
                {
                    topScorers.Add(player);
                }
                else
                {
                    for (int i = 0; i < topScorers.Count; i++)
                    {
                        if (player.Goals > topScorers[i].Goals)
                        {
                            topScorers[i] = player;
                            break;
                        }
                    }
                }

                if (topScorers.Count > 3)
                {
                    Player minPlayer = topScorers[0];
                    int minIndex = 0;

                    for (int i = 1; i < topScorers.Count; i++)
                    {
                        if (topScorers[i].Goals < minPlayer.Goals)
                        {
                            minPlayer = topScorers[i];
                            minIndex = i;
                        }
                    }
                    topScorers.RemoveAt(minIndex);
                }
            }
            return topScorers;
        }

        private static void PrintTopScorers(List<Player> topScorers)
        {
            foreach (var scorer in topScorers)
            {
                Console.WriteLine($"{scorer.Name} - {scorer.Goals} забитых мячей");
            }
        }

        private static int GetMaxPoints(List<Team> teams)
        {
            int maxPoints = 0;
            foreach (var team in teams)
            {
                if (team.Points > maxPoints)
                {
                    maxPoints = team.Points;
                }
            }
            return maxPoints;
        }
    }
}
