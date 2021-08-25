using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SAmuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            //AddSamurais("Julie", "Sampson");
            //AddSamuraisByName("Shimada", "Okamoto", "Kikuchio", "Hayashida");
            //AddVariousTypes();
            //GetSamurais();
            //QueryFilters();
            //QueryAggregates();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            // RetrieveAndDeleteASamurai();
            //QueryAndUpdateBattles_Disconnected();
            Console.Write("Press any key...");
            Console.ReadKey();
        }

        private static void AddSamurai()
        {
            var samurai = new Samurai() { Name = "Sampon" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddSamurais(params string[] names)
        {
            foreach (var name in names)
            {
                var samurai = new Samurai() { Name = name };
                _context.Samurais.Add(samurai);
            }
           


            _context.SaveChanges();
        }
        private static void AddSamuraisByName(params string[] names)
        {
            foreach (var name in names)
            {
                var samurai = new Samurai() { Name = name };
                _context.Samurais.Add(samurai);
            }



            _context.SaveChanges();
        }
        private static void GetSamurais(string text)
        {
            var samuris = _context.Samurais
                .TagWith("ConsoleAppp.Prorame.GetSamurais method") //EntityFrameworkCore er method
                .ToList();

            Console.WriteLine($"{text}: Samurai count is {samuris.Count}");
            foreach (var samurai in samuris)
            {
                Console.WriteLine(samurai.Name);
            }

        }
        private static void AddVariousTypes()
        {
            _context.AddRange(new Samurai { Name = "Shimada" },
                              new Samurai { Name = "Okamoto" },
                              new Battle { Name = "Battle of Anegawa" },
                              new Battle { Name = "Battle of Nagashino" });

            //_context.Samurais.AddRange(
            //    new Samurai { Name = "Shimada" },
            //    new Samurai { Name = "Okamoto" });
            //_context.Battles.AddRange(
            //    new Battle { Name = "Battle of Anegawa" },
            //    new Battle { Name = "Battle of Nagashino" });

            _context.SaveChanges();

        }
        private static void QueryFilters()
        {
            var name = "Sampson";
            //var Samurais = _context.Samurais.Where(s => s.Name == name).ToList();
            var filter = "J%";
            var Samurais = _context.Samurais.Where(s => EF.Functions.Like(s.Name, filter)).ToList();

        }
        private static void QueryAggregates()
        {
            var name = "Sampson";
            //var samurai = _context.Samurais.FirstOrDefault(s => s.Name == name);
            var samurai = _context.Samurais.Find(2); //find is related to context not linq
        }
        private static void GetSamurais()
        {
            var samuris = _context.Samurais.ToList();

            Console.WriteLine($" Samurai count is {samuris.Count}");
            foreach (var samurai in samuris)
            {
                Console.WriteLine(samurai.Name);
            }

        } 

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(4).ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        public static void RetrieveAndDeleteASamurai()
        {
            var samurai = _context.Samurais.Find(10);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattles_Disconnected()
        {
            List<Battle> disconnectedBattles;

            using (var context = new SamuraiContext()) 
            {
                disconnectedBattles = _context.Battles.ToList();
            } // context is disposed

            disconnectedBattles.ForEach(b =>
            {
                b.StartDate = new DateTime(1570, 01, 01);
                b.Enddate = new DateTime(1570, 12, 1);
            });

            using (var context2 = new SamuraiContext())
            {
                context2.UpdateRange(disconnectedBattles); //disconnected hole update/updaterange method call korte hobe
                context2.SaveChanges();
            }

        }


    }
}
