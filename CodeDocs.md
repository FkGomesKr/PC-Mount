Makes a table in the database on confifguaration of the database context:
```csharp
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        // Create the table for the User model
        modelBuilder.Entity<User>().ToTable("users");
        // Pre-populate the table with data
```

```csharp
// Optional: Services used in .NET 6 (not needed in .NET 8)
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("Data Source=app.db"));
// Add Logging
builder.Services.AddLogging();
//app.UseHttpsRedirection(); // If the app is being served over HTTPS, uncomment this line
// Example Api Routes
app.MapGet("/api/v1/hello", () => "Hello World, from API!");
```