using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GolfSlayer.Models
{
    public class ScorecardViewModel 
    {
        public IEnumerable<Scorecard> scores { get; set; }
        public string totalScore
        {
            get
            {
               int value = GlobalRoutines.SumScores(scores);
               return value == 0 ? "Even" : value.ToString() ;
            }
        }
    }
    public class Scorecard : Repositories.Entities.Score
    {
        public Scorecard()
        {
            ID = 0;
            Value = 0;
            DateInserted = DateTime.Now;
            DateUpdated = DateTime.Now;
        }
        public int ParValue { get; set; }
        public string ParValueDisplay { get; set; }
        public int HoleNumber { get; set; }
        public ClosestViewModel Closest = new ClosestViewModel();
    }

    public class ClosestViewModel
    {
        public string ClosestName { get; set; }
        public decimal ClosestDistance { get; set; }
        public string ClosestDistanceDisplay 
        { 
            get
            {
                int feet = Convert.ToInt32(ClosestDistance / 12);
                decimal inches = ClosestDistance % 12.0m;
                return String.Format("{0}\' {1}\"", feet, inches);
            } 
        }
        public bool HasClosest
        {
            get
            {
                if (String.IsNullOrEmpty(ClosestName) || String.IsNullOrEmpty(ClosestDistanceDisplay))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}