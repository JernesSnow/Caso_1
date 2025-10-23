using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoADatos.Models
{
    public class Result
    {
        public bool Ok { get; set; }
        public string? Message { get; set; }

        public static Result Success(string? m = null) => new() { Ok = true, Message = m };
        public static Result Fail(string m) => new() { Ok = false, Message = m };
    }
}

