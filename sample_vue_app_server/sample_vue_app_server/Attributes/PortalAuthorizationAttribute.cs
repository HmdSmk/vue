using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using sample_vue_app_server.Models;

namespace sample_vue_app_server.Attributes;


public class PortalAuthorizationAttribute : TypeFilterAttribute
{
	public PortalAuthorizationAttribute() : base(typeof(PortalAuthorizationFilter))
	{
		Arguments = new object[] { };
	}
}

public class PortalAuthorizationFilter(JwtSettings jwtSettings) : IAuthorizationFilter
{
	public void OnAuthorization(AuthorizationFilterContext context)
	{
		if (context.HttpContext.User.Identity is ClaimsIdentity identity)
		{
			try
			{
				if (context.ActionDescriptor.EndpointMetadata.Any(item => item is IAllowAnonymous))
				{
					return;
				}

				if (identity.IsAuthenticated)
				{
					if (identity?.Claims == null || !identity.Claims.Any())
					{
						context.Result = new UnauthorizedResult();
						return;
					}

					var claims = new List<Claim>
					{
						new Claim(SecurityConstants.UserName, identity.Claims.FirstOrDefault(x => x.Type == SecurityConstants.UserName)?.Value),
					};

					var agentPrincipal = new AgentPrincipal(new ClaimsIdentity(claims, identity.AuthenticationType))
					{
						UserName = identity.Claims.FirstOrDefault(x => x.Type == SecurityConstants.UserName)?.Value,
					};

					if (string.IsNullOrWhiteSpace(agentPrincipal.UserName))
					{
						context.Result = new UnauthorizedResult();
						return;
					}

					if (!agentPrincipal.IsActive)
					{
						context.Result = new UnauthorizedResult();
						return;
					}

					var validationParameters = new TokenValidationParameters
					{
						RequireExpirationTime = true,
						ValidateActor = false,
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidateTokenReplay = false,
						RequireSignedTokens = true,
						RequireAudience = false,
						//ValidateLifetime = true,
						ClockSkew = TimeSpan.FromMinutes(1),
						//RequireSignedTokens = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings?.Secret ?? string.Empty)),
						//IssuerSigningKeys = signingKeys,
						//ValidateIssuer = true,
						//ValidIssuer = issuer,
						//ValidateAudience = true,
						//ValidAudience = audience
					};
					var token = ReadToken(context);
					if (string.IsNullOrWhiteSpace(token))
					{
						throw new Exception("Authorization token is empty");
					}
					ISecurityTokenValidator tokenValidator = new JwtSecurityTokenHandler();
					var claim = tokenValidator.ValidateToken(token, validationParameters, out var _);

					context.HttpContext.User = agentPrincipal;
				}
				else
				{
					context.Result = new UnauthorizedResult();
					return;
				}
			}
			catch (Exception ex)
			{
					
				context.Result = new UnauthorizedResult();
			}
		}

	}

	private string ReadToken(AuthorizationFilterContext context)
	{
		//string issuer = configuration["JwtToken:Issuer"]; //Get issuer value from your configuration
		//string audience = configuration["JwtToken:Audience"]; //Get audience value from your configuration
		//string metaDataAddress = issuer + "/.well-known/oauth-authorization-server";
		//CustomAuthHandler authHandler = new CustomAuthHandler();
		var header = context.HttpContext.Request.Headers["Authorization"];
		if (header.Count == 0) throw new Exception("Authorization header is empty");
		var tokenValue = Convert.ToString(header).Trim().Split(" ");
		var token = string.Empty;
		if (tokenValue.Length > 1) token = tokenValue[1];
		else throw new Exception("Authorization token is empty");
		return token;
	}
}