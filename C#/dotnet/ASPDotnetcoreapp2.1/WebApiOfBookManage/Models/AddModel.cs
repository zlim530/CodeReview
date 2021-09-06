namespace WebApiOfBoolManage.Models
{
    public class AddRequest
    {
        public string ISBN;
        public string name;
        public decimal price;
        public string date;
        public Author[] authors;
    }


    /// <summary>
    /// Author 作者的基本信息
    /// </summary>
    public class Author
    {
        // 作者名
        public string name;
        // 作者性别
        public string sex;
        // 作者生日
        public string birthday;
    }

    
    /// <summary>
    /// 返回处理结果
    /// </summary>
    public class AddRespose
    {
        // 返回结果代码
        public int code;
        // 返回信息
        public string message;
        // 返回的 ISBN 号
        public string ISBN;
    }
}