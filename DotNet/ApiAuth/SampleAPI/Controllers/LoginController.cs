using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleAPI.Auth;

namespace SampleAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController(
	TokenService tokenService,
	IConfiguration config
)
	: ControllerBase
{
	public record struct LoginRequest(string UserName, int OrgID);
	public record struct LoginResult(bool Success, string? Jwt);

	[HttpPost]
	[AllowAnonymous]
	public LoginResult Post(LoginRequest req)
	{
		if (req.UserName == "bad") { return new(false, null); }

		// Validate user's name and password and pull role info from database
		string jwt = tokenService.BuildToken(config, req.UserName, req.OrgID);
		return new(true, jwt);
	}
}
