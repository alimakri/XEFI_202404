using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjetPowWeb.Models
{
    public class Todo
    {
        public int Id { get; set; }
        [DisplayName("Nom")]
        [Required(ErrorMessage = "Ce champ Nom est obligatoire.")]
        public string Name { get; set; }
        [DisplayName("Est fait")]
        public bool Fait { get; set; }
    }
}
