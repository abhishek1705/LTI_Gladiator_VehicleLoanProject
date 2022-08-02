﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VehicleLoanProject.Models.ViewModel
{
    public class AcceptedList
    {
        [Key]
        public int LoanId { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal LoanAmount { get; set; }
        public int LoanTenure { get; set; }
        public int LoanInterestRate { get; set; }
        public string AccountType { get; set; }
        public decimal ProcessingFee { get; set; }
        public int VehicleID { get; set; }
        public decimal OnroadPrice { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
