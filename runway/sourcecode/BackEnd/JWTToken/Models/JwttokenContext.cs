using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JWTToken.Models;

public partial class JwttokenContext : DbContext
{
    public JwttokenContext()
    {
    }

    public JwttokenContext(DbContextOptions<JwttokenContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS05;Database=JWTToken;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }
    
    public DbSet<UserModel> userModels { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
