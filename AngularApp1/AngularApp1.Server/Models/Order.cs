using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;


namespace AngularApp1.Server.Models
{


    public class Order
    {
        [Key]
        [JsonIgnore]
        public Guid OrderId { get; set; }

        [ForeignKey("Id")]
        [JsonIgnore]
        public Guid UserId { get; set; }

        public DateTime? Date { get; set; }

        [Required]
        public float TotalPrice { get; set; }

        public string? Address { get; set; }

        public bool? Status { get; set; }
        //0 - daca nu s a realizat comanda, 1 daca da
    }
}
