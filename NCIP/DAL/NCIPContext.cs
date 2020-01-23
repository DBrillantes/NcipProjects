using NCIP.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace NCIP.DAL
{
    public class NcipDBContext : DbContext
    {
        public NcipDBContext() : base("NCIPContext") { }
        public static NcipDBContext Create()
        {
            return new NcipDBContext();
        }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<Barangay> Barangays { get; set; }
        public DbSet<Community> Communities { get; set; }
        public DbSet<Company> Companies { get; set; }
        //public DbSet<Encoder> Encoders { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Projectfile> Projectfiles { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<EthnicGroup> EthnicGroups { get; set; }
        public DbSet<AffectedCommunity> AffectedCommunities { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}