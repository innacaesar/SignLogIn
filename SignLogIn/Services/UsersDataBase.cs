using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SignLogIn.Models;
using SQLite;

namespace SignLogIn.Services

{
    public class UsersDataBase : IUserRepository
    {
        private readonly SQLiteAsyncConnection _connection;
        public UsersDataBase()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Users.db");//נבנה באופן אוטומטי בפעם הראשונה
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<User>().Wait();
            // Create the Users table if it doesn't exist
            //ועוד טבלאות אם יש צורך
        }


        public async Task<List<User>> GetUsersAsync()
        {
            return await _connection.Table<User>().ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _connection.FindAsync<User>(id);
        }

        public async Task<int> SaveUserAsync(User user)
        {
            if (user.Id != 0)
            {
                return await _connection.UpdateAsync(user);
            }
            else
            {
                return await _connection.InsertAsync(user);
            }
        }
        public async Task<int> DeleteUserAsync(User user)
        {
            return await _connection.DeleteAsync(user);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _connection.Table<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> GetUserByPhoneAsync(string phone)
        {
            return await _connection.Table<User>().FirstOrDefaultAsync(u => u.Phone == phone);
        }


        public async Task<bool> UserExistsAsync(string email, string phone)
        {
            var user = await _connection.Table<User>()  
                .FirstOrDefaultAsync(u => u.Email == email || u.Phone == phone);
            return user != null;
        }
        public async Task<bool> IsValidUserAsync(string email, string password)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user != null;
        }
        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }
        public async Task<bool> IsPhoneRegisteredAsync(string phone)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Phone == phone);
            return user != null;
        }

        public async Task ClearDatabaseAsync()
        {
            await _connection.DeleteAllAsync<User>();
        }
        public async Task<int> GetUserCountAsync()
        {
            return await _connection.Table<User>().CountAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _connection.FindAsync<User>(id);
        }
        public async Task<List<User>> SearchUsersAsync(string searchTerm)
        {
            return await _connection.Table<User>()
                .Where(u => u.Name.Contains(searchTerm) || u.Email.Contains(searchTerm) || u.Phone.Contains(searchTerm))
                .ToListAsync();
        }
        public async Task<bool> UpdateUserPasswordAsync(int userId, string newPassword)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Password = newPassword;
                return await SaveUserAsync(user) > 0;
            }
            return false;
        }
        public async Task<bool> UpdateUserEmailAsync(int userId, string newEmail)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Email = newEmail;
                return await SaveUserAsync(user) > 0;
            }
            return false;
        }
        public async Task<bool> UpdateUserPhoneAsync(int userId, string newPhone)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Phone = newPhone;
                return await SaveUserAsync(user) > 0;
            }
            return false;
        }
        public async Task<bool> UpdateUserNameAsync(int userId, string newName)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Name = newName;
                return await SaveUserAsync(user) > 0;
            }
            return false;
        }
        public async Task<bool> IsUserNameUniqueAsync(string name)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Name == name);
            return user == null;
        }
        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
            return user == null;
        }
        public async Task<bool> IsPhoneUniqueAsync(string phone)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Phone == phone);
            return user == null;
        }
        public async Task<bool> IsUserValidAsync(string email, string password)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user != null;
        }
        public async Task<bool> IsUserRegisteredAsync(string email, string phone)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email || u.Phone == phone);
            return user != null;
        }
        public async Task<bool> IsUserEmailRegisteredAsync(string email)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }
        public async Task<bool> IsUserPhoneRegisteredAsync(string phone)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Phone == phone);
            return user != null;
        }
        public async Task<bool> IsUserExistsAsync(string email, string phone)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email || u.Phone == phone);
            return user != null;
        }
        public async Task<bool> IsUserEmailExistsAsync(string email)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }
        public async Task<bool> IsUserPhoneExistsAsync(string phone)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Phone == phone);
            return user != null;
        }
        public async Task<bool> IsUserNameExistsAsync(string name)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Name == name);
            return user != null;
        }
        public async Task<bool> IsUserNameValidAsync(string name)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Name == name);
            return user != null;
        }
        public async Task<bool> IsUserEmailValidAsync(string email)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }
        public async Task<bool> IsUserPhoneValidAsync(string phone)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Phone == phone);
            return user != null;
        }
        public async Task<bool> IsUserPasswordValidAsync(string email, string password)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user != null;
        }
        public async Task<bool> IsUserRegisteredWithEmailAsync(string email)
        {
            var user = await _connection.Table<User>()
                .FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }
        
        public Task<int> UpdateUserAsync(User user)
        {

            if (user.Id != 0)
            {
                return _connection.UpdateAsync(user);
            }
            else
            {
                throw new ArgumentException("User must have a valid Id to update.");
            }
        }
        //  Returns the count of users in the database    
        public int GetUsersCount()
        {
            return _connection.Table<User>().CountAsync().Result;
        }
    }
    }
