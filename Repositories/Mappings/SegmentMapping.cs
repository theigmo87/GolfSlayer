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
    public class SegmentMapping : EntityTypeConfiguration<Segment>
    {
        public SegmentMapping()
        {
            ToTable("Segment");
            HasKey(t => t.ID);
            Property(t => t.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.CourseID).IsRequired();
            Property(t => t.Name).IsRequired();
            Property(t => t.DateInserted).IsRequired();
            Property(t => t.DateUpdated).IsRequired();
        }
    }
}