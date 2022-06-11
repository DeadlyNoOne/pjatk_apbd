using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Numer indeksu jest wymagany")]
        [RegularExpression(@"^s{1}[0-9]{4}", ErrorMessage = "Błędny format numeru indeksu")]
        public string IndexNumber { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Numer roku jest wymagany")]
        public int NrRoku { get; set; }

    }
}
