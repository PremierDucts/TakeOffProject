using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
using TakeOffAPI.Entities;

public class QldDataContext : DbContext
{
    public QldDataContext(DbContextOptions<QldDataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder optionsBuilder)
    {
        optionsBuilder.Entity<FileUploadDetails>(entity =>
        {
            entity.ToTable("FileUploadDetails");
        });
        optionsBuilder.Entity<DispatchDetail>(entity =>
        {
            entity.HasKey("id");
            entity.ToTable("dispatch_detail_test");
        });
        optionsBuilder.Entity<ControlMachineList>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("tbl_ControlLists");
        });
        optionsBuilder.Entity<FactoryFit>(entity =>
        {
            entity.HasKey("id");
            entity.ToTable("factory_fit_test");
        });
        base.OnModelCreating(optionsBuilder);
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionString = _configuration.GetConnectionString("MySQLConnectionString");
        //    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        //}
    }
    public virtual DbSet<FileUploadDetails> FileUploadDetails { get; set; }
    public virtual DbSet<DispatchDetail> DispatchDetails { get; set; }
    public virtual DbSet<FactoryFit> FactoryFits { get; set; }

    public virtual DbSet<ControlMachineList> ControlMachineLists { get; set; }


}