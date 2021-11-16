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

            builder.Property(x => x.Mcc).HasConversion<int>();
            builder.Property(x => x.Kind).HasConversion<string>()
                                         .IsRequired();

            builder.Property(x => x.CategoryCode);                                         
            builder.HasOne(x => x.Category)
                    .WithMany(x => x.Transactions)
                    .HasForeignKey(x => x.CategoryCode)
                    .OnDelete(DeleteBehavior.SetNull);
        }
    }

    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(x => x.Code);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.ParrentCode);
            builder.Property(x => x.Name).IsRequired();

            builder.HasOne(x => x.ParrentCategory)
                   .WithMany(x => x.SubCategories)
                   .HasForeignKey(x => x.ParrentCode)
                   .OnDelete(DeleteBehavior.SetNull);
                   
            // builder.HasMany(x => x.Transactions)
            //        .WithOne();

            // builder.Navigation(x => x.Transactions)
            //        .UsePropertyAccessMode(PropertyAccessMode.Property);
        }
    }

    public class MerchantTypeConfiguration : IEntityTypeConfiguration<MerchantType>
    {
        public void Configure(EntityTypeBuilder<MerchantType> builder)
        {
            builder.ToTable("merchantTypes");

            builder.HasKey(x => x.Code);
            builder.Property(x => x.Code).IsRequired();
        }
    }
}