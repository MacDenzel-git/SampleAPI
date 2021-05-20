using System;
using System.Collections.Generic;
using System.Text;
using TechArchDataHandler.General;

namespace DataAccessLayer.DataTransferObjects
{
    public class FileUploadDTO: OutputHandler
    {
        public string ImageUrl { get; set; }
        public byte[] ByteRepresentation { get; set; }
    }
}
