namespace PCMount.Services.Database;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PCMount.Data;
using PCMount.Data.Models;

public class OrdersService : IDbContextAcessor<Order> {
    private readonly ApplicationDbContext _dbContext;
    private static readonly SemaphoreSlim semaphore = new(1, 1); // Semaphore to ensure single access to DbContext

    public OrdersService() {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        _ = optionsBuilder.UseSqlServer(DbContextConfig.ConnectionString);
        _dbContext = new ApplicationDbContext(optionsBuilder.Options);
    }

    public async Task<Order[]> GetArrayAsync() {
        await semaphore.WaitAsync(); // Wait for the lock to be available
        try {
            return await _dbContext.Orders.AsNoTracking().ToArrayAsync();
        } finally {
            semaphore.Release(); // Release the lock
        }
    }

    public async Task<List<Order>> GetListAsync() {
        await semaphore.WaitAsync(); // Wait for the lock to be available
        try {
            return await _dbContext.Orders.AsNoTracking().ToListAsync();
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Order?> DeleteAsync(int id) {
        await semaphore.WaitAsync(); // Wait for the lock to be available
        try {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null) {
                return null;
            }
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
            return order;
        } finally {
            semaphore.Release(); // Release the lock
        }
    }

    public async Task<Order?> UpdateAsync(int id, Order item) {
        await semaphore.WaitAsync(); // Wait for the lock to be available
        try {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null) {
                return null;
            }
            _dbContext.Entry(order).CurrentValues.SetValues(item);
            await _dbContext.SaveChangesAsync();
            return order;
        } finally {
            semaphore.Release(); // Release the lock
        }
    }

    public async Task<Order?> CreateAsync(Order item) {
        await semaphore.WaitAsync(); // Wait for the lock to be available
        try {
            var order = await _dbContext.Orders.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return order.Entity;
        } finally {
            semaphore.Release();
        }
    }

    public bool Any(Expression<Func<Order, bool>> predicate) {
        return _dbContext.Orders.AsNoTracking().Any(predicate);
    }

    public async Task<List<Order>> IncludeListAsync<TProperty>(Expression<Func<Order, TProperty>> navigationPropertyPath) {
        await semaphore.WaitAsync();
        try {
            return await _dbContext.Orders.Include(navigationPropertyPath).AsNoTracking().ToListAsync();
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Order?> FindAsync(int id) {
        await semaphore.WaitAsync(); // Wait for the lock to be available
        try {
            return await _dbContext.Orders.FindAsync(id);
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Order?> FindOneAsync(Expression<Func<Order, bool>> predicate) {
        await semaphore.WaitAsync(); // Wait for the lock to be available
        try {
            return await _dbContext.Orders.AsNoTracking().FirstOrDefaultAsync(predicate);
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Order[]> FindAllAsync(Expression<Func<Order, bool>> predicate) {
        await semaphore.WaitAsync(); // Wait for the lock to be available
        try {
            return await _dbContext.Orders.Where(predicate).AsNoTracking().ToArrayAsync();
        } finally {
            semaphore.Release();
        }
    }

    // Custom queries
    public async Task<List<(Part Part, int Quantity)>> GetPartsForOrderAsync(Order order) {
        await semaphore.WaitAsync();
        try {
            var partsWithQuantities = new List<(Part Part, int Quantity)>();
            async Task AddPartWithQuantityAsync(int? partId) {
                if (!partId.HasValue) return;
                // Fetch the part from the Componentes table
                var part = await _dbContext.Componentes.FindAsync(partId);
                if (part != null) {
                    // Check the quantity in the Inventario table
                    var inventory = await _dbContext.Inventario.FindAsync(part.PartId);
                    int quantity = inventory?.Quantidade ?? 0;
                    partsWithQuantities.Add((part, quantity));
                }
            }
            // Add parts to the list
            await AddPartWithQuantityAsync(order.MotherboardId);
            await AddPartWithQuantityAsync(order.ProcessorId);
            await AddPartWithQuantityAsync(order.MemoryId);
            await AddPartWithQuantityAsync(order.StorageId);
            await AddPartWithQuantityAsync(order.GraphicsCardId);
            await AddPartWithQuantityAsync(order.PowerSupplyId);
            await AddPartWithQuantityAsync(order.CaseId);
            await AddPartWithQuantityAsync(order.CoolingId);
            return partsWithQuantities;
        } finally {
            semaphore.Release();
        }
    }

    public async Task<Order[]> GetPendingOrdersAsync() {
        await semaphore.WaitAsync(); // Wait for the lock to be available
        try {
            // Fetch all orders with Status = Pending
            return await _dbContext.Orders
                .Where(o => o.Status == OrderStatus.Pending)
                .AsNoTracking()
                .ToArrayAsync();
        } finally {
            semaphore.Release(); // Release the lock
        }
    }

    public async Task<Order?> UpdateOrderStatusToDoneAsync(int orderId) {
        await semaphore.WaitAsync(); 
        try {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null) {
                return null; 
            }
            order.Status = OrderStatus.Done;
            await _dbContext.SaveChangesAsync();
            return order; 
        } finally {
            semaphore.Release();
        }
    }

    public async Task<List<(Order Order, User User)>> GetOrdersWithUsersAsync() {
        await semaphore.WaitAsync(); 
        try {
            
            var ordersWithUsers = await _dbContext.Orders
                .Join(
                    _dbContext.Users, 
                    order => order.UserId, 
                    user => user.Id,
                    (order, user) => new { Order = order, User = user } 
                )
                .AsNoTracking()
                .ToListAsync();

            // Convert the anonymous type to a list of tuples
            return ordersWithUsers.Select(x => (x.Order, x.User)).ToList();
        } finally {
            semaphore.Release(); // Release the lock
        }
    }
}