using System.Text.Json.Serialization;

namespace aspnetUserAuthTemplate.Models.Request;

public class LoginRequest
{
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
}