using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDR.DataModels
{
    public class CheckDateStudents : BaseDataModel
    {
        private DateTime _dateCheck;
        private bool? _isExists;

        public DateTime DateCheck
        {
            get => _dateCheck;
            set
            {
                if (_dateCheck != null)
                {
                    _dateCheck = value;
                    OnPropertyChanged("DateCheck");
                }
            }
        }
        public bool? IsExists
        {
            get => _isExists;
            set
            {
                _isExists = value;
                 OnPropertyChanged("IsExists");
                
            }
        }
    }
}
