using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository {

    private readonly DataContext _dataContext;   
    public UserRepository(DataContext dataContext) {_dataContext = dataContext;}

    public async Task<UserModel> GetUserByEmailAsync(string email) => 
        await _dataContext.users.FirstOrDefaultAsync(u => u.email == email);
    public async Task AddUserAsync(UserModel user) {
        _dataContext.users.Add(user);
        await _dataContext.SaveChangesAsync();
    }

    public List<UserModel> GetAll() {
        return _dataContext.users.ToList();
    }
}