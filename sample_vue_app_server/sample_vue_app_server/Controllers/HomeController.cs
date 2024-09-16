using Microsoft.AspNetCore.Mvc;

namespace sample_vue_app_server.Controllers;

[Route("[controller]/[action]")]
public class HomeController: ProtectedController
{
    [HttpGet]
    public string Message()
    {
        return "This is a message from home-controller.";
    }
}