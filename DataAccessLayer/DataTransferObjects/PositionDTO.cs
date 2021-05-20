using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
    public class PositionDTO
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public string Abbreviation { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool? IsPublished { get; set; }
    }
}
