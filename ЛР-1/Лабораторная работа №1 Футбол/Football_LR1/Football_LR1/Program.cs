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
                CalcPointsDifference(teams, selectedTeam);
                PrintPointsDifference(teams, selectedTeam);
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
            // Создаем копию списка, чтобы не менять оригинальный
            List<Player> sortedPlayers = new List<Player>(players);

            // Сортируем по убыванию количества голов
            for (int i = 0; i < sortedPlayers.Count - 1; i++)
            {
                for (int j = i + 1; j < sortedPlayers.Count; j++)
                {
                    if (sortedPlayers[j].Goals > sortedPlayers[i].Goals)
                    {
                        Player temp = sortedPlayers[i];
                        sortedPlayers[i] = sortedPlayers[j];
                        sortedPlayers[j] = temp;
                    }
                }
            }

            // Возвращаем первые 3 элемента или меньше, если игроков меньше 3
            int count = Math.Min(3, sortedPlayers.Count);
            return sortedPlayers.GetRange(0, count);
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

        private static int CalcPointsDifference(List<Team> teams, Team selectedTeam)
        {
           
            int maxPoints = GetMaxPoints(teams); // Находим максимальную сумму очков
            int pointDifference = maxPoints - selectedTeam.Points; // Вычисляем разницу
            return pointDifference; // Возвращаем разницу вместо вывода
        }

        private static void PrintPointsDifference(List<Team> teams, Team selectedTeam)
        {
            int difference = CalcPointsDifference(teams, selectedTeam);
            Console.WriteLine($"Команда {selectedTeam.Name} отстаёт на {difference} баллов.");
        }
    }
}
