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
    
    public partial class Teachers
    {
        public Teachers()
        {
            this.TeacherClasses = new HashSet<TeacherClasses>();
        }
    
        public int ID { get; set; }
        public Nullable<int> DeptID { get; set; }
        public string Name { get; set; }
        public string TeacherNo { get; set; }
        public string Password { get; set; }
        public int IsLogin { get; set; }
        public int Status { get; set; }
    
        public virtual Departments Departments { get; set; }
        public virtual ICollection<TeacherClasses> TeacherClasses { get; set; }
    }
}
