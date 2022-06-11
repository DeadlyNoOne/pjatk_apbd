using efDataBase.Models;
using efDataBase.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace efDataBase.Services
{
    public class DbService : IDbService
    {
        private readonly s21962Context _context;

        public DbService(s21962Context context)
        {
            _context = context;
        }
        public async Task<string> AddClientToTrip(SomeSortOfClientThatWantATrip client, int idTrip)
        {
            var result = await _context.Clients.Where(e => e.Pesel == client.Pesel).FirstOrDefaultAsync();
            if (result == null)
            {
                var addClient = new Client
                {
                    IdClient = await _context.Clients.Select(e => e.IdClient).MaxAsync() + 1,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    Email = client.Email,
                    Telephone = client.Telephone,
                    Pesel = client.Pesel
                };
                await _context.Clients.AddAsync(addClient);
                await _context.SaveChangesAsync();
            }

            bool isTripAlreadyReservedForClient = await _context.ClientTrips.AnyAsync(e => e.IdClient == result.IdClient && e.IdTrip == idTrip);
            if (isTripAlreadyReservedForClient)
            {
                throw new Exception("Client have already reserved that trip");
            }

            bool theTripExists = await _context.ClientTrips.AnyAsync(e => e.IdClient == result.IdClient && e.IdTrip == idTrip); 
            if (theTripExists)
            {
                throw new Exception("There's no such trip");
            }

            await _context.ClientTrips.AddAsync(new ClientTrip
            {
                IdClient = result.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = client.PaymentDate
            });
            

            await _context.SaveChangesAsync();
            return "Successfully added client to trip";
        }

        public async Task<string> DeleteClient(int id)
        {
            var result = await _context.ClientTrips.Where(e => e.IdClient == id).FirstOrDefaultAsync();
            if (result != null)
            {
                throw new Exception("Client have at least one scheduled trip, therefore can't delete the client");
            }
            else
            {
                var client = new Client { IdClient = id };
                _context.Attach(client);
                _context.Remove(client);

                await _context.SaveChangesAsync();

                return "Removed without errors";
            }
        }

        public async Task<IEnumerable<SomeSortOfTrip>> GetTrips()
        {
            return await _context.Trips.Select(e => new SomeSortOfTrip
            {
                Name = e.Name,
                Description = e.Description,
                MaxPeople = e.MaxPeople,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
                Countries = e.CountryTrips.Select(e => new SomeSortOfCountry { Name = e.IdCountryNavigation.Name }).ToList(),
                Clients = e.ClientTrips.Select(e => new SomeSortOfClient { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName }).ToList()
            }).OrderByDescending(e => e.DateFrom).ToListAsync();
        }
    }
}
