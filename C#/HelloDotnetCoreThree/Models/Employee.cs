namespace HelloDotnetCoreThree.Models {
    public class Employee {// 一个部门下可以有多个员工，因此 Employee 与 Department 是多对一的关系
        public int Id { get; set; }

        // 这里有一个 DepartmentId 的外键
        public int DepartmentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public bool Fired { get; set; }

        public Employee(){
            
        }
    }

    public enum Gender { 
        女 = 0,
        男 = 1
    }
}
