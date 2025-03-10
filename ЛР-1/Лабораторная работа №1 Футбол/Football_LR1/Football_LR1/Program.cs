using System;
using System.Collections.Generic;
using System.Linq;
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


        }
    }
}
