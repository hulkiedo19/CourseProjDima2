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
    public class UpdateObjectsCommand : Commands
    {
        private MainWindowViewModel _viewModel;

        public UpdateObjectsCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            UpdateObjects();
            (parameter as ItemCollection).Refresh();
        }

        private void UpdateObjects()
        {
            using (var DbContext = new DatabaseEntities())
            {
                _viewModel.Objects = DbContext.Objects
                    .Include(nameof(Models.Object.Department1))
                    .Include(nameof(Models.Object.ObjectType1))
                    .ToList();
            }
        }
    }
}
