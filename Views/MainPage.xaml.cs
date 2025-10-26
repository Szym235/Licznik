using System.Diagnostics;
using Licznik.Models;

namespace Licznik.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new AllCounters();
        }

        public void autoSave(object sender, EventArgs e)
        {
            (BindingContext as AllCounters).saveCounters();
        }

        public void setDefaultValue(object sender, EventArgs e)
        {
            AllCounters allCounters = BindingContext as AllCounters;
            if ((sender as Entry).Text == "-") allCounters.defaultCounterValue = -1;
            else if ((sender as Entry).Text == string.Empty) allCounters.defaultCounterValue = 0;
            else
            {
                try
                {
                    allCounters.defaultCounterValue = (int.Parse((sender as Entry).Text));
                }
                catch
                {
                    DisplayAlert("Error", "Wrong default value, last correct value will be used ("+allCounters.defaultCounterValue+")", "Ok");
                }
            }
        }
    }
}