using Portfolio.Application.Interfaces;
using Portfolio.Domain.Entities;
using Portfolio.Persistence.Context;
using Portfolio.Persistence.Repositories;

namespace Portfolio.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            HeaderRepository = new GenericRepository<Header>(_context);
            IntroRepository = new GenericRepository<Intro>(_context);
            AboutRepository = new GenericRepository<About>(_context);
            UserRepository = new GenericRepository<User>(_context);
            ServiceRepository = new GenericRepository<Services>(_context);
            SkillSectionRepository = new GenericRepository<SkillSection>(_context);
            SkillDetailRepository = new GenericRepository<SkillDetail>(_context);
            ExperienceRepository = new GenericRepository<Experience>(_context);
            EducationRepository = new GenericRepository<Education>(_context);
            ReviewRepository = new GenericRepository<Review>(_context);
            ContactInfoRepository = new GenericRepository<ContactInfo>(_context);
            ClientMessageRepository = new GenericRepository<ClientMessage>(_context);
            AuditLogRepository = new GenericRepository<AuditLog>(_context);
            SocialLinksRepository = new GenericRepository<SocialLinks>(_context);
        }

        public IGenericRepository<Header> HeaderRepository { get; }

        public IGenericRepository<About> AboutRepository { get; }

        public IGenericRepository<User> UserRepository  { get;}

        public IGenericRepository<Intro> IntroRepository { get; }

        public IGenericRepository<Services> ServiceRepository { get; }

        public IGenericRepository<SkillSection> SkillSectionRepository { get; }
        public IGenericRepository<SkillDetail> SkillDetailRepository { get; }
        public IGenericRepository<Experience> ExperienceRepository { get; }
        public IGenericRepository<Education> EducationRepository { get; }
        public IGenericRepository<Review> ReviewRepository { get; }
        public IGenericRepository<ContactInfo> ContactInfoRepository { get; }
        public IGenericRepository<ClientMessage> ClientMessageRepository { get; }
        public IGenericRepository<AuditLog> AuditLogRepository { get; }
        public IGenericRepository<SocialLinks> SocialLinksRepository { get; }
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
