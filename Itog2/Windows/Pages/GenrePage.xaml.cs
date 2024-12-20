using System;
using System.Collections.Generic;
using System.Linq;
using Itog2.Classes;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Windows.Pages
{
    public partial class GenrePage : BaseEntityPage
    {
        public GenrePage()
        {
            InitializeComponent();
            FileName = "Genres";
            CreateEntity = (line) =>
            {
                string[] parts = line.Split('#');
                return new Genre
                {
                    Id = parts[0],
                    Name = parts[1]
                };
            };
        }
        public override void LoadData()
        {
            base.LoadData();
            dataGrid.Columns[0].Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
