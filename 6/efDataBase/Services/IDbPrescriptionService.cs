using efDataBase.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace efDataBase.Services
{
    public interface IDbPrescriptionService
    {
        Task<SomeSortOfPrescription> GetPrescription(int id);
        
    }
}
