using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Data.DataModels
{
    public class User
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string password { get; set; }
        public DateTime joinedUtcDate { get; set; }
    }
}
