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
        public async Task<string> AddDoctor(SomeSortOfDoctor doctor)
        {
            var result = await _context.Doctors.Where(e => e.FirstName == doctor.FirstName && e.LastName == doctor.LastName).FirstOrDefaultAsync();
            if (result == null)
            {
                var addDoctor = new Doctor
                {
                    IdDoctor = await _context.Doctors.Select(e => e.IdDoctor).MaxAsync() + 1,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Email = doctor.Email
                };
                await _context.Doctors.AddAsync(addDoctor);
                await _context.SaveChangesAsync();
                return "Doctor added sucessfully";
            }
            throw new Exception("There's that doctor already");

        }

        public async Task<string> DeleteDoctor(int id)
        {
            var result = await _context.Doctors.Where(e => e.IdDoctor == id).FirstOrDefaultAsync();
            if (result != null)
            {
                throw new Exception("There's no such a doctor");
            }
            else
            {
                var doctor = new Doctor { IdDoctor = id };
                _context.Attach(doctor);
                _context.Remove(doctor);

                await _context.SaveChangesAsync();

                return "Removed without errors";
            }
        }

        public async Task<string> EditDoctor(SomeSortOfDoctor doctor, int id)
        {
            var result = await _context.Doctors.Where(e => e.IdDoctor == id).FirstOrDefaultAsync();
            if (result != null)
            {
                throw new Exception("There's no such a doctor");
            }
            else
            {
                result.FirstName = doctor.FirstName;
                result.LastName = doctor.LastName;
                result.Email = doctor.Email;

                await _context.SaveChangesAsync();

                return "Edited without errors";
            }
        }

        public async Task<IEnumerable<SomeSortOfDoctor>> GetDoctors()
        {
            return await _context.Doctors.Select(e => new SomeSortOfDoctor
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email
            }).OrderByDescending(e => e.LastName).ToListAsync();
        }

    }
}
