using GroupSpace2023.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupSpace2023.Models
{
    public class Parameter
    {
        [Key]
        public string Name { get; set; }

        public string Value { get; set; }

        [ForeignKey ("GroupSpace2023User")]
        public string UserId {  get; set; }

        public DateTime Changed { get; set; }
    }
}
