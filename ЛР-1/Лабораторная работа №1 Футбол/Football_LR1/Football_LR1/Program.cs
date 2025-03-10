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
            List<Team> teams = new List<Team>
            {
        new Team("Торжок FC", 15, new List<Player> { new Player("Игрок1", 10), new Player("Игрок2", 5), new Player("Игрок3", 2) }),
        new Team("Спартак", 20, new List<Player> { new Player("Игрок4", 8), new Player("Игрок5", 7), new Player("Игрок6", 3) }),
        new Team("Локомотив", 18, new List<Player> { new Player("Игрок7", 12), new Player("Игрок8", 5), new Player("Игрок9", 1) }),
        new Team("Зенит", 22, new List<Player> { new Player("Игрок10", 9), new Player("Игрок11", 6), new Player("Игрок12", 4) }),
        new Team("Динамо", 10, new List<Player> { new Player("Игрок13", 1), new Player("Игрок14", 0), new Player("Игрок15", 0) }),
        new Team("Кубань", 12, new List<Player> { new Player("Игрок16", 3), new Player("Игрок17", 5), new Player("Игрок18", 3) })
            };

            
            Console.WriteLine("Введите название команды:");
            string teamName = Console.ReadLine();

            Team selectedTeam = null;
            foreach (var team in teams)
            {
                if (team.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase))
                {
                    selectedTeam = team;
                    break;
                }
            }

            if (selectedTeam != null)
            {
                List<Player> topScorers = new List<Player>();

                foreach (var player in selectedTeam.Players)
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

                    // Определяем топ 3 бомбардира
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

                // Выводим топ 3 бомбардира
                Console.WriteLine($"Топ 3 бомбардира команды {selectedTeam.Name}:");
                foreach (var scorer in topScorers)
                {
                    Console.WriteLine($"{scorer.Name} - {scorer.Goals} забитых мячей");
                }

                // Находим максимальные баллы
                int maxPoints = 0;
                foreach (var team in teams)
                {
                    if (team.Points > maxPoints)
                    {
                        maxPoints = team.Points;
                    }
                }

                // Разница в баллах
                int pointDifference = maxPoints - selectedTeam.Points;
                Console.WriteLine($"Команда {selectedTeam.Name} отстает от первого места на {pointDifference} баллов.");
            }
            else
            {
                Console.WriteLine("Команда не найдена.");
            }
        }
    }
}
