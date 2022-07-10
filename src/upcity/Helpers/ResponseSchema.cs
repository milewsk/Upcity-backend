using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace upcity.Helpers
{
    public class ResponseSchema
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object? ReturnData { get; set; }

        public ResponseSchema() { }
        public ResponseSchema(int status, string message, object? data) {

            StatusCode = status;
            Message = message;
            ReturnData = data;
          
        }
        
        
    }
}
