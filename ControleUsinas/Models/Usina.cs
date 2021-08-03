using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControleUsinas.Models
{
    public class Usina
    {
        public Usina()
        {
            this.Ativo = true;
        }

        public int Id { get; set; }

        public bool Ativo { get; set; }

        [Required]
        public string Fornecedor { get; set; }

        [Required]
        [Display(Name = "UC da usina")]
        public string UC { get; set; }
    }
}
