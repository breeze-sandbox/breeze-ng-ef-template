namespace Template.Data
{
  using System.Data.Entity;

  public partial class TemplateContext : DbContext
  {
    static TemplateContext()
    {
      // Note: Next line - SetInitialize(null) needs to be called for 'gulp generate-metadata' to work.
      Database.SetInitializer<TemplateContext>(null);
    }

    public TemplateContext()
        : base("TemplateContext")
    {
      this.Configuration.ProxyCreationEnabled = false;
      this.Configuration.LazyLoadingEnabled = false;
      Database.SetInitializer<TemplateContext>(null);
    }

    public TemplateContext(string connectionString) : base(connectionString) { }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>()
          .Property(e => e.UserName)
          .IsUnicode(false);

      //modelBuilder.Entity<User>()
      //    .Property(e => e.UserPassword)
      //    .IsUnicode(false);

      modelBuilder.Entity<User>()
          .Property(e => e.FirstName)
          .IsUnicode(false);

      modelBuilder.Entity<User>()
          .Property(e => e.LastName)
          .IsUnicode(false);

      modelBuilder.Entity<User>()
          .Property(e => e.Email)
          .IsUnicode(false);

      modelBuilder.Entity<User>()
          .Property(e => e.RowVersion)
          .HasPrecision(19, 4);

      modelBuilder.Entity<User>()
          .Property(e => e.CreatedBy)
          .IsUnicode(false);

      modelBuilder.Entity<User>()
          .Property(e => e.ModifiedBy)
          .IsUnicode(false);
    }
  }
}
