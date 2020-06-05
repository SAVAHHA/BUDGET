using System;
using System.Collections.Generic;
using System.Text;

namespace BUGDETapp.Classes
{
    public class Expense
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Sum { get; set; }
        public int UserID { get; set; }
        public int TypeID { get; set; }
    }
}
