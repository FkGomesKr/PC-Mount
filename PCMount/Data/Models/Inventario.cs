namespace PCMount.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Inventario")] // Explicitly map to the Inventario table
public class Inventario {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ForeignKey("Part")] // Explicitly define the foreign key
    [Column("PartId")] // Maps to PartId in the table
    public int PartId { get; set; } // Foreign and primary key to Componente

    [Required]
    [Column("Quantidade")]
    public required int Quantidade { get; set; }

    // Navigation property
    public Part? Part { get; set; }
}