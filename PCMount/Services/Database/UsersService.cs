namespace PCMount.Services.Database;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PCMount.Data;
using PCMount.Data.Models;

public class UsersService : IDbContextAcessor<User> {
    private readonly ApplicationDbContext _dbContext;
    private static readonly SemaphoreSlim semaphore = new(1, 1);

    public UsersService() {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        _ = optionsBuilder.UseSqlServer(DbContextConfig.ConnectionString);
        _dbContext = new ApplicationDbContext(optionsBuilder.Options);
    }

    public async Task<User[]> GetArrayAsync() {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Users.AsNoTracking().ToArrayAsync();
        } finally {
            semaphore.Release();
        }
    }

    public async Task<List<User>> GetListAsync() {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Users.AsNoTracking().ToListAsync();
        } finally {
            semaphore.Release();
        }
    }

    public async Task<User?> DeleteAsync(int id) {
        await semaphore.WaitAsync();
        try {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null) {
                return null;
            }
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;
        } finally {
            semaphore.Release();
        }
    }

    public async Task<User?> UpdateAsync(int id, User item) {
        await semaphore.WaitAsync();
        try {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null) {
                return null;
            }
            _dbContext.Entry(user).CurrentValues.SetValues(item);
            await _dbContext.SaveChangesAsync();
            return user;
        } finally {
            semaphore.Release();
        }
    }

    public async Task<User?> CreateAsync(User item) {
        await semaphore.WaitAsync();
        try {
            var user = await _dbContext.Users.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return user.Entity;
        } finally {
            semaphore.Release();
        }
    }

    public async Task<User?> FindAsync(int id) {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Users.FindAsync(id);
        } finally {
            semaphore.Release();
        }
    }

    public async Task<User?> FindOneAsync(Expression<Func<User, bool>> predicate) {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(predicate);
        } finally {
            semaphore.Release();
        }
    }

    public bool Any(Expression<Func<User, bool>> predicate) {
        return _dbContext.Users.AsNoTracking().Any(predicate);
    }

    public async Task<List<User>> IncludeListAsync<TProperty>(Expression<Func<User, TProperty>> navigationPropertyPath) {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Users.Include(navigationPropertyPath).AsNoTracking().ToListAsync();
        } finally {
            semaphore.Release();
        }
    }

    public async Task<User[]> FindAllAsync(Expression<Func<User, bool>> predicate) {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Users.Where(predicate).AsNoTracking().ToArrayAsync();
        } finally {
            semaphore.Release();
        }
    }
}