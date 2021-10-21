using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgileProject.Models
{
    class GameDetail
    {
        [Required]
        [MinLength(1, ErrorMessage = "Please enter a title.")]
        public string Title { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Date Entered")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Date Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
