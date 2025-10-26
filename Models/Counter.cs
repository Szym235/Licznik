using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Licznik.Models
{
    internal partial class Counter : INotifyPropertyChanged
    {

        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _colorName;
        public string ColorName
        {
            get { return _colorName; }
            set
            {
                if (_colorName != value)
                {
                    _colorName = value;
                    switch(value)
                    {
                        case "White":
                            Color = Color.FromRgb(255, 255, 255);
                            break;

                        case "Red":
                            Color = Color.FromRgb(255, 0, 0);
                            break;

                        case "Yellow":
                            Color = Color.FromRgb(255, 255, 0);
                            break;

                        case "Blue":
                            Color = Color.FromRgb(0, 0, 255);
                            break;
                    }
                    OnPropertyChanged();
                }
            }
        }

        private Color _color;
        public Color Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }

        private String _name = string.Empty;
        public String Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand IncrementCommand { get; }
        public ICommand DecrementCommand { get; }

        public Counter()
        {
            IncrementCommand = new Command(Increment);
            DecrementCommand = new Command(Decrement);
            ColorName = "White";
        }
        private void Increment()
        {
            Value++;
            System.Diagnostics.Debug.WriteLine("Incremented");
            System.Diagnostics.Debug.WriteLine(Value);
        }

        private void Decrement()
        {
            Value--;
            System.Diagnostics.Debug.WriteLine("Decremented");
            System.Diagnostics.Debug.WriteLine(Value);
        }
    }
}
