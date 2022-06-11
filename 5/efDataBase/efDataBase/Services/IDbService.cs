using efDataBase.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace efDataBase.Services
{
    public interface IDbService
    {
        Task<IEnumerable<SomeSortOfTrip>> GetTrips();
        Task<string> DeleteClient(int id);
        Task<string> AddClientToTrip(SomeSortOfClientThatWantATrip client, int idTrip);
    }
}
