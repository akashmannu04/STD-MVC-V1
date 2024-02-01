using System.ComponentModel.DataAnnotations;

namespace TopSpeed.Web.Models
{
    public class Brandv2
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } //= string.Empty;   
        public int EstablishedYear {  get; set; }
        public string BrandLogo {  get; set; }
    }
}
