using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsManager.Application.Common.Constants;
using CarsManager.Application.Common.Exceptions;
using CarsManager.Application.Common.Interfaces;
using CarsManager.Domain.Entities;
using CarsManager.Infrastructure.Helpers;
using CarsManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarsManager.Infrastructure.Identity
{
    public class UserService// : IUserService
    {
        //private ApplicationDbContext context;

        //public UserService(ApplicationDbContext context)
        //{
        //    this.context = context;
        //}

        //public User Authenticate(string username, string password)
        //{
        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //        return null;

        //    var user = context.Users
        //        .Include(u => u.Roles)
        //        .SingleOrDefault(x => x.Username == username);

        //    if (user == null)
        //        return null;

        //    if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        //        return null;

        //    // authentication successful
        //    return user;
        //}

        //public async Task<IList<User>> GetAllAsync()
        //    => await context.Users.Include(u => u.Roles).ToListAsync();

        //public async Task<User> GetByIdAsync(string id)
        //    => await context.Users
        //    .Include(u => u.Roles)
        //    .FirstOrDefaultAsync(u => u.Id == id);


        //public async Task<string> GetUserNameAsync(string id)
        //{
        //    var user = await context.Users.FindAsync(id);
        //    return user?.Username;
        //}

        //public async Task<User> CreateAsync(User user, string password)
        //{
        //    // validation
        //    if (string.IsNullOrWhiteSpace(password))
        //        throw new IdentityException("Password is required");

        //    if (context.Users.Any(x => x.Username == user.Username))
        //        throw new IdentityException("Username \"" + user.Username + "\" is already taken");

        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //    user.Id = Guid.NewGuid().ToString();
        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;

        //    await context.Users.AddAsync(user);

        //    if (context.Users.Count() == 1)
        //        await AssignRoleAsync(user.Id, RoleConstants.ADMIN);

        //    await context.SaveChangesAsync();

        //    return user;
        //}

        //public async Task UpdateAsync(User userParam, string password = null)
        //{
        //    var user = await context.Users.FindAsync(userParam.Id);

        //    if (user == null)
        //        throw new NotFoundException(nameof(User), userParam.Id);

        //    // update username if it has changed
        //    if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
        //    {
        //        // throw error if the new username is already taken
        //        if (context.Users.Any(x => x.Username == userParam.Username))
        //            throw new IdentityException("Username " + userParam.Username + " is already taken");

        //        user.Username = userParam.Username;
        //    }

        //    // update user properties if provided
        //    if (!string.IsNullOrWhiteSpace(userParam.FirstName))
        //        user.FirstName = userParam.FirstName;

        //    if (!string.IsNullOrWhiteSpace(userParam.LastName))
        //        user.LastName = userParam.LastName;

        //    // update password if provided
        //    if (!string.IsNullOrWhiteSpace(password))
        //    {
        //        byte[] passwordHash, passwordSalt;
        //        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //        user.PasswordHash = passwordHash;
        //        user.PasswordSalt = passwordSalt;
        //    }

        //    context.Users.Update(user);
        //    await context.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var user = context.Users.Find(id);
        //    if (user == null)
        //        return;

        //    context.Users.Remove(user);
        //    await context.SaveChangesAsync();
        //}

        //private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}

        //private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        //{
        //    if (password == null) return false;
        //    if (string.IsNullOrWhiteSpace(password)) return false;
        //    if (storedHash.Length != 64) return false;
        //    if (storedSalt.Length != 128) return false;

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //        for (int i = 0; i < computedHash.Length; i++)
        //            if (computedHash[i] != storedHash[i]) return false;
        //    }

        //    return true;
        //}

        //public async Task<bool> IsInRoleAsync(string userId, string role)
        //{
        //    var user = await GetByIdAsync(userId);
        //    if (user == null)
        //        return false;

        //    return user.Roles.Any(r => r.Name == role);
        //}

        //public async Task AssignRoleAsync(string userId, string roleName)
        //{
        //    var user = await GetByIdAsync(userId);
        //    if (user == null)
        //        throw new NotFoundException(nameof(User), userId);

        //    var role = await context.UserRoles.FindAsync(roleName);
        //    if (role == null)
        //        role = new UserRole { Name = roleName };

        //    user.Roles.Add(role);
        //    await context.SaveChangesAsync();
        //}
    }
}