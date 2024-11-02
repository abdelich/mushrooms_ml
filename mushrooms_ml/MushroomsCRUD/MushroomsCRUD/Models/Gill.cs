using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("gill")]
public class Gill
{
    [Key]
    [ForeignKey("Mushroom")]
    public int Id { get; set; }
    public string GillAttachment { get; set; }
    public string GillSpacing { get; set; }
    public string GillSize { get; set; }
    public string GillColor { get; set; }
}
