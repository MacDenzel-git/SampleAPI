using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Testimony
    {
        public int TestimonyId { get; set; }
        public string TestifierName { get; set; }
        public string TestimonyHeading { get; set; }
        public string TestimonyFullDescription { get; set; }
        public bool IsPublished { get; set; }
        public DateTime TestimonyDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ImageUrl { get; set; }
       
    }
}
