using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;


public class UserService : IUserService {

    private readonly IUserRepository _repository;
    private readonly IConfiguration _config;

    public UserService(IUserRepository userRepository, IConfiguration config)
    {
        _repository = userRepository;
        _config = config;
    }

    public async Task<string> RegisterAsync(RegisterRequest request) {

        var existingUser = await _repository.GetUserByEmailAsync(request.email);
        if (existingUser != null) return "Usuário já existe!";
        var user = new UserModel {email = request.email, senhaHash = BCrypt.Net.BCrypt.HashPassword(request.senhaHash)};
        await _repository.AddUserAsync(user);
        return "Usuário registrado com sucesso!";
    }

    public async Task<string> LoginAsync(LoginRequest request) {

        var user = await _repository.GetUserByEmailAsync(request.email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.senhaHash, user.senhaHash)) return "Credenciais inválidas!";  
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new [] {new Claim(ClaimTypes.Name, user.email)}),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }

}