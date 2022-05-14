using CourseProj.Models;
using CourseProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CourseProj.Commands
{
    public class UpdateDepartmentCommand : Commands
    {
        private MainWindowViewModel _viewModel;

        public UpdateDepartmentCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            UpdateDepartments();
            (parameter as ItemCollection).Refresh();
        }

        private void UpdateDepartments()
        {
            using (var DbContext = new DatabaseEntities())
            {
                _viewModel.Departments = DbContext.Departments
                    .ToList();
            }
        }
    }
}
