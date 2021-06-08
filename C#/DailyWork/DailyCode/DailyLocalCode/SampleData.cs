using System.Collections.Generic;

namespace DailyLocalCode
{
    public class SampleData
    {
        public static IEnumerable<Defect> Defects { get; set; }
        public static IEnumerable<Users> AllUsers { get; set; }
        public static IEnumerable<Project> AllProjects { get; set; }
        public static IEnumerable<NotificationSubscription> AllSubscriptions { get; set; }

        public class Users  
        {
            public string Name { get; set; }

            public override string ToString()
            {
                return Name.ToString();
            }
        }

        public class Project
        {
            public string Name { get; set; }
        }
    }

    public class NotificationSubscription
    {
        public string EmailAddress { get; set; }
    }

    public class Defect
    {
        public string Created { get; set; }
        public int ID { get; set; }
        public string LastModified { get; set; }
        public string Summary { get; set; }
    }

    public enum Satus
    {
        Created,
        Accepted,
        Fixed,
        Reopened,
        Closed
    }

    public enum Seveity
    {
        Trivial,
        Minor,
        Major,
        Showstopper
    }

    public enum UserType
    {
        Customer,
        Developer,
        Tester,
        Manager
    }
}