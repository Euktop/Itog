using Itog2.Windows;
using Itog2.Windows.Pages;
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

namespace Itog2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemBooks_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new BookPage());
        }

        private void MenuItemAuthors_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new AuthorPage());
        }

        private void MenuItemGenres_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new GenrePage());
        }

        private void MenuItemPublishers_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PublisherPage());
        }

        private void MenuItemEmployees_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new EmployeePage());
        }

        private void MenuItemReaders_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new ReaderPage());
        }
        private void MenuItemJournal_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new JournalPage());
        }
        private void MenuItemIssue_Click(object sender, RoutedEventArgs e)
        {
            var issueWindow = new IssueReturnWindow("Issue");
            issueWindow.ShowDialog();
        }
        private void MenuItemReturn_Click(object sender, RoutedEventArgs e)
        {
            var returnWindow = new IssueReturnWindow("Return");
            returnWindow.ShowDialog();
        }


        private void MenuItemPopularBooksReport_Click(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow(ReportType.PopularBooks);
            reportWindow.ShowDialog();
        }

        private void MenuItemEmployeeRatingReport_Click(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow(ReportType.EmployeeRating);
            reportWindow.ShowDialog();
        }
    }
}
