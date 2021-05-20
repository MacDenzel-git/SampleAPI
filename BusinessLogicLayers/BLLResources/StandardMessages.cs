using System;
using System.Collections.Generic;
using System.Text;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Resources
{
   public static class StandardMessages
    {
        public static OutputHandler getExceptionMessage(Exception ex)
        {
            if (ex.InnerException == null)
            {
                return new OutputHandler { IsErrorOccured = true, Message = $"Error Occured:{ex.Message}" };
            }
            else
            {
                return new OutputHandler { IsErrorOccured = true, Message = $"Error Occured:{ex.InnerException}" };

            }
        }
    }
}
