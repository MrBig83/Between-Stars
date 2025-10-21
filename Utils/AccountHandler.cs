using Between_Stars.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Between_Stars.Utils
{
    public class AccountHandler
    {
        private List<Player> players;

        public AccountHandler(List<Player> players)
        {
            this.players = players;
        }

        // Huvudmeny för konto/logga in/ut/skapa nytt
        public Player ShowAccountMenu()
        {
            while (true)
            {
                Console.WriteLine("\n-- KONTO --");
                Console.WriteLine("1. Logga in");
                Console.WriteLine("2. Skapa nytt konto");
                Console.WriteLine("3. Avsluta");

                Console.Write("Val: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var player = LogInUser();
                        if (player != null)
                        {
                            Console.WriteLine($"Välkommen, {player.Name}!");
                            return player;
                        }
                        break;
                    case "2":
                        string playerFilePath = Path.Combine("Data", "players.json");
                        var newPlayer = CreateUser(); 
                        if (newPlayer != null)
                        {
                            players.Add(newPlayer);
                            // Spara nya spelaren (implementera SavePlayers i JsonHelper)
                            JsonHelper.SavePlayers(playerFilePath);
                            Console.WriteLine("Konto skapat! Logga in med nya användaren.");
                        }
                        break;
                    case "3":
                        Console.WriteLine("Hejdå!");
                        return null;
                    default:
                        Console.WriteLine("Ogiltigt val.");
                        break;
                }
            }
        }

        // Inloggning med 2FA
        public Player LogInUser()
        {
            Console.Write("Ange användarnamn: ");
            string userName = Console.ReadLine();

            var player = players.FirstOrDefault(p => p.Name == userName);

            if (player == null)
            {
                Console.WriteLine("Ingen sådan spelare hittades.");
                return null;
            }

            // Skicka ut (eller visa) 2FA-kod
            string code = Generate2FACode();
            Console.WriteLine($"[DEV-MODE] Din 2FA-kod är: {code}"); // Byt mot epost/sms IRL

            Console.Write("Ange 2FA-kod: ");
            string input = Console.ReadLine();

            if (input == code)
            {
                Console.WriteLine("Inloggning godkänd!");
                return player;
            }
            else
            {
                Console.WriteLine("Fel kod. Återgår till meny.");
                return null;
            }
        }

        // Skapa nytt konto
        public Player CreateUser()
        {
            Console.Write("Välj användarnamn: ");
            string name = Console.ReadLine();

            if (players.Any(p => p.Name == name))
            {
                Console.WriteLine("Det namnet är redan taget.");
                return null;
            }

            // Sätt eventuella startvärden (kan byggas ut med fler frågor)
            var player = new Player
            {
                Name = name,
                Credits = 1000,
                Reputation = 0,
                // ...defaultvärden för resten (Ship, Cargo osv)
            };
            Console.WriteLine("Konto skapat!");
            return player;
        }

        // Generera enkel 2FA-kod
        private string Generate2FACode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        // Här kan du bygga ut med LogOut, Byt lösenord, Ta bort konto osv!
    }
}
