using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileProject.Models
{
    public class GameCreate
    {
        [Required]
        [MinLength(1, ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }

        public string Comment { get; set; }
    }
}
