using KiderApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KiderApp.DAL.Configurations
{
    public class DesignationConfiguration : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.HasMany(x => x.Agents).WithOne(x => x.Designation).HasForeignKey(x => x.DesignationId);
        }
    }
}
