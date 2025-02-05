using Microsoft.AspNetCore.Mvc;
using reeltok.api.gateway.ActionFilters;

namespace reeltok.api.gateway.Controllers
{
    [ApiController]
    [ValidateModel]
    [Route("api/[controller]")]
    public class VideosController : ControllerBase
    {

    }
}