using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsController {
    public class Major {
        public int ID { get; set; }
        public string Code { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public int MinSAT { get; set; }

        public override string ToString() {
            return $"MajorID: [{ID}] | Code: [{Code}] | Description: [{Description}] | minSAT: [{MinSAT}]";
        }
    }
}