using System.ComponentModel.DataAnnotations;

namespace HancerliMarket.DataModels.Models;

public class BasketModel
{
    [Key]
    public Guid Guid { get; set; }
    public int Id { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Adet { get; set; }
}
