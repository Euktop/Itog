using System;
using System.Collections.Generic;
using Itog2.Classes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Windows.Pages
{
    public partial class PublisherPage : BaseEntityPage
    {
        public PublisherPage()
        {
            InitializeComponent();
            FileName = "Publishers";
            CreateEntity = (line) =>
            {
                string[] parts = line.Split('#');
                return new Publisher
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