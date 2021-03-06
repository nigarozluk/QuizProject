using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Models
{
    public class GenericResponse<T>
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
