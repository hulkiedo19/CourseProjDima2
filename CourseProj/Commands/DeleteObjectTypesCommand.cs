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
    public class DeleteObjectTypesCommand : Commands
    {
        private MainWindowViewModel _viewModel;

        public DeleteObjectTypesCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            int objectTypeId = (_viewModel.SelectedObjectType as ObjectType).ObjectTypeId;

            using (var DbContext = new DatabaseEntities())
            {
                var objectType = DbContext.ObjectTypes
                    .Where(t => t.ObjectTypeId == objectTypeId)
                    .Single();

                if (objectType.Objects.Count > 0)
                {
                    MessageBox.Show("Удалите все предметы, прежде чем удалять тип предмета!");
                    return;
                }

                DbContext.ObjectTypes.Remove(objectType);
                DbContext.SaveChanges();

                _viewModel.ObjectTypes.Remove(objectType);
                (parameter as ItemCollection).Refresh();
            }

            UpdateObjectTypes();
            (parameter as ItemCollection).Refresh();
        }

        private void UpdateObjectTypes()
        {
            using (var DbContext = new DatabaseEntities())
            {
                _viewModel.ObjectTypes = DbContext.ObjectTypes
                    .ToList();
            }
        }
    }
}
