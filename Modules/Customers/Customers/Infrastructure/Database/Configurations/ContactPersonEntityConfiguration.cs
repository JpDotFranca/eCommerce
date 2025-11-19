using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.Database.Configurations;

internal class ContactPersonEntityConfiguration : IEntityTypeConfiguration<ContactPerson>
{
    public void Configure(EntityTypeBuilder<ContactPerson> builder)
    {
        builder.ToTable("ContactPersons", "customers");
        builder.HasKey(cp => cp.Id);

        builder.Property(cp => cp.FirstName)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(cp => cp.FullName)
            .HasMaxLength(50)
            .IsRequired();

        builder.OwnsOne(c => c.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName("Email")
                .HasMaxLength(50)
                .IsRequired();

            email.HasIndex(e=> e.Address)
                .IsUnique()
                .HasDatabaseName("UK_CONTACT_PERSONS_EMAIL");
        });
         
        builder.Property(cp => cp.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .IsRequired();
    }
}