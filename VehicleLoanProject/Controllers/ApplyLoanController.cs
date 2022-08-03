using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLoanProject.Models;

namespace VehicleLoanProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplyLoanController : ControllerBase
    {
        VehicleLoanDatabaseContext vc = new VehicleLoanDatabaseContext();

        [HttpGet]
        [Route("ListEmp")]
        public IActionResult GetEmp()
        {
            var data = from EmploymentDetail in vc.EmploymentDetails select new { EmpId = EmploymentDetail.EmpId, TypeOfEmployement = EmploymentDetail.TypeOfEmployement, YearlySalary = EmploymentDetail.YearlySalary, ExistingEmi = EmploymentDetail.ExistingEmi, CustomerId = EmploymentDetail.CustomerId };
            return Ok(data);
        }

        [HttpGet]
        [Route("ListEmp/{id}")]
        public IActionResult GetEmp(int? id)
        {
            if (id == null)
            {
                return BadRequest("Emp ID cannot be null");
            }
            var data = (from EmploymentDetail in vc.EmploymentDetails where EmploymentDetail.EmpId == id select new { EmpId = EmploymentDetail.EmpId, TypeOfEmployement = EmploymentDetail.TypeOfEmployement, YearlySalary = EmploymentDetail.YearlySalary, ExistingEmi = EmploymentDetail.ExistingEmi, CustomerId = EmploymentDetail.CustomerId }).FirstOrDefault();

            if (data == null)
            {
                return NotFound($"Emp {id} not found/available");
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("AddEmp")]
        public IActionResult PostEmp(EmploymentDetail emp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    vc.EmploymentDetails.Add(emp);
                    vc.SaveChanges();
                    /* vc.Database.ExecuteSqlInterpolated($"AddEmp  {emp.TypeOfEmployement}, {emp.YearlySalary},{emp.ExistingEmi},{emp.CustomerId}");*/
                }

                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return Created("Record Successfully Added!", emp); //doubt????
        }









        [HttpGet]
        [Route("ListLD")]
        public IActionResult GetLD()
        {
            var data = from LoanDetail in vc.LoanDetails select new { LoanId = LoanDetail.LoanId, LoanAmount = LoanDetail.LoanAmount, LoanTenure = LoanDetail.LoanTenure, LoanInterestRate = LoanDetail.LoanInterestRate, StatusId = LoanDetail.StatusId, CustomerId = LoanDetail.CustomerId };
            return Ok(data);
        }

        [HttpGet]
        [Route("ListLD/{id}")]
        public IActionResult GetLD(int? id)
        {
            if (id == null)
            {
                return BadRequest("Loan ID cannot be null");
            }
            var data = (from LoanDetail in vc.LoanDetails where LoanDetail.LoanId == id select new { LoanId = LoanDetail.LoanId, LoanAmount = LoanDetail.LoanAmount, LoanTenure = LoanDetail.LoanTenure, LoanInterestRate = LoanDetail.LoanInterestRate, StatusId = LoanDetail.StatusId, CustomerId = LoanDetail.CustomerId }).FirstOrDefault();

            if (data == null)
            {
                return NotFound($"Loan {id} not found/available");
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("AddLD")]
        public IActionResult PostLD(LoanDetail loan)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    vc.LoanDetails.Add(loan);
                    vc.SaveChanges();
                    /*vc.Database.ExecuteSqlInterpolated($"AddLD {loan.LoanAmount}, {loan.LoanTenure},{loan.LoanInterestRate},{loan.CustomerId}");*/
                }

                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return Created("Record Successfully Added!", loan); 
        }










        [HttpGet]
        [Route("ListVehicles")]
        public IActionResult GetVehicle()
        {
            var data = from VehicleDetail in vc.VehicleDetails select new { VehicleId = VehicleDetail.VehicleId, Carmake = VehicleDetail.CarMake, CarModel = VehicleDetail.CarModel, ExshowroomPrice = VehicleDetail.ExshowroomPrice, OnroadPrice = VehicleDetail.OnroadPrice, CustomerId = VehicleDetail.CustomerId };
            return Ok(data);
        }

        [HttpGet]
        [Route("ListVehicles/{id}")]
        public IActionResult GetVehicle(int? id)
        {
            if (id == null)
            {
                return BadRequest("Vehicle ID cannot be null");
            }
            var data = (from VehicleDetail in vc.VehicleDetails where VehicleDetail.VehicleId == id select new { VehicleId = VehicleDetail.VehicleId, Carmake = VehicleDetail.CarMake, CarModel = VehicleDetail.CarModel, ExshowroomPrice = VehicleDetail.ExshowroomPrice, OnroadPrice = VehicleDetail.OnroadPrice, CustomerId = VehicleDetail.CustomerId }).FirstOrDefault();

            if (data == null)
            {
                return NotFound($"Vehicle {id} not found/available");
            }
            return Ok(data);
        }

        [HttpPost]
        [Route("AddVehicle")]
        public IActionResult PostVehicle(VehicleDetail vehicle)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    vc.VehicleDetails.Add(vehicle);
                    vc.SaveChanges();

                }

                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return Created("Record Successfully Added!", vehicle); 
        }



        [HttpPost]
        [Route("LoanScheme")]
        public IActionResult PostLoanScheme(LoanScheme scheme) {
            if (ModelState.IsValid)
            {
                try {
                   vc.LoanSchemes.Add(scheme);
                   vc.SaveChanges();
                }

                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                
            }
            return Created("Records Successfully Added", scheme);
        }
    }
}

