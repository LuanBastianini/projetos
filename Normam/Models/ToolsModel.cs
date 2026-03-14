using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Normam.Models
{
    public class ToolsModel
    {
        public required string type { get; set; }
        public required string[] vector_store_ids { get; set; }
        public int max_num_results { get; set; }
    }
}