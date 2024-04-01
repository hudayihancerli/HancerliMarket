using System.ComponentModel.DataAnnotations;

namespace HancerliMarket.DataModels.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
