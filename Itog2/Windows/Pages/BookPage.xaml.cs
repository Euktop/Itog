using System;
using System.Collections.Generic;
using System.Linq;
using Itog2.Classes;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Windows.Pages
{
    public partial class BookPage : BaseEntityPage
    {
        public BookPage()
        {
            FileName = "Books";
            CreateEntity = (line) =>
            {
                string[] parts = line.Split('#');
                return new Book
                {
                    Id = parts[0],
                    AuthorIda = parts[1],
                    GenreIda = parts[2],
                    PublisherIda = parts[3],
                    Name = parts[4]
                };
            };
            LoadData();
            InitializeComponent();
        }
        public override void LoadData()
        {
            base.LoadData();
            dataGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
