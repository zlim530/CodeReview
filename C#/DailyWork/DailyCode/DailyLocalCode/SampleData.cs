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

        public static IEnumerable<Defect> AllDefects { get { return defects; } }
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
            public static readonly User TesterTara = new User("Tara Tutu",UserType.Tester);
            public static readonly User DeveloperDeborah = new User("Darren Dahlia",UserType.Developer);
            public static readonly User DeveloperDarren = new User("Darren Dahlia",UserType.Developer);
            public static readonly User ManagerMary = new User("Mary Malcop",UserType.Manager);
            public static readonly User CustomerColin = new User("Colin Carton",UserType.Customer);
        }

        static SampleData()
        {
            projects = new List<Project>
            {
                Projects.SkeetyMediaPlayer,
                Projects.SkeetyTalk,
                Projects.SkeetyOffice
            };

            users = new List<User>
            {
                Users.TesterTim,
                Users.TesterTara,
                Users.DeveloperDeborah,
                Users.DeveloperDarren,
                Users.ManagerMary,
                Users.CustomerColin
            };

            subscriptions = new List<NotificationSubscription>
            {
                new NotificationSubscription{ Project = Projects.SkeetyMediaPlayer, EmailAddress = "media-bugs@skeetysoft.com"},
                new NotificationSubscription{
                    Project = Projects.SkeetyTalk,EmailAddress = "talk-bugs@skeetysoft.com"
                },
                new NotificationSubscription{
                    Project = Projects.SkeetyOffice,EmailAddress = "office-bugs@skeetysoft.com"
                },
                new NotificationSubscription{ 
                    Project = Projects.SkeetyMediaPlayer,EmailAddress = "theboss@skeetysoft.com"   
                }
            };

            defects = new List<Defect>
            {
                new Defect
                {
                    Project = Projects.SkeetyMediaPlayer,
                    Created = May(1),
                    CreatedBy = Users.TesterTim,
                    Summary = "MP3 files crash system",
                    Severity = Severity.Showstopper,
                    AssignedTo = Users.DeveloperDarren,
                    Status = Status.Accepted,
                    LastModified = May(23)
                },

                new Defect
                {
                    Project = Projects.SkeetyMediaPlayer,
                    Created = May(3),
                    CreatedBy = Users.DeveloperDeborah,
                    Summary = "Text is too big",
                    Severity = Severity.Trivial,
                    AssignedTo = null,
                    Status = Status.Closed,
                    LastModified = May(9)
                },

                new Defect
                {
                    Project = Projects.SkeetyTalk,
                    Created = May(3),
                    CreatedBy = Users.CustomerColin,
                    Summary = "Sky is wrong shade of blue",
                    Severity = Severity.Minor,
                    AssignedTo = Users.TesterTara,
                    Status = Status.Fixed,
                    LastModified = May(19)
                },

                new Defect
                {
                    Project = Projects.SkeetyMediaPlayer,
                    Created = May(4),
                    CreatedBy = Users.DeveloperDarren,
                    Summary = "Can't play files more than 200 bytes long",
                    Severity = Severity.Major,
                    AssignedTo = Users.DeveloperDarren,
                    Status = Status.Reopened,
                    LastModified = May(23)
                },

                new Defect
                {
                    Project = Projects.SkeetyMediaPlayer,
                    Created = May(6),
                    CreatedBy = Users.TesterTim,
                    Summary = "Installation is slow",
                    Severity = Severity.Trivial,
                    AssignedTo = Users.TesterTim,
                    Status = Status.Fixed,
                    LastModified = May(15)
                },

                new Defect
                {
                    Project = Projects.SkeetyMediaPlayer,
                    Created = May(7),
                    CreatedBy = Users.ManagerMary,
                    Summary = "DivX is choppy on Pentium 100",
                    Severity = Severity.Major,
                    AssignedTo = Users.DeveloperDarren,
                    Status = Status.Accepted,
                    LastModified = May(29)
                },

                new Defect
                {
                    Project = Projects.SkeetyTalk,
                    Created = May(8),
                    CreatedBy = Users.DeveloperDeborah,
                    Summary = "Client acts as virus",
                    Severity = Severity.Showstopper,
                    AssignedTo = null,
                    Status = Status.Closed,
                    LastModified = May(10)
                },

                new Defect
                {
                    Project = Projects.SkeetyMediaPlayer,
                    Created = May(8),
                    CreatedBy = Users.DeveloperDarren,
                    Summary = "Subtitles only work in Welsh",
                    Severity = Severity.Major,
                    AssignedTo = Users.TesterTim,
                    Status = Status.Fixed,
                    LastModified = May(23)
                },

                new Defect
                {
                    Project = Projects.SkeetyTalk,
                    Created = May(9),
                    CreatedBy = Users.CustomerColin,
                    Summary = "Voice recognition is confused by background noise",
                    Severity = Severity.Minor,
                    AssignedTo = null,
                    Status = Status.Closed,
                    LastModified = May(15)
                },

                new Defect
                {
                    Project = Projects.SkeetyTalk,
                    Created = May(9),
                    CreatedBy = Users.TesterTim,
                    Summary = "User interface should be more caramelly",
                    Severity = Severity.Trivial,
                    AssignedTo = Users.DeveloperDarren,
                    Status = Status.Created,
                    LastModified = May(9)
                }
            };
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
        public Severity Severity { get; set; }
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

    public enum Severity : byte
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