using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Football_LR_1.Team;

namespace Football_LR_1
{
    struct Footballer
    {
        public string Name {get; set;}
        public int Goals {get; set;}

        public Footballer(string name, int goals)
        {
            Name = name;
            Goals = goals;
        }
    }

    struct Team
    {
        public string Name {get; set;}
        public int Points {get; set;}
        public List<Footballer> Footballers {get; set;}

        public Team(string name, int points, List<Footballer> footballers)
        {
            Name=name;
            Points = points;
            Footballers = footballers;
        }

        public class Championship
        {
            private List<Team> teams;

            public Championship(List<Team> teams)
            {
                this.teams = teams;
            }

            public void ShowTopFootballers(string teamName)
            {
                var team = teams.FirstOrDefault(t => t.Name.Equals(teamName, StringComparison.OrdinalIgnoreCase));
                if (team.Equals(default(Team)))
                {
                    Console.WriteLine("Ошибка! Такой команды нет.");
                    return;
                }


                var topFootballers = team.Footballers.OrderByDescending(f => f.Goals).Take(3);


                Console.WriteLine($"Топ 3 бомбардира команды {team.Name}: ");
                foreach(var topfootballer in topFootballers)
                {
                    Console.WriteLine($"{topfootballer.Name}: {topfootballer.Goals} голов");
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var teams = new List<Team>
            {
                new Team("Команда_А", 31,
                new List<Footballer> { new Footballer("Игрок_1", 7), new Footballer("Игрок_2", 2), new Footballer("Игрок_3", 3), new Footballer("Игрок_4", 10), new Footballer("Игрок_5", 14) }),
            new Team("Команда_Б", 53,
                new List<Footballer> { new Footballer("Игрок_1", 6), new Footballer("Игрок_2", 9), new Footballer("Игрок_3", 3), new Footballer("Игрок_4", 16), new Footballer("Игрок_5", 5) }),
            new Team("Команда_В", 41,
                new List<Footballer> { new Footballer("Игрок_1", 1), new Footballer("Игрок_2", 4), new Footballer("Игрок_3", 11), new Footballer("Игрок_4", 8), new Footballer("Игрок_5", 19) }),
            };
            Championship championship = new Championship(teams);
            Console.WriteLine("Введите название команды, чтобы посмотреть информацию о бомбардирах: ");
            string teamName = Console.ReadLine();
            championship.ShowTopFootballers(teamName);
            Console.ReadKey();
        }
    }
}
