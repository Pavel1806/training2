using System;
using System.Collections.Generic;
using System.Text;

namespace Entity_Framework.Models
{
    public class CreditCardDetails
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CardHolder { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
