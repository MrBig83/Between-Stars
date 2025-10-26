using Between_Stars.Classes;
using Between_Stars.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Between_Stars.Utils
{
    public class MissionHandler
    {

        public static void AddMission(SessionData session, Mission cleanReply)
        {
            Console.WriteLine("Uppdraget sparas i din Uppdragslog");
            //Ta reda på upptagna MissionIDs
            string missionNumber = Convert.ToString(session.Missions.Count());
            session.Missions.Add(new Mission
            {
                MissionId = "M-" + missionNumber,
                PlayerId = session.LoggedInPlayer.Id,
                Title = cleanReply.Title,
                Description = cleanReply.Description,
                Commodity = cleanReply.Commodity,
                Amount = cleanReply.Amount,
                From_station = cleanReply.From_station,
                To_station = cleanReply.To_station,
                Reward_cr = cleanReply.Reward_cr,
                Reward_reputation = cleanReply.Reward_reputation,
                Status = "Active"
            });
            JsonHelper.SaveMissions(session.Missions);
        }

        public static void ShowMissions(SessionData session)
        {
            var playerMissions = session.Missions
                .Where(m => m.PlayerId == session.LoggedInPlayer.Id && m.Status == "Active")
                .ToList();

            if(playerMissions.Count == 0)
            {
                Console.WriteLine("Du har inga aktiva uppdrag just nu");
                return;
            }
            Console.WriteLine("Aktiva uppdrag:");
            playerMissions.ForEach(m => Console.WriteLine($"- {m.Title} " +
                $"({m.From_station} -> {m.To_station})\n" +
                $"          Material: {m.Amount} x {m.Commodity}\n" +
                $"          Belöning: {m.Reward_cr}cr"));

            
        }

        public static void CompleteMission(SessionData session)
        {
            var currentHangar = session.CelestialBodies
                .FirstOrDefault(c => c.Id == session.LoggedInPlayer.CurrentLocationId);
            if(currentHangar == null)
            {
                Console.WriteLine("Du kan inte lämna in uppdrag ifrån din nuvarande position");
                return;
            }


            var completableMissions = session.Missions
                .Where(m => m.PlayerId == session.LoggedInPlayer.Id &&
                        m.To_station == currentHangar.Name &&
                        m.Status == "Active")
                .ToList();

            if(completableMissions.Count == 0)
            {
                Console.WriteLine("Du har inga uppdrag att slutföra på denna station");
                return;
            }


            Console.WriteLine("Följande uppdrag kan slutföras här:");
            foreach (var mission in completableMissions)
            {
                var cargoItem = session.LoggedInPlayer.Cargo.FirstOrDefault(c => c.Name == mission.Commodity);

                if (cargoItem != null && cargoItem.Amount >= mission.Amount)
                {
                    // Dra av varorna
                    cargoItem.Amount -= mission.Amount;
                    if (cargoItem.Amount == 0)
                        session.LoggedInPlayer.Cargo.Remove(cargoItem);

                    // Ge belöning
                    session.LoggedInPlayer.Credits += mission.Reward_cr;
                    session.LoggedInPlayer.Reputation += mission.Reward_reputation;

                    session.LoggedInPlayer.MissionId.Remove(mission.MissionId);
                    
                    mission.Status = "Completed";
                    
                    Console.WriteLine($"{mission.Title} levererat!");
                    Console.WriteLine($"+{mission.Reward_cr} cr | +{mission.Reward_reputation} rykte");
                    MarketHandler.SaveGame(session);
                }
                else
                {
                    Console.WriteLine($"{mission.Title} kunde inte levereras (saknar {mission.Commodity} x{mission.Amount})");
                }
            }
        }
    }
}


