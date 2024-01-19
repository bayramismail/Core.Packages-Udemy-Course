namespace Core.Security.JWT;

public class TokenOptions
{
    /// <summary>
    /// Hitap ettiğimiz yer
    /// </summary>
    public string Audience { get; set; }
    /// <summary>
    /// Token veren taraf
    /// </summary>
    public string Issuer { get; set; }
    /// <summary>
    /// Token nezamana kadar geçerli
    /// </summary>
    public int AccessTokenExpiration { get; set; }
    /// <summary>
    /// Güvenlik Anahtarı
    /// </summary>
    public string SecurityKey { get; set; }
    public int RefreshTokenTTL { get; set; }

    public TokenOptions()
    {
        Audience=string.Empty; 
        Issuer=string.Empty;
        SecurityKey=string.Empty;
    }

    public TokenOptions(string audience, string ıssuer, int accessTokenExpiration, string securityKey, int refreshTokenTTL)
    {
        Audience=audience;
        Issuer=ıssuer;
        AccessTokenExpiration=accessTokenExpiration;
        SecurityKey=securityKey;
        RefreshTokenTTL=refreshTokenTTL;
    }
}
