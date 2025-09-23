using AIM.API.Data;
using AIM.API.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Threading;

namespace AIM.API.Repositories
{
    public class UserRepository(ApplicationDbContext context)
    {
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User?> GetByAnyLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.LoginInfo.Contains(login), cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await context.Users.AddAsync(user, cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            context.Users.Update(user);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            User? user = await GetByIdAsync(id, cancellationToken);
            if (user is null)
                return;
            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Users.AnyAsync(co => co.Id == id, cancellationToken);
        }

    }
}
