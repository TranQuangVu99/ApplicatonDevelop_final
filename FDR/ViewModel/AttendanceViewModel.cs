using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FDR.DataModels;
using FDR.DependencyInjection;
using FDR.Repositories.Implement;
using FDR.Repositories.Interface;
using Prism.Commands;

namespace FDR.ViewModel
{
    public class AttendanceViewModel : BaseViewModel
    {
        private string _today;
        private IBase<LopHocPhan> lhp_repo;
        private ObservableCollection<AttendanceStudents> _listSV;
        private ObservableCollection<LopHocPhan> _cbbSource;
        private ObservableCollection<AttendanceStudents> _personCollection;
        public DelegateCommand<DataGrid> LoadedCommand
        {
            get;
            private set;
        }

        private AttendanceStudents _selectedCbb;

        public DelegateCommand<UserControl> DDCommand
        {
            get;
            set;
        }

        public DelegateCommand SelectCBB
        {
            get;
            private set;
        }

        public string Today
        {
            get => _today;

            set
            {
                if (value != null)
                {
                    _today = value;
                    OnPropertyChanged("Today");
                }
            }
        }

        public ObservableCollection<AttendanceStudents> ListSV { get => _listSV; set { _listSV = value; OnPropertyChanged("ListSV"); } }

        public ObservableCollection<LopHocPhan> CbbSource
        {
            get => _cbbSource;
            set
            {
                _cbbSource = value;
                OnPropertyChanged("CbbSource");
            }
        }

        public ObservableCollection<string> ColumnHeaders { get; set; }

        public ObservableCollection<AttendanceStudents> PersonCollection
        {
            get => _personCollection;
            set
            {
                _personCollection = value;
                OnPropertyChanged();
            }
        }

        public AttendanceStudents SelectedCbb
        {
            get => _selectedCbb;
            set {
                _selectedCbb = value;
                OnPropertyChanged();
                if(value!=null)
                {
                    StateSubject.MaHP = SelectedCbb.MaLopHocPhan;
                }
            }
        }

        public AttendanceViewModel()
        {
            lhp_repo = new BaseRepositories<LopHocPhan>();

            Today = DateTime.Now.ToShortDateString();

            LoadedCommand = new DelegateCommand<DataGrid>(CreateDataGrid);

            ColumnHeaders = new ObservableCollection<string>();
           
            DDCommand = new DelegateCommand<UserControl>(OpenDialog);
            PersonCollection = new ObservableCollection<AttendanceStudents>(SqlSupport.GetAllStudentsByIdClass("420300224601"));


            foreach (var item in PersonCollection.SelectMany(s => s.Dss).Select(s => s.DateCheck).Distinct())
            {
                ColumnHeaders.Add(item.ToShortDateString());
            }
        }

        private void OpenDialog(UserControl p)
        {
            Window1 view = new Window1();
            view.Show();
            FrameworkElement window = GetWindowParent(p);
            var w = window as Window;
            if (w != null)
            {
                w.Close();
            }
        }

        FrameworkElement GetWindowParent(UserControl p)
        {
            FrameworkElement parent = p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }

        private void CreateDataGrid(DataGrid obj)
        {

           

        }

    }
}
