

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase {

    private readonly IUserService _userService;

    public AuthController(IUserService userService) {_userService = userService; }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request) => Ok(await _userService.RegisterAsync(request));
}