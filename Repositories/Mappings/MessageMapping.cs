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
    public class MessageMapping : EntityTypeConfiguration<Message>
    {
        public MessageMapping()
        {
            ToTable("Message");
            HasKey(t => t.ID);
            Property(t => t.ID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Body).IsRequired();
            Property(t => t.From).IsRequired();
            Property(t => t.To).IsOptional();
            Property(t => t.DateSent).IsRequired();            
        }
    }
}