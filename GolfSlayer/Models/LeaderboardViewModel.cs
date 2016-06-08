using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfSlayer.Models
{
    public class LeaderboardViewModel
    {
        public IEnumerable<TeamScore> TeamScores { get; set; }
    }

    public class TeamScore
    {        
        public string TeamName { get; set; }
        public int Score { get; set; }
        public string ScoreDisplay { get; set; }
        public string Thru { get; set; }
    }
}