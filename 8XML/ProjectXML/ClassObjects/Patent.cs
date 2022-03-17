using System;
using System.Collections.Generic;
using System.Text;

namespace ClassObjects
{
    class Patent
    {
        string Title { get; set; }
        Deviser Deviser { get; set; }
        string Country { get; set; }
        int RegistrationNumber { get; set; }
        DateTime DateApplicationSubmission { get; set; }
        DateTime DatePublication { get; set; }
        int NumberPages { get; set; }
        string Note { get; set; }
    }
}
