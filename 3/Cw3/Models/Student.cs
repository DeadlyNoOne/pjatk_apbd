using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Index number is required")]
        [RegularExpression(@"^s{1}[0-9]{4}", ErrorMessage = "Wrong index number format")]
        public string IndexNumber { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Year is required")]
        public int YearNo { get; set; }

    }
}
