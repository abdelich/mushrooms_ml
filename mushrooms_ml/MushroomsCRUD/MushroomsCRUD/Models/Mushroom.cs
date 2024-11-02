using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("mushroom")]
public class Mushroom
{
    [Key]
    public int Id { get; set; }
    public string CapShape { get; set; }
    public string CapSurface { get; set; }
    public string CapColor { get; set; }
    public string Bruises { get; set; }
    public string Odor { get; set; }
    public string VeilType { get; set; }
    public string VeilColor { get; set; }
    public string RingNumber { get; set; }
    public string RingType { get; set; }
    public string SporePrintColor { get; set; }
    public string Population { get; set; }
    public string Habitat { get; set; }
    public string Poisonous { get; set; }
    public Gill Gill { get; set; }
    public Stalk Stalk { get; set; }
}
