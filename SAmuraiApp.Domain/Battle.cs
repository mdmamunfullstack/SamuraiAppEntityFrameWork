using System;
using System.Collections.Generic;

namespace SAmuraiApp.Domain
{
    public class Battle
    {
        public int BattleId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Enddate { get; set; }
        public List<Samurai> Samurais { get; set; } = new List<Samurai>();
    }
}