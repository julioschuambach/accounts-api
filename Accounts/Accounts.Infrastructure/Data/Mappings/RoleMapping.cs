using Accounts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounts.Infrastructure.Data.Mappings;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id)
            .HasName("PK_Roles_Id");

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("UNIQUEIDENTIFIER")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(25)
            .IsRequired();

        builder.HasMany(x => x.Accounts);

        builder.HasIndex(x => x.Id, "IX_Roles_Id")
            .IsUnique();

        builder.HasIndex(x => x.Name, "IX_Roles_Name")
            .IsUnique();
    }
}