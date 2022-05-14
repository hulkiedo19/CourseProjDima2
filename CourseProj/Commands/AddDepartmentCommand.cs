using CourseProj.Models;
using CourseProj.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CourseProj.Commands
{
    public class AddDepartmentCommand : Commands
    {
        private MainWindowViewModel _viewModel;

        public AddDepartmentCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if(_viewModel.SelectedDepartment != null)
            {
                using(var DbContext = new DatabaseEntities())
                {
                    DbContext.Departments.Add((_viewModel.SelectedDepartment as Department));
                    DbContext.SaveChanges();
                    (parameter as ItemCollection).Refresh();
                }
                return;
            }

            if(_viewModel.DepartmentName == null || _viewModel.DepartmentDescription == null)
            {
                MessageBox.Show("Please enter text to textboxes");
                return;
            }

            Department department = CreateDepartment();

            using(var DbContext = new DatabaseEntities())
            {
                DbContext.Departments.Add(department);
                DbContext.SaveChanges();

                _viewModel.Departments.Add(department);
                (parameter as ItemCollection).Refresh();
            }
        }

        private Department CreateDepartment()
        {
            Department department = new Department()
            {
                DepartmentName = _viewModel.DepartmentName,
                DepartmentDescription = _viewModel.DepartmentDescription
            };

            return department;
        }
    }
}
