using aspnetUserAuthTemplate.Models.Database;
using aspnetUserAuthTemplate.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace aspnetUserAuthTemplate.Controllers;

public class AuthorizedControllerBase : ControllerBase
{
    /// <summary>
    /// The AuthClaims of the user
    /// </summary>
    public AuthClaims AuthClaims { get; set; }
    
    /// <summary>
    /// The user that is currently logged in
    /// </summary>
    public User AuthorizedUser { get; set; }
}