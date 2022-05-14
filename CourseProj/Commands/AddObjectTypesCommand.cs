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
    public class AddObjectTypesCommand : Commands
    {
        private MainWindowViewModel _viewModel;

        public AddObjectTypesCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (_viewModel.SelectedObjectType != null)
            {
                using (var DbContext = new DatabaseEntities())
                {
                    DbContext.ObjectTypes.Add((_viewModel.SelectedObjectType as ObjectType));
                    DbContext.SaveChanges();
                    (parameter as ItemCollection).Refresh();
                }
                return;
            }

            if (_viewModel.Name == null || _viewModel.DescriptionObjectType == null)
            {
                MessageBox.Show("Please enter text to textboxes");
                return;
            }

            ObjectType objectType = CreateObjectType();

            using (var DbContext = new DatabaseEntities())
            {
                DbContext.ObjectTypes.Add(objectType);
                DbContext.SaveChanges();

                _viewModel.ObjectTypes.Add(objectType);
                (parameter as ItemCollection).Refresh();
            }
        }

        private ObjectType CreateObjectType()
        {
            ObjectType objectType = new ObjectType()
            {
                Name = _viewModel.Name,
                Description = _viewModel.DescriptionObjectType
            };

            return objectType;
        }
    }
}
