using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLoanProject.Models;  

namespace VehicleLoanProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        VehicleLoanDatabaseContext db = new VehicleLoanDatabaseContext();


        
        


        /// <summary>
        /// Get method which returns all the columns in applicant details model
        /// </summary>
        /// <returns>returns data in iqueryable type</returns>
        [HttpGet]
        [Route("GetApplicants")]
        public IActionResult GetApplicantDetails()
        {
            var data = from applicant in db.ApplicantDetails select applicant;
            return Ok(data);
        }



        /// <summary>
        /// Post method which adds a new record in the applicant details model
        /// stored procedure named adduser is used
        /// </summary>
        /// <param name="applicantDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddApplicants")]
        public IActionResult PostApplicantDetails(ApplicantDetail applicantDetail)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //db.ApplicantDetails.Add(applicantDetail);
                    //db.SaveChanges();
                    //calling a stored procedure
                    db.Database.ExecuteSqlInterpolated($"adduser {applicantDetail.FirstName},{applicantDetail.LastName},{applicantDetail.Age},{applicantDetail.Gender},{applicantDetail.ContactNo},{applicantDetail.EmailId},{applicantDetail.Address},{applicantDetail.State},{applicantDetail.City},{applicantDetail.Pincode},{applicantDetail.UserId},{applicantDetail.Password}");

                }
                catch (Exception)
                {
                    return BadRequest("Something went wrong while adding the record");
                }

            }
            return Created("Record successfully added", applicantDetail);
        }



        /// <summary>
        ///  Get method to retrieve applicant details using customerID(PK)
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns>returns data in iqueryable type</returns>
        [HttpGet]
        [Route("GetApplicants/{CustomerId}")]
        public IActionResult GetApplicantDetails(int? CustomerID)
        {
            if (CustomerID == null)
            {
                return BadRequest("CustomerId cannot be Null");
            }
            var data = (from ApplicantDetail in db.ApplicantDetails where ApplicantDetail.CustomerId == CustomerID select new { FirstName = ApplicantDetail.FirstName, LastName = ApplicantDetail.LastName, Age = ApplicantDetail.Age, Gender = ApplicantDetail.Gender }).FirstOrDefault();
            if (data == null)
            {
                return NotFound($"Applicant with CustomerId {CustomerID} not found");
            }
            return Ok(data);
        }

        /// <summary>
        /// Put method to make changes to a specific applicant record
        /// using customerid as param
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="applicant"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("EditApplicants/{CustomerId}")]
        public IActionResult PutApplicantDetails(int CustomerId, ApplicantDetail applicant)
        {
            if (ModelState.IsValid)
            {
                ApplicantDetail applicantDetail = db.ApplicantDetails.Find(CustomerId);
                applicantDetail.FirstName = applicant.FirstName;
                applicantDetail.LastName = applicant.LastName;
                applicantDetail.Age = applicant.Age;
                applicantDetail.Gender = applicant.Gender;
                applicantDetail.ContactNo = applicant.ContactNo;
                applicantDetail.EmailId = applicant.EmailId;
                applicantDetail.Address = applicant.Address;
                applicantDetail.State = applicant.State;
                applicantDetail.City = applicant.City;
                applicantDetail.Pincode = applicant.Pincode;
                db.SaveChanges();
                return Ok();


            }
            return BadRequest("Unable to Edit Record");

        }

        //[HttpPost]
        //[Route("userlogin")]
        //public IActionResult UserLogin(UserLogin user)
        //{
        //    var userAvailable = db.ApplicantDetails.Where(u => u.UserId == user.UserId && u.Password == user.PWD).FirstOrDefault();
        //    if (userAvailable != null)
        //    {
        //        return Ok("success");
        //    }
        //    return Ok("Failure");
        //}
        


        /// <summary>
        /// Post method to validate userID and password of user
        /// userid and password is retrieved from frontend and validated with DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UserLogin")]

        public IActionResult UserLogin(UserLogin user) {
            var data = db.ApplicantDetails.Where(u => u.UserId.Equals(user.userid) && u.Password.Equals(user.password)).FirstOrDefault();

            if (data != null)
            {
                return Ok(data);
            }
            else
                return BadRequest("Incorrect");
        }

        /// <summary>
        ///  Post method to validate adminID and adminpassword of user
        ///  adminid and adminpassword is retrieved from frontend and validated with DB
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AdminLogin")]
        public IActionResult AdminLogin(AdminLogin admin) {
            var data = db.AdminLogins.Where(u => u.AdminId.Equals(admin.AdminId) && u.AdminPassword.Equals(admin.AdminPassword)).FirstOrDefault();

            if (data != null) {
                return Ok(data);
            }
            else
                return BadRequest("Incorrect");
        }


    }
}
