using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace efDataBase.Models.DTO
{
    public class SomeSortOfClientThatWantATrip
    {
        [NotNull]
        public string FirstName { get; set; }
        [NotNull]
        public string LastName { get; set; }
        [NotNull]
        public string Email { get; set; }
        [NotNull]
        public string Telephone { get; set; }
        [NotNull]
        public string Pesel { get; set; }
        [NotNull]
        public int IdTrip { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public DateTime? PaymentDate { get; set; }
    }
}
