using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HancerliMarket.DataModels.Models
{
    public class BaseModel
    {
        public bool Status { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
    }
}
