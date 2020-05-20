using cw11.DTOs;
using cw11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Services
{
    interface IDoctorDbServices
    {
        public IEnumerable<Doctor> GetDoctors();
        public Doctor AddNewDoc(NewDoctor request);
        public Doctor UpdateDoc(UpdateDoctor request);
        public string DeleteDoc(int id);
    }
}
