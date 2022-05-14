using CourseProj.Commands;
using CourseProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProj.ViewModels
{
    public class MainWindowViewModel : ViewModels
    {
        private string _objectTypeId;
        private string _invNumber;
        private string _amountObjects;
        private string _departmentId;

        private List<Models.Object> _objects;
        private object _selectedObject;

        private string _name;
        private string _descriptionObjectType;

        private List<Models.ObjectType> _objectTypes;
        private object _selectedObjectType;

        private string _departmentName;
        private string _departmentDescription;

        private List<Models.Department> _departments;
        private object _selectDepartment;

        public ICommand AddObject => new AddObjectCommand(this);
        public ICommand DeleteObject => new DeleteObjectCommand(this);
        public ICommand UpdateObjects => new UpdateObjectsCommand(this);

        public ICommand AddObjectType => new AddObjectTypesCommand(this);
        public ICommand DeleteObjectType => new DeleteObjectTypesCommand(this);
        public ICommand UpdateObjectType => new UpdateObjectTypesCommand(this);

        public ICommand AddDepartment => new AddDepartmentCommand(this);
        public ICommand DeleteDepartment => new DeleteDepartmentCommand(this);
        public ICommand UpdateDepartment => new UpdateDepartmentCommand(this);

        public MainWindowViewModel()
        {
            using (var dbContext = new DatabaseEntities())
            {
                _objects = dbContext.Objects
                    .Include(nameof(Models.Object.Department1))
                    .Include(nameof(Models.Object.ObjectType1))
                    .ToList();

                _departments = dbContext.Departments
                    .ToList();

                _objectTypes = dbContext.ObjectTypes
                    .ToList();
            }
        }


        public List<Models.Object> Objects
        {
            get => _objects;
            set => Set(ref _objects, value, nameof(Objects));
        }

        public object SelectedObject
        {
            get => _selectedObject;
            set => Set(ref _selectedObject, value, nameof(SelectedObject));
        }

        public string ObjectTypeId
        {
            get => _objectTypeId;
            set => Set(ref _objectTypeId, value, nameof(ObjectTypeId));
        }

        public string InvNumber
        {
            get => _invNumber;
            set => Set(ref _invNumber, value, nameof(InvNumber));
        }

        public string AmountObjects
        {
            get => _amountObjects;
            set => Set(ref _amountObjects, value, nameof(AmountObjects));
        }

        public string DepartmentId
        {
            get => _departmentId;
            set => Set(ref _departmentId, value, nameof(DepartmentId));
        }


        public List<ObjectType> ObjectTypes
        {
            get => _objectTypes;
            set => Set(ref _objectTypes, value, nameof(ObjectTypes));
        }

        public object SelectedObjectType
        {
            get => _selectedObjectType;
            set => Set(ref _selectedObjectType, value, nameof(SelectedObjectType));
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value, nameof(Name));
        }

        public string DescriptionObjectType
        {
            get => _descriptionObjectType;
            set => Set(ref _descriptionObjectType, value, nameof(DescriptionObjectType));
        }


        public List<Department> Departments
        {
            get => _departments;
            set => Set(ref _departments, value, nameof(Departments));
        }

        public object SelectedDepartment
        {
            get => _selectDepartment;
            set => Set(ref _selectDepartment, value, nameof(SelectedDepartment));
        }

        public string DepartmentName
        {
            get => _departmentName;
            set => Set(ref _departmentName, value, nameof(DepartmentName));
        }

        public string DepartmentDescription
        {
            get => _departmentDescription;
            set => Set(ref _departmentDescription, value, nameof(DepartmentDescription));
        }
    }
}
