﻿using System;
using System.Collections.Generic;
using System.Linq;
using Itog2.Classes;
using System.Text;
using System.Threading.Tasks;

namespace Itog2.Windows.Pages
{
    public partial class AuthorPage : BaseEntityPage
    {
        public AuthorPage()
        {
            InitializeComponent();
            FileName = "Authors";
            CreateEntity = (line) =>
            {
                string[] parts = line.Split('#');
                return new Author
                {
                    Id = parts[0],
                    Name = parts[1],
                    MiddleName = parts[2],
                    Surname = parts[3]
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