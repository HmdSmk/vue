using Microsoft.AspNetCore.Mvc;
using sample_vue_app_server.Attributes;

namespace sample_vue_app_server.Controllers;
[PortalAuthorization]
[ApiController]
public class ProtectedController : ControllerBase
{
    
}