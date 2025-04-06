

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase {

    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;

    public AuthController(IUserService userService, IUserRepository userRepository) {_userService = userService; _userRepository = userRepository;}

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request) => Ok(await _userService.RegisterAsync(request));

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request) => Ok(await _userService.LoginAsync(request));


    // Endpoint Get para teste da autenticação
    [Authorize]
    [HttpGet]
    public IActionResult GetUsers()
    {
        var users = _userRepository.GetAll()
            .Select(u => new { u.email }) 
            .ToList();

        return Ok(users);
    }
}