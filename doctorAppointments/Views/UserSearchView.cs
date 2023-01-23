using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using domain.Classes;

namespace doctorAppointments.Views;

public class UserSearchView
{
    [JsonPropertyName("phoneNumber")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("role")]
    public Role Role { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }
}