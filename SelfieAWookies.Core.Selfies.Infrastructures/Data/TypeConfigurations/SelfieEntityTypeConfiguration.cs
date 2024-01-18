using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SelfieAWookies.Core.Selfies.Domain;

namespace SelfieAWookies.Core.Selfies.Infrastructures.Data.TypeConfigurations
{
    public class SelfieEntityTypeConfiguration : IEntityTypeConfiguration<Selfie>
    {
        public void Configure(EntityTypeBuilder<Selfie> builder)
        {
            builder.ToTable("Selfie");

            builder.HasKey(x => x.Id);
            builder.HasOne(item => item.Wookie)
                .WithMany(item => item.Selfies);
        }
    }
}
