using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
    }
}