using System.Security.Cryptography;
using System.Text;

namespace Core.Security.Hashing;

public static class HashingHelper
{
    /// <summary>
    /// Şifre için passwordHash ve passwordSalt oluşturur 
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <param name="passwordSalt"></param>
    public static void CreatedPasswordHash(string password,out byte[] passwordHash,out byte[] passwordSalt)
    {
        using HMACSHA512 hmac = new();
        passwordSalt=hmac.Key;
        passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
    /// <summary>
    /// Şifre doğrulamak için
    /// </summary>
    /// <param name="password"></param>
    /// <param name="passwordHash"></param>
    /// <param name="passwordSalt"></param>
    /// <returns></returns>
    public static bool VerifyPasswordHash(string password,byte[] passwordHash, byte[] passwordSalt) {
        using HMACSHA512 hmac = new(passwordSalt);
        byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(passwordHash);
    }
}
