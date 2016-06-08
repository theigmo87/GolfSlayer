using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Repositories.Entities;

namespace Repositories.Mappings
{
    public class ScoreMapping : EntityTypeConfiguration<Score>
    {
        public ScoreMapping()
        {
            ToTable("Score");
            HasKey(t => t.ID);
            Property(t => t.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.TeamID).IsRequired();
            Property(t => t.HoleID).IsRequired();
            Property(t => t.Value).IsRequired();
            Property(t => t.DateInserted).IsRequired();
            Property(t => t.DateUpdated).IsRequired();
        }
    }
}