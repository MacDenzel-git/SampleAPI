using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
   public class TeamMembersDTO
    {
        public long TeamMemberId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int BranchId { get; set; }
        public int PositionId { get; set; }
        public int RoleId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsPublished { get; set; }
        public bool IsTopLeadership { get; set; }
        public bool IsDirector { get; set; }
        public bool IsECMember { get; set; }
        public bool? IsStaffMember { get; set; }
        public bool? IsBranchLeader { get; set; }
        public int projectArmId { get; set; }
        public byte[] Artwork { get; set; }
        public string ImageUrl { get; set; }
        public string Filename { get; set; }
        public string OldImageUrl { get; set; }

        public BranchDTO Branch { get; set; }
        public string BranchName { get; set; }
        public string StorageSize { get; set; }
    }
}
