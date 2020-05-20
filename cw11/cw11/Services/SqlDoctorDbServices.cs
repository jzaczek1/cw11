using cw11.DTOs;
using cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Services
{
    public class SqlDoctorDbServices : IDoctorDbServices
    {
        private readonly CodeFirstContext context;

        public SqlDoctorDbServices(CodeFirstContext c)
        {
            context = c;
        }
        public Doctor AddNewDoc(NewDoctor request)
        {
            var d = new Doctor { FirstName = "Michał", LastName = "Pała", Email = "m.pala@wp.pl" };
            context.Add(d);
            context.SaveChanges();

            var alfa = context.Doctor.Where(a => a.Email == d.Email && a.FirstName == d.FirstName && a.LastName == d.LastName).First();
            return alfa;
        }

        public string DeleteDoc(int id)
        {
            var d = context.Doctor.Where(a => a.IdDoctor == id);
            if (d != null)
            {
                context.Remove(d);
                context.SaveChanges();
                return "Doctor with id: " + id + " has been deleted";
            }
            else
                return "None doctor was deleted";
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return context.Doctor;
        }

        public Doctor UpdateDoc(UpdateDoctor request)
        {
            var d = context.Doctor.FirstOrDefault(a => a.IdDoctor == request.IdDoctor);
            if (d != null) {
                d.FirstName = request.FirstName;
                d.LastName = request.LastName;
                d.Email = request.Email;

                context.Update(d);
                context.SaveChanges();

                return d;
            }
            else
                return null;

        }
    }
}
