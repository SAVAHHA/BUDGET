using System;
using System.Collections.Generic;
using System.Text;

namespace BUGDETapp.Classes
{
    public class Income
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Sum { get; set; }
        public int UserID { get; set; }
        public int KindID { get; set; }
    }
}
