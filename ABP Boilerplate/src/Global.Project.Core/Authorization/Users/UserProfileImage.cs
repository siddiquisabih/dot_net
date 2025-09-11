using Abp.Domain.Entities.Auditing;

namespace Global.Project.Authorization.Users
{
    public class UserProfileImage : FullAuditedEntity
    {
        public string OriginalFileName { get; set; }

        public string SavedFileName { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public long FileSize { get; set; }

        public long UserId { get; set; }

    }
}
