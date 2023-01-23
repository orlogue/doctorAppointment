using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace doctorAppointments.JWT;

public class AuthOptions
{
    public const string ISSUER = "Hospital";
    public const string AUDIENCE = "Patients";
    const string KEY = "mysupersecret_secretkey!123";
    public const int LIFETIME = 20;
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
