using System.Security.Claims;

namespace sample_vue_app_server;


public class AgentPrincipal : ClaimsPrincipal
{
    public AgentPrincipal(ClaimsIdentity identity) : base(identity) { }

    public AgentPrincipal() { }

    public string UserName { get; set; }
    public bool IsActive { get; set; }
    public List<string> AuthorizedShopCodes { get; set; }

}