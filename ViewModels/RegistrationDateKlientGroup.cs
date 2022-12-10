using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Курсач.ViewModels
{
    public class RegistrationDateKlientGroup
    {
        [DataType(DataType.Date)]
        public DateTime? RegistrationDate { get; set; }

        public int IdKlient { get; set; }

    }
}