﻿using System.ComponentModel.DataAnnotations;

namespace L012020CT601.Models
{
    public class clientes
    {
       [Key]

        public int clienteId { get; set; }

        public string? nombreCliente { get; set; }

        public string? direccion { get; set; }
    }
}
