using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;
using Portfolio.Persistence.Interceptors;
namespace Portfolio.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<Header> Headers { get; set; }
        public DbSet<Intro> Intros { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<SkillSection> SkillSections { get; set; }
        public DbSet<SkillDetail> SkillDetails { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ClientMessage> ClientMessages { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<SocialLinks> SocialLinks { get; set; }

        private readonly AddUpdateAuditEntitiesSaveChangesInterceptor _auditInterceptor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
                                     AddUpdateAuditEntitiesSaveChangesInterceptor auditInterceptor)
            : base(options)
        {
            _auditInterceptor = auditInterceptor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.AddInterceptors(_auditInterceptor);
        }

    }
}
