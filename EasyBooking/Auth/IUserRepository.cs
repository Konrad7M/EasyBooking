namespace EasyBooking.Api.Auth;

public interface IUserRepository
{
    Task<User?> ValidateUser(string Username, string password); 
    Task<List<User>> GetAllUsers(); 
    Task<User?> GetUserById(int id); 
    Task<User> AddUser(User User); 
    Task<User> UpdateUser(User User);
    Task DeleteUser(int id);
}

public class UserRepository : IUserRepository
{
    private List<User> Users = new List<User>
            {
                new User { Id = 1, Username = "admin", Password = "admin" },
                new User { Id = 2, Username = "User", Password = "User" },
                new User { Id = 3, Username = "Pranaya", Password = "Test@1234" },
                new User { Id = 4, Username = "Kumar", Password = "Admin@123" }
            };
    public async Task<User?> ValidateUser(string Username, string password)
    {
        return Users.FirstOrDefault(u => u.Username == Username && u.Password == password);
    }
    public async Task<List<User>> GetAllUsers()
    {
        return Users.ToList();
    }
    public async Task<User?> GetUserById(int id)
    {
        return Users.FirstOrDefault(u => u.Id == id); 
    }
    public async Task<User> AddUser(User User)
    {
        if (Users.Any(u => u.Id == User.Id))
        {
            throw new Exception("User already exists with the given ID.");
        }
        Users.Add(User);
        return User;
    }
    public async Task<User> UpdateUser(User User)
    {
        var existingUser = await GetUserById(User.Id);
        if (existingUser == null)
        {
            throw new Exception("User not found.");
        }
        existingUser.Username = User.Username;
        existingUser.Password = User.Password;
        return existingUser;
    }
    public async Task DeleteUser(int id)
    {
        var User = await GetUserById(id);
        if (User == null)
        {
            throw new Exception("User not found."); 
        }
        Users.Remove(User);
    }
}