using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDR.DependencyInjection
{
    public static class StateSubject
    {
        private static string _maHP;
        public static string MaHP
        {
            get
            {
                return _maHP;
            }
            set
            {
                _maHP = value;
            }
        }
    }
}
