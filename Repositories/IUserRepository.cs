using AuthAPI.Models;

public interface IUserRepository {

    Task<UserModel> GetUserByEmailAsync (string email);
    Task AddUserAsync(UserModel user);

    List<UserModel> GetAll();
}