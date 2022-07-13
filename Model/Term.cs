using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Term : ITerm
    {
        [Display(Name = "Termo")]
        [Required] 
        public string Word { get; set; }
        [Display(Name = "Significado")]
        [Required] 
        public string Meaning { get; set; }


        public override string ToString()
        {
            return Word;
        }
    }
}
