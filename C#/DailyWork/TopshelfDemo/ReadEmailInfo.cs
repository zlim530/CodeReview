using log4net;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TopshelfDemo
{
    public static class ReadEmailInfo
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ReadEmailInfo));

        public static void ReadPop3()
        {
            using (var imapclient = new ImapClient())
            {
                try
                {
                    _log.Info("It is " + DateTime.Now + "And The ReturnMailFunction invoked successfully~!");

                    imapclient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    imapclient.Connect("smc.com.cn", 143, false);
                    _log.Info("The mail server connected successfully~!");

                    // 配置文件读取账号与密码
                    var configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("AppSettings.json").Build();
                    var account = configuration.GetSection("account").Value;
                    var passWord = configuration.GetSection("passWord").Value;
                    imapclient.Authenticate(account, passWord);
                    List<IMailFolder> mailFolderList = imapclient.GetFolders(imapclient.PersonalNamespaces[0]).ToList();
                    var folder = imapclient.GetFolder("INBOX");
                    //打开文件夹并设置为读的方式
                    folder.Open(MailKit.FolderAccess.ReadWrite);
                    var deliveredDate = configuration.GetSection("DeliveredAfterDate").Value;
                    //获取大于等于 deliveredDate 的所有邮件的唯一Id
                    var uidss = folder.Search(SearchQuery.DeliveredAfter(DateTime.Parse(/*"2021-4-27"*/deliveredDate)));
                    //只获取收件箱文件夹
                    var boxinfo = imapclient.Inbox;
                    var info = boxinfo.Fetch(uidss, MessageSummaryItems.Flags | MessageSummaryItems.GMailLabels);

                    // 获取数据库对象
                    var context = FluentDataDB.Context();
                    context.CommandTimeout(30);
                    _log.Info("The database server connected successfully~!");
                    // 获取应用程序输出路径 
                    var rootPath = AppDomain.CurrentDomain.BaseDirectory;

                    if (info != null)
                    {
                        //获取邮件信息
                        foreach (var item in info)
                        {
                            MimeMessage message = folder.GetMessage(item.UniqueId);
                            var from = message.From.ToString();
                            var flag = item.Flags;
                            if (from == "AUTO_MAIL_SEND@SMCJPN.CO.JP" && flag != MessageFlags.Seen)
                            {
                                var attachments = message.Attachments;
                                if (attachments.Any())
                                {
                                    //提取该邮件所有普通附件
                                    foreach (var attach in attachments)
                                    {
                                        var day = message.Date.DateTime.ToString("yyyyMMdd HHmmss").Split(" ");
                                        var date = day[0] + day[1];
                                        var rnumber = new Random().Next(1, 9999);
                                        var path = rootPath + "AttachMents" + $"/{date}";
                                        var filePath = path + $"/{attach.ContentDisposition.FileName}";

                                        if (!Directory.Exists(path))
                                        {
                                            Directory.CreateDirectory(path);
                                        }
                                        else
                                        {
                                            Directory.CreateDirectory(path + $"/{rnumber}");
                                        }
                                        var part = (MimePart)attach;
                                        using (var stream = File.Create(filePath))
                                            part.Content.DecodeTo(stream);

                                        if (part != null && context.Sql("select count(*) from ReturnMailInfo where Folder = @0", date).QuerySingle<int>() < 1)
                                        {
                                            //插入数据，返回自增ID
                                            int productId = context.Insert("ReturnMailInfo")
                                                                    .Column("CreationTime", DateTime.Now)
                                                                    .Column("Folder", date)
                                                                    .Column("FileName", attach.ContentDisposition.FileName)
                                                                    .Column("Status", 0)
                                                                    .Column("IsDeleted", 0)
                                                                    .ExecuteReturnLastId<int>();
                                            _log.Info("The insert statement executed successfully~!");
                                        }

                                        var messageFlags = MessageFlags.Seen;
                                        //设置邮件为已读状态
                                        folder.SetFlags(item.UniqueId, messageFlags, false);
                                        _log.Info("The email status updated successfully~!");
                                    }
                                }
                            }
                        }
                    }
                    //关闭文件夹
                    folder.Close();
                    _log.Info("The email folder closed successfully~!");
                }
                catch (Exception e)
                {
                    _log.Error(e.Message);
                }

            }

        }
    }
}
