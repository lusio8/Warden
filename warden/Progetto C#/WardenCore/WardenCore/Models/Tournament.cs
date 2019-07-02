using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WardenCore.Models
{
    public class Tournament
    {
        public string name { get; set; } /* name of the event */
        public DateTime date { get; set; } /* date of the event */
        public int gmid { get; set; } /* game mode id */

    }
}