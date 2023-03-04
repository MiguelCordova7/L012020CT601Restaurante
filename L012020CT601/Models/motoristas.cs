using System.ComponentModel.DataAnnotations;

namespace L012020CT601.Models
{
    public class motoristas
    {
        [Key]

        public int motoristaId { get; set; }

        public string? nombreMotorista { get; set; }


    }
}
