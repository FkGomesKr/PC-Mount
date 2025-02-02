namespace PCMount.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Users")]
public class User {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("Id")]
    public int Id { get; set; } // Maps to id in the table

    [Required]
    [Column("Username")]
    [StringLength(100)]
    public required string UserName { get; set; }

    [Required]
    [Column("Password")]
    [StringLength(100)]
    public required string Password { get; set; }

    [Required]
    [Column("Role")]
    [StringLength(100)]
    public required string Role { get; set; }
}