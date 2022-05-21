using System;
using System.Collections.Generic;

namespace CN311_Platforming_Final_Project_Server
{
    public class JsonFormat
    {
        public String? Type { get; set; }
        public String? Data { get; set; }
    }
    public class JsonConnection
    {
        public String? Type { get; set; }
        public String? ClientID { get; set; }
    }
}