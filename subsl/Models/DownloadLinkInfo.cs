using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subsl.Models
{

    public class DownloadLinkInfo
    {
        public string link { get; set; }
        public string file_name { get; set; }
        public int requests { get; set; }
        public int remaining { get; set; }
        public string message { get; set; }
        public string reset_time { get; set; }
        public DateTime reset_time_utc { get; set; }
    }

}
