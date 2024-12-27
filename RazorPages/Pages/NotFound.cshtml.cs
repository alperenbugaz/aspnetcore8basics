using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPages.Pages;

public class NotFoundModel : PageModel
{
    private readonly ILogger<PrivacyModel> _logger;

    public NotFoundModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}

