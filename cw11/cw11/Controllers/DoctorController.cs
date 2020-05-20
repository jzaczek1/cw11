using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw11.DTOs;
using cw11.Models;
using cw11.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw11.Controllers
{
    [Route("api/doc")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly SqlDoctorDbServices service;

        public DoctorController(SqlDoctorDbServices s)
        {
            service = s;
        }

        [HttpPost]
        public IActionResult AddNewDoctor(NewDoctor request)
        {
            return Ok(service.AddNewDoc(request));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            return Ok(service.DeleteDoc(id));
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            return Ok(service.GetDoctors());
        }

        [HttpPut]
        public IActionResult UpdateDoctor(UpdateDoctor request)
        {
            return Ok(service.UpdateDoc(request));
        }
    }
}