using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DemoEmployeeManagementSolution
{
    [Route("api/[controller]")]
     [ApiController]

    public class AuthenticationController(IUserAccount accountInterface) : ControllerBase
    {
        [HttpPost]


        public async Task<IActionResult>  CreateAsync(Register user)
        {
            if(user == null) return BadRequest("Model is empty");
            var result = await accountInterface.CreateAsync(user);
            return Ok(result);
        }
        
    }
}