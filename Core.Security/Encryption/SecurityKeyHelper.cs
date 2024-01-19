using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Security.Encryption;

public static class SecurityKeyHelper
{
    public static SecurityKey CreateSecurityKey(string securtyKey) => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securtyKey));
}
