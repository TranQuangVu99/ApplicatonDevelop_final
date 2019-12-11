using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDR.DataModels
{
    public class AttendanceStudents : BaseDataModel
    {
        private string _mssv, _ten, _hoDem;

        private int _sTT;
        private string _tenLop;
        private int? _nhomDangKy;
        private string _maLopHocPhan;

        private List<CheckDateStudents> _checkDateStudents;

        public List<CheckDateStudents> Dss
        {
            get => _checkDateStudents;
            set
            {
                _checkDateStudents = value;

            }
        }

        public string Mssv
        {
            get => _mssv;
            set
            {

                _mssv = value;
                OnPropertyChanged("Mssv");

            }
        }
        public string Ten {
            get => _ten;
            set
            {

                _ten = value;
                OnPropertyChanged("Ten");

            }
        }
        public string HoDem {
            get => _hoDem;
            set
            {

                _hoDem = value;
                OnPropertyChanged("HoDem");

            }
        }

        public int STT { get => _sTT; set { _sTT = value; OnPropertyChanged("STT"); } }

        public string TenLop { get => _tenLop; set { _tenLop = value; OnPropertyChanged("TenLop"); } }
        public int? NhomDangKy { get => _nhomDangKy; set { _nhomDangKy = value; OnPropertyChanged("NhomDangKy"); } }
        public string MaLopHocPhan { get => _maLopHocPhan; set { _maLopHocPhan = value; OnPropertyChanged("MaLopHocPhan");} }

        public AttendanceStudents()
        {
            this.Dss = new List<CheckDateStudents>();
        }
    }
}
