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
    public class DeleteDepartmentCommand : Commands
    {
        private MainWindowViewModel _viewModel;

        public DeleteDepartmentCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            int departmentId = (_viewModel.SelectedDepartment as Department).DepartmentId;

            using(var DbContext = new DatabaseEntities())
            {
                var department = DbContext.Departments
                    .Where(d => d.DepartmentId == departmentId)
                    .Single();

                if(department.Objects.Count > 0)
                {
                    MessageBox.Show("Удалите все предметы, прежде чем удалять тип предмета!");
                    return;
                }

                DbContext.Departments.Remove(department);
                DbContext.SaveChanges();

                _viewModel.Departments.Remove(department);
                (parameter as ItemCollection).Refresh();
            }

            UpdateDepartments();
            (parameter as ItemCollection).Refresh();
        }

        private void UpdateDepartments()
        {
            using(var DbContext = new DatabaseEntities())
            {
                _viewModel.Departments = DbContext.Departments
                    .ToList();
            }
        }
    }
}
