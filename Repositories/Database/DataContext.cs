using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Repositories.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using Repositories.Mappings;

namespace Repositories.Database
{
    public interface IDataContext
    {
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        void ExecuteCommand(string command, params object[] parameters);
    }

    public class DataContext : DbContext, IDataContext
    {
        public IDbSet<ClosestToPin> ClosestToPins { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<Hole> Holes { get; set; }
        public IDbSet<Message> Messages { get; set; }
        public IDbSet<Score> Scores { get; set; }
        public IDbSet<Settings> Settings { get; set; }
        public IDbSet<Team> Teams { get; set; }
        public IDbSet<Segment> Segments { get; set; }
         

        public DataContext()
            : base("sqlserver")
        {
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ClosestToPinMapping());
            modelBuilder.Configurations.Add(new CourseMapping());
            modelBuilder.Configurations.Add(new HoleMapping());
            modelBuilder.Configurations.Add(new MessageMapping());
            modelBuilder.Configurations.Add(new ScoreMapping());
            modelBuilder.Configurations.Add(new SettingsMapping());
            modelBuilder.Configurations.Add(new TeamMapping());
            modelBuilder.Configurations.Add(new SegmentMapping());

            base.OnModelCreating(modelBuilder);
        }

        public void ExecuteCommand(string command, params object[] parameters)
        {
            base.Database.ExecuteSqlCommand(command, parameters);
        }

    }
}
