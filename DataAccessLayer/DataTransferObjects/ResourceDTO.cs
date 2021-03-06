using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DataAccessLayer.DataTransferObjects
{
    public class ResourceDTO
    {
        public long ResourceId { get; set; }
        public string ResourceName { get; set; }
        public string Preacher { get; set; }
        public int ResourceCategoryId { get; set; }
        public int ResourceTypeId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Description { get; set; }
        public string SubHeader { get; set; }
        public string SubDescription { get; set; }
        public bool IsPublished { get; set; }
        public bool IsFeatured { get; set; }
        public string FooterHeader { get; set; }
        public string FooterDescription { get; set; }
        public string Filename { get; set; }
        public string OldImageUrl { get; set; }
        public Byte[] Artwork { get; set; }
        public string StorageSize { get; set; }
    }
}
