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
using System.Windows.Shapes;

namespace Itog2.Windows
{
    public partial class IssueReturnWindow : Window
    {
        private string type;
        public IssueReturnWindow(string type)
        {
            InitializeComponent();
            this.type = type;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var journalEntry = new JournalEntry
                {
                    ReaderId = txtReaderId.Text,
                    BookId = txtBookId.Text,
                    ISBN = txtISBN.Text,
                    EmployeeId = txtEmployeeId.Text,
                    Type = type
                };
                var journalEntries = DataManager.LoadEntities<JournalEntry>("Journal", line =>
                {
                    var parts = line.Split('#');
                    return new JournalEntry
                    {
                        ReaderId = parts[0],
                        BookId = parts[1],
                        ISBN = parts[2],
                        EmployeeId = parts[3],
                        Type = parts[4]
                    };
                }).ToList();


                journalEntries.Add(journalEntry);
                DataManager.SaveEntities("Journal", journalEntries);

                MessageBox.Show("Journal entry saved successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving journal entry: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            DialogResult = true;
        }
    }
}