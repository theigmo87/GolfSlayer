using Repositories;
using Repositories.Database;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GolfSlayer.Controllers
{
    public abstract class BaseController : Controller
    {
        private DataContext dataContext = new DataContext();
        private IClosestToPinRepository closestToPinRepository;
        private ICourseRepository courseRepository;
        private ISegmentRepository segmentRepository;
        private IHoleRepository holeRepository;
        private IMessageRepository messageRepository;
        private IScoreRepository scoreRepository;
        private ITeamRepository teamRepository;
        private static string TeamCookieName = "current_team";
        private static string TeamExpiresName = "current_expires";

        public BaseController()
        {
            this.closestToPinRepository = new ClosestToPinRepository(dataContext);
            this.courseRepository = new CourseRepository(dataContext);
            this.segmentRepository = new SegmentRepository(dataContext);
            this.holeRepository = new HoleRepository(dataContext);
            this.messageRepository = new MessageRepository(dataContext);
            this.scoreRepository = new ScoreRepository(dataContext);
            this.teamRepository = new TeamRepository(dataContext);
        }

        public Team CurrentTeam
        {
            get
            {
                if (SessionVars.Item.Team == null)
                {
                    //try to get out of cookie
                    Team team = null;
                    HttpCookie c = Request.Cookies.Get(TeamCookieName);
                    HttpCookie expires = Request.Cookies.Get(TeamExpiresName);
                    if (c != null && expires != null &&  Convert.ToDateTime(expires.Value) > DateTime.Now)
                    {
                        int teamID = Convert.ToInt32(c.Value);
                        team = teamRepository.GetAll().Where(i => i.ID == teamID).FirstOrDefault();
                    }
                    
                    SessionVars.Item.Team = team;
                    return team;
                }
                else
                {
                    return SessionVars.Item.Team;
                }                
            }
        }

        public bool TryLogin(String ID){
            Team team = new Team();
            team = teamRepository.GetAll().Where(p => p.Pin == ID).FirstOrDefault();

            if (team != null)
            {
                //Add to session and a cookie                
                SessionVars.Item.Team = team;
                HttpCookie cookie = new HttpCookie(TeamCookieName);
                HttpCookie expiresCookie = new HttpCookie(TeamExpiresName);
                cookie.Expires = DateTime.Now.AddHours(6);
                expiresCookie.Expires = cookie.Expires;
                cookie.Value = team.ID.ToString();
                expiresCookie.Value = cookie.Expires.ToString();
                HttpContext.Response.Cookies.Remove(TeamCookieName);
                HttpContext.Response.Cookies.Remove(TeamExpiresName);
                HttpContext.Response.SetCookie(cookie);
                HttpContext.Response.SetCookie(expiresCookie);

                return true;
                // Goto Spa page
            }
            return false;
        }

        public void Logout()
        {
            HttpCookie cookie = new HttpCookie(TeamCookieName);
            HttpCookie expiresCookie = new HttpCookie(TeamExpiresName);
            cookie.Expires = DateTime.Now.AddDays(-1);
            cookie.Value = CurrentTeam.ID.ToString();
            expiresCookie.Expires = DateTime.Now.AddDays(-1);
            expiresCookie.Value = DateTime.Now.AddDays(-1).ToShortDateString();
            HttpContext.Response.Cookies.Remove(TeamCookieName);
            HttpContext.Response.Cookies.Remove(TeamExpiresName);
            HttpContext.Response.SetCookie(cookie);
            HttpContext.Response.SetCookie(expiresCookie);

            SessionVars.Item.Team = null;
        }
    }
}