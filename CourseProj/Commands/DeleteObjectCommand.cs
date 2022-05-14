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
    public class DeleteObjectCommand : Commands
    {
        private MainWindowViewModel _viewModel;

        public DeleteObjectCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            int objectId = (_viewModel.SelectedObject as Models.Object).ObjectId;

            using (var DbContext = new DatabaseEntities())
            {
                var _object = DbContext.Objects
                    .Where(s => s.ObjectId == objectId)
                    .Single();

                DbContext.Objects.Remove(_object);
                DbContext.SaveChanges();

                _viewModel.Objects.Remove(_object);
                (parameter as ItemCollection).Refresh();
            }

            UpdateObjects();
            (parameter as ItemCollection).Refresh();
        }

        private void UpdateObjects()
        {
            using (var DbContext = new DatabaseEntities())
            {
                _viewModel.Objects = DbContext.Objects
                    .Include(nameof(Models.Object.ObjectType1))
                    .ToList();
            }
        }
    }
}
