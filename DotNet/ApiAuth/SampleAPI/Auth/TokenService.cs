using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SampleAPI.Extensions;

namespace SampleAPI.Auth;

public class TokenService
{
	private static readonly TimeSpan _expirationDuration = TimeSpan.FromHours(12);

	public string BuildToken(IConfiguration config, string userName, int orgID)
	{
		Jwt jwt = config.Jwt();

		List<Claim> claims =
			[
				new(ClaimTypes.Name, userName),
				new(CustomClaimTypes.OrgID, orgID.ToString())
			];
		switch (userName)
		{
			case "ad":
				claims.Add(new(CustomClaimTypes.Admin, true.ToString()));
				break;
			case "hr":
				claims.Add(new(CustomClaimTypes.HumanResources, true.ToString()));
				break;
		}

		SymmetricSecurityKey securityKey = new(jwt.Key);
		SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);
		JwtSecurityToken tokenDescriptor = new(jwt.Issuer, jwt.Audience, claims,
			expires: DateTime.Now.Add(_expirationDuration), signingCredentials: credentials);
		return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
	}
}
