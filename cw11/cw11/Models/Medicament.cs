using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Models
{
    public class Medicament
    {
        public Medicament()
        {
            prescription_Medicaments = new HashSet<Prescription_Medicament>();
        }
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public ICollection<Prescription_Medicament> prescription_Medicaments { get; set; }
    }
}
