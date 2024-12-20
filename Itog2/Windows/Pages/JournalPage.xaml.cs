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
    public partial class JournalPage : Page
    {
        public JournalPage()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            try
            {
                var journalEntries = DataManager.LoadEntities<Itog2.Classes.JournalEntry>("Journal", line =>
                {
                    var parts = line.Split('#');
                    return new Itog2.Classes.JournalEntry
                    {
                        ReaderId = parts[0],
                        BookId = parts[1],
                        ISBN = parts[2],
                        EmployeeId = parts[3],
                        Type = parts[4]
                    };
                });

                dataGrid.ItemsSource = journalEntries;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}