namespace PCMount.Data;

using System.Text;
using System.Security.Cryptography;

public class PasswordHasher {
    public static string HashPassword(string password)
    {
        byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(password));
        StringBuilder sBuilder = new();
        for (int i = 0; i < data.Length; i++) {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }

    public static bool VerifyHashedPassword(string hashedPassword, string password) {
        string hashOfInput = HashPassword(password);
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        return comparer.Compare(hashedPassword, hashOfInput) == 0;
    }
}