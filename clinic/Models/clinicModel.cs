using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinic.Models
{
    public class clinicModel
    {
        public string name { get; set; }
        public string T { get; set; }

        public long PhoneNumber { get; set; }
        public string Address { get; set; }
       
        public int Age { get; set; }
        
        public int Income { get; set; }
        public Boolean Insurance { get; set; }

        public string Housing { get; set; }

        public int ID { get; set; }
    }
}
