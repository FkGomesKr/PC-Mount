namespace PCMount.Services.Database;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PCMount.Data;
using PCMount.Data.Models;

public class InventarioService : IDbContextAcessor<Inventario> {
    private readonly ApplicationDbContext _dbContext;
    private static readonly SemaphoreSlim semaphore = new(1, 1);

    public InventarioService() {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        _ = optionsBuilder.UseSqlServer(DbContextConfig.ConnectionString);
        _dbContext = new ApplicationDbContext(optionsBuilder.Options);
    }

    public async Task<Inventario[]> GetArrayAsync() {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Inventario.AsNoTracking().ToArrayAsync();
        } finally {
            semaphore.Release();
        }
    }

    public async Task<List<Inventario>> GetListAsync() {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Inventario.AsNoTracking().ToListAsync();
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Inventario?> DeleteAsync(int id) {
        await semaphore.WaitAsync();
        try {
            var inventario = await _dbContext.Inventario.FindAsync(id);
            if (inventario == null) {
                return null;
            }
            _dbContext.Inventario.Remove(inventario);
            await _dbContext.SaveChangesAsync();
            return inventario;
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Inventario?> UpdateAsync(int id, Inventario item) {
        await semaphore.WaitAsync();
        try {
            var inventario = await _dbContext.Inventario.FindAsync(id);
            if (inventario == null) {
                return null;
            }
            _dbContext.Entry(inventario).CurrentValues.SetValues(item);
            await _dbContext.SaveChangesAsync();
            return inventario;
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Inventario?> CreateAsync(Inventario item) {
        await semaphore.WaitAsync();
        try {
            var inventario = await _dbContext.Inventario.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return inventario.Entity;
        } finally {
            semaphore.Release();
        }
    }

    public bool Any(Expression<Func<Inventario, bool>> predicate) {
        semaphore.Wait();
        try {
            return _dbContext.Inventario.Any(predicate);
        } finally {
            semaphore.Release();
        }
    }

    public async Task<List<Inventario>> IncludeListAsync<TProperty>(Expression<Func<Inventario, TProperty>> navigationPropertyPath) {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Inventario
                .AsNoTracking()
                .Include(navigationPropertyPath) // Dynamically include related entities
                .ToListAsync();
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Inventario?> FindAsync(int id) {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Inventario.FindAsync(id);
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Inventario?> FindOneAsync(Expression<Func<Inventario, bool>> predicate) {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Inventario.AsNoTracking().FirstOrDefaultAsync(predicate);
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Inventario[]> FindAllAsync(Expression<Func<Inventario, bool>> predicate) {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Inventario.Where(predicate).AsNoTracking().ToArrayAsync();
        } finally {
            semaphore.Release();
        }
    }

    // Custom queries
    public async Task<Inventario?> UpdateItemQuantityAsync(int id, int quantity) {
        await semaphore.WaitAsync();
        try {
            var inventario = await _dbContext.Inventario.FindAsync(id);
            if (inventario == null) {
                return null;
            }
            inventario.Quantidade = quantity;
            await _dbContext.SaveChangesAsync();
            return inventario;
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Inventario?> TransactionStockItemAsync(int id, int quantity) {
        await semaphore.WaitAsync();
        try {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try {
                var inventoryItem = await _dbContext.Inventario.FirstOrDefaultAsync(i => i.PartId == id);
                if (inventoryItem != null) {
                    // Update the existing item's quantity
                    inventoryItem.Quantidade += quantity;
                } else {
                    // Create a new inventory item
                    inventoryItem = new Inventario {
                        PartId = id,
                        Quantidade = quantity
                    };
                    await _dbContext.Inventario.AddAsync(inventoryItem);
                }
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return inventoryItem;
            } catch (Exception e) {
                Console.WriteLine($"An error occurred: {e.Message}");
                await transaction.RollbackAsync();
                return null;
            }
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Inventario?> UpdateFirstOrDefaultAsync(int PartId, int newQuantidade) {
        await semaphore.WaitAsync();
        try {
            var inventario = await _dbContext.Inventario.FirstOrDefaultAsync(i => i.PartId == PartId);
            if (inventario == null) {
                return null;
            }
            inventario.Quantidade = newQuantidade;
            await _dbContext.SaveChangesAsync();
            return inventario; 
        } finally {
            semaphore.Release();
        }
    }
}