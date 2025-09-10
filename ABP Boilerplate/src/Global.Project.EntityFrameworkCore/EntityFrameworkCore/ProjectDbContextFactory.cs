using Global.Project.Configuration;
using Global.Project.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Global.Project.EntityFrameworkCore
{
    public class ProjectDbContextFactory : IDesignTimeDbContextFactory<ProjectDbContext>
    {
        public ProjectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ProjectDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ProjectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(ProjectConsts.ConnectionStringName));

            return new ProjectDbContext(builder.Options);
        }
    }
}
