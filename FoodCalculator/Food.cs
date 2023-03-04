using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FoodCalculator
{
    internal class Food : INotifyPropertyChanged
    {
        public enum FoodType
        {
            Main,
            Garnish,
            Soup,
            Salad,
            KaWa,
            Eggs
        }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Portions { get { return portions; } set { if (value > 0) { portions = value; OnPropertyChanged("Portions"); } } }
        private int portions;
        public int Modifier { get { return modifier; } set { if (value >= 0) { modifier = value; OnPropertyChanged("Modifier"); } } }
        private int modifier;
        public int Id { get { return id; } set { if (value >= 0) { id = value; OnPropertyChanged("Id"); } } }
        private int id;

        public Food(string name)
        {
            Name = name;
            modifier = 1;
            Portions = 1;
        }
        public Food()
        { modifier = 1; Portions = 1; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
