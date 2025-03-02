using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace subsl.Models
{
        public static class LoginInput
        {
            public static string username { get; set; }
            public static string password { get; set; }
            public static string apikey { get; set; }


        }

        public class LoginOutput
        {
            public User user { get; set; }
            public string base_url { get; set; }
            public string token { get; set; }
            public int status { get; set; }
        }

        public class User
        {
            public int allowed_downloads { get; set; }
            public int allowed_translations { get; set; }
            public string level { get; set; }
            public int user_id { get; set; }
            public bool ext_installed { get; set; }
            public bool vip { get; set; }
        }
}
