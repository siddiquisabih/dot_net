using Abp.Application.Services.Dto;

namespace Global.Project.Users.Dto
{
    public class CreateUserProfileImageDto
    {
        public string OriginalFileName { get; set; }

        public string SavedFileName { get; set; }

        public string FilePath { get; set; }

        public string FileType { get; set; }

        public long FileSize { get; set; }
    }
}
