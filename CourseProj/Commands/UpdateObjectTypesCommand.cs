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
    public class UpdateObjectTypesCommand : Commands
    {
        private MainWindowViewModel _viewModel;

        public UpdateObjectTypesCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
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
