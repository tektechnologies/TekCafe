using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Project
    {
        public string ProjectID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int WorkStation { get; set; }
        public string ProjectTypeID { get; set; }
        public string PhaseID { get; set; }
        public int ClientID { get; set; }
        public bool Active { get; set; }

    }
}
