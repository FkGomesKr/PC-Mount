namespace PCMount.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum OrderStatus { Done, Started, Pending, Canceled }

[Table("Orders")]
public class Order : ComputerComponents {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("OrderId")]
    public int OrderId { get; set; }

    [Required]
    [Column("Status")]
    [EnumDataType(typeof(OrderStatus))]
    public required OrderStatus? Status { get; set; }

    [Required]
    [Column("UserId")]
    [ForeignKey("UserId")]
    public required int UserId { get; set; }
    public User? Buyer { get; set; }
}