using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp42
{
    public class Inquiry
    {
        public Guid Id { get; set; }
        public Guid client_id { get; set; }
        public string department_address { get; set; }
        public decimal amout { get; set; }
        public string UAN { get; set; }
        public int status { get; set; }
    }
}
