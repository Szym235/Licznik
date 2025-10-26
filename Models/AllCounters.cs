using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Xml;

namespace Licznik.Models
{
    internal class AllCounters
    {
        public ObservableCollection<Counter> Counters { get; set; }
        public ICommand AddCounterCommand { get; }
        public int defaultCounterValue {get; set;}

        public AllCounters()
        {
            Counters = new();
            defaultCounterValue = 0;
            Debug.WriteLine("Binding command");
            AddCounterCommand = new Command(addCounter);
            XmlDocument document = new XmlDocument();
            try {
                document.Load(Path.Combine(FileSystem.AppDataDirectory, "CountersSaveFile.xml"));
                Debug.WriteLine(Path.Combine(FileSystem.AppDataDirectory, "CountersSaveFile.xml"));
                Debug.WriteLine(document.InnerXml);
                XmlElement root = document.DocumentElement;
                foreach (XmlNode node in root.ChildNodes)
                {
                    Counter counter = new Counter(int.Parse(getAttributeIfExists(node, "DefaultValue", "0")));
                    counter.Name = getAttributeIfExists(node, "Name", "");
                    counter.ColorName = getAttributeIfExists(node, "Color", "White");
                    counter.Value = int.Parse(node.InnerText);
                    Debug.WriteLine("Added " + counter.Name + " with value " + counter.Value);
                    Counters.Add(counter);
                }
            } catch
            {
                addCounter();
            }
            Debug.WriteLine("Command binded");
        }

        private string getAttributeIfExists(XmlNode node, String name, String defaultValue)
        {
            if (node.Attributes[name] != null) return node.Attributes[name].Value;
            else return defaultValue;
        }

        private void addCounter()
        {
            Debug.WriteLine("AddStart");
            Counters.Add(new Counter(defaultCounterValue));
            Debug.WriteLine("AddEnd");
            Debug.WriteLine(Counters.Count);
        }

        public void saveCounters()
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("Counters");
            document.AppendChild(root);
            foreach (Counter counter in Counters)
            {
                XmlElement newCounter = document.CreateElement("Counter");
                newCounter.InnerText = counter.Value.ToString();
                newCounter.Attributes.Append(document.CreateAttribute("Name")).Value = counter.Name;
                newCounter.Attributes.Append(document.CreateAttribute("Color")).Value= counter.ColorName;
                newCounter.Attributes.Append(document.CreateAttribute("DefaultValue")).Value = counter.DefaultValue.ToString();
                root.AppendChild(newCounter);
            }
            document.Save(Path.Combine(FileSystem.AppDataDirectory, "CountersSaveFile.xml"));
            Debug.WriteLine(Path.Combine(FileSystem.AppDataDirectory, "CountersSaveFile.xml"));
        }
    }
}
