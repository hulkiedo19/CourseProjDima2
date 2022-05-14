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
    public class AddObjectCommand : Commands
    {
        private MainWindowViewModel _viewModel;

        public AddObjectCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            // add selected subject
            if (_viewModel.SelectedObject != null)
            {
                using (var DbContext = new DatabaseEntities())
                {
                    DbContext.Objects.Add((_viewModel.SelectedObject as Models.Object));
                    DbContext.SaveChanges();
                    (parameter as ItemCollection).Refresh();
                }

                return;
            }

            if (_viewModel.ObjectTypeId == null || _viewModel.InvNumber == null || _viewModel.AmountObjects == null || _viewModel.DepartmentId == null)
            {
                MessageBox.Show("Please enter text to textboxes");
                return;
            }

            // add default subject
            Models.Object _object = CreateObject();

            using (var DbContext = new DatabaseEntities())
            {
                var ObjectType = DbContext.ObjectTypes
                    .Where(x => x.ObjectTypeId == _object.ObjectType)
                    .FirstOrDefault();

                var Department = DbContext.Departments
                    .Where(d => d.DepartmentId == _object.Department)
                    .FirstOrDefault();

                if (ObjectType == default || Department == default)
                {
                    MessageBox.Show("this type of subject doesn't exists, please try again");
                    return;
                }

                _object.ObjectType1 = ObjectType;
                _object.Department1 = Department;

                DbContext.Objects.Add(_object);
                DbContext.SaveChanges();

                _viewModel.Objects.Add(_object);
                (parameter as ItemCollection).Refresh();
            }
        }

        private Models.Object CreateObject()
        {
            Models.Object _object = new Models.Object()
            {
                ObjectType = Convert.ToInt32(_viewModel.ObjectTypeId),
                InventoryNumber = _viewModel.InvNumber,
                AmountObjects = Convert.ToInt32(_viewModel.AmountObjects),
                Department = Convert.ToInt32(_viewModel.DepartmentId)
            };

            return _object;
        }
    }
}
