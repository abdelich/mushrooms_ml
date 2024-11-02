using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("stalk")]
public class Stalk
{
    [Key]
    [ForeignKey("Mushroom")]
    public int Id { get; set; }
    public string? StalkShape { get; set; }
    public string? StalkRoot { get; set; }
    public string? StalkSurfaceAboveRing { get; set; }
    public string? StalkSurfaceBelowRing { get; set; }
    public string? StalkColorAboveRing { get; set; }
    public string? StalkColorBelowRing { get; set; }
}
