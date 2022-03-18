using System;
using System.Collections.Generic;
using System.Text;

namespace ClassObjects
{
    class Patent
    {
        public string Title { get; set; }
        public Deviser Deviser { get; set; }
        public string Country { get; set; }
        public int RegistrationNumber { get; set; }
        public DateTime DateApplicationSubmission { get; set; }
        public DateTime DatePublication { get; set; }
        public int NumberPages { get; set; }
        public string Note { get; set; }
    }
}
