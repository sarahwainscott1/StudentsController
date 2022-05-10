using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsController {
    public class Student {
        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string StateCode { get; set; }
        public int? SAT { get; set; } //? for can be null
        public decimal GPA { get; set; }
        public int? MajorID { get; set; }

        //-----------------------

        public override string ToString() { //built in method to CW info
            return $"ID[{ID}] | FirstName [{Firstname}] | LastName [{Lastname}] | Statecode [{StateCode}] | SAT [{SAT}] | GPA [{GPA}] | MajorID [{MajorID}]";
        }
    }

}

