using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.Entities;

namespace Data.Configurations
{
    public class TransactionsConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.BeneficiaryName).IsRequired();

            builder.Property(x => x.Direction).HasConversion<string>()
                                              .HasMaxLength(1)
                                              .IsRequired();

            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Description);

            builder.Property(x => x.Currency).HasConversion<string>()
                                             .HasMaxLength(3)
                                             .IsRequired();
        }
    }

    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(x => x.Code);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ParrentCode);
        }
    }
}