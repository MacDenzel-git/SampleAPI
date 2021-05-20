using System;

namespace DataAccessLayer.DataTransferObjects
{
    public class TeamDTO
    {
        public int TeamId { get; set; }
        public string Description { get; set; }
        public int? Hierachy { get; set; }
        public string Position { get; set; }
        public byte[] ImageFile { get; set; }
        public string CreateBy { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
