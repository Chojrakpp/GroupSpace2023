using System.ComponentModel.DataAnnotations;

namespace GroupSpace2023.Models
{
    public class Groep
    {
        public int Id { get; set; }

        [Display (Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Omschrijving")]
        public string Description { get; set; }
        [Display(Name = "Groep Aangemaakt")]

        [DataType(DataType.Date)]
        public DateTime Started { get; set; } = DateTime.Now;
        [Display(Name = "Groep Gestopt")]

        [DataType (DataType.Date)]
        public DateTime Ended { get; set;} = DateTime.MaxValue;

    }
}
