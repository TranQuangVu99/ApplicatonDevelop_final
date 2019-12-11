using FDR.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FDR.DependencyInjection
{
    public static class ViewComponents
    {
        public static IEnumerable<DateTime> GetAllDateByIdSubject(string idSub)
        {
            var db = new DbEntities();

            var query = from c in db.NgayHocs
                        where c.MaLopHocPhan.Equals(idSub)
                        group c by  c.NgayHoc1  into g
                        select g.Key;


            return query.ToList();
        }
        public static IEnumerable<ListGetStudents_Result> GetAllStudentsByIdClass(string idClass)
        {
            var db = new DbEntities();
           
            var query = from c in db.ListGetStudents(idClass)
                        
                        select c;
            return query.ToList();
        }

    }
}
