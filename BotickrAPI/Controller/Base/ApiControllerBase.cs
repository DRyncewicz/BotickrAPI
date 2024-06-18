using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BotickrAPI.Controller.Base;
//TODO: Change from AllowAnonymous to Authorize after AuthServer implementation
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[AllowAnonymous]
public class ApiControllerBase : ControllerBase
{
}