using Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Data.Mappings;

public class AccountMapping : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");

        builder.HasKey(x => x.Id)
            .HasName("PK_Accounts_Id");

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(x => x.Username)
            .HasColumnName("Username")
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasColumnName("Email")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasColumnName("Password")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .HasColumnName("Created")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.Property(x => x.LastUpdatedDate)
            .HasColumnName("LastUpdated")
            .HasColumnType("DATETIME")
            .IsRequired();

        builder.HasMany(x => x.Roles)
               .WithMany(x => x.Accounts)
               .UsingEntity<Dictionary<string, object>>
                ("AccountRoles",
                    account => account
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_AccountRoles_RoleId"),
                    role => role
                        .HasOne<Account>()
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_AccountRoles_AccountId"));

        builder.HasIndex(x => x.Id, "IX_Accounts_Id")
            .IsUnique();

        builder.HasIndex(x => x.Username, "IX_Accounts_Username")
            .IsUnique();

        builder.HasIndex(x => x.Email, "IX_Accounts_Email")
            .IsUnique();
    }
}