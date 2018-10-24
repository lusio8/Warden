using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WardenCore.Models
{
    public class Rules
    {

        public Rules(int r, string n, int p, int rnd){
            RulesId = r;
            Name = n;
            Partecipants = p;
            Round = rnd;

            }
        public int RulesId { get; set; }

    
        public string Name { get; set; }


        public int Partecipants { get; set; }

        public int Round { get; set; }
    }
}