using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Repositories;
using Repositories.Database;
using Repositories.Entities;
using GolfSlayer.Models;
using GolfSlayer.Models.Common;

namespace GolfSlayer.Hubs
{

    [HubName("scoresHub")]
    public class ScoresHub : Hub
    {
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();

        public void Connect(int teamID){
            var id = Context.ConnectionId;

            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, TeamID = teamID });
                Models.ScorecardViewModel scvm = getTeamScore(teamID);
                Clients.Caller.updateScores(scvm);
                Models.LeaderboardViewModel lbvm = GetLeaderboardViewModel();
                Clients.Caller.updateLeaderboard(lbvm);
            }
        }

        private Models.ScorecardViewModel getTeamScore(int teamID)
        {
            ScoreRepository scoreRepo = new ScoreRepository();
            IEnumerable<Score> teamScores = scoreRepo.GetAll().Where(x => x.TeamID == teamID).OrderBy(x => x.HoleID);
            HoleRepository holeRepo = new HoleRepository();
            ClosestToPinRepository closestRepo = new ClosestToPinRepository();
            IEnumerable<Hole> holeList = holeRepo.GetAll();
            List<Models.Scorecard> scoresWithHoleInfo = new List<Models.Scorecard>();
            int counter = 0;
            foreach (var hole in holeList.Where(x => x.SegmentID == 1 || x.SegmentID == 2))
            {               

                int holeNumber = counter < 9 ? hole.Number : 9 + hole.Number;
                counter++;
                Models.Scorecard sc = new Models.Scorecard();
                sc.HoleID = hole.ID;
                sc.HoleNumber = holeNumber;
                sc.ParValue = hole.Par;
                sc.ParValueDisplay = String.Format("Par {0}", hole.Par);
                sc.TeamID = teamID;
                Score score = teamScores.Where(x => x.HoleID == hole.ID).SingleOrDefault();
                if (score != null)
                {
                    sc.Value = score.Value;
                    sc.ID = score.ID;
                    sc.DateInserted = score.DateInserted;
                    sc.DateUpdated = score.DateUpdated;
                }
                if (hole.Par == 3)
                {
                    ClosestToPin closest = closestRepo.GetAll().OrderBy(d => d.Distance).Where(h => h.HoleID == hole.ID).FirstOrDefault();

                    if (closest != null)
                    {                        
                        sc.Closest.ClosestName = closest.Name;
                        sc.Closest.ClosestDistance = closest.Distance;
                    }
                }

                scoresWithHoleInfo.Add(sc);
            }
            Models.ScorecardViewModel vm = new Models.ScorecardViewModel() { scores = scoresWithHoleInfo };
            return vm;
        }

        private Models.LeaderboardViewModel GetLeaderboardViewModel()
        {
            TeamRepository teamRepo = new TeamRepository();
            List<TeamScore> teamScores = new List<TeamScore>();
            List<Team> teams = teamRepo.GetAll().Where(i => !i.IsAdmin).ToList();

            foreach (var team in teams)
            {
                List<Scorecard> scores = getTeamScore(team.ID).scores.ToList();
                int summedScore = GlobalRoutines.SumScores(scores);
                int thru = scores.Where(s => s.Value > 0).Count();
                teamScores.Add(new TeamScore
                {
                    TeamName = team.Name,
                    Score = summedScore,
                    ScoreDisplay = summedScore == 0 ? "Even" : summedScore.ToString(),
                    Thru = thru > 0 ? thru.ToString() : "-"
                });
            }


            Models.LeaderboardViewModel vm = new Models.LeaderboardViewModel()
            {
                TeamScores = teamScores.OrderBy(s => s.Score)
            };

            return vm;

        }

        public void SendTeamScoreUpdate(int teamID)
        {
            Models.ScorecardViewModel vm = getTeamScore(teamID);
            List<string> teamClients = ConnectedUsers.Where(x => x.TeamID == teamID).Select(x => x.ConnectionId).ToList();
            Clients.Clients(teamClients).updateScores(vm);
        }

        public void GetLeaderboardUpdate()
        {
            Clients.Caller.updateLeaderboard(GetLeaderboardViewModel());
        }

        public void SendLeaderboardUpdate()
        {
            Clients.All.updateLeaderboard(GetLeaderboardViewModel());
        }

        public void saveScore(Models.Scorecard scorecard)
        {
            ScoreRepository repo = new ScoreRepository();
            Score score = new Score()
            {
                HoleID = scorecard.HoleID,
                Value = scorecard.Value,
                TeamID = scorecard.TeamID,
                DateUpdated = DateTime.Now,
                DateInserted = scorecard.ID == 0 ? DateTime.Now : scorecard.DateInserted,
                ID = scorecard.ID
            };
            repo.InsertOrUpdate(score);
            repo.Save();
            SendTeamScoreUpdate(score.TeamID);
            SendLeaderboardUpdate();
        }

        public void deleteScore(Models.Scorecard scorecard)
        {
            ScoreRepository repo = new ScoreRepository();
            repo.HardDelete(scorecard.ID);
            repo.Save();
            SendTeamScoreUpdate(scorecard.TeamID);
            SendLeaderboardUpdate();

        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                ConnectedUsers.Remove(item);
            }

            return base.OnDisconnected(stopCalled);
        }

    }
}