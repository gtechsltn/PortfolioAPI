

using Portfolio.Domain.Common;

namespace Portfolio.Domain.Entities
{
    public class Services : AddUpdateAuditEntities
    {
        public Guid Id { get; set; }
        public string? ServiceIconPath { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set; }

        public static Services Create(
        string? serviceIconPath,
        string? serviceName,
        string? serviceDescription)
        {
            return new Services
            {
                ServiceIconPath = serviceIconPath,
                ServiceName = serviceName,
                ServiceDescription = serviceDescription
            };
        }
        public void Update(
            string? serviceIconPath = null,
            string? serviceName = null,
            string? serviceDescription = null)
        {
            if (!string.IsNullOrWhiteSpace(serviceIconPath)) ServiceIconPath = serviceIconPath;
            if (!string.IsNullOrWhiteSpace(serviceName)) ServiceName = serviceName;
            if (!string.IsNullOrWhiteSpace(serviceDescription)) ServiceDescription = serviceDescription;
        }

    }
}
