namespace PCMount.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ComputerComponents {
    [Required]
    [Column("Name")]
    [StringLength(45)]
    public required string Name { get; set; }

    [Required]
    [Column("Price")]
    [DataType(DataType.Currency)]
    public required double Price { get; set; }

    [Column("Discount")]
    public double? Discount { get; set; }

    [Required]
    [Column("Image")]
    [StringLength(255)]
    public required string Image { get; set; }

    [Column("MotherboardId")]
    [ForeignKey("Motherboard")]
    public int? MotherboardId { get; set; }

    [Column("ProcessorId")]
    [ForeignKey("Processor")]
    public int? ProcessorId { get; set; }

    [Column("MemoryId")]
    [ForeignKey("Memory")]
    public int? MemoryId { get; set; }

    [Column("StorageId")]
    [ForeignKey("Storage")]
    public int? StorageId { get; set; }

    [Column("GraphicsCardId")]
    [ForeignKey("GraphicsCard")]
    public int? GraphicsCardId { get; set; }

    [Column("PowerSupplyId")]
    [ForeignKey("PowerSupply")]
    public int? PowerSupplyId { get; set; }

    [Column("CaseId")]
    [ForeignKey("Case")]
    public int? CaseId { get; set; }

    [Column("CoolingId")]
    [ForeignKey("Cooling")]
    public int? CoolingId { get; set; }

    // Navigation properties
    public Part? Motherboard { get; set; }
    public Part? Processor { get; set; }
    public Part? Memory { get; set; }
    public Part? Storage { get; set; }
    public Part? GraphicsCard { get; set; }
    public Part? PowerSupply { get; set; }
    public Part? Case { get; set; }
    public Part? Cooling { get; set; }
}