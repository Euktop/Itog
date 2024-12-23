using Itog.Class;
using System;

namespace Itog.Windows.Pages
{
    internal class EmployeeEditWindow
    {
        private EmployeeRepository employeeRepository;

        public EmployeeEditWindow(EmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        internal bool ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}