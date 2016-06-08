using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GolfSlayer.AppCode
{
    public class AuthorizeTeam : AuthorizeAttribute
    {
        public string AccessLevel { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            HttpCookie c = httpContext.Request.Cookies.Get("current_team");
            HttpCookie expires = httpContext.Request.Cookies.Get("current_expires");

            if (SessionVars.Item.Team == null)
            {
                if (c != null && expires != null && Convert.ToDateTime(expires.Value) > DateTime.Now && c.Value != "")
                {
                    TeamRepository teamRepo = new TeamRepository();
                    int TeamID = 0;
                    int.TryParse(c.Value, out TeamID);
                    SessionVars.Item.Team = teamRepo.GetByID(TeamID);
                    teamRepo.Dispose();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
            
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Home",
                                action = "Index"
                            })
                        );
        }
    }
}