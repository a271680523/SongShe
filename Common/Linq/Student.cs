using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Linq
{
    [Table("Student")]
    public class Student
    {
        private int _ID;
        private string _StuName;
        private string _Address;
        private int _Sex;
        private int _CollegeID;

        [Column("ID", ColumnType = DataType.Int)]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        [Column("StuName")]
        public string Name
        {
            get { return _StuName; }
            set { _StuName = value; }
        }
        [Column("Address")]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        [Column("Sex", ColumnType = DataType.Int)]
        public int Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }
        [Column("CollegeID", ColumnType = DataType.Int)]
        public int CollegeID
        {
            get { return _CollegeID; }
            set { _CollegeID = value; }
        }
    }
}
