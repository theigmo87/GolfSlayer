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
    public class SettingsMapping : EntityTypeConfiguration<Settings>
    {
        public SettingsMapping()
        {
            ToTable("Settings");
            HasKey(t => t.Key);
            Property(t => t.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Key).IsUnicode(false).IsRequired();
            Property(t => t.Value).IsUnicode(false).IsRequired();
            Property(t => t.DateInserted).IsRequired();
            Property(t => t.DateUpdated).IsRequired();
        }
    }
}