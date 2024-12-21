using Itog2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Itog2.Windows.Pages
{
    public partial class BaseEntityPage : Page
    {
        public string FileName { get; set; }
        public List<object> _items { get; set; }
        public Func<string, object> CreateEntity { get; set; }


        public BaseEntityPage()
        {
            InitializeComponent();
        }
        public virtual void LoadData()
        {
            try
            {
                _items = DataManager.LoadEntities(FileName, CreateEntity).Cast<object>().ToList();
                dataGrid.ItemsSource = _items;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void SaveData()
        {
            try
            {
                DataManager.SaveEntities(FileName, _items.Cast<object>().ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void UpdateDataGrid()
        {
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = _items;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EditEntityWindow window = new EditEntityWindow();
            window.SetProperties(_items[0]);
            if (window.ShowDialog() == true)
            {
                object newEntity = window.GetEntity();
                _items.Add(newEntity);
                UpdateDataGrid();
                SaveData();
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select a row to edit.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            object selectedEntity = dataGrid.SelectedItem;
            EditEntityWindow window = new EditEntityWindow();

            window.SetProperties(selectedEntity);

            if (window.ShowDialog() == true)
            {
                object updatedEntity = window.GetEntity();

                int index = _items.IndexOf(selectedEntity);

                _items[index] = updatedEntity;
                UpdateDataGrid();
                SaveData();
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Select a row to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this item?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _items.Remove(dataGrid.SelectedItem);
                UpdateDataGrid();
                SaveData();
            }
        }
    }
}
