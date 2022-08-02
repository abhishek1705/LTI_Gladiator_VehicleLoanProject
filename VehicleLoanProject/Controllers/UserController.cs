using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLoanProject.Models;
using VehicleLoanProject.Models.ViewModel;

namespace VehicleLoanProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        VehicleLoanDatabaseContext db = new VehicleLoanDatabaseContext();

        [HttpGet]
        [Route("ApplicantDetails/{userid}")]
        public IActionResult GetApplicantDetails(string userid)
        {
            if (userid == null)
            {
                return BadRequest("Id Can't be Null");
            }

            var data = (from ad in db.ApplicantDetails
                        where ad.UserId == userid
                        select new
                        {
                            CId = ad.CustomerId,
                            Fname = ad.FirstName,
                            Lname = ad.LastName,
                            Age = ad.Age,
                            Gender = ad.Gender,
                            ContactNo = ad.ContactNo,
                            EmailId = ad.EmailId,
                            address = ad.Address,
                            State = ad.State,
                            City = ad.City,
                            Pincode = ad.Pincode,
                            UserId = ad.UserId,
                            Password = ad.Password
                        }).FirstOrDefault();


            if (data == null)
            {
                return NotFound($"User with user ID {userid} not present");
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("ApplicationDetailed/{id}")]
        public IActionResult GetApplicationDetailed(int? id)
        {
            if (id == null)
            {
                return BadRequest("Id Can't be Null");
            }

            var data = db.ApplicationDetailed.FromSqlInterpolated<ApplicationDetailed>($"ApplicationDetailed {id}").AsEnumerable().FirstOrDefault();

            if (data == null)
            {
                return NotFound($"Application of Customer with ID: {id} is not present");
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("ApplyLoanValidation/{id}")]
        public IActionResult GetApplyLoanValidation(int? id)
        {
            if (id == null)
            {
                return BadRequest("Login to Apply for Loan");
            }
            int y = 1;
            int n = 0;
            var data = (from loan in db.LoanDetails where loan.CustomerId == id select new { Id = loan.LoanId }).FirstOrDefault();
            //var data = db.Depts.Where(d => d.Id == id).Select(d => new { Id = d.Id, Name = d.Dname, Location = d.Location }).FirstOrDefault();
            if (data == null)
            {
                return Ok($"{y}");
            }
            return Ok($"{n}");
        }

        [HttpGet]
        [Route("ApplicationStatusValidation/{id}")]
        public IActionResult GetApplicationStatusValidation(int? id)
        {
            if (id == null)
            {
                return BadRequest("Login to Apply for Loan");
            }
            int y = 1;
            int n = 0;
            var data = (from loan in db.LoanDetails where loan.CustomerId == id select new { Id = loan.LoanId }).FirstOrDefault();
            //var data = db.Depts.Where(d => d.Id == id).Select(d => new { Id = d.Id, Name = d.Dname, Location = d.Location }).FirstOrDefault();
            if (data == null)
            {
                return Ok($"{y}");
            }
            return Ok($"{n}");
        }


        [HttpPost]
        [Route("AddDocuments")]
        public IActionResult PostIdentityDocuments(IdentityDocument idoc)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    /*db.Depts.Add(dept);
                    db.SaveChanges();*/
                    db.Database.ExecuteSqlInterpolated($"adddocs {idoc.Adharcard}, {idoc.Pancard}, {idoc.Photo}, {idoc.Salaryslip}, {idoc.CustomerId}");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Created("Documents Successfully Uploaded", idoc);
        }


    }
}

