using KiderApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiderApp.DAL.Configurations
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(20);
            builder.Property(x=>x.ImgUrl).IsRequired().HasMaxLength(100);
            builder.HasOne(x=>x.Designation).WithMany(x => x.Agents).HasForeignKey(x=>x.DesignationId);
        }
    }
}
