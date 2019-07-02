using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WardenCore.Models
{
    public class Score
    {
        public int gameId { get; set; }
        public int player1 { get; set; } /* id of the first player */
        public int player2 { get; set; } /* id of the second player */
        public int winner { get; set; } /* id of the winner */
        public int delta { get; set; } /* delta score */

    }
}