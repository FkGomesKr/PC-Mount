namespace PCMount.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PCMount.Data.Models;

public static class DbContextConfig {
    private static string? _connectionString;

    public static void Initialize(IConfiguration configuration) {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public static string ConnectionString {
        get {
            if (string.IsNullOrEmpty(_connectionString))
                throw new InvalidOperationException("Connection string is not initialized. Call Initialize() first.");
            return _connectionString;
        }
    }
}

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options) {
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        // Pre-populate the table with data
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, UserName = "admin", Password = PasswordHasher.HashPassword("admin"), Role = "Admin" }
        );
        modelBuilder.Entity<Part>().HasData(
            new Part { PartId = 1, Name = "Intel Core i9-14900K",  Preco = 599.99,  Tipo = PartTipo.Processor, Descricao = "The Intel Core i9-14900K is a high-performance 14th-generation desktop processor designed for demanding workloads. With a base clock speed of 3.2GHz and a turbo boost up to 6.0GHz, it offers exceptional performance for gaming, content creation, and multitasking. Featuring multiple cores and threads, advanced overclocking capabilities, and support for DDR5 and PCIe 5.0, it ensures seamless performance for the most intensive applications. Perfect for enthusiasts and professionals seeking top-tier computing power.", Image = "/assets/cdn/part-intelcore-i9-14900k.png", PortId = 1 },
            new Part { PartId = 2, Name = "NVIDIA GeForce RTX 4090", Preco = 2189.69, Tipo = PartTipo.GraphicsCard, Descricao = "The NVIDIA GeForce RTX 4090 is the pinnacle of gaming and creative performance, powered by the advanced NVIDIA Ada Lovelace architecture. With an unprecedented level of GPU power, it delivers stunning 4K and beyond gaming experiences, lightning-fast ray tracing, and AI-enhanced graphics through DLSS 3. Equipped with 24GB of GDDR6X memory, it handles the most demanding games and creative applications with ease. Perfect for gamers, 3D artists, and professionals seeking unmatched graphics performance and efficiency.", Image = "/assets/cdn/part-nvidia-rtx4090.png", PortId = 2 },
            new Part { PartId = 3, Name = "ASUS ROG Strix Z590-E", Preco = 300, Tipo = PartTipo.Motherboard, Descricao = "Motherboard ATX Intel® Z590 LGA1200 com PCIe® 4.0, 14+2 fases de energia emparelhadas, AI Noise Cancelation Bidirecional, AI Overclocking, AI Cooling, AI Netrworking, WiFi 6E (802.11ax), Dual Ethernet Intel® 2.5Gb, quatro ranhuras M.2 com dissipadores, USB 3.2 Gen, SATA e iluminação Aura Sync RGB", Image = "/assets/cdn/part-asus-rog-strix-z590.png", PortId = 2 },
            new Part { PartId = 4, Name = "NVIDIA GeForce RTX 3080", Preco = 800, Tipo = PartTipo.GraphicsCard, Descricao = "A intenção da NVIDIA é fazer-nos voar nos jogos com as suas GeForce RTX 3080 e RTX 3080 Ti, equipadas com 10 e 12 GB de GDDR6X, respetivamente, e uma potência que te leva ao Ray Tracing em 4K a mais de 60 FPS; de facto, a RTX 3080 Ti possui a mesma GPU que a RTX 3090. Na PcComponentes, tens à tua disposição modelos personalizados que vão ao encontro de todas as tuas expectativas em qualquer jogo AAA.", Image = "/assets/cdn/part-nvidia-rtx-3080.png", PortId = 3 },
            new Part { PartId = 5, Name = "Intel Core i9-11900K", Preco = 529.99, Tipo = PartTipo.Processor, Descricao = "Apresentamos o processador para desktop Intel® Core™ i9-11900K desbloqueado, de 11ª Geração. Com a tecnologia Intel® Turbo Boost Max 3.0 e suporte PCIe Gen 4.0, os processadores para desktop Intel® Core™ 11th Gen desbloqueados são otimizados para os jogadores entusiastas e construtores sérios e ajudam a proporcionar um overclocking de alto desempenho para um impulso extra.", Image = "/assets/cdn/part-intel-i9-11900k.png", PortId = 4 },
            new Part { PartId = 6, Name = "Corsair Vengeance RGB Pro 16GB", Preco = 87, Tipo = PartTipo.Memory, Descricao = "A memória DDR4 VENGEANCE RGB PRO Series da CORSAIR com overclocking ilumina seu PC com luzes RGB dinâmicas multizona enquanto oferece o melhor desempenho.", Image = "/assets/cdn/part-vengeance-rgbpro.png", PortId = 5 },
            new Part { PartId = 7, Name = "Samsung 970 EVO Plus", Preco = 164, Tipo = PartTipo.Storage, Descricao = "Apresentamos o Samsung 970 EVO Plus, um disco SSD com a mais recente tecnologia de armazenamento. O 970 EVO Plus é mais rápido que o seu antecessor, o 970 EVO, alcançando velocidades de gravação e leitura até 3500 MB/s.", Image = "/assets/cdn/part-samsung-970-ssd.png", PortId = 6 },
            new Part { PartId = 8, Name = "Corsair RM850x", Preco = 189.13, Tipo = PartTipo.PowerSupply, Descricao = "As fontes de alimentação totalmente modulares CORSAIR RM750x Series foram projetadas com componentes de alta qualidade para fornecer energia com a eficiência da certificação 80 PLUS Gold ao seu PC e para operar silenciosamente.", Image = "/assets/cdn/part-corsair-rm850x.png", PortId = 7 },
            new Part { PartId = 9, Name = "NZXT H510 Case", Preco = 100, Tipo = PartTipo.Case, Descricao = "As caixas ATX mid-tower compactas H510 são impressionantes e oferecem excelente valor. Um suporte de montagem de radiador removível, janela de vidro temperado, vários filtros de ventoinhas e USB 3.1 Gen 2 tipo C frontal são apenas algumas das comodidades.", Image = "/assets/cdn/part-atx-nzxt-case.png", PortId = 8 },
            new Part { PartId = 10, Name = "MGC 50 Black Tower", Preco = 100, Tipo = PartTipo.Case, Descricao = "", Image = "/assets/cdn/part-mgc-50-tower.png", PortId = 9 },
            new Part { PartId = 11, Name = "Corsair 4000D Case", Preco = 100, Tipo = PartTipo.Case, Descricao = "", Image = "/assets/cdn/part-corsair4000d.png", PortId = 10 },
            new Part { PartId = 12, Name = "Lian-Li ROG Edition White Tower", Preco = 212, Tipo = PartTipo.Case, Descricao = "", Image = "/assets/cdn/part-rog-tower-atx.png", PortId = 11 },
            new Part { PartId = 13, Name = "Fractal Design Meshify", Preco = 141.32, Tipo = PartTipo.Case, Descricao = "", Image = "/assets/cdn/part-fractal-design.png", PortId = 12 },
            new Part { PartId = 14, Name = "Phanteks Eclipse P500A", Preco = 177.21, Tipo = PartTipo.Case, Descricao = "", Image = "/assets/cdn/part-phanteks-eclipse.png", PortId = 13 },
            new Part { PartId = 15, Name = "ASUS TUF Gaming GT501", Preco = 237.99, Tipo = PartTipo.Case, Descricao = "", Image = "/assets/cdn/part-atx-asuscase.png", PortId = 14 },
            new Part { PartId = 16, Name = "Cooler Master", Preco = 57, Tipo = PartTipo.Cooling, Descricao = "", Image = "/assets/cdn/part-cooler.png", PortId = 14 },
            new Part { PartId = 17, Name = "NVIDIA GeForce RTX 4060", Preco = 384.69, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-nvidia-rtx4060.png", PortId = 15 },
            new Part { PartId = 18, Name = "NVIDIA GeForce RTX 3060", Preco = 318.89, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-nvidia-rtx3060.png", PortId = 16 },
            new Part { PartId = 19, Name = "NVIDIA GeForce RTX 3050", Preco = 207.99, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-nvidia-rtx3050.png", PortId = 17 },
            new Part { PartId = 20, Name = "NVIDIA GeForce RTX 1060", Preco = 107.99, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-nvidia-rtx1060.png", PortId = 18 },
            new Part { PartId = 21, Name = "GIGABYTE Radeon Rx 7600 Gaming", Preco = 307.99, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-radeon-rx7600.png", PortId = 19 },
            new Part { PartId = 22, Name = "NVIDIA GeForce GTX 1060", Preco = 462.34, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-nvidia-gtx1660.png", PortId = 20 },
            new Part { PartId = 23, Name = "ZotacGaming GeForce RTX 4070TI", Preco = 872.61, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-zotac-rtx4070.png", PortId = 21 },
            new Part { PartId = 24, Name = "ZotacGaming GeForce RTX 3060", Preco = 435.12, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-zotac-rtx3060.png", PortId = 22 },
            new Part { PartId = 25, Name = "Asrock Taichi Radeon RX7900", Preco = 936.78, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-asrock-rx7900.png", PortId = 23 },
            new Part { PartId = 26, Name = "Asrock Radeon RX7700 XT", Preco = 444.00, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-asrock-rx7700.png", PortId = 24 },
            new Part { PartId = 27, Name = "Asus GeForce GT 730", Preco = 99.99, Tipo = PartTipo.GraphicsCard, Descricao = "", Image = "/assets/cdn/part-asus-gt730.png", PortId = 25 },
            new Part { PartId = 28, Name = "Intel Core i5-12600K", Preco = 199.99, Tipo = PartTipo.Processor, Descricao = "", Image = "/assets/cdn/part-intel-i5-12600k.png", PortId = 26 },
            new Part { PartId = 29, Name = "AMD Ryzen 5 3600", Preco = 99.99, Tipo = PartTipo.Processor, Descricao = "", Image = "/assets/cdn/part-amd-ryzen-5-3600.png", PortId = 26 },
            new Part { PartId = 30, Name = "AMD Ryzen 9 5950", Preco = 403.23, Tipo = PartTipo.Processor, Descricao = "", Image = "/assets/cdn/part-amd-ryzen-9-5950.png", PortId = 26 },
            new Part { PartId = 31, Name = "Intel Core i7-10700", Preco = 293.99, Tipo = PartTipo.Processor, Descricao = "", Image = "/assets/cdn/part-intel-i7-10700.png", PortId = 26 },
            new Part { PartId = 32, Name = "Intel Core i7-13700", Preco = 367.90, Tipo = PartTipo.Processor, Descricao = "", Image = "/assets/cdn/part-intel-i7-13700.png", PortId = 26 },
            new Part { PartId = 33, Name = "AMD Ryzen 7 5800", Preco = 198.90, Tipo = PartTipo.Processor, Descricao = "", Image = "/assets/cdn/part-amd-ryzen-7-5800.png", PortId = 26 },
            new Part { PartId = 34, Name = "Intel i3-14100", Preco = 95.95, Tipo = PartTipo.Processor, Descricao = "", Image = "/assets/cdn/part-i3-14100.png", PortId = 26 },
            new Part { PartId = 35, Name = "AMD Ryzen 3 3200", Preco = 75.23, Tipo = PartTipo.Processor, Descricao = "", Image = "/assets/cdn/part-amd-ryzen-3-3200.png", PortId = 26 },
            new Part { PartId = 36, Name = "Aorus ELITE B550M", Preco = 134.12, Tipo = PartTipo.Motherboard, Descricao = "", Image = "/assets/cdn/part-aorus-elite-b550m.png", PortId = 2 },
            new Part { PartId = 37, Name = "Asus  Prime A520M-K", Preco = 74.67, Tipo = PartTipo.Motherboard, Descricao = "", Image = "/assets/cdn/part-asus-a520m.png", PortId = 2 },
            new Part { PartId = 38, Name = "MSI MEG  X870", Preco = 1434.17, Tipo = PartTipo.Motherboard, Descricao = "", Image = "/assets/cdn/part-msi-meg-x870e.png", PortId = 2 },
            new Part { PartId = 39, Name = "AsRock X670E Taichi", Preco = 889.99, Tipo = PartTipo.Motherboard, Descricao = "", Image = "/assets/cdn/part-asrock-x670e.png", PortId = 2 },
            new Part { PartId = 40, Name = "AsRock Creator R2.0", Preco = 756.42, Tipo = PartTipo.Motherboard, Descricao = "", Image = "/assets/cdn/part-asrock-creator.png", PortId = 2 },
            new Part { PartId = 41, Name = "ROG STRIX Z790-A", Preco = 443.36, Tipo = PartTipo.Motherboard, Descricao = "", Image = "/assets/cdn/part-rog-strix-z790.png", PortId = 2 },
            new Part { PartId = 42, Name = "MSI MPG X670E", Preco = 468.99, Tipo = PartTipo.Motherboard, Descricao = "", Image = "/assets/cdn/part-msi-mpg-x670e.png", PortId = 2 },
            new Part { PartId = 43, Name = "MSI MAG X570S", Preco = 406.13, Tipo = PartTipo.Motherboard, Descricao = "", Image = "/assets/cdn/part-msi-mag-x570s.png", PortId = 2 },
            new Part { PartId = 44, Name = "Kingston FURY DDR4 3200 16GB", Preco = 63.21, Tipo = PartTipo.Memory, Descricao = "", Image = "/assets/cdn/part-kingston-fury-ddr4.png", PortId = 5 },
            new Part { PartId = 45, Name = "Corsair Dominator Titanium DDR5 64GB", Preco = 313.45, Tipo = PartTipo.Memory, Descricao = "", Image = "/assets/cdn/part-corsair-dominator.png", PortId = 5 },
            new Part { PartId = 46, Name = "Corsair Vengeance RGB 64GB", Preco = 203.21, Tipo = PartTipo.Memory, Descricao = "", Image = "/assets/cdn/part-vengeance rgb-64GB.png", PortId = 5 },
            new Part { PartId = 47, Name = "Lenovo DDR4 8GB", Preco = 73.34, Tipo = PartTipo.Memory, Descricao = "", Image = "/assets/cdn/part-lenovo-ddr48gb.png", PortId = 5 },
            new Part { PartId = 48, Name = "GSkill Trident Z5 32GB", Preco = 183.4, Tipo = PartTipo.Memory, Descricao = "", Image = "/assets/cdn/part-gskill-z5.png", PortId = 5 },
            new Part { PartId = 49, Name = "Kingston SATA 2.5 960 GB", Preco = 84.99, Tipo = PartTipo.Storage, Descricao = "", Image = "/assets/cdn/part-ssd-kingston-sata.png", PortId = 6 },
            new Part { PartId = 50, Name = "Kingston NV2 1TB", Preco = 99.99, Tipo = PartTipo.Storage, Descricao = "", Image = "/assets/cdn/part-kingston-nv2-1tb.png", PortId = 6 },
            new Part { PartId = 51, Name = "Integral V Series V2 480GB", Preco = 39.99, Tipo = PartTipo.Storage, Descricao = "", Image = "/assets/cdn/part-integral-v-480gb.png", PortId = 6 },
            new Part { PartId = 52, Name = "M2 Verbatim de 512 GB", Preco = 79.99, Tipo = PartTipo.Storage, Descricao = "", Image = "/assets/cdn/part-m2-verbatim-512gb.png", PortId = 6 },
            new Part { PartId = 53, Name = "Western Digital 2,5 240 GB", Preco = 39.99, Tipo = PartTipo.Storage, Descricao = "", Image = "/assets/cdn/part-western-240gb.png", PortId = 6 },
            new Part { PartId = 54, Name = "Kingston M.2 4,096 TB", Preco = 1019.99, Tipo = PartTipo.Storage, Descricao = "", Image = "/assets/cdn/part-kingstom-4tb.png", PortId = 6 },
            new Part { PartId = 55, Name = "Corsair CX750", Preco = 79.99, Tipo = PartTipo.PowerSupply, Descricao = "", Image = "/assets/cdn/part-corsair-cx750.png", PortId = 7 },
            new Part { PartId = 56, Name = "Nfortec Sagitta X 1000W", Preco = 147.99, Tipo = PartTipo.PowerSupply, Descricao = "", Image = "/assets/cdn/part-nfortec.png", PortId = 7 },
            new Part { PartId = 57, Name = "Tempest Gaming GPSU 650W", Preco = 26.39, Tipo = PartTipo.PowerSupply, Descricao = "", Image = "/assets/cdn/part-tempest-650w.png", PortId = 7 },
            new Part { PartId = 58, Name = "Water Cooler Master (X2)", Preco = 78.21, Tipo = PartTipo.Cooling, Descricao = "", Image = "/assets/cdn/part-cooler2.png", PortId = 14 },
            new Part { PartId = 59, Name = "Water Cooler Master (X1)", Preco = 51.99, Tipo = PartTipo.Cooling, Descricao = "", Image = "/assets/cdn/part-cooler3.png", PortId = 14 },
            new Part { PartId = 60, Name = "Atmos Cooler Kit", Preco = 129.99, Tipo = PartTipo.Cooling, Descricao = "", Image = "/assets/cdn/part-cooler4.png", PortId = 14 }
        );
        modelBuilder.Entity<Inventario>().HasData(
            new Inventario { PartId = 1, Quantidade = 10 },
            new Inventario { PartId = 2, Quantidade = 10 },
            new Inventario { PartId = 3, Quantidade = 10 },
            new Inventario { PartId = 4, Quantidade = 10 },
            new Inventario { PartId = 5, Quantidade = 10 },
            new Inventario { PartId = 6, Quantidade = 10 },
            new Inventario { PartId = 7, Quantidade = 10 },
            new Inventario { PartId = 8, Quantidade = 10 }
        );
        modelBuilder.Entity<Computer>().HasData(
            new Computer { Id = 1, Name = "NZXT H5 Gaming Elite", Price = 1300, Image = "/assets/cdn/part-atx-nzxt-case.png", Discount = 10, MotherboardId = 3, GraphicsCardId = 4, ProcessorId = 5, MemoryId = 6, StorageId = 7, PowerSupplyId = 8, CaseId = 9, CoolingId = 16 },
            new Computer { Id = 2, Name = "MGC-50 Gaming", Price = 2368, Image = "/assets/cdn/part-mgc-50-tower.png", MotherboardId = 41, GraphicsCardId = 24, ProcessorId = 32, MemoryId = 46, StorageId = 50, PowerSupplyId = 8, CaseId = 10, CoolingId = 60 },
            new Computer { Id = 3, Name = "Desktop Corsair 4000", Price = 1870, Image = "/assets/cdn/part-corsair4000d.png", Discount = 25, MotherboardId = 36, GraphicsCardId = 19, ProcessorId = 35, MemoryId = 45, StorageId = 7, PowerSupplyId = 55, CaseId = 11, CoolingId = 59 },
            new Computer { Id = 4, Name = "Lian-Li Desktop Edition", Price = 1460, Image = "/assets/cdn/part-rog-tower-atx.png", MotherboardId = 3, GraphicsCardId = 4, ProcessorId = 32, MemoryId = 44, StorageId = 51, PowerSupplyId = 57, CaseId = 12, CoolingId = 58 },
            new Computer { Id = 5, Name = "Fractal Meshify Desktop", Price = 1230, Image = "/assets/cdn/part-fractal-design.png", MotherboardId = 3, GraphicsCardId = 4, ProcessorId = 31, MemoryId = 47, StorageId = 52, PowerSupplyId = 55, CaseId = 13, CoolingId = 16 },
            new Computer { Id = 6, Name = "ASUS TUF Gaming", Price = 2100, Image = "/assets/cdn/part-atx-asuscase.png", MotherboardId = 37, GraphicsCardId = 22, ProcessorId = 29, MemoryId = 48, StorageId = 50, PowerSupplyId = 56, CaseId = 15, CoolingId = 58 }
        );
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Part> Componentes { get; set; }
    public DbSet<Inventario> Inventario { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Computer> Computers { get; set; }
}