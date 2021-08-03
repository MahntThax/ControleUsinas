using ControleUsinas.Helpers;
using System.ComponentModel.DataAnnotations;

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

        [RequiredHelper]
        public string Fornecedor { get; set; }

        [RequiredHelper]
        [Display(Name = "UC da usina")]
        public string UC { get; set; }
    }
}
