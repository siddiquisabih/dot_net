using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Tatweer.YourServiceName.Pages;

public class IndexModel : YourServiceNamePageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
