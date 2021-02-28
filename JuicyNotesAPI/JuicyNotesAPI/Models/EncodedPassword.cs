using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuicyNotesAPI.Models
{
    public class EncodedPassword
    {
        public string salt { get; set; }
        public string hash { get; set; }
    }
}
