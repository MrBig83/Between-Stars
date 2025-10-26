using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Classes
{
    public class Mission
    {
        public string MissionId { get; set; }
        public int PlayerId { get; set; } // ====== Stryk PlayerID ifrån Mission. Kör på MissionID på player istället. 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Commodity { get; set; }
        public int Amount { get; set; }
        public string From_station { get; set; }
        public string To_station { get; set; }
        public int Reward_cr { get; set; }
        public int Reward_reputation { get; set; }
        public string Status { get; set; }

        public Mission() { }

        public Mission(string missionId, int playerId, string title, string description, string commodity, int amount, string from_station, string to_station, int reward_cr, int reward_reputation, string status)
        {
            MissionId = missionId;
            PlayerId = playerId;
            Title = title;
            Description = description;
            Commodity = commodity;
            Amount = amount;
            From_station = from_station;
            To_station = to_station;
            Reward_cr = reward_cr;
            Reward_reputation = reward_reputation;
            Status = status;
        }
    }
}
