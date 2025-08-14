using Portfolio.Domain.Entities;
namespace Portfolio.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Header> HeaderRepository { get; }
        IGenericRepository<Intro> IntroRepository { get; }
        IGenericRepository<About> AboutRepository { get; }
        IGenericRepository<Services> ServiceRepository { get; }
        IGenericRepository<SkillSection> SkillSectionRepository { get; }
        IGenericRepository<SkillDetail> SkillDetailRepository { get; }
        IGenericRepository<Experience> ExperienceRepository { get; }
        IGenericRepository<Education> EducationRepository { get; }
        IGenericRepository<Review> ReviewRepository { get; }  
        IGenericRepository<ContactInfo> ContactInfoRepository { get; }
        IGenericRepository<ClientMessage> ClientMessageRepository { get; }
        IGenericRepository<AuditLog> AuditLogRepository { get; }
        IGenericRepository<SocialLinks> SocialLinksRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
