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
    public class HoleMapping : EntityTypeConfiguration<Hole>
    {
        public HoleMapping()
        {
            ToTable("Hole");
            HasKey(t => t.ID);
            Property(t => t.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.SegmentID).IsRequired();
            Property(t => t.Distance).IsRequired();
            Property(t => t.Number).IsRequired();
            Property(t => t.Par).IsRequired();
            Property(t => t.DateInserted).IsRequired();
            Property(t => t.DateUpdated).IsRequired();
        }
    }
}