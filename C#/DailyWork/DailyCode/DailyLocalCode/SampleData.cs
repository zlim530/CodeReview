using System;
using System.Collections.Generic;

namespace DailyLocalCode
{
    public class SampleData
    {
        static List<Defect> defects;
        static List<User> users;
        static List<Project> projects;
        static List<NotificationSubscription> subscriptions;

        public static IEnumerable<Defect> Defects { get { return defects; } }
        public static IEnumerable<User> AllUsers { get { return users; } }
        public static IEnumerable<Project> AllProjects { get { return projects; } }
        public static IEnumerable<NotificationSubscription> AllSubscriptions { get { return subscriptions; } }

        public static readonly DateTime Start = May(1);

        public static readonly DateTime End = May(31);

        public static DateTime May(int day)
        {
            return new DateTime(2013, 5, day);
        }

        public static class Projects
        {
            public static readonly Project SkeetyMediaPlayer = new Project { Name = "Skeety Media Player"};
            public static readonly Project SkeetyTalk = new Project { Name = "Skeety Talk" };
            public static readonly Project SkeetyOffice = new Project { Name = "Skeety Office" };
        }

        public static class Users
        {
            public static readonly User TesterTim = new User("Tim Trotter",UserType.Tester);
            public static readonly User TeseterTara = new User("Tara Tutu",UserType.Tester);
            public static readonly User DeveloperDeborah = new User("Darren Dahlia",UserType.Developer);
            public static readonly User ManagerMary = new User("Mary Malcop",UserType.Manager);
            public static readonly User CutomerColin = new User("Colin Carton",UserType.Customer);
        }

    }

    public class User
    {
        public string Name { get; set; }
        public UserType UserType { get; set; }

        public User(string name, UserType userType)
        {
            Name = name;
            UserType = userType;
        }

        public override string ToString()
        {
            return string.Format("User:{0}({1})",Name, UserType);
        }
    }

    public class Project
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("Project:{0}", Name);
        }
    }

    public class NotificationSubscription
    {
        public Project Project { get; set; }
        public string EmailAddress { get; set; }
    }

    public class Defect
    {
        public Project Project { get; set; }
        public User AssignedTo { get; set; }
        public string Summary { get; set; }
        public Seveity Severity { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public User CreatedBy { get; set; }
        public int ID { get; set; }

        public Defect()
        {
            ID = StaticCounter.Next();
        }

        public override string ToString()
        {
            return string.Format("{0,2}: {1}\r\n    ({2:d}-{3:d}, {4}/{5}, {6} -> {7})",ID,Summary,Created,LastModified, Severity,Status,CreatedBy.Name,AssignedTo == null ? "n/a":AssignedTo.Name);
        }

    }

    public static class StaticCounter
    {
        static int next = 1;

        public static int Next()
        {
            return next++;
        }
    }

    public enum Status: byte
    {
        Created,
        Accepted,
        Fixed,
        Reopened,
        Closed
    }

    public enum Seveity:byte
    {
        Trivial,
        Minor,
        Major,
        Showstopper
    }

    public enum UserType:byte
    {
        Customer,
        Developer,
        Tester,
        Manager
    }


}