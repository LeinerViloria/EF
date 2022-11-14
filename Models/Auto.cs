using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Index(nameof(Placa), Name = "IX_e05001_1", IsUnique = true)]
    public class Auto : BaseMaster
    {
        [Required]
        [MaxLength(6)]
        public string Placa { get; set; }
        [Required]
        [MaxLength(15)]
        public string Marca { get; set; }
        [Required]
        public int Modelo { get; set; }
        [Required]
        [MaxLength(10)]
        public string Color { get; set; }
    }
}
