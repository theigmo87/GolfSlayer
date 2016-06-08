using GolfSlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public static class GlobalRoutines
{
    public static int SumScores(IEnumerable<Scorecard> scores)
    {
        int value = 0;
        foreach (Scorecard sc in scores.Where(x => x.ID > 0))
        {
            value += (sc.Value - sc.ParValue);
        }
        return value;
    }
}
