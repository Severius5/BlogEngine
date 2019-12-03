using BlogEngine.DTO.Models;
using BlogEngine.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Storage.Repositories.Internals
{
    internal class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;

        public UserRepository(BlogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<BlogUser> GetUser(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .Where(x => x.NormalizedEmail == email.ToUpperInvariant())
                .FirstOrDefaultAsync();

            return Mapper.ConvertToModel(userEntity);
        }

        public async Task<BlogUser> GetUser(int id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return Mapper.ConvertToModel(userEntity);
        }

        public async Task<(IList<BlogUser> users, int totalUsers)> GetUsers(UsersFilter filter)
        {
            var query = _context.Users
                .AsNoTracking();

            query = QueryUsersBySearchText(query, filter.Search);

            List<User> users = null;

            var count = await query.CountAsync();
            if (count != 0)
            {
                var skip = (filter.Page - 1) * filter.PageSize;
                users = await query
                    .OrderBy(x => x.Id)
                    .Skip(skip)
                    .Take(filter.PageSize)
                    .ToListAsync();
            }

            var dtoUsers = users?.Select(x => Mapper.ConvertToModel(x))?.ToList();

            return (dtoUsers, count);
        }

        public async Task ChangeUserAdminStatus(int userId, bool isAdmin)
        {
            var entity = new User
            {
                Id = userId,
                IsAdmin = isAdmin,
                DetailsStamp = Guid.NewGuid().ToString()
            };

            _context.Attach(entity);
            _context.Entry(entity).Property(x => x.IsAdmin).IsModified = true;
            _context.Entry(entity).Property(x => x.DetailsStamp).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task BlockUser(int userId)
        {
            var entity = new User
            {
                Id = userId,
                IsBlocked = true,
                IsAdmin = false,
                DetailsStamp = Guid.NewGuid().ToString()
            };

            _context.Attach(entity);
            _context.Entry(entity).Property(x => x.IsBlocked).IsModified = true;
            _context.Entry(entity).Property(x => x.IsAdmin).IsModified = true;
            _context.Entry(entity).Property(x => x.DetailsStamp).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task UnblockUser(int userId)
        {
            var entity = new User
            {
                Id = userId,
                IsBlocked = false
            };

            _context.Attach(entity);
            _context.Entry(entity).Property(x => x.IsBlocked).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task EditUser(BlogUser existingUser)
        {
            var entity = Mapper.ConvertToEntity(existingUser);
            entity.DetailsStamp = Guid.NewGuid().ToString();

            _context.Attach(entity);
            _context.Entry(entity).Property(x => x.Bio).IsModified = true;
            _context.Entry(entity).Property(x => x.Username).IsModified = true;
            _context.Entry(entity).Property(x => x.NormalizedUsername).IsModified = true;
            _context.Entry(entity).Property(x => x.Slug).IsModified = true;
            _context.Entry(entity).Property(x => x.DetailsStamp).IsModified = true;

            await _context.SaveChangesAsync();
        }

        private IQueryable<User> QueryUsersBySearchText(IQueryable<User> query, string search)
        {
            search = search?.ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(search))
                return query;
            else
                return query.Where(x => x.NormalizedUsername.Contains(search));
        }
    }
}
