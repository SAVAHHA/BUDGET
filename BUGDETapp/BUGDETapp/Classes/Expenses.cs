using System;
using System.Collections.Generic;
using System.Text;

namespace BUGDETapp.Classes
{
    public static class Expenses
    {
        public static IList<Expense> ExpenseListDesc { get; set; }
        public static IList<Expense> ExpenseListAsc { get; set; }
    }
}
