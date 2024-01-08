using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ChristmasWishList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
    }
}
