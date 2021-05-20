//using Microsoft.AspNetCore.Http;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;
//using Swashbuckle.AspNetCore.Swagger;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Google.Apis.Discovery;

//namespace BusinessLogicLayer.Resources
//{
//    public class Swaggerfileuploadfilter : IOperationFilter
//    {
//        public void Apply(OpenApiOperation operation, OperationFilterContext context)
//        {
//            if (operation.OperationId == "Post")
//            {
//                operation.Parameters = new List<IParameter>
//                {
//                    new NonBodyParameter
//                    {
//                        Name = "myFile",
//                        Required = true,
//                        Type = "file",
//                        In = "formData"
//                    }
//                };
//            }
//        }
//    }
//}
