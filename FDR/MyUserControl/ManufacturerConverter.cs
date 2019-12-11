using FDR.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FDR.MyUserControl
{
    public class ManufacturerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var smartPhones = value as IEnumerable<SmartPhone>;
            //if (smartPhones != null && parameter != null)
            //{
            //    var phone = smartPhones.FirstOrDefault(s => s.Manufacturer == parameter.ToString());
            //    if (phone != null)
            //        return phone.IsWorking;
            //    return false;
            //}
            //return false;

            var smartPhones = value as IEnumerable<CheckDateStudents>;
            if (smartPhones != null && parameter != null)
            {
                var date = parameter.ToString();

                var phone = smartPhones.FirstOrDefault(s => s.DateCheck.ToShortDateString().Equals(date));
                if (phone != null)
                    return phone.IsExists;
                return false;
            }
            return false;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        
    }
}
