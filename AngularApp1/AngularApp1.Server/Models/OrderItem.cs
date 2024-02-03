using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;




namespace AngularApp1.Server.Models
{


    public class OrderItem
    {
        [Key]
        [JsonIgnore]
        public Guid OrderItemId { get; set; }

        [ForeignKey("OrderId")]
        [JsonIgnore]
        public Guid OrderId { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore]
        public Guid ProductId { get; set; }
    }


}
