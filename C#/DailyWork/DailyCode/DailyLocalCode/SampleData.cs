using System;
using System.Collections.Generic;
using static DailyLocalCode.SampleData;

namespace DailyLocalCode
{
    public class SampleData
    {
        public static IEnumerable<Defect> Defects { get; set; }
        public static IEnumerable<User> AllUsers { get; set; }
        public static IEnumerable<Project> AllProjects { get; set; }
        public static IEnumerable<NotificationSubscription> AllSubscriptions { get; set; }

        public static readonly DateTime Start = May(1);

        public static readonly DateTime End = May(31);

        public class User  
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

            public override string ToString()
            {
                return string.Format("Project:{0}",Name);
            }
        }

        public static DateTime May(int day)
        {
            return new DateTime(2013, 5, day);
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

    public enum Status
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