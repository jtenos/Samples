namespace SampleAPI.Auth;

public record class Jwt(
	string Issuer,
	string Audience,
	byte[] Key
);
