using System.ComponentModel.DataAnnotations;

namespace L012020CT601.Models
{
    public class platos
    {
        [Key]

        public int platoId { get; set; }

        public string? nombrePlato { get; set; }

        public decimal precio { get; set; }


    }
}
