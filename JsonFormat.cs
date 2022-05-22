using System;
using System.Collections.Generic;

namespace CN311_Platforming_Final_Project_Server
{
    public class JsonFormat
    {
        public String Type { get; set; }
        public String Data { get; set; }
    }
    public class JsonConnection
    {
        public String Type { get; set; }
        public String ClientID { get; set; }
    }

    public class JsonEvent
    {
        public String Type { get; set; }
        public String ClientID { get; set; }
        public String Info { get; set; }
    }
    
    public class JsonPlayerPosition
    {
        public String ClientID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}