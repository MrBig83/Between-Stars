using Between_Stars.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Between_Stars.Utils
{
    public class TravelHandler
    {
        public static void DisplayTravelDestinations(SessionData session)
        {
            bool displayTravelMenu = true;
            while (displayTravelMenu)
            {

            
            // Exempel: maxdistans är 30
            double maxdistans = 1030.0 * session.LoggedInPlayer.Tier;

            var current = session.CelestialBodies.First(cb => cb.Id == session.LoggedInPlayer.CurrentLocationId);

            var possibleDestinations = session.CelestialBodies
                .Where(cb => cb.Id != current.Id)
                .Select(cb => new
                {
                    Station = cb,
                    Dist = Math.Sqrt(
                        Math.Pow(cb.Position.X - current.Position.X, 2) +
                        Math.Pow(cb.Position.Y - current.Position.Y, 2) +
                        Math.Pow(cb.Position.Z - current.Position.Z, 2)
                    )
                })
                .Where(x => x.Dist <= maxdistans)
                .ToList();

            Console.WriteLine("Du kan resa till:");
            for (int i = 0; i < possibleDestinations.Count; i++)
            {
                var dest = possibleDestinations[i];
                Console.WriteLine($"{i + 1}. {dest.Station.Name} (avstånd: {dest.Dist:F1} AU)");
            }


            Console.WriteLine("Välj din destination (nummer) eller 'x' för att abryta.");
            string userInput = Console.ReadLine().ToLower();

            if (userInput == "x")
            {
                Console.Clear();
                displayTravelMenu = false;
                }
            else
            {
                int index;
                if (int.TryParse(userInput, out index))
                {
                    index -= 1;

                    if (index >= 0 && index < possibleDestinations.Count)
                    {


                        Console.Write($"Vill du resa till {possibleDestinations[index].Station.Name}? (J - Ja/N - Nej/X - Abryt) ");
                        string boarding = Console.ReadLine().ToLower();
                        if (boarding == "j")
                        {
                                Console.Clear();

                            Console.WriteLine("Trevlig resa!");
                                
                                double effektPerLevel = 0.85; // 15% snabbare per level
                                //restid = baseRestid * Math.Pow(effektPerLevel, engineLevel - 1);
                                double restidSek = possibleDestinations[index].Dist * Math.Pow(effektPerLevel, session.LoggedInPlayer.EngineLevel - 1); ; // T.ex. 25.4 sekunder
                                int width = 30;              // Bredd på progressbaren
                                DateTime start = DateTime.Now;
                                double kvar = restidSek;

                                while (kvar > 0)
                                {
                                    double gone = restidSek - kvar;
                                    int done = (int)(gone / restidSek * width);
                                    string bar = "[" + new string('#', done) + new string('-', width - done) + "]";
                                    Console.Write($"\rReser... {bar} {kvar:F1} sekunder kvar   ");
                                    Thread.Sleep(100);
                                    kvar = restidSek - (DateTime.Now - start).TotalSeconds;
                                    if (kvar < 0) kvar = 0;
                                }
                                Console.Clear();
                                MarketHandler.SaveGame(session);
                                Console.WriteLine($"\rDu har nu anlänt till {possibleDestinations[index].Station.Name}!                                   ");



                                session.LoggedInPlayer.CurrentLocationId = possibleDestinations[index].Station.Id;
                                //Console.WriteLine(session.LoggedInPlayer.CurrentLocationId);
                                displayTravelMenu = false;
                        }
                        else if (boarding == "n")
                        {
                            Console.WriteLine("Vi stannar en stund till/byter resmål, återgår till resemenyn");
                        }
                        else
                        {
                            Console.WriteLine("Avbryter och återgår till stationsmenyn");
                                displayTravelMenu = false;
                        }

                    }
                }
            }
        }
        }
    }
}
