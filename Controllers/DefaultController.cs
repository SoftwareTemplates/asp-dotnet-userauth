using System.Net.Mime;
using aspnetUserAuthTemplate.Filter;
using Microsoft.AspNetCore.Mvc;

namespace aspnetUserAuthTemplate.Controllers;

[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
[ProxyAddressFilter]
[ApiController]
public class DefaultController : ControllerBase
{


    [HttpGet("/")]
    [RateLimitFilter]
    public ActionResult Default()
    {
        return Ok();
    }
    
}