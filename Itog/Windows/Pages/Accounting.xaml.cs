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
using static Itog.Windows.Pages.Accounting;

namespace Itog.Windows.Pages
{
    public partial class Accounting : Page
    {
        public event EventHandler PageClosed;
        DataManager _dataManager;
        public Accounting(DataManager dataManager,string title)
        {
            InitializeComponent();
            _dataManager = dataManager;
            Title.Text = title;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            PageClosed?.Invoke(this, EventArgs.Empty);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }




    }
    public partial class Accounting<T> : Accounting where T : Entity
    {
        private Repository<T> _repository;
        public Accounting(DataManager dataManager, Repository<T> repository,string title) : base(dataManager,title)
        {
            InitializeComponent();
            _repository = repository;
            LoadData();
        }
        private void LoadData()
        {
            DataGrid_.ItemsSource = _repository.GetAll();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            if (typeof(T) == typeof(Book))
            {
                var window = new BookEditWindow(_repository as BookRepository);
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else if (typeof(T) == typeof(Author))
            {
                var window = new AuthorEditWindow(_repository as AuthorRepository);
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else if (typeof(T) == typeof(Genre))
            {
                var window = new GenreEditWindow(_repository as GenreRepository);
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else if (typeof(T) == typeof(Publisher))
            {
                var window = new PublisherEditWindow(_repository as PublisherRepository);
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else if (typeof(T) == typeof(Employee))
            {
                var window = new EmployeeEditWindow(_repository as EmployeeRepository);
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else if (typeof(T) == typeof(Reader))
            {
                var window = new ReaderEditWindow(_repository as ReaderRepository);
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                // Для других сущностей реализация добавления по умолчанию

            }

        }
    }
}
