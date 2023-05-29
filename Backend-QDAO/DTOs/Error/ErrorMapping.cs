using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QDAO.Endpoint.DTOs.Error
{
    public static class ErrorMapping
    {
        public static ObjectResult ToHttp (this Exception ex)
        {
            if (ex is ArgumentException)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 400
                };
            }
            if (ex is ArgumentOutOfRangeException)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 404
                };
            }
            
            return new ObjectResult(ex.Message)
            {
                StatusCode = 500
            };
        }
    }
}
