using Itog.Class;
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

namespace Itog
{
    public partial class MainWindow : Window
    {
        private DataManager _dataManager;
        public MainWindow()
        {
            InitializeComponent();
            _dataManager = new DataManager();
        }


        private void Book(object sender, RoutedEventArgs e)
        {
            OpenGenericData<Book>(_dataManager.BookRepository, "Book");
        }




        private void OpenGenericData<T>(Repository<T> repository, string title) where T : Entity
        {
            LoadPage();

            void LoadPage()
            {
                var page = new Windows.Pages.Accounting<T>(_dataManager, repository,title);
                page.PageClosed += Page_PageClosed; // Подписываемся на событие
                frame.Content = page;
            }

            void Page_PageClosed(object sender, EventArgs e)
            {
                frame.Content = null; // Закрываем страницу, установив Content в null
            }
        }
    }
}
