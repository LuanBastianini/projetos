using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Normam.Models
{
    public class OutputModel
    {
        public IEnumerable<ContentModel> content { get; set; }
        public string type { get; set; }
    }
}