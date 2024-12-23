using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Itog2.Windows
{
    public partial class EditEntityWindow : Window
    {
        private object _entity;
        private List<KeyValuePair<string, string>> propertyPairs = new List<KeyValuePair<string, string>>();
        public EditEntityWindow()
        {
            InitializeComponent();
        }

        public void SetProperties(object entity)
        {
            _entity = entity;
            propertyPairs = GetPropertyValues(entity);
            fieldsPanel.ItemsSource = propertyPairs;
        }

        private List<KeyValuePair<string, string>> GetPropertyValues(object entity)
        {
            if (entity == null) return new List<KeyValuePair<string, string>>();

            var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>();
            foreach (var prop in properties)
            {
                if (prop.Name == "Id") continue;
                pairs.Add(new KeyValuePair<string, string>(prop.Name, prop.GetValue(entity)?.ToString() ?? ""));
            }
            return pairs;
        }

        public object GetEntity()
        {
            var properties = _entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var pair = propertyPairs.FirstOrDefault(p => p.Key == prop.Name);
                if (!string.IsNullOrEmpty(pair.Key) && prop.Name != "Id")
                {
                    if (prop.PropertyType == typeof(int))
                    {
                        if (int.TryParse(pair.Value, out int value))
                        {
                            prop.SetValue(_entity, value);
                        }
                    }
                    else
                    {
                        prop.SetValue(_entity, pair.Value);
                    }

                }
            }
            return _entity;
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}