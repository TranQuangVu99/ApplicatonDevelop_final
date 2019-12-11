using FDR.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FDR.DependencyInjection
{
    public static class SqlSupport
    {
        public static DbEntities db = new DbEntities();

        public static IEnumerable<AttendanceStudents> GetAllStudentByIdClass(string idClass)
        {
            //lay toan bo ngay diem danh
            var query = from c in db.NgayHocs
                        where c.MaLopHocPhan.Equals(idClass)
                        select c;

            var ds = query.ToList();
            //lay toan bo ngay 
            var tongngay = from c in ds
                           group c by c.NgayHoc1 into g
                           select g.Key;

            //lay danh sach sinh vien
            var dsSv = from c in ds
                       group c by c.MaSinhVien into g
                       select g.Key;

            return null;
        }

        public static IEnumerable<AttendanceStudents> GetAllStudentsByIdClass(string idClass)
        {
            var db = new DbEntities();

            var query = from c in db.ListGetStudents(idClass)
                        select c;
            var days = idClass.GetAllDayFromMLHP_ID();

            var dsDD = (from c in db.NgayHocs
                       where c.MaLopHocPhan.Equals(idClass)
                       select c).ToList();
            var ds = new List<AttendanceStudents>();

            int i = 0;
            foreach (var t in query.ToList())
            {
                i++;
                var newe = new AttendanceStudents();
                newe.STT = i;
                newe.HoDem = t.Họ_Đệm;
                newe.NhomDangKy = t.Nhóm_Đăng_Ký;
                newe.Mssv = t.MSSV;
                newe.Ten = t.Tên;
                newe.TenLop = t.Lớp;
                foreach (var day in days)
                {
                    var statusday = dsDD.SingleOrDefault(n => n.MaSinhVien.Equals(newe.Mssv) && n.NgayHoc1.Equals(day));
                    var date = (DateTime)day;
                    if (statusday != null)
                    {
                        newe.Dss.Add(new CheckDateStudents()
                        {
                            DateCheck = date,
                            IsExists = true
                        });
                    }
                    else
                    {
                        newe.Dss.Add(new CheckDateStudents()
                        {
                            DateCheck = date,
                            IsExists = false
                        });
                    }
                }
                ds.Add(newe);
            }
            return ds;
        }

        public static IEnumerable<DateTime?> GetAllDayFromMLHP_ID(this string mlhp)
        {
            return db.GetAllNgayHocByMLHP(mlhp);
        }

        private static void ConvertToDateTime(this string value)
        {
            DateTime convertedDate;
            try
            {
                convertedDate = Convert.ToDateTime(value);
                Console.WriteLine("'{0}' converts to {1} {2} time.",
                                  value, convertedDate,
                                  convertedDate.Kind.ToString());
            }
            catch (FormatException)
            {
                Console.WriteLine("'{0}' is not in the proper format.", value);
            }
        }
    }
}
