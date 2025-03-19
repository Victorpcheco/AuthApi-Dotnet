using AuthAPI.Models;
using BCrypt.Net;

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



}