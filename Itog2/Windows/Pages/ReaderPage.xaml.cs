using Itog2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Windows.Pages
{
    public partial class ReaderPage : BaseEntityPage
    {
        public ReaderPage()
        {
            InitializeComponent();
            FileName = "Readers";
            CreateEntity = (line) =>
            {
                string[] parts = line.Split('#');
                return new Reader
                {
                    Id = parts[0],
                    Name = parts[1],
                    MiddleName = parts[2],
                    Surname = parts[3],
                    Phone = parts[4],
                    Email = parts[5],
                    Age = int.Parse(parts[6]),
                    BookIda = parts[7]
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