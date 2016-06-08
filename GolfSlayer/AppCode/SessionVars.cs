using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

[Serializable]
public sealed class SessionVars
{
    #region Singleton
    private const string SESSION_SINGLETON_NAME = "Singleton_502E69E5-668B-E011-951F-00155DF26207";
    private SessionVars() { }
    public static SessionVars Item
    {
        get
        {
            if (HttpContext.Current.Session[SESSION_SINGLETON_NAME] == null)
            {
                HttpContext.Current.Session[SESSION_SINGLETON_NAME] = new SessionVars();
            }
            return HttpContext.Current.Session[SESSION_SINGLETON_NAME] as SessionVars;
        }
    }
    #endregion
    public Team Team { get; set; }

}