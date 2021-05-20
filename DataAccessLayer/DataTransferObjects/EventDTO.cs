using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using TechArchDataHandler.General;

namespace DataAccessLayer.DataTransferObjects
{
    public class EventDTO
    {
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public bool IsOnEventBrite { get; set; }
        public DateTime DateOfEvent { get; set; }
        public DateTime EventEndDate { get; set; }

        public string Heading { get; set; }
        public string Article { get; set; }
        public bool IsPublished { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ImageUrl { get; set; }
        public bool IsTimeActive { get; set; }
        public int projectArmId { get; set; }
        public byte[] ImgBytes { get; set; }
        public string FileName { get; set; }
        public string EventBriteLink { get; set; }
        public OutputHandler OutputHandler { get; set; }
        public string OldImageUrl { get; set; }
        //public virtual projectArm projectArm { get; set; }
        public bool IsAnEvent { get; set; }
        public bool IsFeatured { get; set; }
        public string StorageSize { get; set; }
    }
}
