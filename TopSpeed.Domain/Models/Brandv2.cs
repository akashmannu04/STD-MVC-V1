using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Domain.Common;

namespace TopSpeed.Domain.Models
{
    public class Brandv2:BaseModel
    {
       
        [Required]
        public string Name { get; set; } //= string.Empty;   
        public int EstablishedYear { get; set; }
        public string BrandLogo { get; set; }
    }
}
