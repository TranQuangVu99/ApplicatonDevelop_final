using FDR.DataModels;
using FDR.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FDR.MyUserControl
{
    /// <summary>
    /// Interaction logic for PageAttendance.xaml
    /// </summary>
    public partial class PageAttendance : UserControl
    {
        public PageAttendance()
        {
            InitializeComponent();


            var ListSV = new ObservableCollection<AttendanceStudents>(SqlSupport.GetAllStudentsByIdClass("420300224601"));

        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
           // MessageBox.Show("loaded");
        }
        // Cach 1 : Xử lý trong đây 
        // Cách 2 : Xử lý trong ViewModel  ( Bắt sự kiện Loaded View XAML -> parameter truyền vào là : DataGridView )
    }
}
