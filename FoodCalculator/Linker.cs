using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Documents;

namespace FoodCalculator
{
    internal static class Linker
    {
        public static List<object> ViewModels { get; set; } = new List<object>();
        public static List<object> Windows { get;set; } = new List<object>();
        static Linker()
        {
            
        }
        
    }
}
