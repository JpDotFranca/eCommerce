using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.Database.Configurations;
 
internal class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", "customers");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(c => c.ComercialName)
            .HasMaxLength(100)
            .IsRequired();
         
        builder.OwnsOne(c => c.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName("Email")
                .HasMaxLength(50)
                .IsRequired();

            email.HasIndex(e=> e.Address)
                .IsUnique()
                .HasDatabaseName("UK_CUSTOMER_EMAIL"); 
        });
         
        builder.OwnsOne(c => c.CompanyIdentity, companyIdentifier =>
        {
            companyIdentifier.Property(ci => ci.Identifier)
                .HasColumnName("CompanyIdentity")
                .HasMaxLength(20)
                .IsRequired();

            companyIdentifier.HasIndex(ci=> ci.Identifier)
                .IsUnique()
                .HasDatabaseName("UK_CUSTOMER_COMPANY_IDENTITY");
        }); 

        builder.HasOne(c => c.ContactPerson)
            .WithMany()
            .HasForeignKey("ContactPersonId")
            .HasConstraintName("FK_CUSTOMER_CONTACT_PERSON")
            .IsRequired();
         
        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .IsRequired(); 
    }
}
