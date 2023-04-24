using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanProjects
{
    public class Project
    {


        #region ATRIBUTOS

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int ProjectManagerID { get; set; }

        public int TemaLeadID { get; set; }

        public decimal Budget { get; set; }

        public decimal ActualCost { get; set; }

        public string StatusProject { get; set; }

        #endregion


    }
}
