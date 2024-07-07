using SampleAPI.Auth;

namespace SampleAPI.Extensions;

internal static class ConfigExtensions
{
	public static Jwt Jwt(this IConfiguration config) => new(
		Issuer: config["Jwt:Issuer"]!,
		Audience: config["Jwt:Audience"]!,
		Key: Convert.FromHexString(config["Jwt:Key"]!)
	);
}
