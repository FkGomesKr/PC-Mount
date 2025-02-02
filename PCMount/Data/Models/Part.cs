namespace PCMount.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum PartTipo { Processor, Motherboard, GraphicsCard, Memory, Storage, PowerSupply, Case, Cooling }

[Table("Componentes")]
public class Part {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PartId")]
    public int PartId { get; set; }

    [Required]
    [Column("Name")]
    [StringLength(45)]
    public required string Name { get; set; }

    [Required]
    [Column("Preco")]
    [DataType(DataType.Currency)]
    public required double Preco { get; set; }

    [Required]
    [Column("Tipo")]
    [EnumDataType(typeof(PartTipo))]
    public required PartTipo Tipo { get; set; }

    [Column("Descricao")]
    [StringLength(600)]
    public string? Descricao { get; set; }

    [Column("Image")]
    [StringLength(100)]
    public string? Image { get; set; }

    [Column("PortId")]
    [ForeignKey("PortId")]
    public int? PortId { get; set; }
}