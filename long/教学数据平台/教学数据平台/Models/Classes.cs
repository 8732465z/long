//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace 教学数据平台.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Classes
    {
        public Classes()
        {
            this.Students = new HashSet<Students>();
            this.TeacherClasses = new HashSet<TeacherClasses>();
        }
    
        public int ID { get; set; }
        public Nullable<int> MajorID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    
        public virtual Majors Majors { get; set; }
        public virtual ICollection<Students> Students { get; set; }
        public virtual ICollection<TeacherClasses> TeacherClasses { get; set; }
    }
}
