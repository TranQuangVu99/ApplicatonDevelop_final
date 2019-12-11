using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FDR.MyUserControl
{
    public class MyDataGrid : DataGrid
    {
        public ObservableCollection<string> ColumnHeaders
        {
            get { return GetValue(ColumnHeadersProperty) as ObservableCollection<string>; }
            set { SetValue(ColumnHeadersProperty, value); }
        }

        public static readonly DependencyProperty ColumnHeadersProperty = DependencyProperty.Register("ColumnHeaders", typeof(ObservableCollection<string>), typeof(MyDataGrid), new PropertyMetadata(new PropertyChangedCallback(OnColumnsChanged)));

    static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as MyDataGrid;
            dataGrid.Columns.Clear();
            //Add Person Column
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "STT", Binding = new Binding("STT") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Họ Đệm", Binding = new Binding("HoDem") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Tên", Binding = new Binding("Ten") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Tên Lớp", Binding = new Binding("TenLop") });
            dataGrid.Columns.Add(new DataGridTextColumn() { Header = "Nhóm Đăng Kí", Binding = new Binding("NhomDangKi") });


            //Add Manufactures Columns
            foreach (var value in dataGrid.ColumnHeaders)
            {
                var column = new DataGridCheckBoxColumn()
                { Header = value, Binding = new Binding("Dss"){ ConverterParameter = value, Converter = new ManufacturerConverter() },
                
                  };
                column.Width = 100;
                dataGrid.Columns.Add(column);
            }
        }
    }
}
