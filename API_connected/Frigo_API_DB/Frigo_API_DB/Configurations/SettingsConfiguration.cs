using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frigo_API_DB.Configurations
{
    public class SettingsConfiguration :  IEntityTypeConfiguration<Settings>
    {
        public SettingsConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.ToTable("SettingsTable");
            builder.Property(nameof(Settings.UserId)).IsRequired().HasColumnName("UserId");
            builder.Property(nameof(Settings.EmailToSendTo)).HasColumnName("EmailForNotifications");
            builder.Property(nameof(Settings.SendAmount)).HasColumnName("MinumumAmount");
            builder.Property(nameof(Settings.WantToRecieveNotification)).HasColumnName("RecieveNotification");
            builder.HasKey(nameof(Settings.UserId));
        }
    }


       
    
}
