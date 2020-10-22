public class Student{
	public string FirstName {get;}
	public string LastName {get;}

	public Student(string firstName,string lastName)		
	{
		if (IsNullOrWhiteSpace(lastName))	
		{
			throw new ArgumentException(message:"Cannot be blank",paramName:nameof(lastName));
		}
		FirstName = firstName;
		LastName = lastName;
	}
}




#region 2020年10月20日
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
class Program
{
	static void Main()
	{
		string rootDirectory = Environment.CurrentDirectory;
		Console.WriteLine("开始连接，端口号：8090");
		Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		socket.Bind(new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, 8090));
		socket.Listen(30);
		while (true)
		{
			Socket socketClient = socket.Accept();
			Console.WriteLine("新请求");
			byte[] buffer = new byte[4096];
			int length = socketClient.Receive(buffer, 4096, SocketFlags.None);
			string requestStr = Encoding.UTF8.GetString(buffer, 0, length);
			Console.WriteLine(requestStr);
			//
			string[] strs = requestStr.Split(new string[] { "\r\n" }, StringSplitOptions.None);
			string url = strs[0].Split(' ')[1];

			byte[] statusBytes, headerBytes, bodyBytes;

			if (Path.GetExtension(url) == ".jpg")
			{
				string status = "HTTP/1.1 200 OK\r\n";
				statusBytes = Encoding.UTF8.GetBytes(status);
				bodyBytes = File.ReadAllBytes(rootDirectory + url);
				string header = string.Format("Content-Type:image/jpg;\r\ncharset=UTF-8\r\nContent-Length:{0}\r\n", bodyBytes.Length);
				headerBytes = Encoding.UTF8.GetBytes(header);
			}
			else
			{
				if (url == "/")
					url = "默认页";
				string status = "HTTP/1.1 200 OK\r\n";
				statusBytes = Encoding.UTF8.GetBytes(status);
				string body = "<html>" +
					"<head>" +
						"<title>socket webServer  -- Login</title>" +
					"</head>" +
					"<body>" +
						"<div style=\"text-align:center\">" +
							"当前访问" + url +
						"</div>" +
					"</body>" +
				"</html>";
				bodyBytes = Encoding.UTF8.GetBytes(body);
				string header = string.Format("Content-Type:text/html;charset=UTF-8\r\nContent-Length:{0}\r\n", bodyBytes.Length);
				headerBytes = Encoding.UTF8.GetBytes(header);
			}
			socketClient.Send(statusBytes);
			socketClient.Send(headerBytes);
			socketClient.Send(new byte[] { (byte)'\r', (byte)'\n' });
			socketClient.Send(bodyBytes);

			socketClient.Close();
		}
	}
}
#endregion






#region IQeurable、IEnumerable、IList的区别
基本概念：

IEnumerable：使用的是LINQ to Object方式，它会将AsEnumerable()时对应的所有记录都先加载到内存，然后在此基础上再执行后来的Query

IQeurable（IQuerable<T>）:不在内存加载持久数据,因为这家伙只是在组装SQL，(延迟执行) 到你要使用的时候，例如  list.Tolist() or list.Count()的时候，数据才从数据库进行加载 (AsQueryable())。

IList（IList<T>）：泛型接口是 ICollection 泛型接口的子代，作为所有泛型列表的基接口，在用途方面如果作为数据集合的载体这是莫有问题的，只是如果需要对集合做各种的操作，例如 排序 编辑 统计等等，它不行。

List <> ：泛型类,它已经实现了IList <> 定义的那些方法,IList<T> list=new List<T>();只是想创建一个基于接口IList<Class1>的对象的实例，这个接口是由List<T>实现的。只是希望使用到IList<T>接口规定的功能而已


//IList
IList users = res.ToList(); //此时已把users加载到内存，而每个user的关联实体（UserInfos）未被加载，所以下一行代码无法顺利通过
var ss = users.Where(p => p.UserInfos.ID != 3); //此处报错，因为P的UserInfos实体无法被加载

// IQuerable的
IQueryable users = res.AsQueryable(); //users未被立即加载，关联实体可通过“延迟加载”获得
var ss = users.Where(p => p.UserInfos.ID != 3);//此处顺利获得对应的ss


总结:

基于性能和数据一致性这两点，使用IQueryable时必须谨慎，而在大多数情况下我们应使用IList。

1.当你打算马上使用查询后的结果(比如循环作逻辑处理或者填充到一个table/grid中)，并且你不介意该查询即时被执行后的结果可以供调用者(Consummer)作后续查询(比如这是一个"GetAll"的方法)，或者你希望该查执行，使用ToList()
2.当你希望查询后的结果可以供调用者(Consummer)作后续查询(比如这是一个"GetAll"的方法)，或者你希望该查询延时执行，使用AsQueryable()
3.按照功能由低到高：List<T> IList<T> IQueryable<T> IEnumerable<T>(IEnumerable的功能最丰富)
4.按照性能由低到高：IEnumerable<T> IQueryable<T> IList<T> List<T>(IEnumerable的性能最低)
#endregion


#region ExcelPackage类源码：namespace OfficeOpenXml Represents an Excel 2007/2010 XLSX file package. This is the top-level object
to access all parts of the document.
//
// 摘要:
//     Copies the Package to the Outstream The package is closed after it has been saved
//
// 参数:
//   OutputStream:
//     The stream to copy the package to
public void SaveAs(Stream OutputStream)
{
	File = null;
	Save();
	if (OutputStream != _stream)
	{
		CopyStream(_stream, ref OutputStream);
	}
}



//
// 摘要:
//     Saves all the components back into the package. This method recursively calls
//     the Save method on all sub-components. We close the package after the save is
//     done.
public void Save()
{
	try
	{
		if (_stream is MemoryStream && _stream.Length > 0)
		{
			CloseStream();
		}

		Workbook.Save();
		if (File == null)
		{
			if (Encryption.IsEncrypted)
			{
				MemoryStream memoryStream = new MemoryStream();
				_package.Save(memoryStream);
				byte[] package = memoryStream.ToArray();
				CopyStream(new EncryptedPackageHandler().EncryptPackage(package, Encryption), ref _stream);
			}
			else
			{
				_package.Save(_stream);
			}

			_stream.Flush();
			_package.Close();
		}
		else // if (File != null)
		{
			if (System.IO.File.Exists(File.FullName))
			{
				try
				{
					System.IO.File.Delete(File.FullName);
				}
				catch (Exception innerException)
				{
					throw new Exception($"Error overwriting file {File.FullName}", innerException);
				}
			}

			_package.Save(_stream);
			_package.Close();
			if (Stream is MemoryStream)
			{
				FileStream fileStream = new FileStream(File.FullName, FileMode.Create);
				if (Encryption.IsEncrypted)
				{
					byte[] package2 = ((MemoryStream)Stream).ToArray();
					MemoryStream memoryStream2 = new EncryptedPackageHandler().EncryptPackage(package2, Encryption);
					fileStream.Write(memoryStream2.ToArray(), 0, (int)memoryStream2.Length);
				}
				else
				{
					fileStream.Write(((MemoryStream)Stream).ToArray(), 0, (int)Stream.Length);
				}

				fileStream.Close();
				fileStream.Dispose();
			}
			else
			{
				System.IO.File.WriteAllBytes(File.FullName, GetAsByteArray(save: false));
			}
		}
	}
	catch (Exception innerException2)
	{
		if (File == null)
		{
			throw;
		}

		throw new InvalidOperationException($"Error saving file {File.FullName}", innerException2);
	}
}
#endregion




#region SMC.MES.DeviceProcessReport/Report/DeviceProcessReportAppService.cs
/// <summary>
/// 根据机器号MachinNo= A,B,C 得到今天这台机器红灯的列表, 要去掉DownTimeReport中的ListNo的停机记录
/// </summary>
/// <returns></returns>
public async Task<List<GetDownTimeByTripleLightOutput>> GetDownTimeByTripleLight(string MachineNoListString)
{
	// 尽量避免直接去 new 对象：有违背 IoC、OOP 原则
	List<GetDownTimeByTripleLightOutput> downList = new List<GetDownTimeByTripleLightOutput>();
	
	//如果传入的机器列表为空，直接返回
	if (string.IsNullOrEmpty(MachineNoListString)) {
		//MachinNoListString
		return downList;
	}

	//得到机器号列表2434,1111
	// StringSplitOptions.RemoveEmptyEntries 选项去除被分割数组中的无意义字符
	List<string> MachineNoList = MachineNoListString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();


	//从DownTimeReport表中得到今天已经报停机的列表
	// 任何 List 在使用之前都要进行非空判断！！！因为我们不能保证此 List 一定是有值的
	var exceptListNoList = await _downTimeReportRepository.GetAll()
		.Where(d => d.CreationTime.Date == DateTime.Now.Date)//今天汇报的
		.Select(d => d.ListNo).Distinct()
		.ToListAsync();

	#region 根据 PlantNo-DepartNo-MachineNo 得到停机信息
	//根据机器号得到机器Id
	// 不要使用具体的数据类型去接收，而是方法返回什么数据类型我们就要接收什么数据类型
	// 再加上注释，让人看注释就可以知道返回的List是什么类型的集合
	// List<PlanDepartMachineDto> planDepartMachineDtoList = await
	var planDepartMachineDtoList = await _v_manuDNC_Machine_collect_dicRepository.GetAll()
		.Where(dic => MachineNoList.Contains(dic.MachineNo))
		.Select(dic => new PlanDepartMachineDto
		{
			PlanNo = dic.PlanNo,
			DepartNo = dic.DepartNo,
			MachineID = dic.MachineID
		})
		.Distinct()
		.ToListAsync();
	//List<string> planDepartMachineStringList = planDepartMachineDtoList.Select(d => d.ToString()).ToList();

	//根据机器Id和Today得到三色灯停机列表，但是排除已经汇报过的
	List<V_manuDNC_V_Machine_Chart> tempV_manuDNC_V_Machine_ChartList = new List<V_manuDNC_V_Machine_Chart>();
	foreach (PlanDepartMachineDto  pdm in planDepartMachineDtoList) { 
	
		var partList = await _v_manuDNC_V_Machine_ChartRepository.GetAll()
			.Where(sd =>   pdm.PlanNo == sd.PlantNo 
						&& pdm.DepartNo == sd.DepartNo 
						&& pdm.MachineID == sd.MachineNo  //工单对应的机器
						&& (sd.Starttime.Value.Date == DateTime.Now.Date || sd.Endtime.Value.Date == DateTime.Now.Date) //今天
						&& (!exceptListNoList.Contains(sd.Id))
						&& (_showcolorList.Contains(sd.Showcolor))  // 红 黄 蓝
						//&& (sd.sj >= this._downTimeThresholdSecond)
						)  //大于阈值
			.Select(sd => sd)
			.OrderBy(sd => sd.Id).ThenBy(sd => sd.Starttime)
			.ToListAsync();

		tempV_manuDNC_V_Machine_ChartList.AddRange(partList);
	}
	#endregion

	#region 有时间再修改多个条件join
	//result = (from d in (_v_manuDNC_Machine_collect_dicRepository
	//           .GetAll()
	//           .Where(dic => MachineNoList.Contains(dic.MachineNo))
	//           .Select(dic => dic).ToList())
	//          join down in await _v_manuDNC_V_Machine_ChartRepository.GetAll().ToListAsync()
	//          on new { d.PlanNo, d.DepartNo, d.MachineID } equals new { down.PlantNo, down.DepartNo, down.MachineNo } into d_down
	//          from data1 in d_down.DefaultIfEmpty()
	//          select new V_manuDNC_V_Machine_Chart
	//          {
	//              Id = data1 == null ? 0 : data1.,
	//              MachineNo = i.WorkOrder,
	//              Starttime = i.ProcessName,
	//              Endtime = i.ProcessNo,
	//              sj = i.MachineNum,
	//              Showcolor = i.ProdKOGONo
	//          }).OrderBy(i => i.Id).ToList();

	//var query =(_v_manuDNC_Machine_collect_dicRepository
	//           .GetAll()
	//           .Where(dic => MachineNoList.Contains(dic.MachineNo))
	//           .Select(dic => dic).ToList())
	//           .GroupJoin((await _v_manuDNC_V_Machine_ChartRepository.GetAll().ToListAsync()),
	//             d => new { d.PlanNo, d.DepartNo, d.MachineID },
	//             down => new { down.PlantNo, down.DepartNo, down.MachineNo },
	//             (d, down) => down.Select(d2 => new V_manuDNC_V_Machine_Chart
	//             {
	//                 Id = d2.Id,
	//                 MachineNo = d2.MachineNo,
	//                 Starttime = d2.Starttime,
	//                 Endtime = d2.Endtime,
	//                 sj = d2.sj,
	//                 Showcolor = d2.Showcolor
	//             }));

	#endregion

	downList = _autoMapper.Map<List<GetDownTimeByTripleLightOutput>>(tempV_manuDNC_V_Machine_ChartList);

	return downList;
}



/// <summary>
/// 根据加工指示号打印随行票：根据加工指示号processNo从数据库中找到数据，斑马打印机
///   1 如果文件路径存在就返回，并且打印次数+1，OperationTime改成now
///   2 如果保存的文件路径的zpl文件不存在了，就根据数据库中的数据生成zpl文件，返回路径。
///   3 如果processNo没有查询到数据，就根据3个view生成zpl，并且写库。PrintCount=1 and OperationTime改成now
/// </summary>
public async Task<List<ATPrintDto>> GetAccompanyTicketPrintByProcessNo(getTicketinfoinput input)
{
	
	List<ATPrintDto> result = new List<ATPrintDto>();
	List<string> printFileFullPath = new List<string>();
	List<AccompanyTicketOutput> atList = new List<AccompanyTicketOutput>();
	List<AccompanyTicket> accompanyTicketList = new List<AccompanyTicket>();
	// WorkOrderOnWork workOrder = new WorkOrderOnWork();
	List<AccompanyTicket> InsertAccompanyTicketList = new List<AccompanyTicket>(); //插入列表
	List<AccompanyTicket> UpdateAccompanyTicketList = new List<AccompanyTicket>(); //更新列表

	
	string workOrderNo = "";
	long wId = 0;
	int pageCount = 0; //总共几页          
	int lastCount = 0; //最后一页的余数

	var userId = _abpSession.UserId.Value;
	// List<WorkOrderOnWork> 开工工单list：根据生产指示号和工单号查询
	var res = await _workOrderOnWorkRepository.GetAll().Where(w => w.ProcessNo == input.processNo && w.WorkOrder == input.workOrder).ToListAsync();
	//try
	//{
		if (res.Count == 0)
		{
			throw new Exception("未找到工单信息");
		}
		else if (res.Count == 1)
		{
			var workOrderOnWork = res[0];
			workOrderNo = workOrderOnWork.WorkOrder;
			wId = workOrderOnWork.Id;

			// 工序名称数组：即工艺路径
			string[] gxDescArr = new string[15] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
			// 工艺路径中对应工序所需的设备号：有的工序并不需要用到设备，并不是所有的工序都需要用到设备
			string[] macDescArr1 = new string[30] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

			#region 根据processNo从accompanyTicket中看是否有打印记录
			bool isAllZPLFileExist = true;
			// 根据7位的生产指示号获取随行票打印记录：getTicketinfoinput.processNo int 类型
			var oldATList = await _accompanyTicketRepository.GetAll().Where(a => a.IssueID == input.processNo.ToString())
				.OrderBy(at => at.CurrentBoxNumber)// 根据当前装箱数排序
				.ToListAsync();

			if (oldATList != null && oldATList.Count > 0)
			{
				//如果有旧的打印记录
				result = oldATList.Select(a => new ATPrintDto
				{
					printFileFullPath = a.ZplPath,// string
					PrintAmount = a.PrintAmount.Value + 1,
					CurrentBoxNumber = a.CurrentBoxNumber.Value
				}).OrderBy(r => r.CurrentBoxNumber).ToList();

				printFileFullPath = oldATList.Select(a => a.ZplPath).ToList();
				if (printFileFullPath != null && printFileFullPath.Count > 0)
				{
					//遍历所有的文件路径，有一个zpl文件找不到，就返回false,如果这个IssueId的ZPL全部存在，就直接返回
					foreach (string aRelativePath in printFileFullPath)
					{
						string aFullPath = $"{_hostingEnvironment.WebRootPath}\\{aRelativePath}";//ContentRootPath

						isAllZPLFileExist = File.Exists(aFullPath);
						if (!isAllZPLFileExist)
						{
							result = new List<ATPrintDto>();
							break;
						}
					}

					if (isAllZPLFileExist == true)
					{
						#region 1 情况一文件存在
						oldATList.ForEach(a =>
						{
							a.PrintAmount = a.PrintAmount == null ? 1 : a.PrintAmount + 1;
							_accompanyTicketRepository.Update(a);
						});

						//await _accompanyTicketRepository.BulkUpdateAsync(oldATList);  //替换Z.EntityFramework.Plus.EFCore
						//var str = input.processNo.ToString();
						//await _accompanyTicketRepository.GetAll().Where(a => a.IssueID == str).BatchUpdateAsync(a => new AccompanyTicket
						//{
						//    PrintAmount = (a.PrintAmount == null ? 1 : a.PrintAmount + 1)
						//});

						return result.OrderBy(a => a.CurrentBoxNumber).ToList();
						#endregion
					}
					else // if ( !isAllZPLFileExist)
					{//oldATList存在，但是不是所有的文件都存在，根据以前保存的随行票信息，构建新的ZPL文件
						#region 2 情况2文件不存在,AccompanyTicket库中随行票数据存在，根据AccompanyTicket库中随行票数据 生成随行票，更新PrintAmount, ZplPath

						#region 2.0 根据AccompanyTicket库中随行票数据构造随行票数据
						oldATList.ForEach(a =>
						{
						#region AccompanyTicket库中相应记录构造每个打印的entity
						AccompanyTicketOutput temp = new AccompanyTicketOutput();
							temp.AccompanyTicketNo = a.AccompanyTicketNo; //1 随行票号
						temp.WorkOrder = a.WorkOrder;           // 37 工单号
						temp.ProdKOGONo = a.ProdKOGONo;         //3 生产工号
						temp.IssueID = int.Parse(a.IssueID);    //9 生产指示号
						temp.Model = a.Model;                   //18 品番

						temp.Quantity = a.Quantity.Value; //22 工单数

						if (temp.Quantity < 0)
							{
								throw new Exception($"工单{temp.WorkOrder},工单数小于零不允许打印。");
							}

							temp.InstrDlvyDate = a.InstrDlvyDate; //20 出库日期
						temp.MasterID = a.MasterID.Value; //29 Master唯一编号
						temp.BOMNo = a.BOMNo.Value; //21 单耗版本号

						temp.Holon = a.Holon; //0 Holon编号
						temp.BINManage = a.BINManage; //30 BIN管理标识
						temp.BoxFixedQty = a.BoxFixedQty == 0 ? 1 : a.BoxFixedQty; //4 外箱装箱数量  //如果为0就让人工输入。wwwwgg
						temp.BoxType = a.BoxType; //10 外箱型号

						temp.PartLength = a.PartLength; //5 工件全长
						temp.UnitWeight = a.UnitWeight; //31 单重
						temp.Series = a.Series;//sxp.MR_Series; //17 系列
						temp.DrawingNo = a.DrawingNo;//sxp.MR_DrawingNo == null? "" : sxp.MR_DrawingNo; //24 图号
						temp.DesignChangeNo = a.DesignChangeNo;//sxp.MR_DesignChangeNo == null ? "" : sxp.MR_DesignChangeNo; //25 设变

						temp.TuZhiFanHao = a.TuZhiFanHao;//sxpWithFanHao.TuZhiFanHao.Trim(); //23 图纸番号
						temp.SuCaiFanHao = a.SuCaiFanHao;//sxpWithFanHao.SuCaiFanHao.Trim(); //26 素材番号
						temp.MaterialModel = a.MaterialModel; //27 素材
						temp.RequestQty = a.RequestQty; //28 素材数量

						temp.TotalBoxCount = a.TotalBoxCount.Value; //32 总箱号
						temp.CurrentBoxNumber = a.CurrentBoxNumber.Value; //33 当前箱号
						temp.OperateTime = a.OperateTime.Value; //34 操作日期

						#region 备注
						Remark tempRemark = this.GetRemarkByRemarkString(a.Description);
							temp.Remark8 = tempRemark.Remark8;   //8 备注
						temp.Remark12 = tempRemark.Remark12; //12 Remark12
						temp.Remark13 = tempRemark.Remark13; //13 Remark13
						temp.Remark14 = tempRemark.Remark14; //14 Remark14
						temp.Remark15 = tempRemark.Remark15; //15 Remark15
						#endregion

						temp.PrintMonth = a.OperateTime.Value.Month.ToString() + "/"; //2 打印月份
						temp.PrintDay = a.OperateTime.Value.Day.ToString(); // 7 打印日期

						temp.Description = a.Description; //38 描述
						temp.MacNoGroup = a.MacNoGroup;  // 39 工序对应的设备编号列表
						temp.ProNameGroup = a.ProNameGroup; // 40 工艺对应的工序

						#region 构造工艺和设备(2行)的数据
						var gxSrcArr = a.ProNameGroup.Split(new char[] { ',' }).ToArray();
							Array.Copy(gxSrcArr, gxDescArr, gxSrcArr.Length);
						//gxDescArr[0] = "NC";

						int macIndex = 0;
							List<string> macNameList = a.MacNoGroup.Split(new char[] { ',' }).ToList();
							foreach (var macstr in macNameList)
							{
								var macNameLine1 = macstr.Length >= 3 ? macstr.Substring(0, 3) : macstr;
								var macNameLine2 = macstr.Length > 3 ? macstr.Substring(3) : "";
								macDescArr1[macIndex++] = macNameLine1;
								macDescArr1[macIndex++] = macNameLine2;
							}
						#endregion
						temp.LengthTolerance = a.LengthTolerance;
							atList.Add(temp);

							result.Add(new ATPrintDto
							{
								aAT = temp
							});
						#endregion
					});
						#endregion

						#region 2.1 调用斑马打印方法，返回随行票的路径
						printFileFullPath = (await _zplHelps.OpenZPLModelFile<AccompanyTicketOutput, AccompanyTicketEnum>(
							string.Empty,
							userId,
							gxDescArr,
							macDescArr1,
							atList)).OrderBy(at => at.CurrentBoxNumber).Select(a => a.ZplPath).ToList();
						#endregion

						#region 2.2 落库
						InsertAccompanyTicketList = new List<AccompanyTicket>(); //插入列表
						UpdateAccompanyTicketList = new List<AccompanyTicket>(); //更新列表
						atList.ForEach(temp =>
						{
							var newAccompanyTicket = _autoMapper.Map<AccompanyTicket>(temp);
							newAccompanyTicket.WorkOrderOnWorkId = wId;

							var oldAccompanyTicket = _accompanyTicketRepository.GetAll().Where(a => a.AccompanyTicketNo == newAccompanyTicket.AccompanyTicketNo).FirstOrDefault();
							if (oldAccompanyTicket != null)
							{
								oldAccompanyTicket.PrintAmount = oldAccompanyTicket.PrintAmount == null ? 1 : oldAccompanyTicket.PrintAmount.Value + 1;
								oldAccompanyTicket.ZplPath = newAccompanyTicket.ZplPath;
								UpdateAccompanyTicketList.Add(oldAccompanyTicket);

								result.Add(new ATPrintDto
								{
									printFileFullPath = oldAccompanyTicket.ZplPath,
									PrintAmount = oldAccompanyTicket.PrintAmount.Value,
									CurrentBoxNumber = oldAccompanyTicket.CurrentBoxNumber.Value
								});

								_accompanyTicketRepository.Update(oldAccompanyTicket);
							}
							else
							{
								newAccompanyTicket.PrintAmount = newAccompanyTicket.PrintAmount == null ? 1 : newAccompanyTicket.PrintAmount.Value + 1;
								InsertAccompanyTicketList.Add(newAccompanyTicket);

								result.Add(new ATPrintDto
								{
									printFileFullPath = temp.ZplPath,
									PrintAmount = newAccompanyTicket.PrintAmount.Value,
									CurrentBoxNumber = newAccompanyTicket.CurrentBoxNumber.Value
								});
							}
						});

						await _accompanyTicketRepository.BulkInsertAsync(InsertAccompanyTicketList);
						//await _accompanyTicketRepository.BulkUpdateAsync(UpdateAccompanyTicketList);
						//var ids1 = UpdateAccompanyTicketList.Select(a => a.Id).ToList();
						//await _accompanyTicketRepository.GetAll().Where(a=> ids1.Contains(a.Id)).BatchUpdateAsync(UpdateAccompanyTicketList);
						#endregion

					#endregion
					return result.OrderBy(a => a.CurrentBoxNumber).ToList();
					}
				}

			}
			#endregion

			#region 3 从3个View中得到数据打印

			#region 3.1 得到打印数据
			// ProcessProcessNo 生产/加工指示号 ProcessProcessName：工序名称
			V_Technology_Process_Technology_ProcessGroup ptg = await _v_Technology_Process_Technology_ProcessGroupRepository.GetAll()
				.Where(ptg => ptg.ProcessProcessNo == input.processNo).FirstOrDefaultAsync();
			
			// V_V_DFPJ_IncmpProdInstr.Id 即 V_V_DFPJ_IncmpProdInstr表中的IssueID，即为生产/加工指示号
			V_V_DFPJ_IncmpProdInstr sxpWithFanHao = await _v_V_DFPJ_IncmpProdInstr.GetAll().Where(sxp => sxp.Id == input.processNo).FirstOrDefaultAsync();

			V_IncmpProdInstr_Master_ModelRegister sxp = await _v_IncmpProdInstr_Master_ModelRegister.GetAll().Where(imm => imm.Id == input.processNo).FirstOrDefaultAsync();
			// 计算要几张随行票
			{ //如果 V_Technology_Process_Technology_ProcessGroup 或者 V_V_DFPJ_IncmpProdInstr 或者 V_IncmpProdInstr_Master_ModelRegister 有一个是空就返回
			if (ptg == null || sxpWithFanHao == null || sxp == null)
				throw new Exception("没有数据");
			}
			#endregion

			#region 3.2 构造打印数据, 包含打印
			//如果数据为每箱为0，就打印一张随行票
			if (sxp.M_BoxFixedQty == null || sxp.M_BoxFixedQty == 0)
			{
				sxp.M_BoxFixedQty = (ptg.TechnologyWorkOrderCount == null || ptg.TechnologyWorkOrderCount == 0) ? 1 : ptg.TechnologyWorkOrderCount;
			}
			
			//页数
			pageCount = (int)Math.Ceiling(ptg.TechnologyWorkOrderCount.Value / sxp.M_BoxFixedQty.Value);  //wwwwgg
																											//最后一页的余数
			lastCount = ((int)(ptg.TechnologyWorkOrderCount % sxp.M_BoxFixedQty) == 0) ? (int)sxp.M_BoxFixedQty : (int)(ptg.TechnologyWorkOrderCount % sxp.M_BoxFixedQty); //wwwwgg

			for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
			{

				AccompanyTicketOutput temp = new AccompanyTicketOutput();

				temp.AccompanyTicketNo = string.Format("{0}-{1}", workOrderNo, pageIndex.ToString().PadLeft(3, '0')); //1 随行票号
				temp.WorkOrder = workOrderNo;       // 37 工单号
				temp.ProdKOGONo = sxp.P_ProdKOGONo; //3 生产工号
				temp.IssueID = sxp.Id;              //9 生产指示号
				temp.Model = sxp.P_Model;           //18 品番

				temp.Quantity = (ptg.TechnologyWorkOrderCount == null) ? 0 : (int)ptg.TechnologyWorkOrderCount.Value; //22 工单数

				if (temp.Quantity < 0)
				{
					throw new Exception($"工单{temp.WorkOrder},工单数小于零不允许打印。");
				}

				temp.InstrDlvyDate = sxp.P_InstrDlvyDate.Value.ToString("yyyy/MM/dd"); //20 出库日期
				temp.MasterID = sxp.P_MasterID; //29 Master唯一编号
				temp.BOMNo = sxp.P_BOMNo; //21 单耗版本号

				temp.Holon = sxp.M_Holon; //0 Holon编号
				temp.BINManage = sxp.M_BINManage; //30 BIN管理标识
				// temp.BoxFixedQty = (pageIndex < pageCount) ? (int)sxp.M_BoxFixedQty : lastCount; //4 外箱装箱数量
				temp.BoxFixedQty = (pageIndex < pageCount) ? (sxp.M_BoxFixedQty == null ? (ptg.TechnologyWorkOrderCount == null ? 0 : (int)ptg.TechnologyWorkOrderCount.Value) : (int)sxp.M_BoxFixedQty) : lastCount; //4 外箱装箱数量
				temp.BoxType = sxp.M_BoxType; //10 外箱型号

				temp.PartLength = (sxp.MR_PartLength.Value).ToString("#0.00"); //5 工件全长
				temp.UnitWeight = sxp.MR_UnitWeight == null ? "0" : sxp.MR_UnitWeight.Value.ToString("#0.000"); //31 单重
				temp.Series = sxp.MR_Series; //17 系列 "MXS"
				temp.DrawingNo = sxp.MR_DrawingNo == null ? "" : sxp.MR_DrawingNo; //24 图号 "C5550BBAQ294-#";
				temp.DesignChangeNo = sxp.MR_DesignChangeNo == null ? "" : sxp.MR_DesignChangeNo; //25 设变 "9";

				temp.TuZhiFanHao = sxpWithFanHao.TuZhiFanHao.Trim(); //23 图纸番号 "200";
				temp.SuCaiFanHao = sxpWithFanHao.SuCaiFanHao.Trim(); //26 素材番号 "2433C9";
				temp.MaterialModel = sxpWithFanHao.MaterialModel; //27 素材
				temp.RequestQty = sxpWithFanHao.RequestQty.Value.ToString("#0"); //28 素材数量

				// 三个字段在数据库中均可为null
				temp.TotalBoxCount = pageCount; //32 总箱号
				temp.CurrentBoxNumber = pageIndex; //33 当前箱号
				temp.OperateTime = DateTime.Now; //34 操作日期

				//35 工序号  36 设备号 
				//temp.ProcessMachineList = this.GetListByString(ptg.TechnologyProNameGroup, ptg.TechnologyMacNoGroup);

				#region 备注
				Remark tempRemark = this.GetRemarkByRemarkString(ptg.ProcessDescription == null ? "" : ptg.ProcessDescription);
				temp.Remark8 = tempRemark.Remark8;   //8 备注
				temp.Remark12 = tempRemark.Remark12; //12 Remark12
				temp.Remark13 = tempRemark.Remark13; //13 Remark13
				temp.Remark14 = tempRemark.Remark14; //14 Remark14
				temp.Remark15 = tempRemark.Remark15; //15 Remark15
				#endregion

				temp.PrintMonth = string.Format(@"{0}/", DateTime.Now.Month.ToString()); //2 打印月份
				temp.PrintDay = DateTime.Now.Day.ToString(); // 7 打印日期

				temp.Description = ptg.ProcessDescription == null ? "" : ptg.ProcessDescription; //38 描述
				temp.MacNoGroup = ptg.TechnologyMacNoGroup;  // 39 工序对应的设备编号列表
				temp.ProNameGroup = ptg.TechnologyProNameGroup; // 40 工艺对应的工序

				#region 构造工艺和设备(2行)的数据
				// 工序名称数组：在这里进行的工序名称数组长度的截取
				// var gxSrcArr = ptg.TechnologyProNameGroup.Split(new char[] { ',' }).Select(p => p.Length > 3 ? p.Substring(0, 3) : p).ToArray();
				var gxSrcArr = ptg.TechnologyProNameGroup.Split(new char[] { ',' }).ToArray();
				Array.Copy(gxSrcArr, gxDescArr, gxSrcArr.Length);


				int macIndex = 0;
				List<string> macNameList = ptg.TechnologyMacNoGroup.Split(new char[] { ',' }).ToList();
				foreach (var macstr in macNameList)
				{
					var macNameLine1 = macstr.Length >= 3 ? macstr.Substring(0, 3) : macstr;
					var macNameLine2 = macstr.Length > 3 ? macstr.Substring(3) : "";
					macDescArr1[macIndex++] = macNameLine1;
					macDescArr1[macIndex++] = macNameLine2;
				}
				#endregion

				temp.LengthTolerance = input.lt;      //11 长度公差
				atList.Add(temp);
				result.Add(new ATPrintDto
				{
					aAT = temp
				});
			}
			#endregion

			#region 3.3 调用斑马打印方法，返回随行票的相对路径\wwwroot\Upload\ZPL\SXP\20200921\18400\094021\272896586.zpl
			printFileFullPath = (await _zplHelps.OpenZPLModelFile<AccompanyTicketOutput, AccompanyTicketEnum>(
				string.Empty,
				userId,
				gxDescArr,
				macDescArr1,
				atList)).OrderBy(at => at.CurrentBoxNumber).Select(a => a.ZplPath).ToList();
			#endregion

			#region 3.4 落库
			// InsertAccompayTicketList 、UpdateAccompanyTicketList 已经在最前面进行定义并且赋值，如果在这里进行赋值，则在最前面赋值为 null；如果在最前面进行赋值，则此两条语句不需要
			// InsertAccompanyTicketList = new List<AccompanyTicket>(); //插入列表
			// UpdateAccompanyTicketList = new List<AccompanyTicket>(); //更新列表
			atList.ForEach(temp =>
			{
				var newAccompanyTicket = _autoMapper.Map<AccompanyTicket>(temp);
				newAccompanyTicket.WorkOrderOnWorkId = wId;

				var oldAccompanyTicket = _accompanyTicketRepository.GetAll().Where(a => a.AccompanyTicketNo == newAccompanyTicket.AccompanyTicketNo).FirstOrDefault();
				if (oldAccompanyTicket != null)
				{
					oldAccompanyTicket.PrintAmount = oldAccompanyTicket.PrintAmount == null ? 1 : oldAccompanyTicket.PrintAmount.Value + 1;
					UpdateAccompanyTicketList.Add(oldAccompanyTicket);

					result.Add(new ATPrintDto
					{
						printFileFullPath = oldAccompanyTicket.ZplPath,
						PrintAmount = oldAccompanyTicket.PrintAmount.Value,
						CurrentBoxNumber = oldAccompanyTicket.CurrentBoxNumber.Value
					});
					_accompanyTicketRepository.Update(oldAccompanyTicket);
				}
				else
				{
					newAccompanyTicket.PrintAmount = newAccompanyTicket.PrintAmount == null ? 1 : newAccompanyTicket.PrintAmount.Value + 1;
					InsertAccompanyTicketList.Add(newAccompanyTicket);

					result.Add(new ATPrintDto
					{
						printFileFullPath = temp.ZplPath,
						PrintAmount = newAccompanyTicket.PrintAmount.Value,
						CurrentBoxNumber = newAccompanyTicket.CurrentBoxNumber.Value
					});
				}
			});

			await _accompanyTicketRepository.BulkInsertAsync(InsertAccompanyTicketList);
			//await _accompanyTicketRepository.BulkUpdateAsync(UpdateAccompanyTicketList);
			//var ids = UpdateAccompanyTicketList.Select(a => a.Id).ToList();
			//await _accompanyTicketRepository.GetAll().Where(a=>ids.Contains(a.Id)).BatchUpdateAsync(UpdateAccompanyTicketList);
		#endregion

		return result.OrderBy(c => c.CurrentBoxNumber).ToList();

			#endregion
		}
		else
		{
			throw new Exception("根据生产指示号和工单找到多个工单");
		}
	//}
	//catch (Exception ex) {
	//    var i = 10;
	//    result.OrderBy(c => c.CurrentBoxNumber).ToList();
	//}
	//result.OrderBy(c => c.CurrentBoxNumber).ToList();
}
#endregion





#region ASP .NET Core 路由相关
public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
{
	if (env.IsDevelopment)
	{
		app.UseDevelopmentExceptionPage();
	}

	app.UseRouting();

	app.UseEndpoints(endpoints => {
		endpoints.MapGet("/",async context => {
			await context.Response.WriteAsync("Hello World!");
		})
	});
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    // Matches request to an endpoint.
    app.UseRouting();

    // Endpoint aware middleware. 
    // Middleware can use metadata from the matched endpoint.
    app.UseAuthentication();
    app.UseAuthorization();

    // Execute the matched endpoint.
    app.UseEndpoints(endpoints =>
    {
        // Configure the Health Check endpoint and require an authorized user.
        endpoints.MapHealthChecks("/healthz").RequireAuthorization();

        // Configure another endpoint, no authorization requirements.
        endpoints.MapGet("/", async context =>
        {
            await context.Response.WriteAsync("Hello World!");
        });
    });
}


// Location 1: before routing runs, endpoint is always null here
app.Use(next => context =>
{
    Console.WriteLine($"1. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
    return next(context);
});

app.UseRouting();

// Location 2: after routing runs, endpoint will be non-null if routing found a match
app.Use(next => context =>
{
    Console.WriteLine($"2. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
    return next(context);
});

app.UseEndpoints(endpoints =>
{
    // Location 3: runs when this endpoint matches
    endpoints.MapGet("/", context =>
    {
        Console.WriteLine(
            $"3. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
        return Task.CompletedTask;
    }).WithDisplayName("Hello");
});

// Location 4: runs after UseEndpoints - will only run if there was no match
app.Use(next => context =>
{
    Console.WriteLine($"4. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
    return next(context);
});
#endregion








#region 有关 JwtBearer 相关：
using System;
using Microsoft.IdentityModel.Tokens;

namespace SMC.MES.Authentication.JwtBearer
{
    public class TokenAuthConfiguration
    {
        // 加密的Key：SecretKey 必须大于16个，是大于，不是大于等于
        public SymmetricSecurityKey SecurityKey { get; set; }
        
        // token是谁颁发
        public string Issuer { get; set; }
        
        // token 可以给哪些客户端使用
        public string Audience { get; set; }

        // 定义token中的安全密钥与加密算法
        public SigningCredentials SigningCredentials { get; set; }
        
        // token的过期时间
        public TimeSpan Expiration { get; set; }
    }
}






// 创建 Jwttoken 中的 handler
private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
{
    var now = DateTime.UtcNow;

    var jwtSecurityToken = new JwtSecurityToken(
        issuer: _configuration.Issuer,
        audience: _configuration.Audience,
        claims: claims,
        notBefore: now,
        expires: now.Add(expiration ?? _configuration.Expiration),
        signingCredentials: _configuration.SigningCredentials
    );

    // JwtSecurityToken 类生成jwt，JwtSecurityTokenHandler将jwt编码，你可以在claims中添加任何chaims
    //  创建一个JwtSecurityTokenHandler类用来生成Token，生成token字符串，返回值为 string
    return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
}

private async Task<List<Claim>> CreateJwtClaimsAsync(ClaimsIdentity identity)
{
    var claims = identity.Claims.ToList();
    var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);
    var orgid = await _ouManager.GetOuIdByUserId(nameIdClaim.Value);
    //var orgobj = await _ouManager.GetOuByUserId(orgid);
    if (orgid==null)
        throw new Exception("未找到当前用户部门!");
    var orglevel = await _ouManager.GetOuIdByLevel(nameIdClaim.Value);
    if (orglevel<=0)
        throw new Exception("未找到当前用户组织机构等级!");
    // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
    long intorgid;
    string userCode = string.Empty;
    if(Int64.TryParse(orgid,out intorgid))
    {
        #region 判断是否为班组
        if (Convert.ToInt32(orglevel) == (int)OranizationEnum.OrgunitLevel.Machine)
        {
            // 如果不是班组级别，则userCode为4位部门（HOLON）代码
            userCode = string.Join(",", _ouManager.GetOrganizationTree(intorgid).Select(x => x.Code).ToArray());
        }
        else
        {
            // 如果是班组级别，则userCode为6位部门代码（本MES系统中独创的）
            userCode = string.Join(",", _ouManager.GetOrganizationTree(intorgid).Select(x => x.OldCode).ToArray());
        }
    }
    #endregion

    claims.AddRange(new[]
    {
        new Claim("Application_OrganizationUnitId",orgid),
        new Claim("Application_OrganizationLevel",orglevel.ToString()),
        new Claim("Application_OrganizationCode",userCode),
        // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
        new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
    });

    return claims;
}

private string GetEncryptedAccessToken(string accessToken)
{
	// AppConsts.DefaultPassPhrase 加密字符串的 value
    return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
}

AppConsts.cs：
namespace SMC.MES
{
    public class AppConsts
    {
        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public const string DefaultPassPhrase = "gsKxGZ012HLL3MI5";
    }
}






using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Abp.Runtime.Security;

namespace SMC.MES.Web.Host.Startup
{
    public static class AuthConfigurer
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                // 配置认证服务
                services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                }).AddJwtBearer("JwtBearer", options =>
                {
                    options.Audience = configuration["Authentication:JwtBearer:Audience"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The signing key must match!
                        // 是否验证密钥
                        ValidateIssuerSigningKey = true,
                        // 密钥
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                        // Validate the JWT Issuer (iss) claim
                        // 是否验证发行人
                        ValidateIssuer = true,
                        // 发行人
                        ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                        // Validate the JWT Audience (aud) claim
                        // 是否验证受众人
                        ValidateAudience = true,
                        // 受众人
                        ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                        // Validate the token expiry
                        // 验证生命周期
                        ValidateLifetime = true,

                        // If you want to allow a certain amount of clock drift, set that here
                        // 如果你想允许一定数量的时钟漂移，在这里设置
                        // ClockSkew = TimeSpan.FromSeconds(300), ----- 允许服务器时间偏移量300秒，即我们配置的过期时间加上这个允许偏移的时间值，才是真正过期的时间(过期时间 +偏移值)你也可以设置为0，ClockSkew = TimeSpan.Zero
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = QueryStringTokenResolver
                    };
                });
            }
        }

        /* This method is needed to authorize SignalR javascript client.
         * SignalR can not send authorization header. So, we are getting it from query string as an encrypted text. */
        private static Task QueryStringTokenResolver(MessageReceivedContext context)
        {
            if (!context.HttpContext.Request.Path.HasValue ||
                !context.HttpContext.Request.Path.Value.StartsWith("/signalr"))
            {
                // We are just looking for signalr clients
                return Task.CompletedTask;
            }

            var qsAuthToken = context.HttpContext.Request.Query["enc_auth_token"].FirstOrDefault();
            if (qsAuthToken == null)
            {
                // Cookie value does not matches to querystring value
                return Task.CompletedTask;
            }

            // Set auth token from cookie
            context.Token = SimpleStringCipher.Instance.Decrypt(qsAuthToken, AppConsts.DefaultPassPhrase);
            return Task.CompletedTask;
        }
    }
}


public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
{
    //app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.
    app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

    app.UseCors(_defaultCorsPolicyName); // Enable CORS!

    app.UseStaticFiles();

    app.UseRouting();

    // 关于认证授权方案之JwtBearer认证实现认证服务注册的两个中间件
    // 1.先开启认证
    app.UseAuthentication();
    // 2.再开启授权
    app.UseAbpRequestLocalization();

    //添加MIME
    var provider = new FileExtensionContentTypeProvider();
    provider.Mappings[".zpl"] = "application/octet-stream";//解决斑马打印问题
    app.UseStaticFiles(new StaticFileOptions
    {
        ContentTypeProvider = provider
    });

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHub<AbpCommonHub>("/signalr");
        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
    });

    // Enable middleware to serve generated Swagger as a JSON endpoint
    app.UseSwagger();
    // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(_appConfiguration["App:ServerRootAddress"].EnsureEndsWith('/') + "swagger/v1/swagger.json", "MES API V1");
        options.IndexStream = () => Assembly.GetExecutingAssembly()
            .GetManifestResourceStream("SMC.MES.Web.Host.wwwroot.swagger.ui.index.html");
    }); // URL: /swagger
}
#endregion




#region SMC.MES.Technology/Techologys/TechnologyAppService.cs 构造DI
// 构造DI
public TechnologyAppService(
	// ProductionLine 生产线表：在 SMC_MOM_EquipmentWorkReport 数据库中：对应当前HOLON下的生产线：在 设备管理/生产线 界面中
	IRepository<ProductionLine, long> productionLinesRepository
	// T_ProduceData_Inputs 首工序导入主表：在 SMC_MOM_Technology 数据库中：对应当前班组长导入本班每天排产信息首工序数据，在 作业调度管理/排产计划导入 界面中
	, IRepository<T_ProduceData_Inputs, int> datainputRepository
	// T_Tech_ProcessDetail 工艺过程/路径详情表：在 SMC_MOM_Technology 数据库中：对应标准工艺路径(从生技那获得的标准数据)中的数据，在 工艺管理/标准工艺路径(生技) 界面中
	, IRepository<ProcessDetailInfo, int> processdetailinfoRepository
	// 
	, IRepository<T_Tech_ProcessChildDatas, int> processChildDatasRepository
	, IRepository<T_Process_Matchings, int> Process_MatchingsRepository
	, IRepository<T_Process_ProductionInfos, int> Process_ProductionInfosRepository
	, IRepository<T_ProduceJoinTech_Datas, int> ProduceJoinTech_DatasRepository
	, IRepository<V_New_ProductionInfos, int> New_ProductionInfosDatasRepository
	, IRepository<T_WorkOrder_Groups, int> workordergroupsRepository
	, IAbpSession abpSession
	, IExcelCommon excelCommon
	, ICacheManager cacheManager
	, IWebHostEnvironment hostingEnvironment
	, IMapper autoMapper
	, DeviceManage deviceManage
	, OuManager ouManager
	, ITemplateCommon templateCommon
	, ITechManage techManage
	)

{
	_productionLinesRepository = productionLinesRepository;
	_datainputRepository = datainputRepository;
	_processdetailinfoRepository = processdetailinfoRepository;
	_processChildDatasRepository = processChildDatasRepository;
	_Process_MatchingsRepository = Process_MatchingsRepository;
	_Process_ProductionInfosRepository = Process_ProductionInfosRepository;
	_ProduceJoinTech_DatasRepository = ProduceJoinTech_DatasRepository;
	_New_ProductionInfosDatasRepository = New_ProductionInfosDatasRepository;
	_workordergroupsRepository = workordergroupsRepository;
	_abpSession = abpSession;
	_excelCommon = excelCommon;
	_cacheManager = cacheManager;
	_bulkImporyCache = "BulkImporyTechnologyCache";
	_hostingEnvironment = hostingEnvironment;
	_autoMapper = autoMapper;
	_devicemanage = deviceManage;
	strundefined = "undefined";
	_ouManager = ouManager;
	_templateCommon = templateCommon;
	_timeoutminutes = 600;
	_techManage = techManage;
}
#endregion










using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.MES.Technologys
{
    [Table("T_Tech_ProcessDetail")]
    public class ProcessDetailInfo : Entity<int>
    {
		// 部品型号
        public string MaterialModel { get; set; }
        // HOLON名称
		public string HOLON { get; set; }
        // 4位部门代码
		public string DepartNo { get; set; }
        // 工艺路径:如NC(瓶颈),研磨,清洗,外观检查,出库
		public string TechProcessName { get; set; }
		public string TechLineGroup { get; set; }
        public string BattLeMachGroup { get; set; }
        public string CytimeGroup { get; set; }
        public string TechProcessNum { get; set; }
        public string PTMachGroup { get; set; }
        public string BattleProcessNum { get; set; }
		// 是否为原价基准
        public Nullable<bool> Standard { get; set; }
    }
}



using Abp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMC.MES.Technologys
{
    [Table("T_Tech_ProcessChildData")]
    public class T_Tech_ProcessChildDatas : Entity<int>
    {
        /// <summary>
        /// 部品型号
        /// </summary>
        public string MaterialModel { get; set; }
        /// <summary>
        /// holon代码
        /// </summary>
        public string HOLON { get; set; }
        /// <summary>
        /// 四位部门代码
        /// </summary>
        public string DepartNo { get; set; }
        /// <summary>
        /// 原价基准
        /// </summary>
        public Nullable<bool> Standard { get; set; }
        /// <summary>
        ///工艺工序代码
        /// </summary>
        public string ProcessNum { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string MachNo { get; set; }
        /// <summary>
        /// 设备工序名称
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 测量日期
        /// </summary>
        public Nullable<System.DateTime> MeterDate { get; set; }
        /// <summary>
        /// 瓶颈设备标记
        /// </summary>
        public Nullable<bool> BottleStamp { get; set; }
        /// <summary>
        /// 循环时间
        /// </summary>
        public Nullable<decimal> CycletTime { get; set; }
        /// <summary>
        /// 模具编号
        /// </summary>
        public string DieNum { get; set; }
        /// <summary>
        /// 瓶颈设备组可替代
        /// </summary>
        public string BattleMachGroup { get; set; }
        /// <summary>
        /// 瓶颈设备组可替代循环时间
        /// </summary>
        public string CytimeGroup { get; set; }
    }
}




























//格式化日期
FormatDate: function (timespan, fmt) {//formatstyle:例如：yyyy-MM-dd hh:mm:ss
	if (timespan == "1-01-01" || timespan == undefined) {
		return "";
	}
	else {
		if (!fmt) fmt = "yyyy-MM-dd hh:mm:ss";
		var d = new Date(timespan);
		var o = {
			"M+": d.getMonth() + 1, //月份  
			"d+": d.getDate(), //日  
			"h+": d.getHours(), //小时  
			"m+": d.getMinutes(), //分  
			"s+": d.getSeconds(), //秒  
			"q+": Math.floor((d.getMonth() + 3) / 3), //季度  
			"S": d.getMilliseconds() //毫秒  
		};
		if (/(y+)/.test(fmt))
			fmt = fmt.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
		for (var k in o)
			if (new RegExp("(" + k + ")").test(fmt))
				fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
		return fmt;
	}
},








变量    含义
$data    要返回的数据
$msg    页面提示信息
$code    返回的code
$wait    跳转等待时间 单位为秒
$url    跳转页面地址


public OutputPageInfo(int count,IRepositoryList<TEntity> items)
{
	Msg = "",
	Data = items,
	Count = count;
}

new OutputPageInfo<TechGetherOutput>(taskCount,taskList);











SMC.MES.Common/LinqExtension:
IQueryableExtension.cs:IQueryable 的扩展
public static IQueryable<TSource> WhereIfContains<TSource>(this IQueryable<TSource> source,string propertyName,string propertyValue)
{
	ParameterExpression parameter = Expression.Parameter(typeof(TSource),"p");
	MemberExpression member = Expression.PropertyOrField(parameter,propertyName);
	MethodInfo method = typeof(string).GetMethod("Contains",new[] { typeof(string)});
	ConstantExpression constant = Expression.Constant(propertyValue,typeof(string));
	return source.Where(Expression.Lambda<Func<TSource,bool>>(Expression.Call(member,method,constant),parameter));
}








IEnumerableExtension.cs:IEnumerable 的扩展

public static IEnumerable<TSource> SkipTakeEnumerable<TSource>(this IEnumerable<TSource> source,int index,int limit)
{
	return source.Skip((index - 1) * limit).Take(limit);
}





IRepositoryExtension.cs: IRepository 的扩展

public async static Task BulkInsertAsync<TEntity,TPrimaryKey>([NotNull] this IRepository<TEntity,TPrimaryKey> repository,List<TEntity> entities) where TEntity : Entity<TPrimaryKey>,new ()
{
	if (entites.Any())
	{
		foreach (var item in entities)
		{
			var itemType = item.GetType();
			var objcuser = itemType.GetProperty(_strCreateUserId);
			if (objcuser != null)
			{
				objcuser.SetValue(item,_abpSession.UserId,null);
			}
			var objctime = itemType.GetProperty(_strCreateDateTimes);
			if (objctime != null)
			{
				objctime.SetValue(item,DateTime.Now,null);
			}
		}
	}
	await repository.GetDbContext().BulkInsertAsync(entities);
}










SMC.MES.Core/Technologys：

namespace SMC.MES.Technologys
{
    [Table("T_Tech_ProcessDetail")]
    public class ProcessDetailInfo : Entity<int>
    {
		// 型号：C1A40B-Q7156-850
        public string MaterialModel { get; set; }

		// 所属HOLON：731、732等
        public string HOLON { get; set; }
        
		// 四位部门代码：6211
		public string DepartNo { get; set; }
        
		// 工艺路径：NC(瓶颈),铣扁,去毛刺,研磨,挤丝,清洗,精修,出库
		public string TechProcessName { get; set; }

		// 对应 T_Tech_ProcessChildData 表中的 ProcessNum 字段取前两位
        public string TechLineGroup { get; set; }

		// 
        public string BattLeMachGroup { get; set; }
        public string CytimeGroup { get; set; }
        public string TechProcessNum { get; set; }
        public string PTMachGroup { get; set; }
        public string BattleProcessNum { get; set; }
        public Nullable<bool> Standard { get; set; }
    }
}


namespace SMC.MES.Technologys
{
    [Table("T_Process_Matching")]
    public class T_Process_Matchings : Entity<int>
    {
        /// <summary>
        /// 配套ID：14、22
        /// </summary>
        public Nullable<int> Ptid { get; set; }
        /// <summary>
        /// 加工指示号：8738730、8732595
        /// </summary>
        public int? ProInstrid { get; set; }
        /// <summary>
        /// 组装指示号：11133549
        /// </summary>
        public Nullable<int> AssInstrid { get; set; }
        /// <summary>
        /// 部品型号：MGP50A-CA007-050
        /// </summary>
        public string MaterialModel { get; set; }
        /// <summary>
        /// 组装开工日：2020-9-18 00:00:00:000
        /// </summary>
        public Nullable<System.DateTime> AssStartDate { get; set; }
        /// <summary>
        /// 运输方式：LAND、AIR
        /// </summary>
        public string DlvyWay { get; set; }
        /// <summary>
        /// 欠品数量：1.00、402.00、2.00
        /// </summary>
        public Nullable<decimal> ShortCount { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateDate { get; set; }
        /// <summary>
        /// 加工指示状态：首工序可生产数、表面上架数、加工打票待入数、BJ在库
        /// </summary>
        public string OrderStates { get; set; }
    }
}








var queryList = query.Skip(skipCount).Take(input.Limit).Select(a => a);

var query = datainputRepository.GetAll()
			.WhereIf(!string.IsNullOrEmpty(code),(c => c.DepartNo.Equals(code)))
			.WhereIf(true,c => c.status == (int)InputStatus.status.dr)
			.WhereIf(!string.IsNullOrEmpty(input.MacNo),c => c.MachineNum == input.MacNo)
			.WhereIf(!string.IsNullOrEmpty(input.ProcessName),c => c.ProcessName == input.ProcessName)
			.Where(c => c.CreationTime >= stime && c.CreationTime <= dtime)
			.OrderByDescending
















/*
ConfigureServices 用来配置依赖注入的
注入容器的声明周期：依赖注入，IoC容器
- Transient：每次服务类被请求都会创建一个新的实例
- Scoped：每次Web请求会创建一个实例
- Singleton：一旦被创建实例，就会一直使用这个实例，直到应用停止
*/
ABP框架主要还是基于领域驱动的理念来构建整个架构的，其中领域驱动包含的概念有 域对象Entities、仓储对象Repositories、域服务接口层Domain Service、域事件Domain Events、应用服务接口
ABP公共结构-依赖注入：
1.传统方式的问题
在一个应用程序中，类之间相互依赖。假设我们有一个应用程序服务，使用仓储（repository）类插入实体到数据库。在这种情况下，应用程序服务类依赖于仓储（repository）类。看下例子：
	public class PersonAppService
	{
		private IPersonRepository _personRepository;

		public PersonAppService()
		{
			// PersonAppService 依赖于 PersonRepository 具体实现类，而不是 IPersonRepository 接口 
			_personRepository = new PersonAppService()
		}

		public void CreatePerson(string name,int age)
		{
			var person = new Person{Name = name,Age = age};
			_personRepository.Insert(person);
		}
	}

工厂模式：不再依赖于接口的具体实现类，而是通过静态类创建
	public class PersonAppService
	{
		private IPersonRepository _personRepository;

		public PersonAppService()
		{
			_personRepository = PersonRepositoryFactory.Create();
		}

		public void CreatePerson(string name,int age)
		{
			var person = new Person{Name = name,Age = age};
			_personRepository.Insert(person);
		}
	}
PersonRepositoryFactory是一个静态类，创建并返回一个IPersonRepository。这就是所谓的服务定位器模式。以上依赖问题得到解决，因为PersonAppService不需要创建一个IPersonRepository的实现的对象，这个对象取决于PersonRepositoryFactory的Create方法。

2.解决方案
2.1 构造函数注入：Construction injection
	public PersonAppService
	{
		private IPersonRepository _personRepository;

		public PersonAppService(IPersonAppService personRepository)
		{
			_personRepository = personRepository;
		}

		public void CreatePerson(string name,int age)
		{
			var person = new Person{Name = name,Age = age};
			_personRepository.Insert(person);
		}
	}

2.2 属性注入(Property Injection)
采用构造函数的注入模式是一个完美的提供类的依赖关系的方式。通过这种方式，只有提供了依赖你才能创建类的实例。同时这也是一个强大的方式显式地声明，类需要什么样的依赖才能正确的工作。

但是，在有些情况下，该类依赖于另一个类，但也可以没有它。这通常是适用于横切关注点(如日志记录)。一个类可以没有工作日志，但它可以写日志如果你提供一个日志对象。在这种情况下，你可以定义依赖为公共属性，而不是让他们放在构造函数。想想，如果我们想在PersonAppService写日志。我们可以重写类如下:

public class PersonAppService
{
	public ILogger Logger{get; set;}

	private IPersonRepository _personRepository;

	public PersonAppService(IPersonRepository personRepository)
	{
		_personRepository = personRepository;
		Logger = NullLogger.Instance;
	}

	public void CreatePerson(string name,int age)
	{
		Logger.Debug("Inserting a new person to database with name = " + name);
		var person = new Person{Nam = name,Age = age};
		_personRepository.Insert(person);
		Logger.Debug("Successfully inserted!");
	}
}
NullLogger.Instance 是一个单例对象，实现了ILogger接口，但实际上什么都没做(不写日志。它实现了ILogger实例，且方法体为空)。现在，PersonAppService可以写日志了，如果你为PersonAppService实例设置了Logger，如下面:

var personService = new PersonAppService(new PersonRepository());
personService.Logger = new Log4NetLogger();
personService.CreatePerson("Tim",19);

假设Log4NetLogger实现ILogger实例，使得我们可以使用Log4Net库写日志。因此，PersonAppService可以写日志。如果我们不设置Logger，PersonAppService就不写日志。因此，我们可以说PersonAppService ILogger实例是一个可选的依赖。

几乎所有的依赖注入框架都支持属性注入模式。

3.依赖注入框架：
有许多依赖注入框架，都可以自动解决依赖关系。他们可以创建所有的依赖项(递归地依赖和依赖关系)。你只需要依赖注入模式写类和类构造函数&属性，其他的均交给DI框架处理。我们只需要将依赖注册(入)到DI框架中即可。

4.ABP依赖注入的基础结构
4.1 注册(Registering)
常规注册(Conventional registration)
ABP按照约定注册程序集。所以，你应该告诉ABP按照约定注册你的程序集。
在模块即 xxxModule类 中的初始化方法 Initialize 中进行注册：

public class MyModule:AbpModule
{
	public override void Initialize()
	{
		IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly);
	}
}







ABP领域层-仓储
仓储的定义：在领域层和数据映射层的中介，使用类似集合的接口来存取领域对象。
一般来说，我们针对不同的实体会创建相应的仓储。
仓储在领域层定义，但在基础设施层实现。


IRepository.GetAll() 方法
当你在仓储外调用GetAll方法方法时，数据库的连接必须是开启的，因为它返回IQueryable类型的对象。这是必须的，因为IQueryable对象是延迟执行的，它并不会马上执行数据库查询，直到你调用ToList()方法或在foreach循环中使用IQueryable(或以某种方式访问查询项时)。因此，当你调用ToList()方法，数据库连接必需是启用状态。

请看下面示例：

[UnitOfWork]
public SerachPeopleOutput SearchPeople(SerachPeopleInput input)
{
	// 取得 IQueryable<Person>
	var query = _personRepository.GetAll();

	// 若有选取，则添加一些过滤条件
	if (!string.IsNullOrEmpty(input.SearchName))
	{
		query = query.Where(person => person.Name.StartsWith(input.SerachName));
	}

	if (input.IsActive.HasValue)
	{
		quert = quert.Where(person => person.IsActive == input.IsActive.Value);
	}

	// 取得分页结果集
	var people = quert.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

	// 将 <List<PeopleDto>>(people) 对象映射为 SearchPeopleOutput 对象
	return new SerachPeopleOutput{ People = Mapper.Map<List<PeopleDto>>(people)};
}
在这里，SearchPeople方法必需是工作单元，因为IQueryable的ToList()方法在方法体内调用，在执行IQueryable.ToList()方法的时候，数据库连接必须开启。

在大多数情况下，web应用需要你安全的使用GetAll方法，因为所有控制器的Action默认是工作单元的，因此在整个连接中，数据库连接必须是有效连接的。



ABP应用层：
public class Task:Enitiy,IHasCreationTime
{
	public string Title { get; set; }

	public string  Description { get; set; }

	public DateTime CreationTime { get; set; }

	public TaskState State { get; set;}

	public Person AssignedPerson { get; set; }
	public Guid? AssignedPersonId { get; set; }

	public Task()
	{
		CreationTime = Clock.Now;
		State = TaskState.Open;
	}
}


然后，我们为该实体创建一个DTO：
[AutoMap(typeof(Task))]
public class TaskDto:EntityDto,IHasCreationTime
{
	public string Title { get; set;}

	public string  Description { get; set; }

	public DateTime CreationTime { get; set; }

	public TaskState State { get; set;}

	public Guid? AssignedPersonId { get; set; }

	public string AssignedPersonName { get; set; }

}


// 我们添加了 GetAllTasksInput 作为第4个泛型参数到AsyncCrudAppService类(第三个参数是实体的主键)
public class TaskAppService:AsyncCrudService<Task,TaskDto,int,GetAllTasksInput>
{
	public TaskAppService(IRepository<Task> repository):base(repository)
	{

	}

	// 重写 CreateFilteredQuery 方法来应用自定义过滤。这个方法是作为 AsyncCrudAppService 类的一个扩展点(为了简化条件过滤，我们使用了ABP的一个扩展方法WhereIf，但实际上我们所做的就是对IQueryable的过滤)。
	protected override IQueryable<Task> CreatedFilteredQueryed(GetAllTasksInput input)
	{
		return base.CreateFilteredQuery(input)
			.WhereIf(input.State.HasValue,t => t.State == input.State.Value)
	}

}


ABP应用服务：
public class Person : Entity
{
	public virtual string Name { get; set; }
	public virtual string EmialAddress { get; set; }
	public virtual string Password { get; set; }
}

定义应用服务接口：
public interface IPersonAppService :IApplicationService
{
	SearchPeopleOutput SearchPeople(SearchPeopleInput input);
}

Input 和 Output DTO 类型定义如下：
public class SearchPeopleInput
{
	[StringLength(40,MinmumLength = 1)]
	public string SearchedName { get; set; }
}

public class SearchPeopleOutput
{
	public List<PersonDto> People { get; set; }
}

// EntityDto是一个简单具有与实体相同的Id属性的简单类型。如果你的实体Id不为int型你可以使用它泛型版本。EntityDto也实现了IDto接口。你可以看到PersonDto并不包含Password属性，因为展现层并不需要它。
public class PersonDto : EntityDto
{
	public string Name { get; set; }
	public string EmailAddress { get; set; }
}


实现 IPersonAppService
public class PersonAppService : IPersonAppService
{
	private readonly IPersonRepository _personRepository;

	public PersonAppService(IPersonRepository personRepository)
	{
		_personRepository = personRepository;
	}

	/* 
	我们从数据库获取实体，将实体转换成DTO并返回output。注意我们没有手动检测Input的数据有效性。ABP会自动验证它。ABP甚至会检查Input是否为null，如果为null则会抛出异常。这避免了我们在每个方法中都手动检查数据有效性。
	*/
	public SearchPeopleOutput SearchPeople(SearchPeopleInput input)
	{
		// 获取实体
		var peopleEntityList = _personRepository.GetAllList(person => person.Name.Contains(input.SearchName));

		// 转换为 DTO 对象
		var peopleDtoList = peopleEntityList.select(
			person => new PersonDto
						{
							Id = person.Id,
							Name = person.Name,
							EmailAddress = person.EmailAddress
						}).ToList();

		return new SearchPeopleOutput{ People = peopleDtoList}
	}
}


DTO 和实体间的自动映射：
使用 AutoMapper 来重写 SearchPeople 方法：

public SearchPeopleOutput SearchPeople(SearchPeopleInput input)
{
	var peopleEntityList = _personRepository.GetAllList(person => person.Name.Contains(input.SearchName));

	return new SearchPeopleOutput{ People = Mapper.Map<List<PersonDto>>(peopleEntityList) }
}



使用特性(attributes)和扩展方法来映射(Mapping using attributes and extension methods)
[AutoMap(typeof(MyClass2))
public class MyClass1
{
	public string TestProp { get; set; }
}

public class MyClass2
{
	public string TestProp { get; set; }
}

通过 MapTo 扩展方法进行映射：
var obj1 = new MyClass1 { TestProp = "Test Value"};
var obj2 = obj1.MapTo<MyClass2>();// 创建了新的 MyClass2 对象，并将 obj1.TestProp 的值赋值给新的 MyClass2 对象的 TestProp 属性
// 上述代码根据 MyClass1 创建了新的 MyClass2 对象，你也可以映射已存在的对象，如下所示：
var obj1 = new MyClass1 { TestProp = "Test Value"};
var obj2 = new MyClass2();
obj1.MapTo(obj2);// 根据 obj1 设置 obj2 的属性



StringBuilder query = new StringBuilder("");
while (dem.MoveNext())
{
	string key = dem.Current.Key;
	string value = dem.Current.Value;
	if(!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value)
	)
	{
		query.Append(key).Append("=").Append(value).Append("&");
	}
	/* 
	SortedDictionary<string,string> = (a,1) (b,2)
	"a=1&b=2"
	 */
}



ABP应用层：权限验证
在使用验证权限前，我们需要为每一个操作定义唯一的权限。Abp的设计是基于模块化，所以不同的模块可以有不同的权限。为了定义权限，一个模块应该创建AuthorizationProvider的派生类。MyAuthorizationProvider继承自AuthorizationProvider，换句话说就是AuthorizationProvider派生出MyAuthorizationProvider。例子如下：

public class MyAuthorizationProvider : MyAuthorizationProvider
{
	public override void SetPermission(IPermissionDefinitionContext context)
	{
		var administration = context.CreatePermission("Administration");

		var userManagement = administration.CreateChildPermission("Administration.UserManagement");
		userManagement.CreateChildPermission("Administration.UserManagement.CreateUser");

		var roleManagement = administration.CreateChildPermission("Administration.RoleManagement");
		
	}
}



ABP后台服务之后台作业和后台工人：
public class TestJob : BackgroundJob<int>,ITransientDependency
{
	public override void Execute(int number)
	{
		Logger.Debug(number.ToString());
	}
}


public class SimpleSendEmailJob : BackgroundJob<SimpleSendEmailJob>,ITransientDependency
{
	private readonly IRepository<User,long> _userRepository;
	private readonly IEmailSender _emailSender;

	public SimpleSendEmailJob(IRepository<User,long> userRepository,IEmailSender emailSender)
	{
		_userRepository = userRepository;
		_emailSender = emailSender;
	}

	public override void Execute(SimpleSendEmailJobArgs args)
	{
		var senderUser = _userRepository.Get(args.SenderUserId);
		var targetUSer = _userRepository.Get(args.TargetUserId);

		_emailSender.Send(senderUser.EmailAddress,targetUSer.EmailAddress,args.Subject,args.Body);
	}
}













================================一ABP入门系列：编写单元测试=============================================================


一ABP入门系列：编写单元测试
1.对ABP模板测试项目一探究竟：
在 ABP 框架的 Test 文件下存在 xxx.Tests (xxx为你的ABP项目解决方案名)控制台应用程序。
通过在Abp官网创建的模板项目中，默认就已经为我们创建好了测试项目。并对Session、User创建了单元测试。其中 xxxTestBase 是继承的集成测试基类，主要用来伪造一个数据库连接。该项目添加了对Application、Core、EntityFramework项目的引用，以便于我们针对它们进行测试。
从这我们也可以看出，Abp是按照Service-->Repository-->Domain这条线来进行集成测试。
即 ABP 框架的测试路径：Application --> EntityFramwork --> Core
打开测试项目的NuGet程序包我们可以发现主要依赖了以下几个NuGet包：
	·Abp.TestBase：提供了测试基类和基础架构以便我们创建单元集成测试。
	·Effort.EF6：对基于EF的应用程序提供了一种便利的方式来进行单元测试。
	·XUnit：.Net上好用的测试框架。
	·Shouldly：断言框架，方便我们书写断言。

2.xUnit：.NET 测试框架
xUnit.net 支持两种主要类型的单元测试：facts and theories（事实和理论）。

Facts are tests which are always true. They test invariant conditions.
Theories are tests which are only true for a particular set of data.

Facts：使用[Fact]标记的测试方法，表示不需要传参的常态测试方法。
Theories：使用[Theory]标记的测试方法，表示期望一个或多个DataAttribute实例用来提供参数化测试的方法的参数的值。

3.Shouldly：断言框架
Shouldly提供的断言方式与传统的Assert相比更实用易懂。
对比一下就明白了：

	Assert.That(contestant.Points, Is.EqualTo(1337));
	//Expected 1337 but was 0
	contestant.Points.ShouldBe(1337);
	//contestant.Points should be 1337 but was 0

4.测试基类 XxxTestBase.cs：是ABP框架自动生成的
#region MESTestBase 
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp;
using Abp.Authorization.Users;
using Abp.Events.Bus;
using Abp.Events.Bus.Entities;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.TestBase;
using SMC.MES.Authorization.Users;
using SMC.MES.EntityFrameworkCore.Seed.Host;
using SMC.MES.EntityFrameworkCore.Seed.Tenants;
using SMC.MES.MultiTenancy;
using SMC.MES.EntityFrameworkCore.SysDbContext;

namespace SMC.MES.Tests
{
    public abstract class MESTestBase : AbpIntegratedTestBase<MESTestModule>
    {
        protected MESTestBase()
        {
            void NormalizeDbContext(MESPlatFormDbContext context)
            {
                context.EntityChangeEventHelper = NullEntityChangeEventHelper.Instance;
                context.EventBus = NullEventBus.Instance;
                context.SuppressAutoSetTenantId = true;
            }

            // Seed initial data for host
            AbpSession.TenantId = null;
            UsingDbContext(context =>
            {
                NormalizeDbContext(context);
                new InitialHostDbBuilder(context).Create();
                new DefaultTenantBuilder(context).Create();
            });

            // Seed initial data for default tenant
            AbpSession.TenantId = 1;
            UsingDbContext(context =>
            {
                NormalizeDbContext(context);
                new TenantRoleAndUserBuilder(context, 1).Create();
            });

            LoginAsDefaultTenantAdmin();
        }

        #region UsingDbContext

        protected IDisposable UsingTenantId(int? tenantId)
        {
            var previousTenantId = AbpSession.TenantId;
            AbpSession.TenantId = tenantId;
            return new DisposeAction(() => AbpSession.TenantId = previousTenantId);
        }

        protected void UsingDbContext(Action<MESPlatFormDbContext> action)
        {
            UsingDbContext(AbpSession.TenantId, action);
        }

        protected Task UsingDbContextAsync(Func<MESPlatFormDbContext, Task> action)
        {
            return UsingDbContextAsync(AbpSession.TenantId, action);
        }

        protected T UsingDbContext<T>(Func<MESPlatFormDbContext, T> func)
        {
            return UsingDbContext(AbpSession.TenantId, func);
        }

        protected Task<T> UsingDbContextAsync<T>(Func<MESPlatFormDbContext, Task<T>> func)
        {
            return UsingDbContextAsync(AbpSession.TenantId, func);
        }

        protected void UsingDbContext(int? tenantId, Action<MESPlatFormDbContext> action)
        {
            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<MESPlatFormDbContext>())
                {
                    action(context);
                    context.SaveChanges();
                }
            }
        }

        protected async Task UsingDbContextAsync(int? tenantId, Func<MESPlatFormDbContext, Task> action)
        {
            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<MESPlatFormDbContext>())
                {
                    await action(context);
                    await context.SaveChangesAsync();
                }
            }
        }

        protected T UsingDbContext<T>(int? tenantId, Func<MESPlatFormDbContext, T> func)
        {
            T result;

            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<MESPlatFormDbContext>())
                {
                    result = func(context);
                    context.SaveChanges();
                }
            }

            return result;
        }

        protected async Task<T> UsingDbContextAsync<T>(int? tenantId, Func<MESPlatFormDbContext, Task<T>> func)
        {
            T result;

            using (UsingTenantId(tenantId))
            {
                using (var context = LocalIocManager.Resolve<MESPlatFormDbContext>())
                {
                    result = await func(context);
                    await context.SaveChangesAsync();
                }
            }

            return result;
        }

        #endregion

        #region Login

        protected void LoginAsHostAdmin()
        {
            LoginAsHost(AbpUserBase.AdminUserName);
        }

        protected void LoginAsDefaultTenantAdmin()
        {
            LoginAsTenant(AbpTenantBase.DefaultTenantName, AbpUserBase.AdminUserName);
        }

        protected void LoginAsHost(string userName)
        {
            AbpSession.TenantId = null;

            var user =
                UsingDbContext(
                    context =>
                        context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName));
            if (user == null)
            {
                throw new Exception("There is no user: " + userName + " for host.");
            }

            AbpSession.UserId = user.Id;
        }

        protected void LoginAsTenant(string tenancyName, string userName)
        {
            var tenant = UsingDbContext(context => context.Tenants.FirstOrDefault(t => t.TenancyName == tenancyName));
            if (tenant == null)
            {
                throw new Exception("There is no tenant: " + tenancyName);
            }

            AbpSession.TenantId = tenant.Id;

            var user =
                UsingDbContext(
                    context =>
                        context.Users.FirstOrDefault(u => u.TenantId == AbpSession.TenantId && u.UserName == userName));
            if (user == null)
            {
                throw new Exception("There is no user: " + userName + " for tenant: " + tenancyName);
            }

            AbpSession.UserId = user.Id;
        }

        #endregion

        /// <summary>
        /// Gets current user if <see cref="IAbpSession.UserId"/> is not null.
        /// Throws exception if it's null.
        /// </summary>
        protected async Task<User> GetCurrentUserAsync()
        {
            var userId = AbpSession.GetUserId();
            return await UsingDbContext(context => context.Users.SingleAsync(u => u.Id == userId));
        }

        /// <summary>
        /// Gets current tenant if <see cref="IAbpSession.TenantId"/> is not null.
        /// Throws exception if there is no current tenant.
        /// </summary>
        protected async Task<Tenant> GetCurrentTenantAsync()
        {
            var tenantId = AbpSession.GetTenantId();
            return await UsingDbContext(context => context.Tenants.SingleAsync(t => t.Id == tenantId));
        }
    }
}

#endregion
从上述代码中可以看出 MESTestBase 继承自 AbpIntegratedTestBase<MESTestModule>
在构造函数中主要做了两件事，预置了初始数据和种子数据，并以默认租户Admin登录。

5.单元测试实战
5.1 理清要测试的方法逻辑







xUnit.Net提供了三种继承于DataAttribute的特性（[InlineData]、 [ClassData]、 [PropertyData]）用于为[Theory]标记的参数化测试方法传参。
下面是使用这三种特性传参的实例：
InlineData Example：
public class StringTest1
{
	[Theory,
	InlineData("goodnight moom","moon",true),
	InlineData("hello world","hi",false)]
	publci void Contains(string input,string sub,bool expected)
	{
		var actual = input.Contains(sub);
		Assert.Equal(expected,actual);
	}
}

PropertyData Example:
public class StringTest2
{
	[Theory,PropertyData("SplitCountData")]
	public void SplitCount(string input,int expectedCount)
	{
		var actualCount = input.Split('').Count();
		Assert.Equal(expectedCount,actualCount);
	}
	
	public static IEnumerable<object[]> SplitCountData
	{
		get
		{
			// Or this could read from a file
			return new[]
			{
				new object[]{"xUnit",1},
				new object[]{"is fun",2},
				new object[]{"to test with",3}
			}
		}
	}

}

ClassData Example:
public class StringTest3
{
	[Theory,ClassDate(typeof(IndexOfData))]
	public void IndexOf(string input,char letter,int expected)
	{
		var actual = input.IndexOf(letter);
		Assert.Equal(expected,actual);
	}

	public class IndexOfData:IEnumerable<object[]>
	{
		private readonly List<object[]> _data = new List<object[]>
		{
			new object[]{"hello world","w",6},
			new object[]{"goodnight moon","w",-1}
		};

		public IEnumerable<object[]> GetEnumerator()
		{
			return _data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}




[Fact]
public void Should_Get_All_Tasks()
{
	var totalCount = UsingDbContext(ctx => ctx.Tasks.Count());

	var actualCount = _taskAppService.GetAllTasks().Count;

	actualCount.ShouldBe(totalCount);
}









ABP领域层：规约模式
public class CustomerManager
{
	private readonly IRepository<Customer> _customerRepository;

	public CustomerManager(IRepository<Customer> customerRepository)
	{
		_customerRepository = customerRepository;
	}

	public int GetCustomerCount(ISpecification<Customer> spec)
	{
		var customers = _customerRepository.GetAllList();

		var customerCount = 0;

		foreach (var customer in customers)
		{
			if (spec.IsSatisfiedBy<customer>)
			{
				customerCount++;
			}
		}

		return customerCount;
	}
	
}






=============================================SMC_MES========================================================



// 扩展方法
public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source,Expression<Func<TSource,bool>> predicate)
{
    if (source == null)
    {
        throw Error.ArgumentNull(nameof(source);
    }
    if (predicate == null)
    {
        throw Error.ArgumentNull(nameof(predicate));
    }

    // 将 IQueryable 中原有的 Expression 对象以及我们输入的 Lambda 表达式等对象“打包”，作为参数让 Provider 创建新的 Query
    return source.Provider.CreateQuery<TSource>(
        Expression.Call(
            null,
            CachedReflectionInfo.Where_TSource_2(typeof(TSource)),
            source.Expression,Expression.Quote(predicate)
        ));
}


class Program{
    static void Main(){
        using var context = new DemoContext();

        // Skip(n):跳过 n 个; Take(n):取 n 个; 常用于分页需求
        var leagues = context.Leagues.Skip(1).Take(3).ToList();

        foreach (var league in leagues)
        {
            league.Name += "-";
        }
        
        // 只有在调用 SaveChanges 方法之后才会执行 sql 语句
        var count = context.SaveChanges();

        Console.WriteLine(count);
    }
}


SMC.MES.EquipmentWorkReport/PDLine/PDLineAppService（属于 Application 层：Action业务编码 ）：
#region 获取分页数据
[httpGet]
public async Task<OutputPageInfo<PdlineOutput>> QueryPage(PagePdlineInput input){
    var query = await GetPDLineData(input.d,input.c);
    int skipCount = (input.Page - 1) * input.Limit;
    var queryList = from a in query.Skip(skipCount).Take(input.Limit).Include(x => x.RelaLineDevice) select a;
    var list = await queryList.Selcet(i => new PDLineDto{
        id = i.Id,
        n = i.Name,
        c = i.LineCode,
        t = i.WorkContent,
        d = i.DepartCode,
        h = i.Holon,
        r = i.Remark,
        l = i.Leader,
        g = i.Gx,
        od = i.OldDepartCode,
        ch = _autoMapper.Map<List<relaLineDevice>>(i.RelaLineDevice),
        devices = string.Join(",",i.RelaLineDevice.Select(j => j.Device.MachNm + "(" + j.Device.MachNO + ")"))
    }).ToListAsync();
    var totalCount = query.Count();
    var mapresult = _autoMapper.Map<List<PdlineOutput>>(list);
    var res = new OutputPageInfo<PdlineOutput>(totalCOunt,mapresult);
    return res;
}


#endregion



public class PersonAppService
{
	priavte IPersonRepository _personRepository;

	// 这就是知名的构造器注入模式。此时，PersonAppService代码，不知道哪个类实现了IPersonRepository和如何创建它。如果要使用PersonAppService，首先创建一个IpersonRepository，然后把它传递给PersonAppService的构造器
	public PersonAppService(IPersonRepository personRepository)
	{
		_personRepository = personRepository;
	}

	public void CreatePerson(string name,int age)
	{
		var person = new Person{ Name = name,Age = age};
		_personRepository.Insert(person);
	}
}



===========================================EF Core 教程：======================================


EF Core 教程：
namespace EFGetStarted
{
	public class BloggingContext:DbContext
	{
		public DbSet<Blog> Blogs { get; set;}
		public DbSet<Post> Posts { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("Data Source=blogging.db");

	}

	public class Blog
	{
		public int BlogId { get; set;}
		public string Url { get; set; }

		public List<Post> Posts { get; } = new List<Post>();
	}

	public class Post
	{
		public int PostId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }

		public int BlogId { get; set; }
		public Blog Blog { get; set; }

	}
}





===========================================LINQ相关：======================================
class Example{
	public static void Main(){
		// Func 是一个泛型委托，参数为 int，返回值也是 int
		Func<int,int> square = x => x * x;
		// lambda 表达式对应的是 expression 对象
		Expression<Func<int,int>> exp_square = x => x * x;
		// 一个 expression 对象可以被“编译”为委托对象
		Func<int,int> square2 = exp_square.Compile();

		Console.WriteLine(square(5));
		Console.WriteLine(square2(6));
	}
}



什么时候使用查询表达式？
1.使用于 LINQ to Object 和 没有建立主外键的EF查询
var list1 = new Dictionary<string,string>{ {1,"Tom"},{2,"Tom"},{3,"Tim"},{4,"Tim"}};
var list2 = new Dictionary<string,string>{{1,"Tom"},{2,"Tom"},{3,"Tim"},{4,"Tim"}};

var query = from l1 in list1 
			join l2 in list2
			on l1.Key equals l2.Key
			select new {l1,l2};

var quer1y = list1.Join(list2,l1 => l1.key,l2 => l2.Key,(l1,l2) => new {l1,l2});

2.点标记需要区分 OrderBy、ThenBy 比较麻烦
var query2 = from l1 in list1
			 join l2 in list2
			 on l1.key equals l2.key
			 select new {l1,l2};

var quer2y2 = list1.Join(list2,l1 => l1.Key,l2 => l2.Key,(l1,l2)=> new {l1,l2}).OrderBy(li => li.l1.Key).ThenByDescending(li => li.l2.Key).Select(t => new {t.l1,t.l2});

3.联接查询：内连接、外连接：左连接、右连接、交叉连接
var list1 = new Dictionary<string,string> {{1,"Tom"},{2,"Tom"},{3,"Tim"},{4,"Tim"}};
var list2 = new Dictionary<string,string> {{1,"Tom"},{2,"Tim"}};

// 内联接
var query3 = (from l1 in list1
			 join l2 in list2
			 on l1.Key equals l2.Key
			 select new {l1,l2}).ToList();

var quer3y3 = list1.Join(list2,l1 => l1.Key,l2 => l2.Key,(l1,l2) => new {l1,l2}).ToList();

// 左联接
var query4 = from l1 in list1
			 join l2 in list2
			 on l1.Key equals l2.Key into list
			 from l2 in list


在 C# 中，从功能上 LINQ 可分为两类：LINQ to Object 和 LINQ to Provider（如：XML）；从语法上 LINQ 可以分为 LINQ to Object 和 LINQ 扩展方法。大多数 LINQ to Object 都可以用 LINQ 扩展方法实现等同的效果，而且平时开发中用的最多的是 LINQ 扩展方法。

LINQ to Object 多用于映射数据库的查询，LINQ to XML 用于查询 XML 元素数据。使用 LINQ 查询的前提是对象必须是一个 IEnumerable 集合（注意，为了描述方便，本文说的集合都是指 IEnumerable 对象，包含字面上的 ICollection 对象）。另外，LINQ 查询大多是都是链式查询，即操作的数据源是 IEnumerable<T1> 类型，返回的是 IEnumerable<T2> 类型。

形如下面这样的查询就是 LINQ to Object：

var list = from user in users
  where user.Name.Contains("Wang")
  select user.Id;
等同于使用下面的 LINQ 扩展方法：

var list = users
  .Where(u => user.Name.Contains("Wang"))
  .Select(u => u.id);
LINQ 查询支持在语句中间根据需要定义变量，比如取出数组中平方值大于平均值的数字：

int[] numbers = {0,1,2,3,4,5,6,7,8,9};
var result = from number in numbers
			let average = numbers.Average()
			let squared = Math.Pow(number,2)
			where squared > average
			select number;
// 平均值为 4.5，result 为 {3,4,5,6,7,8,9}


SelectMany 集合降维


SelectMany 可以把多维集合降维，比如把二维的集合平铺成一个一维的集合。举例：

var collection = new int[][]
{
    new int[] {1, 2, 3},
    new int[] {4, 5, 6},
};
var result = collection.SelectMany(x => x);
// result = [1, 2, 3, 4, 5, 6]
再来举个更贴合实际应用的例子。例如有如下实体类（一个部门有多个员工）：

class Department
{
    public Employee[] Employees { get; set; }
}

class Employee
{
    public string Name { get; set; }
}
此时，我们拥有一个这样的数据集合：

var departments = new[]
{
    new Department()
    {
        Employees = new []
        {
            new Employee { Name = "Bob" },
            new Employee { Name = "Jack" }
        }
    },
    new Department()
    {
        Employees = new []
        {
            new Employee { Name = "Jim" },
            new Employee { Name = "John" }
        }
    }
};
现在我们可以使用 SelectMany 把各部门的员工查询到一个结果集中：

var allEmployees = departments.SelectMany(x => x.Employees);
foreach(var emp in allEmployees)
{
    Console.WriteLine(emp.Name);
}
// 依次输出：Bob Jack Jim John


Skip & Take 分页


Skip 扩展方法用来跳过从起始位置开始的指定数量的元素读取集合；Take 扩展方法用来从集合中只读取指定数量的元素。

var values = new[] { 5, 4, 3, 2, 1 };
var skipTwo = values.Skip(2);  // { 3, 2, 1 }
var takeThree = values.Take(3);  // { 5, 4, 3 }
var skipOneTakeTwo = values.Skip(1).Take(2); // { 4, 3 }
Skip 与 Take 两个方法结合即可实现我们常见的分页查询：

public IEnumerable<T> GetPage<T>(this IEnumerable<T> collection, int pageNumber, int pageSize)
{
    int startIndex = (pageNumber - 1) * pageSize;
    return collection.Skip(startIndex).Take(pageSize);
}
使用过 EF (Core) 的同学一定很熟悉。

另外，还有 SkipWhile 和 TakeWhile 扩展方法，它与 Skip 和 Take 不同的是，它们的参数是具体的条件。SkipWhile 从起始位置开始忽略元素，直到匹配到符合条件的元素停止忽略，往后就是要查询的结果；TakeWhile 从起始位置开始读取符合条件的元素，一旦遇到不符合条件的就停止读取，即使后面还有符合条件的也不再读取。示例：

SkipWhile：

int[] list = { 42, 42, 6, 6, 6, 42 };
var result = list.SkipWhile(i => i == 42);
// result: 6, 6, 6, 42
TakeWhile：

int[] list = { 1, 10, 40, 50, 44, 70, 4 };
var result = list.TakeWhile(item => item < 50).ToList();
// result = { 1, 10, 40 }











==========================================数据库SQL相关============================


#region sqlserver 数据类型
Sql Server 数据类型：一共有25种，分为 10 类：
一、整数类型：
    1.bigint：8字节
        其中一个二进制位表示符号位，其它63个二进制位表示长度和大小，可以表示-2的63次方到2的63次方-1范围内的所有整数。超大应用场合，考虑用bigint。

    2.int/integer：4字节
        其中一个二进制位表示符号位，其它31个二进制位表示长度和大小，，可以表示-2的31次方~2的31次方-1范围内的所有整数。占用空间小，运算速度快。
        
    3.smallint：2字节
        其中一个二进制位表示整数值的正负号，其它15个二进制位表示长度和大小，，可以表示-2的15次方~2的15次方-1范围内的所有整数。

    4.tinyint：1字节
        允许从 0 到 255 的所有数字。占用空间更小。



二、浮点数类型：
    浮点数据类型存储十进制小数，浮点数据为近似值。Sql Server中采用了只入不舍的方式进行存储，即当且仅当要舍入的数是一个非零数时，对其保留数字部分的最低有效位上加1，并进行必要的近位。
    1.float[(n)]：4或8字节
        从 -1.79E + 308 到 1.79E + 308 的浮动精度数字数据。 参数 n 指示该字段保存 4 字节还是 8 字节。float(24) 保存 4 字节，而 float(53) 保存 8 字节。n 的默认值是 53。尽量少用，性能不好，精度不高，一般只用于科学计算。

    2.decimal(p,s)：5-17字节
        固定精度和比例的数字。允许从 -10^38 +1 到 10^38 -1 之间的数字。p(精度)指定了最多可以存储十进制数字的总位数，包括小数点左边和右边的位数。p 必须是 1 到 38 之间的值，默认是 18。s(小数位数)指定小数点右边可以存储的十进制数字的最大位数，小数位数必须是从0到p之间的值，仅在指定精度后才可以指定小数的位数。默认小数位数是0；因此，0<=s<=p。最大存储大小基于精度而变化。
        例如：decimal(10,5)表示共有10位数，其中整数5位，小数5位。

    3.numeric(p,s)：5-17字节
        和 decimal 一样

    4.real：4字节
        可以存储正的或者负的十进制数值，它的存储范围从-3.40E+38~-1.18E-38、0以及1.18E-38~3.40E+38.
    
    货币数据：-money 和 smallmoney 尽量用decimal代替，因为decimal性能稍好一些，相对其他数据库兼容性好。
    5.money：8字节
        用于存储货币值，介于 -922,337,203,685,477.5808 和 922,337,203,685,477.5807 之间的货币数据。money数据类型中整数部分包含19个数字，小数部分包含4个数字，因此money数据类型的精度是19，
    6.smallmoney：4字节
        介于 -214,748.3648 和 214,748.3647 之间的货币数据。与money类型相似，输入数据时在前面加上一个货币符号，如人民币为￥或其它定义的货币符号。
    



三、字符数据类型：
    字符数据类也是Sql Server中最常用的数据类型之一，用来存储各种字符，数字符号和特殊符号。在使用字符数据类型时，需要在其前后加上英文单引号或者双引号。
    1.char(n)：固定长度的字符串。最多8000个字符
        当用char数据类型存储数据时，每个字符和符号占用一个字节存储空间，n表示所有字符所占的存储空间，n的取值为1~8000。若不指定n的值，系统默认n的值为1。若输入数据的字符串长度小于n，则系统自动在其后添加空格来填满设定好的空间；若输入的数据过长，则会截掉其超出部分。

    2.varchar(n|max)：可变长度的字符串。最多 8,000 个字符
        n为存储字符的最大长度，其取值范围是1~8000，但可根据实际存储的字符数改变存储空间，max表示最大存储大小是(2^31)-1个字符。存储大小是输入数据的实际长度加2个字节。所输入数据的长度可以为0个字符。
        如varchcar(20),则对应的变量最多只能存储20个字符，不够20个字符的按实际存储。常用于10字节以上的数据

    3.text：可变长度的字符串。最多 2GB 字符数据。
        考虑到维护方便、效率以及程序开发的方便性，最好不用，即不将其存入数据库中；采用varchar指向其相应的存储路径

    Unicode 字符串：-n类型的占用空间大，性能低，如果不准备存中文或中亚文字则尽量避免使用
    3.nchar(n)：若需要存储中文，则用
        n个字符的固定长度Unicode字符数据。n值必须在1~4000之间(含)，如果没有数据定义的或变量声明语句中指定n，默认长度为1。此数据类型采用Unicode字符集，因此每一个存储单位占两个字节，可将全世界文字囊括在内（当然除了部分生僻字）。

    4.nvarchar(n|max)：若需要存储中文，则用
        与varchar类似，存储可变长度Unicode字符数据。n值必须在1~4000之间(含)，如果没有数据定义的或变量声明语句中指定n，默认长度为1。max指最大存储大小为2的31次方-1字节。存储大小是输入字符个数的两倍+2个字节。所输入的数据长度可以为0个字符.

    5.ntext：可变长度的 Unicode 数据。最多 2GB 字符数据。
        
    

四、日期与时间数据类型：
    1.date：3字节
        仅存储日期，存储用字符串表示的日期数据，可以表示0001-01-01~9999-12-31(公元元年1月1日到公元9999年12月31日)间的任意日期值。数据格式为“YYYY-MM-DD”:
        YYYY:表示年份的四位数字，范围为0001~9999；
        MM：表示指定年份中月份的两位数字，范围为01~12；
        DD：表示指定月份中某一天的两位数字，范围为01~31（最高值取决于具体月份）

    2.time：3-5字节，精度为 100 纳秒。
        仅存储时间，以字符串形式记录一天的某个时间，取值范围为 00:00:00.0000000~23:59:59.9999999,数据格式为“hh:mm:ss[.nnnnnnn]”:
        hh:表示小时的两位数字，范围为0~23。
        mm：表示分钟的两位数字，范围为0~59。
        ss:表示秒的两位数字，范围为0~59。
        n*是0~7为数字，范围为0~9999999，它表示秒的小部分.
    因此常用的日期时间格式为：YYYY-MM-DD hh:mm:ss 即 年-月-日 时-分-秒

    3.datetime：8字节，精度为 3.33 毫秒。
        用于存储时间和日期数据，从1753年1月1日到9999年12月31日，默认值为1900-01-01 00：00：00，当插入数据或在其它地方使用时，需用单引号或双引号括起来。可以使用“/”、“-”和“.”作为分隔符，

    4.datetime2：6-8字节，精度为 100 纳秒。
        datetime的扩展类型，其数据范围更大，默认的最小精度最高，并具有可选的用户定义的精度。默认格式为：YYYY-MM-DD hh:mm:ss[.fractional seconds],日期的存取范围是0001-01-01~9999-12-31(公元元年1月1日到公元9999年12月31日).

		DateTime字段类型对应的时间格式是yyyy-MM-dd HH:mm:ss.fff，3个f，精确到1毫秒(ms)；示例：2014-12-0317:06:15.433。
		DateTime2(7)字段类型对应的时间格式是yyyy-MM-dd HH:mm:ss.fffffff，7个f，精确到0.1微秒(μs)；
		示例：2014-12-0317:23:19.2880929。
		如果用SQL的日期函数进行赋值，DateTime字段类型要用GETDATE()，DateTime2字段类型要用SYSDATETIME()。
    
    5.smalldatetime：4字节，精度为 1 分钟。
        smalldatetime类型与datetime类型相似，只是其存储范围是从1900年1月1日到2079年6月6日。

    6.datetimeoffset：8-10字节，与 datetime2 相同，外加时区偏移。
        用于定义一个采用24小时制与日期相组合并可识别时区的时间。默认格式是：“YYYY-MM-DD hh:mm:ss[.nnnnnnn][{+|-}hh:mm]”
        hh:两位数，范围是-14~14
        mm：两位数，范围为00~59；
        这里hh是时区偏移量，该类型数据中保存的是世界标准时间（UTC）值
        eg：要存储北京时间2011年11月11日12点整，存储时该值将是2011-11-11 12:00:00+08:00,因为北京处于东八区，比UTC早8个小时。



五、二进制数据类型：
    1.image：可变长度的二进制数据，最多 2GB。
        范围为:0~2的31次方-1个字节。用于存储照片、目录图片或者图画，由系统根据数据的长度自动分配空间，存储该字段的数据一般不能使用insert语句直接输入。考虑到维护方便、效率以及程序开发的方便性，最好不用，即不将其存入数据库中；采用varchar指向其相应的存储路径
    
    2.bit：1字节
        位数据，只取0或1为值，相当于 boolean 型。bit值经常当作逻辑值用于判断true(1)或false(0),输入非0值时系统将其替换为1。

    3.binary(n)：固定长度的二进制数据。最多 8,000 字节。
        长度为n个字节的固定长度二进制数据，其中n是从1~8000的值。存储大小为n个字节。在输入binary值时，必须在前面带0x,可以使用0xAA5代表AA5，如果输入数据长度大于定于的长度，超出的部分会被截断。
    
    4.varbinary(n|max)：可变长度的二进制数据。最多 8,000 | 2GB 字节。
        可变长度二进制数据。其中n是从1~8000的值，max指示存储大小为2的31次方-1字节。存储大小为所输入数据的实际长度+2个字节。
        在定义的范围内，不论输入的时间长度是多少，binary类型的数据都占用相同的存储空
        间，即定义时空间，而对于varbinary类型的数据，在存储时实际值的长度使用存储空间.



六、其他数据类型：
    1.rowversion：8字节
        每个数据都有一个计数器，当对数据库中包含rowversion列的表执行插入或者更新操作时，该计数器数值就会增加。此计数器是数据库行版本。一个表只能有一个rowversion列。公开数据库中自动生成的唯一二进制数字的数据类型。rowversion通常用作给表行加版本戳的机制。rowversion数据类型只是递增的数字，不保留日期或时间。
    
    2.timestamp：
        时间戳数据类型，timestamp的数据类型为rowversion数据类型的同义词，提供数据库范围内的唯一值，反映数据修改的唯一顺序，是一个单调上升的计数器。
        eg：create table testTable （id int primary key,timestamp ）;
        此时Sql Server数据库引擎将生成timestamp列名；但rowversion不具备这样的行为，在使用rowversion时，必须指定列名.
    
    3.uniqueidentifier：存储全局标识符（GUID）
        16字节的GUID(Globally Unique Identifier,全球唯一标识符)，是Sql Server根据网络适配器地址和主机CPU时钟产生的唯一号码，其中，每个为都是0~9或a~f范围内的十六进制数字。
        例如：6F9619FF-8B86-D011-B42D-00C04FC964FF,此号码可以通过newid()函数获得，在全世界各地的计算机由次函数产生的数字不会相同。

    4.cursor：存储对用于数据库操作的指针的引用

    5.sql_variant：存储最多 8000 字节不同的数据类型的数据，除了 text、ntext、timestamp 类型
        用于存储除文本，图形数据和timestamp数据外的其它任何合法的Sql Server数据，可以方便Sql Server的开发工作。

    6.table：存储对表或视图处理后结果集，供稍后处理
        用于存储对表或视图处理后的结果集。这种新的数据类型使得变量可以存储一个表，从而使函数或过程返回查询结果更加方便、快捷。
    
    7.xml：存储xml数据结果集，供稍后处理
        存储xml数据的数据类型。可以在列中或者xml类型的变量中存储xml实例。存储的xml数据类型表示实例大小不能超过2GB。

#endregion









sqlserver 中的 sql 语句：
create table Status(
    StatusId int identity(1,1) not null,
    StatusName varchar(50) not null,
    DateCreated datetime not null constraint DF_Status_DateCreated DEFault (gatdate()),
    constraint PK_Status primary key clustered (StatusId)
)

insert into Status (statusName) values ('To Do');
insert into Status (statusName) values ('In Progress');
insert into Status (statusName) values ('Done');

select * from Status;

update Tasks set StatusId = '1' where TaskId = '2';

使用查询设计器建立查询
select Tasks.TaskName,Tasks.Description Status inner join 
Tasks on Status.StatusId = Tasks.StatusId where (Status.StatusId = 1)

创建视图：
create view ToDoList as 
select Tasks.TaskName,Tasks.Description 
from Status inner join Tasks on Status.StatusId = Tasks.StatusId
where (Status.StatusId = 1)
修改视图：
alter view ToDoList as 
select Tasks.TaskName,Tasks.Description
from Status inner join Tasks on Status.StatusId = Tasks.StatusId
where (Status.StatusName = 'To Do')


// 相当于 C# 中的 if-else：可以进行区级判断
// 要求 then 后面的数据类型必须一致
select *,
	   rank=case 
	   			when [level]=1 then 'Low'
				when [level]=2 then 'Mid'
				when [level]=3 then 'Master'
				else 'Master of Master'
			end

select tscoreId,
	   tsid,
	   tenglish
	   等级=case
	   			when tenglish>=95 then '优'
				when tenglish>=80 then '良'
				when tenglish>=70 then '中'
				else '差'
			end
from TbScore

select *,
	   IsQualified=case
	   				when tenglish>=60 and tMath>=60 then 'Qualified'
					else 'NotQualified'
				   end
from TbScore;



select 
	销售员
	总金额=sum(销售价格*销售数量)
	称号=case
			when sum(销售价格*销售数量)>6000 then '金牌'
			when sum(销售价格*销售数量)>5500 then '银牌'
			when sum(销售价格*销售数量)>4500 then '铜牌'
			else '普通'
		end
from MyOrder
group by 销售员
			

select 
	x=case
		when A>B then A
		else B
	  end,
	Y=case
		when B>C then B
		else C
	  end
from TestA









===============================MySQL==========================================

		
MYSQL：
	多张表形成一个库 多个库交给数据库服务器管理
	一台服务器下有多个库 一个库下有1到多张表 表中有多行多列的数据


数据库的基本操作：在mysql数据库中SQL语言是以分号（;）或者\g作为结束
	1、初识SQL语言
	SQL：structured query language 即结构化查询语言 是对下面四种语言的统称
	SQL语言主要用于存取数据、查询数据、更新数据和管理关系数据库系统，SQL语言由IBM开发，可分为：

	DDL ：数据定义语句：定义和管理数据对象，如数据库，数据表等，命令有 create、drop、alter		
        数据库定义语言：数据库、表、视图、索引、存储过程，例如create drop alter 通常是开发人员关注 它创造了数据库的整个框架 如数据库、表等

	DML ：数据操作语句：用于操作数据库对象中所包含的数据，命令有 insert、update、delete
        数据库操纵语言：插入数据INSERT、删除数据DELETE、更新数据UPDATE、查询数据SELECT 对象是表当中的数据 “增删改查” DML是往框架中添数据
	
	DQL ：数据查询语句：用于查询数据库数据，命令有 select 
        数据库查询语句：查询数据SELECT 单独分出来是因为查询语句实际上是数据库中使用最多的 DQL则是从框架中读取数据，只有DQL是读操作 其他均为写操作

    DCL ：数据控制语句：用于管理数据库的语言，包括管理权限及数据更改，命令有 grant、commit、rollback
        数据库控制语言：例如控制用户的访问权限GRANT（授权）、REVOKE（撤销）	
	
	2、系统数据库
	information_schema：虚拟库，主要存储了系统中的一些数据库对象，例如用户表信息、列信息、权限信息、
						字符信息等
	performance_schema：主要存储数据库服务器的性能参数
	mysql			  ：授权库，主要存储系统用户的权限信息 
	sys				  ：主要存储数据库服务器的性能参数
	

	mysql> show databases;#查看数据库 
	+--------------------+
	| Database           |
	+--------------------+
	| information_schema |
	| mysql              |
	| performance_schema |
	| sys                |
	+--------------------+
	4 rows in set (0.44 sec)
	#information_schema是一个虚拟数据库，并不物理存在，在select的时候，从其他数据库获取相应的信息。
	#它实际不存在~ 相当于系统数据库。是内建的。
	#这个并不是一个实际的数据库，只不过是数据字典。在MYSQL的内存中，并由MYSQL本身来维护。				
	
	3、创建数据库
		语法：create database [if not exists] 数据库名；
		数据库命名规则：
						区分大小写 - 指令不区分大小写 但数据库的名字是区分的
						唯一性
						不能使用关键字如create、select等
						不能单独使用数字
        
        使用数据库可视化工具创建数据库时，选择基字符集为 utf8 数据库排序规则为 utf8_general_ci（校对不区分大小写）

		sql server query;
		/* 
		clustered：
		聚簇索引(Clustered Index)和非聚簇索引 (Non- Clustered Index)
		最通俗的解释是:聚簇索引的顺序就是数据的物理存储顺序，而对非聚簇索引的索引顺序与数据物理排列顺序无关。举例来说，你翻到新华字典的汉字“爬”那一页就是P开头的部分，这就是物理存储顺序（聚簇索引）；而不用你到目录，找到汉字“爬”所在的页码，然后根据页码找到这个字（非聚簇索引）。

		聚簇索引的唯一性
		正式聚簇索引的顺序就是数据的物理存储顺序，所以一个表最多只能有一个聚簇索引，因为物理存储只能有一个顺序。正因为一个表最多只能有一个聚簇索引，所以它显得更为珍贵，一个表设置什么为聚簇索引对性能很关键。

		非聚簇索引
		索引节点的叶子页面就好比一片叶子。叶子头便是索引键值。
		通常，我们会在每个表中都建立一个ID列，以区分每条数据，并且这个ID列是自动增大的，步长一般为1。我们的这个办公自动化的实例中的列Gid就是如此。此时，如果我们将这个列设为主键，SQL SERVER会将此列默认为聚集索引。这样做有好处，就是可以让您的数据在数据库中按照ID进行物理排序，但笔者认为这样做意义不大。      
		SQL SERVER默认是在主键上建立聚集索引的
		 */
		create table [dbo].[Process_PathDetail_bak](
		// identity表示自增列的意思，而int identity(1,1)表示从1开始递增，每次自增1。表的第一列是id，它是int型的，并且是自增的，也就是你向表中插入数据的时候，不用给id列赋值，id列会自己复制。
			[ID] int identity(1,1) not null,
			[部品型号] nvarchar(50) null,
			[HOLON]	nvarchar(10) null,
			[原价基准] int null,
			[工序编号] nvarchar(50) null,
			[设备编号] nvarchar(50) null,
			[测量日期] smalldetetime null,
			[工序名称] nvarchar(50) null,
			[瓶颈设备标记] nvarchar(10) null,
			[循环时间] decimal(18,2) null,
			[模具编号] nvarchar(20) null,
			[设备组] nvarchar(50) null,
			[循环时间组] nvarchar(50) null,
		// constraint 约束
			constraint [Pk_Process_PathDetail_bak] primary key clustered ([ID] ASC)
		)



		删除数据库：drop database [if exists] 数据库名；


	mysql> drop database escshop;	
    #删除数据库 注：若数据库不存在则会报错 如不想报错 可以写：
    #drop database IF EXISTS escshop;
    #可以使用help drop来查看帮助 加不加分号都可以
	Query OK, 0 rows affected (0.61 sec)




---------------------------------------------
mysql表操作 DDL语句：
	mysql数据库中的表：
		将字段的列称为列 将字段的行称为记录

	表是数据库存储数据的基本单位，由若干个字段组成，主要用来存储数据记录。
	表的操作包括创建表、查看表、修改表和删除表。

	创建表：create table 表名;
	查看表结构：desc 表名; ，show create table 表名;
	修改表：alter table
	复制表：create table 
	删除表：drop 表名;

1、创建表：
	语法：create table 表名(
				字段名1 类型[(宽度) 约束条件],
				字段名2 类型[(宽度) 约束条件],
				字段名3 类型[(宽度) 约束条件],
				);

	sqlserver 语法：
		create table [dbo].[表名](
			// [字段名1] 中的 [] 表示语法格式，类型[(宽度) 约束条件] 后的 [] 表示可选项
			[字段名1] 类型[(宽度) 约束条件],
			[字段名1] 类型[(宽度) 约束条件],
			[字段名1] 类型[(宽度) 约束条件],
			... ...
		)

	在同一张表中 字段名不能相同
	宽度和约束条件为可选项
	字段名和数据类型是必须的

2、查看表结构
	describle 表名; 或者 desc 表名;
	
	show create table 表名; 查看表创建时的详细结构

3、表完整约束性
	约束条件是约束我们将来插入的数据的 用于保证数据的完整性和一致性
	
	约束条件		说明		
	primary key		标识该字段为该表的主键	可以唯一的表示记录	不可以为空
	foreign key		标识该字段为该表的外键	实现表与表（父表主键/子表1外键/..）之间的关联
	not null		表示该字段不能为空（即不能为null）
	unique key		标识该字段的值是唯一的	但可以为空	一个表中可以有多个unique key
	// 在 sqlserver 中为 identity(1,1) 表示自增，并且 seed 为1
	auto_increment	表示该字段的值是自动增长的 	但要求改字段必须为主键且为整数类型
	default			为该字段设定默认值
	unsigend		指定该字段是无符号的 即整数
	zerofill		使用0进行填充

	说明：
		1.是否允许为空 默认为允许 可设置not null 则字段不允许为空 必须赋值
			空（null）和空白以及‘null’是不同的 空指没有插入任何数据 而空白则是插入了空白格或是换行符
			之类的
		2.默认值 不设置则默认为null 如果插入记录时不给字段赋值 则此时字段就会使用默认值
		3.是否为key：
			主键 primary key
			外键 foreign key
			索引 (index,unique...)
		4.可以将某一列设为主键  也可以将多列设为主键
			但一旦设为主键 则一定是唯一的且不能为空（即不能为NULL）
			null和'null'是不一样的 null为空 而‘null’则表示输入为null
			但创造表时 primary key关键字仅能出现一次 故想要将多了设为主键时 需要：
			primary key(字段1,字段2,...) 
				可以给主键设定一个名称 若不设置则默认以字段名为主键名
				当选择多列同时作为主键时 此时是复合主键 
				在复合主键中某一个字段有重复也可以 只要所有字段共同组成的复合主键是唯一的即可
			即若将某一列设为主键  则此列下面的所有数据不能重复且不能为空 故有唯一标识记录的作用 
			主键的作用是用来区分表中唯一的记录  特性：唯一且非空 

		5.外键是用来关联父表的子键
		6.UNIQUE键只要不重复就可以 可以为NULL（空）
		7.自增是有前提的 要为主键且为整型数据
		8.只要是key就是有名字的 字段名即为键名：


 
mysql>select * from school.student1; #使用绝对路径查看表中所有字段的信息 *表示所有字段 

mysql>

===设置外键约束 foreign key

在父表中某一字段为主键 则在子表中的同一字段为外键 而不是主键 但子表中外键字段的名字、数据类型与约束条
件都要与父表一致  且子表中的外键的值的插入是基于父表的

结论：当父表的主键字段的某条记录修改时 子表中的外键字段也会同步修改
	  当父表的主键字段删除某条记录时 子表中的外键字段也会同步删除
一般我们在开发过程中，并不会实际使用这样的子父表结构，即不会同步删除与修改

当我们使用insert插入语句对表插入值时  若没有指定表中的字段名 则在插值时 插入的数值个数一定要和
表中的字段相匹配 若不想插入值 也要使用null 而不能不写


mysql> create table s4(
    -> id int  primary key auto_increment,
    -> name varchar(50) not null,
    -> sex enum('m','f') default 'm' not null,
    -> age int unsigned default '20' not null,
    -> bobby set('music','disc','dance','book') default 'music' 
    -> );
Query OK, 0 rows affected (0.12 sec)

mysql> desc s4;
+-------+------------------------------------+------+-----+---------+----------------+
| Field | Type                               | Null | Key | Default | Extra          |
+-------+------------------------------------+------+-----+---------+----------------+
| id    | int(11)                            | NO   | PRI | NULL    | auto_increment |
| name  | varchar(50)                        | NO   |     | NULL    |                |
| sex   | enum('m','f')                      | NO   |     | m       |                |
| age   | int(10) unsigned                   | NO   |     | 20      |                |
| bobby | set('music','disc','dance','book') | YES  |     | music   |                |
+-------+------------------------------------+------+-----+---------+----------------+
5 rows in set (0.18 sec)

mysql> insert into s4 values('tom','m',18,'book');
ERROR 1136 (21S01): Column count doesn't match value count at row 1
	#当插入数据时没有指定特定字段时 插入数据的个数必须与字段数保持一致！
	
mysql> insert into s4 values(1,'tom','m',18,'book');
Query OK, 1 row affected (0.03 sec)

mysql> insert into s4(name) values('lim');
Query OK, 1 row affected (0.09 sec)

mysql> insert into s4(name) values('lal');	
Query OK, 1 row affected (0.01 sec)

mysql> select * from s4;
+----+------+-----+-----+-------+
| id | name | sex | age | bobby |
+----+------+-----+-----+-------+
|  1 | tom  | m   |  18 | book  |
|  2 | lim  | m   |  20 | music |
|  3 | jj   | f   |  50 | NULL  |
|  4 | lal  | m   |  20 | music |
+----+------+-----+-----+-------+
4 rows in set (0.00 sec)


= = = =================设置唯一约束unique键：
	
mysql> create table department(dep_id int,dep_name varchar(30) unique, dep_comment varchar(100) );
Query OK, 0 rows affected (0.20 sec)

mysql> desc department;
+-------------+--------------+------+-----+---------+-------+
| Field       | Type         | Null | Key | Default | Extra |
+-------------+--------------+------+-----+---------+-------+
| dep_id      | int(11)      | YES  |     | NULL    |       |
| dep_name    | varchar(30)  | YES  | UNI | NULL    |       |
| dep_comment | varchar(100) | YES  |     | NULL    |       |
+-------------+--------------+------+-----+---------+-------+
3 rows in set (0.03 sec)

mysql> insert into department values(1,'hr','hire people');
Query OK, 1 row affected (0.04 sec)

mysql> insert into department values(2,null,'null');
	#unique键不允许重复 但可以为空 注意：null和'null'是不一样的 null为空 而‘null’则表示输入为null
Query OK, 1 row affected (0.00 sec)

mysql> select * from department;
+--------+----------+-------------+
| dep_id | dep_name | dep_comment |
+--------+----------+-------------+
|      1 | hr       | hire people |
|      2 | NULL     | null        |
+--------+----------+-------------+
2 rows in set (0.00 sec)


====设置主键约束primary key：
primary key 字段的值是不允许重复，且不允许为NULL
既可以单列做主键 也可以多列做主键（复合主键）

单列做主键：
	mysql> create table s1(
		-> id int primary key auto_increment,
		-> name varchar(50) not null,
		-> sex enum('m','f') not null default 'f',
		-> age int not null default 18
		-> );
	Query OK, 0 rows affected (0.21 sec)

	mysql> desc s1;
	+-------+---------------+------+-----+---------+----------------+
	| Field | Type          | Null | Key | Default | Extra          |
	+-------+---------------+------+-----+---------+----------------+
	| id    | int(11)       | NO   | PRI | NULL    | auto_increment |
	| name  | varchar(50)   | NO   |     | NULL    |                |
	| sex   | enum('m','f') | NO   |     | f       |                |
	| age   | int(11)       | NO   |     | 18      |                |
	+-------+---------------+------+-----+---------+----------------+
	4 rows in set (0.04 sec)

	mysql> insert into s1(name,sex,age) values
		-> ('jack','f',17),('alice','f',20),('tianjin','m',54);
	Query OK, 3 rows affected (0.03 sec)
	Records: 3  Duplicates: 0  Warnings: 0

	mysql> select * from s1;
	+----+---------+-----+-----+
	| id | name    | sex | age |
	+----+---------+-----+-----+
	|  1 | jack    | f   |  17 |
	|  2 | alice   | f   |  20 |
	|  3 | tianjin | m   |  54 |
	+----+---------+-----+-----+
	3 rows in set (0.01 sec)

	mysql> 


复合主键：
	主键：host_ip + port = primary key 为复合主键 只要host_ip + port不重复 且不为空即可
	mysql> create table service(
    -> host_ip varchar(15) not null,
    -> service_name varchar(10) not null,
    -> allow enum('Y','N') not null default 'N',
    -> port int not null,
    -> primary key(host_ip,port)
    -> );
	Query OK, 0 rows affected (0.11 sec)

	mysql> desc service;
	+--------------+---------------+------+-----+---------+-------+
	| Field        | Type          | Null | Key | Default | Extra |
	+--------------+---------------+------+-----+---------+-------+
	| host_ip      | varchar(15)   | NO   | PRI | NULL    |       |
	| service_name | varchar(10)   | NO   |     | NULL    |       |
	| allow        | enum('Y','N') | NO   |     | N       |       |
	| port         | int(11)       | NO   | PRI | NULL    |       |
	+--------------+---------------+------+-----+---------+-------+
	4 rows in set (0.03 sec)

	mysql> insert into service values('192.168.122.223','http','Y',80);
	Query OK, 1 row affected (0.05 sec)

	mysql> insert into service values('192.168.122.223','ftp','Y',21);
	Query OK, 1 row affected (0.01 sec)


	mysql> insert into service values('192.168.122.222','http','Y',80);
	Query OK, 1 row affected (0.02 sec)

	mysql> select * from service;
	+-----------------+--------------+-------+------+
	| host_ip         | service_name | allow | port |
	+-----------------+--------------+-------+------+
	| 192.168.122.222 | http         | Y     |   80 |
	| 192.168.122.223 | ftp          | Y     |   21 |
	| 192.168.122.223 | http         | Y     |   80 |
	+-----------------+--------------+-------+------+
	3 rows in set (0.00 sec)
		
	mysql> 

	
===========设置外键约束foreign key
父表：
	mysql> create table employee(
    -> name varchar(50) not null,		#主键
    -> mail varchar(20),
    -> primary key(name)		
    -> );
子表：
	mysql> create table payroll(
    -> id int auto_increment,
    -> name varchar(50) not null,		#外键
    -> payroll float(8,2) not null,
    -> primary key(id),
    -> foreign key(name) references employee(name) on update cascade on delete cascade);
子表name为外键，关联父表employee的主键name 同步更新 同步删除
外键设置为on delete cascade：表示当删除父表中的时候主键字段内容，同时把会把字表中对应的外键删除 

	mysql> insert into employee values('alice',null);
	Query OK, 1 row affected (0.08 sec)

	mysql> insert into employee values('jack','jack@123.com');
	Query OK, 1 row affected (0.00 sec)

	mysql> select * from employee;
	+-------+--------------+
	| name  | mail         |
	+-------+--------------+
	| alice | NULL         |
	| jack  | jack@123.com |
	+-------+--------------+
	2 rows in set (1.65 sec)

	mysql> insert into payroll(name,payroll) values('alice',80000);
	Query OK, 1 row affected (0.09 sec)
		#子表中原先并没有插入alice的记录 但父表中有 故子表可以直接使用

	mysql> select * from payroll;
	+----+-------+----------+
	| id | name  | payroll  |
	+----+-------+----------+
	|  1 | alice | 80000.00 |
	+----+-------+----------+
	1 row in set (0.00 sec)

	mysql>update employee set name='jackccc' where name=‘jack’;
		将父表中jack的name改为jackccc 若没有后面的where语句 则父表中name字符的所有名字都将改为jackccc

	mysql>delete from employee where name=‘alice’;
		在父表employee表中删除name为alice的数据/记录 

	mysql> select * from employee;
	+---------+--------------+
	| name    | mail         |
	+---------+--------------+
	| jackccc | jack@123.com |
	+---------+--------------+
	1 row in set (0.00 sec)

	mysql> select * from payroll;	#同步删除 此时子表中已经没有alice的记录
	Empty set (0.00 sec)

	mysql> insert into payroll(name,payroll) values('jack',20000);
		#若子表试图插入父表中没有定义的记录 会报错
	ERROR 1452 (23000): Cannot add or update a child row: a foreign key constraint fails 
	(`company`.`payroll`, CONSTRAINT `payroll_ibfk_1` FOREIGN KEY (`name`) REFERENCES `employee` 
	(`name`) ON DELETE CASCADE ON UPDATE CASCADE)
	mysql> 

	
-------------------------------------------------	
修改表alter table：
语法：
	1.修改表名
		alter table 表名 rename 新表名;
	
	2.增加字段
		alter table 表名 add 字段名 数据类型 [完整的约束条件...]；
		alter table 表名 add 字段名 数据类型 [完整的约束条件...] first;
		alter table 表名 add 字段名 数据类型 [完整的约束条件...] after 字段名;
		
	3.删除字段
		alter table 表名 drop 字段名;
	
	4.修改字段
		alter table 表名 modify 字段名 数据类型 [完整的约束条件...];
		alter table 表名 change 旧字段名 新字段名 旧数据类型 [完整的约束条件...];
		alter table 表名 change 旧字段名 新字段名 新数据类型 [完整的约束条件...];

	注意：
		1.使用alter修改字段类型时  若想保留原有字段的约束条件 在修改时应该加上这些约束条件 	
			但是若字段为主键 则在修改字段属性时 不需要再加上primary key 关键字

		 2.当字段有自增约束时  想要删除字段的主键属性 则要先修改自增属性 再删除主键属性
			因为自增属性是依赖与主键属性 若直接删除主键属性则会报错

--------------------
mysql> desc s1;
+-------+---------------+------+-----+---------+----------------+
| Field | Type          | Null | Key | Default | Extra          |
+-------+---------------+------+-----+---------+----------------+
| id    | int(11)       | NO   | PRI | NULL    | auto_increment |
| name  | varchar(50)   | NO   |     | NULL    |                |
| sex   | enum('m','f') | NO   |     | f       |                |
| age   | int(11)       | NO   |     | 18      |                |
+-------+---------------+------+-----+---------+----------------+
4 rows in set (0.00 sec)

mysql> alter table s1 
	-> add hobby set('music','book','film','chatting') default 'music,film';
Query OK, 0 rows affected (0.35 sec)
Records: 0  Duplicates: 0  Warnings: 0

mysql> alter table s1  
    -> add stu_num int not null after name, 
	-> add birth year default 1998 first;
Query OK, 0 rows affected (0.20 sec)
Records: 0  Duplicates: 0  Warnings: 0

mysql> desc s1;
+---------+---------------------------------------+------+-----+------------+----------------+
| Field   | Type                                  | Null | Key | Default    | Extra          |
+---------+---------------------------------------+------+-----+------------+----------------+
| birth   | year(4)                               | YES  |     | 1998       |                |
| id      | int(11)                               | NO   | PRI | NULL       | auto_increment |
| name    | varchar(50)                           | NO   |     | NULL       |                |
| stu_num | int(11)                               | NO   |     | NULL       |                |
| sex     | enum('m','f')                         | NO   |     | f          |                |
| age     | int(11)                               | NO   |     | 18         |                |
| hobby   | set('music','book','film','chatting') | YES  |     | music,film |                |
+---------+---------------------------------------+------+-----+------------+----------------+
7 rows in set (0.00 sec)


------------------------------------------
添加约束：针对已有的主键增加auto_increment
	mysql> desc student2;
	+-----------+-------------+------+-----+---------+-------+
	| Field     | Type        | Null | Key | Default | Extra |
	+-----------+-------------+------+-----+---------+-------+
	| id        | int(11)     | YES  |     | NULL    |       |
	| name      | varchar(50) | YES  |     | NULL    |       |
	| born_year | year(4)     | YES  |     | NULL    |       |
	| bir       | date        | YES  |     | NULL    |       |
	| reg_time  | datetime    | YES  |     | NULL    |       |
	+-----------+-------------+------+-----+---------+-------+
	5 rows in set (0.00 sec)

	mysql> alter table student2 
		-> add primary key(id);		#添加主键
	Query OK, 0 rows affected (0.32 sec)
	Records: 0  Duplicates: 0  Warnings: 0

	mysql> desc student2;
	+-----------+-------------+------+-----+---------+-------+
	| Field     | Type        | Null | Key | Default | Extra |
	+-----------+-------------+------+-----+---------+-------+
	| id        | int(11)     | NO   | PRI | NULL    |       |
	| name      | varchar(50) | YES  |     | NULL    |       |
	| born_year | year(4)     | YES  |     | NULL    |       |
	| bir       | date        | YES  |     | NULL    |       |
	| reg_time  | datetime    | YES  |     | NULL    |       |
	+-----------+-------------+------+-----+---------+-------+
	5 rows in set (0.01 sec)

	mysql> alter table student2 
		-> modify id int not null primary key auto_increment;	#错误 该字段已经是主键了
	ERROR 1068 (42000): Multiple primary key defined
	mysql> alter table student2
		-> modify 
		id int auto_increment;	
		#为已有主键属性的字段添加自增属性不需要重新定义主键属性
	Query OK, 1 row affected (0.20 sec)
	Records: 1  Duplicates: 0  Warnings: 0

	mysql> desc student2;
	+-----------+-------------+------+-----+---------+----------------+
	| Field     | Type        | Null | Key | Default | Extra          |
	+-----------+-------------+------+-----+---------+----------------+
	| id        | int(11)     | NO   | PRI | NULL    | auto_increment |
	| name      | varchar(50) | YES  |     | NULL    |                |
	| born_year | year(4)     | YES  |     | NULL    |                |
	| bir       | date        | YES  |     | NULL    |                |
	| reg_time  | datetime    | YES  |     | NULL    |                |
	+-----------+-------------+------+-----+---------+----------------+
	5 rows in set (0.06 sec)

	mysql> 

------------
添加复合主键：
	mysql> desc student;
	+-------+-----------------------------------+------+-----+---------+-------+
	| Field | Type                              | Null | Key | Default | Extra |
	+-------+-----------------------------------+------+-----+---------+-------+
	| name  | varchar(50)                       | YES  |     | NULL    |       |
	| sex   | enum('m','f')                     | YES  |     | NULL    |       |
	| hobby | set('music','book','game','film') | YES  |     | NULL    |       |
	+-------+-----------------------------------+------+-----+---------+-------+
	3 rows in set (0.00 sec)

	mysql> alter table student
		-> add primary key(name,sex);
	Query OK, 0 rows affected (0.58 sec)
	Records: 0  Duplicates: 0  Warnings: 0

	mysql> desc student;
	+-------+-----------------------------------+------+-----+---------+-------+
	| Field | Type                              | Null | Key | Default | Extra |
	+-------+-----------------------------------+------+-----+---------+-------+
	| name  | varchar(50)                       | NO   | PRI | NULL    |       |
	| sex   | enum('m','f')                     | NO   | PRI | NULL    |       |
	| hobby | set('music','book','game','film') | YES  |     | NULL    |       |
	+-------+-----------------------------------+------+-----+---------+-------+
	3 rows in set (0.00 sec)

	mysql> 

	
--------------------
添加主键和自动增长：
	mysql> alter table test1 modify int_test int primary key auto_increment;
	Query OK, 1 row affected (0.54 sec)
	Records: 1  Duplicates: 0  Warnings: 0

---------
删除主键：
	若主键有自增属性 则要先删除自增属性 再删除主键属性
	mysql> desc test1;
	+-----------+------------+------+-----+---------+----------------+
	| Field     | Type       | Null | Key | Default | Extra          |
	+-----------+------------+------+-----+---------+----------------+
	| tiny_test | tinyint(4) | YES  |     | NULL    |                |
	| int_test  | int(11)    | NO   | PRI | NULL    | auto_increment |
	+-----------+------------+------+-----+---------+----------------+
	2 rows in set (0.00 sec)

	mysql> alter table test1
		-> modify int_test int not null;	#修改字段时 若不带上原有的属性 则默认消除
	Query OK, 1 row affected (0.42 sec)
	Records: 1  Duplicates: 0  Warnings: 0

	mysql> desc test1;
	+-----------+------------+------+-----+---------+-------+
	| Field     | Type       | Null | Key | Default | Extra |
	+-----------+------------+------+-----+---------+-------+
	| tiny_test | tinyint(4) | YES  |     | NULL    |       |
	| int_test  | int(11)    | NO   | PRI | NULL    |       |
	+-----------+------------+------+-----+---------+-------+
	2 rows in set (0.00 sec)

	mysql> alter table test1
		-> drop primary key;	#删除主键
	Query OK, 1 row affected (0.28 sec)
	Records: 1  Duplicates: 0  Warnings: 0

	mysql> desc test1;
	+-----------+------------+------+-----+---------+-------+
	| Field     | Type       | Null | Key | Default | Extra |
	+-----------+------------+------+-----+---------+-------+
	| tiny_test | tinyint(4) | YES  |     | NULL    |       |
	| int_test  | int(11)    | NO   |     | NULL    |       |
	+-----------+------------+------+-----+---------+-------+
	2 rows in set (0.00 sec)

	mysql> 


-----------------------------------------------
复制表：
	1.复制表的结构和记录：key不会复制：主键、外键和索引都不会复制
	mysql> desc s1;
	+---------+---------------------------------------+------+-----+------------+----------------+
	| Field   | Type                                  | Null | Key | Default    | Extra          |
	+---------+---------------------------------------+------+-----+------------+----------------+
	| birth   | year(4)                               | YES  |     | 1998       |                |
	| id      | int(11)                               | NO   | PRI | NULL       | auto_increment |
	| name    | varchar(50)                           | NO   |     | NULL       |                |
	| stu_num | int(11)                               | NO   |     | NULL       |                |
	| sex     | enum('m','f')                         | NO   |     | f          |                |
	| age     | int(11)                               | NO   |     | 18         |                |
	| hobby   | set('music','book','film','chatting') | YES  |     | music,film |                |
	+---------+---------------------------------------+------+-----+------------+----------------+
	7 rows in set (0.00 sec)

	mysql> create table s1_copy select * from s1;
	Query OK, 3 rows affected (0.15 sec)
	Records: 3  Duplicates: 0  Warnings: 0

	mysql> desc s1_copy;
	+---------+---------------------------------------+------+-----+------------+-------+
	| Field   | Type                                  | Null | Key | Default    | Extra |
	+---------+---------------------------------------+------+-----+------------+-------+
	| birth   | year(4)                               | YES  |     | 1998       |       |
	| id      | int(11)                               | NO   |     | 0          |       |
	| name    | varchar(50)                           | NO   |     | NULL       |       |
	| stu_num | int(11)                               | NO   |     | NULL       |       |
	| sex     | enum('m','f')                         | NO   |     | f          |       |
	| age     | int(11)                               | NO   |     | 18         |       |
	| hobby   | set('music','book','film','chatting') | YES  |     | music,film |       |
	+---------+---------------------------------------+------+-----+------------+-------+
	7 rows in set (0.00 sec)

	mysql> 

	2.只复制表的结构：
		mysql>create table t1_copy2 select * from t1 where 1=2;
			#当where后的条件为假时 则查询不到数据 故新创建的复制表中也就不会有原表中的数据 
			#而仅有原表中的结构
	
	3.复制表的结构，包括key也一同复制
		mysql> desc s4;
		+-------+------------------------------------+------+-----+---------+----------------+
		| Field | Type                               | Null | Key | Default | Extra          |
		+-------+------------------------------------+------+-----+---------+----------------+
		| id    | int(11)                            | NO   | PRI | NULL    | auto_increment |
		| name  | varchar(50)                        | NO   |     | NULL    |                |
		| sex   | enum('m','f')                      | NO   |     | m       |                |
		| age   | int(10) unsigned                   | NO   |     | 20      |                |
		| bobby | set('music','disc','dance','book') | YES  |     | music   |                |
		+-------+------------------------------------+------+-----+---------+----------------+
		5 rows in set (0.00 sec)

		mysql> create table s4_copy like s4;
		Query OK, 0 rows affected (0.07 sec)

		mysql> desc s4_copy;
		+-------+------------------------------------+------+-----+---------+----------------+
		| Field | Type                               | Null | Key | Default | Extra          |
		+-------+------------------------------------+------+-----+---------+----------------+
		| id    | int(11)                            | NO   | PRI | NULL    | auto_increment |
		| name  | varchar(50)                        | NO   |     | NULL    |                |
		| sex   | enum('m','f')                      | NO   |     | m       |                |
		| age   | int(10) unsigned                   | NO   |     | 20      |                |
		| bobby | set('music','disc','dance','book') | YES  |     | music   |                |
		+-------+------------------------------------+------+-----+---------+----------------+
		5 rows in set (0.05 sec)

		mysql> 

-------
删除表：
	drop table 表名;


--------------------------------------------------------------
mysql数据操作：DML
在mysql管理软件中，可以通过sql语句中的DML语言来实现数据的操作，包括使用insert实现数据的插入、delete
实现数据的删除以及update实现数据的更新。

一、插入数据insert
	1.顺序插入：插入完整数据
		insert into 表名 values(值1,值2,值3,...);	#值的个数与字段一一对应
	
	2.指定字段插入数据：
		insert into 表名(字段2,字段3,...) values(值2,值3,...);
		
	3.插入多条记录:
		insert into 表名 values(值1,值2,值3,...),(值1,值2,值3,...),(值1,值2,值3,...);
		同时插入三条记录
		
	4.插入查询结果：
		insert into 表1(字段2,字段3,...) select (字段2,字段3,...) from 表2 where ...;
	如：
		mysql> create table s4_new select * from s4 where 1=2;
		Query OK, 0 rows affected (0.17 sec)
		Records: 0  Duplicates: 0  Warnings: 0

		mysql> select * from s4_new;
		Empty set (0.00 sec)

		mysql> insert into s4_new select * from s4 where sex='m';
		Query OK, 3 rows affected (0.00 sec)
		Records: 3  Duplicates: 0  Warnings: 0

		mysql> select * from s4;
		+----+------+-----+-----+-------+
		| id | name | sex | age | bobby |
		+----+------+-----+-----+-------+
		|  1 | tom  | m   |  18 | book  |
		|  2 | lim  | m   |  20 | music |
		|  3 | jj   | f   |  50 | NULL  |
		|  4 | lal  | m   |  20 | music |
		+----+------+-----+-----+-------+
		4 rows in set (0.00 sec)

		mysql> select * from s4_new;
		+----+------+-----+-----+-------+
		| id | name | sex | age | bobby |
		+----+------+-----+-----+-------+
		|  1 | tom  | m   |  18 | book  |
		|  2 | lim  | m   |  20 | music |
		|  4 | lal  | m   |  20 | music |
		+----+------+-----+-----+-------+
		3 rows in set (0.00 sec)

		mysql> 


二、更新数据updata
	语法：
	updata 表名 set 字段1=值1,字段2=值2, where condition;
	如：
	mysql> update s4_new set name='lala',id=3 where name='lal';
	Query OK, 1 row affected (0.01 sec)
	Rows matched: 1  Changed: 1  Warnings: 0

	mysql> select * from s4_new;
	+----+------+-----+-----+-------+
	| id | name | sex | age | bobby |
	+----+------+-----+-----+-------+
	|  1 | tom  | m   |  18 | book  |
	|  2 | lim  | m   |  20 | music |
	|  3 | lala | m   |  20 | music |
	+----+------+-----+-----+-------+
	3 rows in set (0.00 sec)

	mysql> 



应用：使用updata修改数据库用户的密码：
mysql> desc mysql.user;
+------------------------+-----------------------------------+------+-----+-----------------------+-------+
| Field                  | Type                              | Null | Key | Default               | Extra |
+------------------------+-----------------------------------+------+-----+-----------------------+-------+
| Host                   | char(60)                          | NO   | PRI |                       |       |
| User                   | char(32)                          | NO   | PRI |                       |       |
| Select_priv            | enum('N','Y')                     | NO   |     | N                     |       |
| Insert_priv            | enum('N','Y')                     | NO   |     | N                     |       |
| Update_priv            | enum('N','Y')                     | NO   |     | N                     |       |
| Delete_priv            | enum('N','Y')                     | NO   |     | N                     |       |
| Create_priv            | enum('N','Y')                     | NO   |     | N                     |       |
| Drop_priv              | enum('N','Y')                     | NO   |     | N                     |       |
| Reload_priv            | enum('N','Y')                     | NO   |     | N                     |       |
| Shutdown_priv          | enum('N','Y')                     | NO   |     | N                     |       |
| Process_priv           | enum('N','Y')                     | NO   |     | N                     |       |
| File_priv              | enum('N','Y')                     | NO   |     | N                     |       |
| Grant_priv             | enum('N','Y')                     | NO   |     | N                     |       |
| References_priv        | enum('N','Y')                     | NO   |     | N                     |       |
| Index_priv             | enum('N','Y')                     | NO   |     | N                     |       |
| Alter_priv             | enum('N','Y')                     | NO   |     | N                     |       |
| Show_db_priv           | enum('N','Y')                     | NO   |     | N                     |       |
| Super_priv             | enum('N','Y')                     | NO   |     | N                     |       |
| Create_tmp_table_priv  | enum('N','Y')                     | NO   |     | N                     |       |
| Lock_tables_priv       | enum('N','Y')                     | NO   |     | N                     |       |
| Execute_priv           | enum('N','Y')                     | NO   |     | N                     |       |
| Repl_slave_priv        | enum('N','Y')                     | NO   |     | N                     |       |
| Repl_client_priv       | enum('N','Y')                     | NO   |     | N                     |       |
| Create_view_priv       | enum('N','Y')                     | NO   |     | N                     |       |
| Show_view_priv         | enum('N','Y')                     | NO   |     | N                     |       |
| Create_routine_priv    | enum('N','Y')                     | NO   |     | N                     |       |
| Alter_routine_priv     | enum('N','Y')                     | NO   |     | N                     |       |
| Create_user_priv       | enum('N','Y')                     | NO   |     | N                     |       |
| Event_priv             | enum('N','Y')                     | NO   |     | N                     |       |
| Trigger_priv           | enum('N','Y')                     | NO   |     | N                     |       |
| Create_tablespace_priv | enum('N','Y')                     | NO   |     | N                     |       |
| ssl_type               | enum('','ANY','X509','SPECIFIED') | NO   |     |                       |       |
| ssl_cipher             | blob                              | NO   |     | NULL                  |       |
| x509_issuer            | blob                              | NO   |     | NULL                  |       |
| x509_subject           | blob                              | NO   |     | NULL                  |       |
| max_questions          | int(11) unsigned                  | NO   |     | 0                     |       |
| max_updates            | int(11) unsigned                  | NO   |     | 0                     |       |
| max_connections        | int(11) unsigned                  | NO   |     | 0                     |       |
| max_user_connections   | int(11) unsigned                  | NO   |     | 0                     |       |
| plugin                 | char(64)                          | NO   |     | mysql_native_password |       |
| authentication_string  | text                              | YES  |     | NULL                  |       |
| password_expired       | enum('N','Y')                     | NO   |     | N                     |       |
| password_last_changed  | timestamp                         | YES  |     | NULL                  |       |
| password_lifetime      | smallint(5) unsigned              | YES  |     | NULL                  |       |
| account_locked         | enum('N','Y')                     | NO   |     | N                     |       |
+------------------------+-----------------------------------+------+-----+-----------------------+-------+
45 rows in set (0.11 sec)

mysql> select user,host,authentication_string from mysql.user;
+---------------+-----------+-------------------------------------------+
| user          | host      | authentication_string                     |
+---------------+-----------+-------------------------------------------+
| root          | localhost | *817544858CEB203ADB1DC95D8C048AD37C2B3AD4 |
| mysql.session | localhost | *THISISNOTAVALIDPASSWORDTHATCANBEUSEDHERE |
| mysql.sys     | localhost | *THISISNOTAVALIDPASSWORDTHATCANBEUSEDHERE |
+---------------+-----------+-------------------------------------------+
3 rows in set (0.02 sec)

mysql> update mysql.user set authentication_string=password('LIMforever@530') 
	-> where user='root' and host='localhost';
Query OK, 1 row affected, 1 warning (0.17 sec)
Rows matched: 1  Changed: 1  Warnings: 1

mysql> flush privileges;		#刷新授权表
Query OK, 0 rows affected (0.16 sec)

mysql> 


三、删除数据
	语法：
		delete from 表名 where condition;
	如：
		mysql> delete from mysql.user where authentication_string='';
		Query OK, 0 rows affected (0.00 sec)
		
		mysql> select * from s4;
		+----+------+-----+-----+-------+
		| id | name | sex | age | bobby |
		+----+------+-----+-----+-------+
		|  1 | tom  | m   |  18 | book  |
		|  2 | lim  | m   |  20 | music |
		|  3 | jj   | f   |  50 | NULL  |
		|  4 | lal  | m   |  20 | music |
		+----+------+-----+-----+-------+
		4 rows in set (0.00 sec)

		mysql> delete from s4 where bobby is null;
		Query OK, 1 row affected (0.09 sec)

		mysql> select * from s4;
		+----+------+-----+-----+-------+
		| id | name | sex | age | bobby |
		+----+------+-----+-----+-------+
		|  1 | tom  | m   |  18 | book  |
		|  2 | lim  | m   |  20 | music |
		|  4 | lal  | m   |  20 | music |
		+----+------+-----+-----+-------+
		3 rows in set (0.00 sec)

		mysql> 


delete与drop的区别：
	drop是DDL语句 是针对表的语句 而delet是DML语句 它是针对表中的数据
	在删除表中数据时通常是有条件的 没有条件则数据会全部删除




-----------------------------------------------------
MySQL数据库单表查询select：DQL语句
	简单查询
	通过条件查询
	查询排序
	限制查询记录数
	使用集合函数查询
	分组查询
	使用正则表达式查询

	一、简单查询
		select * from 表名;
		select 字段1,字段2,... from 表名;

	二、去重查询：distinct 注意：distinct去重方法仅能用于一个字段
		select distinct 字段名 from 表名;
		去重后记录数量查询：
		select count(distinct(字段名)) from 表名;

		如：
			mysql> select post from employee5;
			+------+
			| post |
			+------+
			| ins  |
			| ins  |
			| ins  |
			| ins  |
			| hr   |
			| hr   |
			+------+
			6 rows in set (0.00 sec)

			mysql> select distinct post from employee5;
			+------+
			| post |
			+------+
			| ins  |
			| hr   |
			+------+
			2 rows in set (0.00 sec)

			mysql> 

	三、通过四则运算查询：
		select name,salary,salary*14 from employee5;
		select name,salary,salary*14 as annul_salary from employee5; #为新增加的字段取别名
		select name,salary,salary*14 annul_salary from employee5;  	 #取别名的另一种方法
			显示别名可以用as 也可以用空格将原名和别名分开
		如：
			mysql> select name,salary,salary*14 from employee5;
			+-------+---------+-----------+
			| name  | salary  | salary*14 |
			+-------+---------+-----------+
			| jack  | 5000.00 |  70000.00 |
			| tom   | 5500.00 |  77000.00 |
			| robin | 8000.00 | 112000.00 |
			| alice | 7200.00 | 100800.00 |
			| ty    |  600.00 |   8400.00 |
			| harry | 6000.00 |  84000.00 |
			+-------+---------+-----------+
			6 rows in set (0.01 sec)

			mysql> select name,salary,salary*14 as annual_salary from employee5;
			+-------+---------+---------------+
			| name  | salary  | annual_salary |
			+-------+---------+---------------+
			| jack  | 5000.00 |      70000.00 |
			| tom   | 5500.00 |      77000.00 |
			| robin | 8000.00 |     112000.00 |
			| alice | 7200.00 |     100800.00 |
			| ty    |  600.00 |       8400.00 |
			| harry | 6000.00 |      84000.00 |
			+-------+---------+---------------+
			6 rows in set (0.00 sec)

			mysql> select name,salary,salary*14 annual_salary from employee5;
			+-------+---------+---------------+
			| name  | salary  | annual_salary |
			+-------+---------+---------------+
			| jack  | 5000.00 |      70000.00 |
			| tom   | 5500.00 |      77000.00 |
			| robin | 8000.00 |     112000.00 |
			| alice | 7200.00 |     100800.00 |
			| ty    |  600.00 |       8400.00 |
			| harry | 6000.00 |      84000.00 |
			+-------+---------+---------------+
			6 rows in set (0.00 sec)


	四、定义显示格式
		concat()函数用于连接字符串
		mysql> select concat(name,' annual salary:',salary*14) as annual_salary from employee5;
		+-------------------------------+
		| annual_salary                 |
		+-------------------------------+
		| jack annual salary:70000.00   |
		| tom annual salary:77000.00    |
		| robin annual salary:112000.00 |
		| alice annual salary:100800.00 |
		| ty annual salary:8400.00      |
		| harry annual salary:84000.00  |
		+-------------------------------+
		6 rows in set (0.00 sec)

			#()小括号中用单引号‘’引起来的并不是字段 而是要在屏幕上显示的语句 
			#将concat函数连接起来的字符串作为一个整体的字段并取别名为annual_salary
		
	五、条件查询
		1.单条件查询
			select name,post from employee5 where post='hr';
			
		2.多条件查询
			mysql> select name,salary,post from employee5 where post='hr'and salary>2000;
			+-------+---------+------+
			| name  | salary  | post |
			+-------+---------+------+
			| harry | 6000.00 | hr   |
			+-------+---------+------+
			1 row in set (0.00 sec)


		
		3.关键字between...and查询
			mysql> select name,salary from employee5 where salary between 2000 and 12000;
			+-------+---------+
			| name  | salary  |
			+-------+---------+
			| jack  | 5000.00 |
			| tom   | 5500.00 |
			| robin | 8000.00 |
			| alice | 7200.00 |
			| harry | 6000.00 |
			+-------+---------+
			5 rows in set (0.06 sec)

			mysql> select name,salary from employee5 where salary not between 6000 and 12000;
			+------+---------+
			| name | salary  |
			+------+---------+
			| jack | 5000.00 |
			| tom  | 5500.00 |
			| ty   |  600.00 |
			+------+---------+
			3 rows in set (0.00 sec)

		
		4.关键字is null
			mysql> select name,salary,jobdesc from employee5 where jobdesc is null;
			Empty set (0.00 sec)

			mysql> select name,salary,jobdesc from employee5 where jobdesc is not null;
			+-------+---------+---------+
			| name  | salary  | jobdesc |
			+-------+---------+---------+
			| jack  | 5000.00 | teach   |
			| tom   | 5500.00 | teach   |
			| robin | 8000.00 | teach   |
			| alice | 7200.00 | teach   |
			| ty    |  600.00 | hrrr    |
			| harry | 6000.00 | hrrr    |
			+-------+---------+---------+
			6 rows in set (0.00 sec)

			mysql> 

		5.关键字in集合查询：
			mysql> select name,salary from employee5 
				-> where salary=4000 or salary=5000 or salary=6000 or salary=9000;
			+-------+---------+
			| name  | salary  |
			+-------+---------+
			| jack  | 5000.00 |
			| harry | 6000.00 |
			+-------+---------+
			2 rows in set (0.00 sec)

			mysql> select name,salary from employee5  where salary in (4000,5000,6000,9000);
			+-------+---------+
			| name  | salary  |
			+-------+---------+
			| jack  | 5000.00 |
			| harry | 6000.00 |
			+-------+---------+
			2 rows in set (0.00 sec)
			
			mysql> select name,salary from employee5  where salary not in (4000,5000,6000,9000);
			+-------+---------+
			| name  | salary  |
			+-------+---------+
			| tom   | 5500.00 |
			| robin | 8000.00 |
			| alice | 7200.00 |
			| ty    |  600.00 |
			+-------+---------+
			4 rows in set (0.00 sec)

			mysql> 


			
		6.关键字like模糊查询：
			mysql> select * from employee5 where name like 'al%';
			#这里的%是通配符 相当于 *  即任意多个字符 
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| 16 | alice | f   | 2018-02-02 | ins  | teach   | 7200.00 |    501 |    100 |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			1 row in set (0.19 sec)

			mysql> select * from employee5 where name like 'al___';
			#而_表任意一个字符 有几个下划线就表示有几个字符
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| 16 | alice | f   | 2018-02-02 | ins  | teach   | 7200.00 |    501 |    100 |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			1 row in set (0.00 sec)

			mysql> 

			
	六、查询排序
		order by 按照某一列排序 默认是升序ASC 若想要降序 则加上desc
		1.按单列排序
			select * from 表名 order by 字段名; #默认为升序
			select 字段1,字段2,... from 表名 order by 某一字段 asc;  	#按某一个字段的升序排列
			select 字段1,字段2,... from 表名 order by 某一字段 desc;	#按某一个字段的降序排列
		如：
			mysql> select * from employee5 where post='ins' order by salary;
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| 13 | jack  | m   | 2018-02-06 | ins  | teach   | 5000.00 |    501 |    100 |
			| 14 | tom   | m   | 2018-02-03 | ins  | teach   | 5500.00 |    501 |    100 |
			| 16 | alice | f   | 2018-02-02 | ins  | teach   | 7200.00 |    501 |    100 |
			| 15 | robin | m   | 2018-02-02 | ins  | teach   | 8000.00 |    501 |    101 |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			4 rows in set (0.10 sec)

			mysql> select * from employee5 where post='ins' order by salary desc;
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| 15 | robin | m   | 2018-02-02 | ins  | teach   | 8000.00 |    501 |    101 |
			| 16 | alice | f   | 2018-02-02 | ins  | teach   | 7200.00 |    501 |    100 |
			| 14 | tom   | m   | 2018-02-03 | ins  | teach   | 5500.00 |    501 |    100 |
			| 13 | jack  | m   | 2018-02-06 | ins  | teach   | 5000.00 |    501 |    100 |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			4 rows in set (0.00 sec)


		
		2.按多列排序
			select * from 表名 order by 字段1 desc,字段2 asc;
			
			若为多列排序 观察排序是否正确时  后写列的排序是建立在前面写的列的排序中
				
		如：
			先按职位的升序排序，在职位相同的记录中再将薪水按降序排序
			mysql> select * from employee5 order by post,salary desc;
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			| 18 | harry | m   | 2018-02-02 | hr   | hrrr    | 6000.00 |    502 |    101 |
			| 17 | ty    | m   | 2018-02-02 | hr   | hrrr    |  600.00 |    502 |    101 |
			| 15 | robin | m   | 2018-02-02 | ins  | teach   | 8000.00 |    501 |    101 |
			| 16 | alice | f   | 2018-02-02 | ins  | teach   | 7200.00 |    501 |    100 |
			| 14 | tom   | m   | 2018-02-03 | ins  | teach   | 5500.00 |    501 |    100 |
			| 13 | jack  | m   | 2018-02-06 | ins  | teach   | 5000.00 |    501 |    100 |
			+----+-------+-----+------------+------+---------+---------+--------+--------+
			6 rows in set (0.05 sec)

			mysql> 

	七、限制查询的记录数：
		limit M,N #m表示从第几条开始取 N表示取几条 注意M是从0开始的 也即0表示第一条
		
		mysql> select * from employee5 order by post,salary desc;
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| 18 | harry | m   | 2018-02-02 | hr   | hrrr    | 6000.00 |    502 |    101 |
		| 17 | ty    | m   | 2018-02-02 | hr   | hrrr    |  600.00 |    502 |    101 |
		| 15 | robin | m   | 2018-02-02 | ins  | teach   | 8000.00 |    501 |    101 |
		| 16 | alice | f   | 2018-02-02 | ins  | teach   | 7200.00 |    501 |    100 |
		| 14 | tom   | m   | 2018-02-03 | ins  | teach   | 5500.00 |    501 |    100 |
		| 13 | jack  | m   | 2018-02-06 | ins  | teach   | 5000.00 |    501 |    100 |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		6 rows in set (0.00 sec)
		
		mysql> select * from employee5 order by post,salary desc limit 5;	#默认初始位置为0
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| 18 | harry | m   | 2018-02-02 | hr   | hrrr    | 6000.00 |    502 |    101 |
		| 17 | ty    | m   | 2018-02-02 | hr   | hrrr    |  600.00 |    502 |    101 |
		| 15 | robin | m   | 2018-02-02 | ins  | teach   | 8000.00 |    501 |    101 |
		| 16 | alice | f   | 2018-02-02 | ins  | teach   | 7200.00 |    501 |    100 |
		| 14 | tom   | m   | 2018-02-03 | ins  | teach   | 5500.00 |    501 |    100 |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		5 rows in set (0.00 sec)

		mysql> select * from employee5 order by post,salary desc limit 2,2;
			#从第3条开始显示 共显示2条
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| 15 | robin | m   | 2018-02-02 | ins  | teach   | 8000.00 |    501 |    101 |
		| 16 | alice | f   | 2018-02-02 | ins  | teach   | 7200.00 |    501 |    100 |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		2 rows in set (0.00 sec)

		mysql> 
	 
			
		
	八、使用结合函数查询
		mysql> select count(8) from employee5;	#count()函数获得表中有多少条记录 也即返回表中的行数
		+----------+
		| count(8) |
		+----------+
		|        6 |
		+----------+
		1 row in set (0.02 sec)

		mysql> select count(8) from employee5 where dep_id=101;
			#查询employee5表中dep_id=101的有几条	count(8)不管小括号中的数字是几 都表示count(*)
		+----------+
		| count(8) |
		+----------+
		|        3 |
		+----------+
		1 row in set (0.00 sec)

		mysql> select max(salary) from employee5;
		+-------------+
		| max(salary) |
		+-------------+
		|     8000.00 |
		+-------------+
		1 row in set (0.00 sec)


		mysql> select min(salary) from employee5;
		+-------------+
		| min(salary) |
		+-------------+
		|      600.00 |
		+-------------+
		1 row in set (0.05 sec)

		mysql> select avg(salary) from employee5;
		+-------------+
		| avg(salary) |
		+-------------+
		| 5383.333333 |
		+-------------+
		1 row in set (0.00 sec)

		mysql> select avg(salary) from employee5 where post='ins';
		+-------------+
		| avg(salary) |
		+-------------+
		| 6425.000000 |
		+-------------+
		1 row in set (0.00 sec)

		mysql> select avg(salary) from employee5 where post='hr';
		+-------------+
		| avg(salary) |
		+-------------+
		| 3300.000000 |
		+-------------+
		1 row in set (0.00 sec)

		mysql> select sum(salary) from employee5 where dep_id=101;
		+-------------+
		| sum(salary) |
		+-------------+
		|    14600.00 |
		+-------------+
		1 row in set (0.00 sec)

		mysql> 

		mysql> select name,sex,hire_data,post,salary,dep_id from employee5 
		-> where salary=(select max(salary) from employee5);
			#where 后面加() 则()里的语句就是子查询语句
			#这条语句就相当于 查询employee5表中薪水最高的那个人的记录
		+-------+-----+------------+------+---------+--------+
		| name  | sex | hire_data  | post | salary  | dep_id |
		+-------+-----+------------+------+---------+--------+
		| robin | m   | 2018-02-02 | ins  | 8000.00 |    101 |
		+-------+-----+------------+------+---------+--------+
		1 row in set (0.11 sec)

		mysql> 


	九、分组查询
		1.group by 和 group_concat()函数一起使用：
			mysql> select dep_id,group_concat(name) from employee5 group by dep_id;
				#将部门id相同的记录按名字拼接在一起
			+--------+--------------------+
			| dep_id | group_concat(name) |
			+--------+--------------------+
			|    100 | jack,tom,alice     |
			|    101 | robin,ty,harry     |
			+--------+--------------------+
			2 rows in set (0.20 sec)

			mysql> select dep_id,group_concat(name)as emp_meb from employee5 group by dep_id;
			+--------+----------------+
			| dep_id | emp_meb        |
			+--------+----------------+
			|    100 | jack,tom,alice |
			|    101 | robin,ty,harry |
			+--------+----------------+
			2 rows in set (0.00 sec)

			mysql> 
		
		2.group by和集合函数一起使用：
			mysql> select dep_id,sum(salary) from employee5 group by dep_id;
			+--------+-------------+
			| dep_id | sum(salary) |
			+--------+-------------+
			|    100 |    17700.00 |
			|    101 |    14600.00 |
			+--------+-------------+
			2 rows in set (0.00 sec)

			mysql> select dep_id,avg(salary) from employee5 group by dep_id;
			+--------+-------------+
			| dep_id | avg(salary) |
			+--------+-------------+
			|    100 | 5900.000000 |
			|    101 | 4866.666667 |
			+--------+-------------+
			2 rows in set (0.00 sec)

			mysql> 


	十、使用正则表达式查询：
		regexp表示使用正则表达式 ^al 表示以al开头 yun$表示以yun结尾
		m{2} 表示m出现2次的

		mysql> select * from employee5 where name regexp '^ali';
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| 16 | alice | f   | 2018-02-02 | ins  | teach   | 7200.00 |    501 |    100 |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		1 row in set (0.10 sec)

		mysql> select * from employee5 where name regexp 'y$';
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| 17 | ty    | m   | 2018-02-02 | hr   | hrrr    |  600.00 |    502 |    101 |
		| 18 | harry | m   | 2018-02-02 | hr   | hrrr    | 6000.00 |    502 |    101 |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		2 rows in set (0.00 sec)

		mysql> select * from employee5 where name regexp 'r{2}';
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		| 18 | harry | m   | 2018-02-02 | hr   | hrrr    | 6000.00 |    502 |    101 |
		+----+-------+-----+------------+------+---------+---------+--------+--------+
		1 row in set (0.00 sec)

		mysql> 

	小结：对字符串匹配的方式
		where name='tom';
		where name like 'to%';	这里的%是通配符 相当于 *  即任意多个字符
		where name regexp 'yun$'
		

		
		
-------------------------------------------------------
MySQL多表查询DQL：
	多表连接查询
	复合条件连接查询
	子查询
	
	一、多表的连接查询
	交叉连接：		生成笛卡尔积，它不使用任何匹配条件
	内连接：		只连接匹配的行		#最重要的一种连接！！！
	外连接；
		左连接：	会显示左边表内所有的值，无论匹不匹配右边表中的查询条件
		右连接：	会显示右边表内所有的值，无论匹不匹配左边表中的查询条件

	1.交叉连接查询：没有太多的实际意义
		mysql> select employee6.emp_name,employee6.age,employee6.dept_id, 
			-> department6.dept_name from employee6,department6;
				#相当于在linux终端中执行[root@zlim opt]# touch {a..b}{1..3}
										[root@zlim opt]# ls
										a1  a2  a3  b1  b2  b3  dump_sdb1  nginx.conf.backup  py
										[root@zlim opt]# 
		+----------+------+---------+-----------+
		| emp_name | age  | dept_id | dept_name |
		+----------+------+---------+-----------+
		| tianyun  |   19 |     200 | hr        |
		| tianyun  |   19 |     200 | it        |
		| tianyun  |   19 |     200 | sale      |
		| tianyun  |   19 |     200 | fd        |
		| tom      |   26 |     201 | hr        |
		| tom      |   26 |     201 | it        |
		| tom      |   26 |     201 | sale      |
		| tom      |   26 |     201 | fd        |
		| jack     |   30 |     201 | hr        |
		| jack     |   30 |     201 | it        |
		| jack     |   30 |     201 | sale      |
		| jack     |   30 |     201 | fd        |
		| alice    |   24 |     200 | hr        |
		| alice    |   24 |     200 | it        |
		| alice    |   24 |     200 | sale      |
		| alice    |   24 |     200 | fd        |
		| robin    |   40 |     200 | hr        |
		| robin    |   40 |     200 | it        |
		| robin    |   40 |     200 | sale      |
		| robin    |   40 |     200 | fd        |
		| natasha  |   28 |     204 | hr        |
		| natasha  |   28 |     204 | it        |
		| natasha  |   28 |     204 | sale      |
		| natasha  |   28 |     204 | fd        |
		+----------+------+---------+-----------+
		24 rows in set (0.00 sec)

		mysql> 

	2.内连接查询：
		select 字段名（若字段名是为唯一标识的 则不用在前面写上表名.的前缀） from 表1,表2 where
		两张表之间的联系纽带 即我们要联系的那个字段 内连接只会匹配那些连接的行
		mysql> select employee6.emp_name,employee6.age,employee6.dept_id, 
			-> department6.dept_name from employee6,department6
			-> where employee6.dept_id = department6.dept_id;
		+----------+------+---------+-----------+
		| emp_name | age  | dept_id | dept_name |
		+----------+------+---------+-----------+
		| tianyun  |   19 |     200 | hr        |
		| tom      |   26 |     201 | it        |
		| jack     |   30 |     201 | it        |
		| alice    |   24 |     200 | hr        |
		| robin    |   40 |     200 | hr        |
		+----------+------+---------+-----------+
		5 rows in set (0.05 sec)

		mysql> 
		
		mysql> select emp_id,emp_name,age,dept_name from employee6,department6
			-> where employee6.dept_id = department6.dept_id;
				#可以不写表employee6的前缀 只要字段名在数据库中是唯一标识的就行
		+--------+----------+------+-----------+
		| emp_id | emp_name | age  | dept_name |
		+--------+----------+------+-----------+
		|      1 | tianyun  |   19 | hr        |
		|      2 | tom      |   26 | it        |
		|      3 | jack     |   30 | it        |
		|      4 | alice    |   24 | hr        |
		|      5 | robin    |   40 | hr        |
		+--------+----------+------+-----------+
		5 rows in set (0.00 sec)

		#以内连接的方式查询employee6表和department6表，并且以age字段的升序方式显示
		mysql> select emp_id ,emp_name,age,dept_name
			-> from employee6,department6
			-> where employee6.dept_id = department6.dept_id
			-> order by age;
		+--------+----------+------+-----------+
		| emp_id | emp_name | age  | dept_name |
		+--------+----------+------+-----------+
		|      1 | tianyun  |   19 | hr        |
		|      4 | alice    |   24 | hr        |
		|      2 | tom      |   26 | it        |
		|      3 | jack     |   30 | it        |
		|      5 | robin    |   40 | hr        |
		+--------+----------+------+-----------+
		5 rows in set (0.03 sec)

		mysql> 
 


	
	3.外连接查询：
		select 字段列表	from 表1 left 或 right join 表2 on 表1.字段=表2.字段
		
		mysql> select * from employee6;
		+--------+----------+------+---------+
		| emp_id | emp_name | age  | dept_id |
		+--------+----------+------+---------+
		|      1 | tianyun  |   19 |     200 |
		|      2 | tom      |   26 |     201 |
		|      3 | jack     |   30 |     201 |
		|      4 | alice    |   24 |     200 |
		|      5 | robin    |   40 |     200 |
		|      6 | natasha  |   28 |     204 |
		+--------+----------+------+---------+
		6 rows in set (0.00 sec)
		
		mysql> select * from department6;
		+---------+-----------+
		| dept_id | dept_name |
		+---------+-----------+
		|     200 | hr        |
		|     201 | it        |
		|     202 | sale      |
		|     203 | fd        |
		+---------+-----------+
		4 rows in set (0.01 sec)

		mysql> select emp_id,emp_name,dept_name from employee6 left join department6 on 
			-> employee6.dept_id = department6.dept_id;
			#找出所有员工以及所属部门，包括没有部门的员工 
			#虽然natasha的dept_id在department6表中并没有相同的 但是因为左连接了employee6表
			#所有employee6表中的所有值都会显示
		+--------+----------+-----------+
		| emp_id | emp_name | dept_name |
		+--------+----------+-----------+
		|      1 | tianyun  | hr        |
		|      4 | alice    | hr        |
		|      5 | robin    | hr        |
		|      2 | tom      | it        |
		|      3 | jack     | it        |
		|      6 | natasha  | NULL      |
		+--------+----------+-----------+
		6 rows in set (0.22 sec)

		
		#右连接	：以department6表为右连接 故会找出所有部门包含的员工 包括空部门
		mysql> select emp_id,emp_name,dept_name from employee6 right join department6
			-> on employee6.dept_id = department6.dept_id;
		+--------+----------+-----------+
		| emp_id | emp_name | dept_name |
		+--------+----------+-----------+
		|      1 | tianyun  | hr        |
		|      2 | tom      | it        |
		|      3 | jack     | it        |
		|      4 | alice    | hr        |
		|      5 | robin    | hr        |
		|   NULL | NULL     | sale      |
		|   NULL | NULL     | fd        |
		+--------+----------+-----------+
		7 rows in set (0.00 sec)

		mysql> 

	4.复合条件查询：
		#以内连接的方式查询employee6和department6表 并且employee6表中的age字段值必须大于25
		mysql> select emp_id,emp_name,age,dept_name
			-> from employee6,department6
			-> where employee6.dept_id = department6.dept_id
			-> and age > 25;
			+--------+----------+------+-----------+
			| emp_id | emp_name | age  | dept_name |
			+--------+----------+------+-----------+
			|      5 | robin    |   40 | hr        |
			|      2 | tom      |   26 | it        |
			|      3 | jack     |   30 | it        |
			+--------+----------+------+-----------+
			3 rows in set (0.00 sec)

			mysql> 

	5.子查询：
		子查询是将一个查询语句嵌套在另一个查询语句中
		子查询的结果会作为外查询的依据
		即内层查询语句的结果 可以作为外层查询语句的查询条件
		子查询中可以包含：in、not in、any、all、exist和not exist等关键字，还可以包含比较运算符：=、
		!=、>、<等
		
		1.带in关键字的子查询
		mysql> select * from employee6 where dept_id in 
			-> (select dept_id from department6);
		+--------+----------+------+---------+
		| emp_id | emp_name | age  | dept_id |
		+--------+----------+------+---------+
		|      1 | tianyun  |   19 |     200 |
		|      2 | tom      |   26 |     201 |
		|      3 | jack     |   30 |     201 |
		|      4 | alice    |   24 |     200 |
		|      5 | robin    |   40 |     200 |
		+--------+----------+------+---------+
		5 rows in set (0.35 sec)
		
		2.带比较运算符的子查询：
		mysql> select dept_id,dept_name from department6
			-> where dept_id in
			-> (select distinct dept_id from employee6 where age >= 25);
			+---------+-----------+
			| dept_id | dept_name |
			+---------+-----------+
			|     201 | it        |
			|     200 | hr        |
			+---------+-----------+
			2 rows in set (0.00 sec)

			mysql> select distinct dept_id from employee6 where age >= 25;
			+---------+
			| dept_id |
			+---------+
			|     201 |
			|     200 |
			|     204 |
			+---------+
			3 rows in set (0.00 sec)

			mysql> select dept_id from employee6 where age >= 25;
			+---------+
			| dept_id |
			+---------+
			|     201 |
			|     201 |
			|     200 |
			|     204 |
			+---------+
			4 rows in set (0.00 sec)
		
		3.带exist关键字的子查询
			exist关键字表示存在。在使用exist关键字时，内层查询语句不会返回查询结果，而是返回一个
			真假值：即True或False。
			当返回True时，外层查询语句将进行查询；当返回值为False时，外层查询语句将不再进行查询。
			mysql> select * from employee6 
				-> where exists (select * from department6 where dept_id = 203);
				// select * from department6 where dept_id = 203
				//为true 故接着执行外查询语句 与即：
				//select * from employee6；
			+--------+----------+------+---------+
			| emp_id | emp_name | age  | dept_id |
			+--------+----------+------+---------+
			|      1 | tianyun  |   19 |     200 |
			|      2 | tom      |   26 |     201 |
			|      3 | jack     |   30 |     201 |
			|      4 | alice    |   24 |     200 |
			|      5 | robin    |   40 |     200 |
			|      6 | natasha  |   28 |     204 |
			+--------+----------+------+---------+
			6 rows in set (0.00 sec)

			mysql> 




		
		
		
	

	
	
MySQL索引
===================================================================
创建索引
	创建表时创建索引
	create在不存在的表上创建索引
	alter tabla在已存在的表上创建索引
查看并测试索引
删除索引

一、索引简介
索引在MySQL中也叫作‘键’，是存储引擎用于快速找到记录的一种数据结构。索引对于良好的性能非常关键，
尤其是当表中的数据量越来越大时，索引对于性能的影响愈发重要。
索引优化应该是对查询性能优化最有效的手段。索引能够轻易将查询性能提高好几个数量级。
索引的增加和删除对数据是没有影响的 但是会对数据的大量插入和查询有影响。

二、索引的分类：	
	普通索引：允许重复
	唯一索引：unique key 不允许重复数据
	全文索引：fulltext
	
	mysql> create table t2(id int,name varchar(20));
	Query OK, 0 rows affected (0.63 sec)

	mysql> desc t2;
	+-------+-------------+------+-----+---------+-------+
	| Field | Type        | Null | Key | Default | Extra |
	+-------+-------------+------+-----+---------+-------+
	| id    | int(11)     | YES  |     | NULL    |       |
	| name  | varchar(20) | YES  |     | NULL    |       |
	+-------+-------------+------+-----+---------+-------+
	2 rows in set (0.17 sec)
	
三、有无索引查询差别：

	#创建存储过程：
	#在创建存储过程当中（类似于创建函数） 要用分号表示换行 而分号在mysql中是结束语句去执行语句的意思
	#所以在开始我们重新定义一个mysql中的结束执行标志
	#\d = delimiter 改变结束符
	
	mysql> \d $$
	mysql> create procedure autoinsert_t2()
		-> begin
		-> declare i int default 1;			#declare声明一个变量 定义为整型 默认值为1
		-> while(i<20000)do
		->    insert into t2 values(i,'cc');
		->    set i = i + 1;
		-> end while;
		-> end$$
	Query OK, 0 rows affected (0.67 sec)

	mysql> \d ;	 #存储过程书写完毕后再次将结束符换回分号 ; 
	
	#查看存储过程创建的基本信息	 ：	show create procedure 存储过程名字\G 
	mysql> show create procedure autoinsert_t2\G
	*************************** 1. row ***************************
			   Procedure: autoinsert_t2
				sql_mode: ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,
				ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION
		Create Procedure: CREATE DEFINER=`root`@`localhost` PROCEDURE `autoinsert_t2`()
	begin
	declare i int default 1;
	while(i<20000)do
	   insert into t2 values(i,'cc');
	   set i = i + 1;
	end while;
	end
	character_set_client: utf8
	collation_connection: utf8_general_ci
	  Database Collation: latin1_swedish_ci
	1 row in set (0.00 sec)
	
	#调用存储过程  ： call 存储过程名(); 
	mysql> call autoinsert_t2();	#调用autoinsert_t2的存储过程 自动往t2表中插入19999条数据
	
	mysql> select * from t2 where id= 19999;	#未创建索引时的查询时间0.04
	+-------+------+
	| id    | name |
	+-------+------+
	| 19999 | cc   |
	+-------+------+
	1 row in set (0.04 sec)

	mysql> create index index_id on t2(id);		#创建索引
	Query OK, 0 rows affected (0.18 sec)
	Records: 0  Duplicates: 0  Warnings: 0

	mysql> select * from t2 where id= 19999;	#创建索引后的查询时间：0.00
			#使用索引 ：where条件一定是拥有索引的那一列(字段)
	+-------+------+
	| id    | name |
	+-------+------+
	| 19999 | cc   |
	+-------+------+
	1 row in set (0.00 sec)

	mysql> explain select * from t2 where id= 19999\G
			#explain：查看查询优化器是如何决定执行查询的 而并没有真正的查询
	*************************** 1. row ***************************
			   id: 1
	  select_type: SIMPLE
			table: t2
	   partitions: NULL
			 type: ref
	possible_keys: index_id
			  key: index_id
		  key_len: 5
			  ref: const
			 rows: 1
		 filtered: 100.00
			Extra: NULL
	1 row in set, 1 warning (0.00 sec)

	mysql> 


四、创建索引
1.创建表时创建索引：
语法：
	create table 表名(
		字段名1 数据类型 [完整性约束条件...],
		字段名2 数据类型 [完整性约束条件...],
		[unique | fulltext | spatial] index | key
		[索引名] (字段名[(长度)]) [asc | desc]
		);
	注：
		索引类型：unique唯一索引 fulltext全文索引  是可选项
		创建索引的关键字：index 或者 key  是必写项
		索引名（很少使用索引名字）	以及对哪些字段进行索引	索引是升序还是降序
		不起索引名 则默认是索引列的字段名  
		不加索引类型则默认是普通索引 即这一列的值不是唯一的 可以重复
		如果是unique index或是 unique key  则表示唯一索引  它的值是不可以重复的
		

2.在已存在的表上创建索引：
语法：
	create [unique | fulltext | spatial] index | key 索引名（此时索引名不可以省略）
		on 表名 (字段名[(长度)]) [asc | desc];
	
	还可以使用alter table 表名 add的方式添加索引：只需将上述的create 换成 alter table 表名 add即可
	例如：
		创建普通索引：
			create index index_dept_name on department (dept_name);
			
		创建唯一索引：
			create unique index index_dept_name on department (dept_name);
		
		创建全文索引：
			create fulltext index index_dept_name on department (dept_name);
	
		创建多列索引：
			create index index_comment_name on department (dept_name,comment);
	
	mysql> create table department2(
		-> id int ,
		-> name varchar(50) unique key ,
		-> comment varchar(100)
		-> );
	Query OK, 0 rows affected (0.23 sec)

	mysql> create index comment_index on department2 (comment);
	Query OK, 0 rows affected (0.05 sec)
	Records: 0  Duplicates: 0  Warnings: 0

	mysql> desc department2;
	+---------+--------------+------+-----+---------+-------+
	| Field   | Type         | Null | Key | Default | Extra |
	+---------+--------------+------+-----+---------+-------+
	| id      | int(11)      | YES  |     | NULL    |       |
	| name    | varchar(50)  | YES  | UNI | NULL    |       |
	| comment | varchar(100) | YES  | MUL | NULL    |       |
	+---------+--------------+------+-----+---------+-------+
	3 rows in set (0.05 sec)

	mysql> 


五、管理索引
查看索引：show create table 表名\G
测试查询过程：explain select * from department where dept_name='hr';
删除索引：
	show create table 表名\G  #先查看索引的名称
	drop index 索引名 on 表名; #再删除索引
	
	
	
	
	

	
	
MySQL视图VIEW
=========================================================
一、视图简介
	MySQL视图是一个虚拟表，其内容由查询定义。同真实的表一样，视图包含一系列带有名称的列和行记录。但是
视图并不是在数据库中以存储的数据值集形式存在。行和列数据来自由定义视图时的查询所引用的表的某些数据
结果，并且在引用视图时动态生成，即引用表发生变化，查询结果发生了变化，则视图也会相应的发生变化。
对于所引用的基础表而言，MySQL视图的作用相当于筛选。
	定义视图的筛选可以来自当前或其他的数据库中的一张或多张表，甚至是其他视图。通过视图进行查询没有任何
限制，通过它们进行数据修改时的限制也很少。（但很少会通过视图对基础表进行修改） 
	即视图由实际表的查询结果构成，视图的背后可以是一张表的查询 也可以是N张表的查询。
	视图是存储在数据库的SQL查询语句 使用它主要出于两种原因：安全原意，视图可以隐藏一些数据，如：一些
敏感的信息，另一个原因是可以使复杂的查询简易化
 	

二、创建视图
语法：
	create view 视图名 as select 语句;

	例1：创建单表的视图
	mysql> create view user_view
		-> as select user,host,authentication_string from mysql.user;
	Query OK, 0 rows affected (0.69 sec)

	mysql> select * from user_view;
	+---------------+-----------+-------------------------------------------+
	| user          | host      | authentication_string                     |
	+---------------+-----------+-------------------------------------------+
	| root          | localhost | *E0C730C64A09DEA39435CA899E42BD7EDBB6FD68 |
	| mysql.session | localhost | *THISISNOTAVALIDPASSWORDTHATCANBEUSEDHERE |
	| mysql.sys     | localhost | *THISISNOTAVALIDPASSWORDTHATCANBEUSEDHERE |
	+---------------+-----------+-------------------------------------------+
	3 rows in set (0.10 sec)

	mysql> 

	例2：创建多表的视图
		
		mysql> create database shop;
		Query OK, 1 row affected (0.18 sec)

		mysql> use shop;
		Database changed
		mysql> show tables;
		Empty set (0.00 sec)

		mysql> create table product(
			-> id int unsigned primary key auto_increment,
			-> name varchar(60) not null,
			-> price double not null
			-> );
		Query OK, 0 rows affected (0.30 sec)

		mysql> insert into product(name,price) values
			-> ('pear',4.2),
			-> ('orange',6.5),
			-> ('apple',5.0)
			-> ;
		Query OK, 3 rows affected (0.04 sec)
		Records: 3  Duplicates: 0  Warnings: 0

		mysql> create table purchase(
			-> id int unsigned primary key auto_increment,
			-> name varchar(60) not null,
			-> quantity int not null default 0,
			-> gen_time datetime not null
			-> );
		Query OK, 0 rows affected (0.12 sec)

		mysql> insert into purchase(name,quantity,gen_time) values
			-> ('apple',7,now()),
			-> ('pear',10,now())
			-> ;
		Query OK, 2 rows affected (0.04 sec)
		Records: 2  Duplicates: 0  Warnings: 0

		mysql> create view purchase_detail			#根据两张表创建视图
			-> as select product.name,product.price,purchase.quantity,
			-> product.price * purchase.quantity as total_valu
			-> from product,purchase
			-> where product.name = purchase.name;
		Query OK, 0 rows affected (0.07 sec)

		mysql> select * from purchase_detail;
		+-------+-------+----------+------------+
		| name  | price | quantity | total_valu |
		+-------+-------+----------+------------+
		| pear  |   4.2 |       10 |         42 |
		| apple |     5 |        7 |         35 |
		+-------+-------+----------+------------+
		2 rows in set (0.01 sec)

		mysql> insert into purchase(name,quantity,gen_time) values	#当purchase表插入新值时
			-> ('orange',20,now());
		Query OK, 1 row affected (0.00 sec)

		mysql> select * from purchase_detail;     					#视图purchase_detail也会随之变化                                                                                                        +--------+-------+----------+------------+
		| name   | price | quantity | total_valu |
		+--------+-------+----------+------------+
		| apple  |     5 |        7 |         35 |
		| pear   |   4.2 |       10 |         42 |
		| orange |   6.5 |       20 |        130 |
		+--------+-------+----------+------------+
		3 rows in set (0.00 sec)

		mysql> 


三、查看视图
	1.show table; 	
		查看视图名
	
	2.show table status;	
		查看当前数据库中视图以及所有表详细信息
		如：show table status from mysql\G	
			#以\G的形式查看mysql数据库中的所有表
		
	3.show create view	视图名;		
		查看视图创建信息
		如：show create view view_user\G
	
	4.desc 视图名;		
		查看视图结构
		如：desc view_user;
	
四、修改视图
	1.删除后新创建			#通常都是删除后再创建
		drop view 视图名;
		create view 视图名
			as select 语句;
	
	2.alter修改视图
		alter view 视图名 as select 语句;
		如：alter view view_user as select user,password from mysql.user;

五、通过视图操作基表	
	1.查询数据
		select * from 视图名;
	2.更新数据 updata
	3.删除数据 delete

六、删除视图
	语法：
		drop view view_name;
	如：
		mysql> drop view purchase_detail;
		Query OK, 0 rows affected (0.00 sec)


	
	
	
	
	
	

MySQL触发器Triggers
=========================================================================================	
一、触发器简介
	触发器（trigger）是一个特殊的存储过程，它的执行不是由程序调用，也不是手工启动，而是由DML语句形成
的事件触发，比如对一个表进行insert、delete、updata等操作时就会激活触发器而执行定义好的语句。
	触发器经常用于加强数据的完整性约束和业务规则等。

	
二、创建trigger
	语法;
		create trigger 触发器名称 before | after 触发事件
		on 表名 for each row
		begin
			触发器程序体;
		end
		
	说明：
		触发器名称		：	最多64个字符，它和mysql中其他对象的命名方式一样
		before | after	：	触发器的时机，通常是在触发事件之后
		触发事件		：	即insert 或 updata 或 delete等操作事件
		on 表名			：	标识建立触发器的表名，即在哪张表上建立触发器
		for each row	：  表明触发器的执行间隔：for each row 即每变化一行就执行一次动作，而不是
							对整个表执行一次 只是对每一行
		触发器程序体	：	要触发的SQL语句：可以用顺序、判断、循环等语句实现一般程序需要的逻辑功能
	
	如：
		mysql> create table student1(
			-> id int unsigned primary key auto_increment,
			-> name varchar(50)
			-> );
		Query OK, 0 rows affected (0.10 sec)

		mysql> create table stu_total(total int);
		Query OK, 0 rows affected (0.09 sec)

		mysql> insert into stu_total values(0);		
				#stu_total里面一定要有初始值 不然后面创建trigger会错误									
		Query OK, 1 row affected (0.01 sec)

		mysql> select * from stu_total;
		+-------+
		| total |
		+-------+
		|     0 |
		+-------+
		1 row in set (0.00 sec)

	
		mysql> \d $$
				#创建触发器时同样要使用分号作为常规换行符 所以我们使用\d来更换mysql中的结束执行符号
		
		mysql> create trigger stu_delete_trigger after delete
			-> on student1 for each row
			-> begin
			->     update stu_total set total = total -1;
			-> end$$
		Query OK, 0 rows affected (0.05 sec)
		
		mysql> create trigger stu_insert_trigger after insert
			-> on student1 for each row
			-> begin
			->     update stu_total set total = total+1;
			-> end$$
		Query OK, 0 rows affected (0.10 sec)

		mysql> \d ;
		
		mysql> show triggers\G		#查看触发器
		*************************** 1. row ***************************
					 Trigger: stu_insert_trigger
					   Event: INSERT
					   Table: student1
				   Statement: begin
			update stu_total set total = total+1;
		end
					  Timing: AFTER
					 Created: 2019-08-11 18:50:55.10
					sql_mode: ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,
							  NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,
							  NO_ENGINE_SUBSTITUTION
					 Definer: root@localhost
		character_set_client: utf8
		collation_connection: utf8_general_ci
		  Database Collation: latin1_swedish_ci
		*************************** 2. row ***************************
					 Trigger: stu_delete_trigger
					   Event: DELETE
					   Table: student1
				   Statement: begin
			update stu_total set total = total -1;
		end
					  Timing: AFTER
					 Created: 2019-08-11 18:49:05.98
					sql_mode: ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,
							  NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_AUTO_CREATE_USER,
							  NO_ENGINE_SUBSTITUTION
					 Definer: root@localhost
		character_set_client: utf8
		collation_connection: utf8_general_ci
		  Database Collation: latin1_swedish_ci
		2 rows in set (0.02 sec)

		mysql> insert into student1(name) values('jack'),('alice'),('tom'),('natasha');
		Query OK, 4 rows affected (0.12 sec)
		Records: 4  Duplicates: 0  Warnings: 0

		mysql> select * from stu_total;
		+-------+
		| total |
		+-------+
		|     4 |
		+-------+
		1 row in set (0.00 sec)

		mysql> delete from student1 where name = 'tom';

		Query OK, 1 row affected (1.95 sec)

		mysql> select * from stu_total;
		+-------+
		| total |
		+-------+
		|     3 |
		+-------+
		1 row in set (0.01 sec)

		mysql> delete from student1;
		Query OK, 3 rows affected (0.06 sec)

		mysql> select * from stu_total;
		+-------+
		| total |
		+-------+
		|     0 |
		+-------+
		1 row in set (0.00 sec)

		mysql> 


三、查看触发器
	1.show triggers\G  
		查看所有触发器的详细信息
	
	2.通过系统表triggers查看：
		use	information_schema
		select * from triggers\G
		select * from triggers where trigger_name='触发器名称'\G
		

四、删除触发器
	语法：
		drop trigger 触发器名称;
	
		
五、触发器案列：
	mysql> create table tab1(
		-> id int primary key auto_increment,
		-> name varchar(50),
		-> sex enum('m','f'),
		-> age int
		-> );
	Query OK, 0 rows affected (0.19 sec)

	mysql> create table tab2(
		-> id int primary key auto_increment,
		-> name varchar(50),
		-> salary double(10,2)
		-> );
	Query OK, 0 rows affected (0.16 sec)

	mysql> \d $$
	
	#创建触发器tab1_delete_trigger
	mysql> create trigger tab1_delete_trigger after delete on tab1
		-> for each row
		-> begin
		->     delete from tab2 where name = old.name;
		-> end$$
	Query OK, 0 rows affected (0.06 sec)

	
	#创建触发器tab1_update_trigger	
	mysql> create trigger tab1_update_trigger after update on tab1
		-> for each row
		-> begin
		->     update tab2 set name=new.name where name=old.name;
		-> end$$
	Query OK, 0 rows affected (0.04 sec)

	
	#创建触发器tab1_insert_trigger
	mysql> create trigger tab1_insert_trigger after insert on tab1
		-> for each row
		-> begin
		->      insert into tab2(name,salary) values(new.name,5000);
		-> end$$
	Query OK, 0 rows affected (0.01 sec)

	mysql> \d ;
	
	mysql> insert into tab1(name,sex,age) values('tom','m',18);
	Query OK, 1 row affected (0.07 sec)

	mysql> select * from tab1;
	+----+------+------+------+
	| id | name | sex  | age  |
	+----+------+------+------+
	|  1 | tom  | m    |   18 |
	+----+------+------+------+
	1 row in set (0.05 sec)

	mysql> select * from tab2;
	+----+------+---------+
	| id | name | salary  |
	+----+------+---------+
	|  1 | tom  | 5000.00 |
	+----+------+---------+
	1 row in set (0.00 sec)

	
	mysql> insert into tab1(name,sex,age) values('alice','f',21);
	Query OK, 1 row affected (0.05 sec)

	mysql> update tab1 set name= 'alicecc' where name = 'alice';
	Query OK, 1 row affected (0.08 sec)
	Rows matched: 1  Changed: 1  Warnings: 0

	mysql> select * from tab2;		#tab2会同步更新
	+----+---------+---------+
	| id | name    | salary  |
	+----+---------+---------+
	|  1 | tom     | 5000.00 |
	|  2 | alicecc | 5000.00 |
	+----+---------+---------+
	2 rows in set (0.00 sec)

	mysql> delete from tab1 where name = 'alicecc';
	Query OK, 1 row affected (0.01 sec)

	mysql> select * from tab2;		#tab2会同步删除
	+----+------+---------+
	| id | name | salary  |
	+----+------+---------+
	|  1 | tom  | 5000.00 |
	+----+------+---------+
	1 row in set (0.00 sec)

	mysql> insert into tab1(name,sex,age) values('tom','m',19);
	Query OK, 1 row affected (0.05 sec)

	mysql> insert into tab1(name,sex,age) values('tom','m',20);
	Query OK, 1 row affected (0.04 sec)

	mysql> insert into tab1(name,sex,age) values('tom','m',21);
	Query OK, 1 row affected (0.04 sec)

	mysql> insert into tab1(name,sex,age) values('tom','m',22);
	Query OK, 1 row affected (0.00 sec)

	mysql> insert into tab1(name,sex,age) values('tom','m',23);
	Query OK, 1 row affected (0.00 sec)

	mysql> insert into tab1(name,sex,age) values('tom','m',23);
	Query OK, 1 row affected (0.03 sec)

	mysql> insert into tab1(name,sex,age) values('tom','m',23);
	Query OK, 1 row affected (0.01 sec)

	mysql> select * from tab1;
	+----+------+------+------+
	| id | name | sex  | age  |
	+----+------+------+------+
	|  1 | tom  | m    |   18 |
	|  3 | tom  | m    |   19 |
	|  4 | tom  | m    |   20 |
	|  5 | tom  | m    |   21 |
	|  6 | tom  | m    |   22 |
	|  7 | tom  | m    |   23 |
	|  8 | tom  | m    |   23 |
	|  9 | tom  | m    |   23 |
	+----+------+------+------+
	8 rows in set (0.00 sec)

	mysql> delete from tab1 where id = 8;	#tab1按照主键id删除了一条记录
	Query OK, 1 row affected (0.20 sec)

	mysql> select * from tab1;				#可见tab1表中的记录是正确的
	+----+------+------+------+
	| id | name | sex  | age  |
	+----+------+------+------+
	|  1 | tom  | m    |   18 |
	|  3 | tom  | m    |   19 |
	|  4 | tom  | m    |   20 |
	|  5 | tom  | m    |   21 |
	|  6 | tom  | m    |   22 |
	|  7 | tom  | m    |   23 |
	|  9 | tom  | m    |   23 |
	+----+------+------+------+
	7 rows in set (0.00 sec)

	mysql> select * from tab2;		#但因为创建tab1_delete_trigger时没有通过主键删除 所以tab2表中
									#的所有数据均被删除 因为name是可以重复的 但主键一定是不重复的
	Empty set (0.00 sec)

	mysql> 

说明：无论是删除还是更新 where条件一定要根据主键！！！

	故上述案例中delete、update触发器的创建均是有误的
	删除触发器一定要根据主键删除 因为只有主键是唯一的 若删除不根据主键 
	则可能在表1中删除根据主键仅删除了一条记录 而触发器触发的表中可能删除了多条记录
	
	修改：
		删除触发器：
			create trigger tab1_delete_trigger after delete on tab1
			for each row
		    begin
		        delete from tab2 where id = old.id;		#删除必须通过主键primary key！！！
		    end$$
		
		更新触发器：更新的话 则一定要将全部所有两张表相同的字段更新
	
		若被触发表中有多个字段与触发表中的字段相同，则设置更新触发器时所有全部相同的字段都应该更新：
		如：
		mysql> create table tab111 select * from tab1;
		Query OK, 7 rows affected (0.20 sec)
		Records: 7  Duplicates: 0  Warnings: 0

		mysql> \d $$
		
		mysql> create trigger tab111_update_trigger after update on tab1
			-> for each row
			-> begin
			->     update tab111 set id=new.id,name=new.name,sex=new.sex,age=new.age
			->     where id = old.id;
			-> end$$
		
		mysql> \d ;
		
		mysql> update tab1 set id=11 where id = 5;
		Query OK, 1 row affected (0.03 sec)
		Rows matched: 1  Changed: 1  Warnings: 0

		mysql> select * from tab1;
		+----+-------+------+------+
		| id | name  | sex  | age  |
		+----+-------+------+------+
		|  1 | tom   | m    |   18 |
		|  3 | tom   | m    |   19 |
		|  4 | tom   | m    |   20 |
		|  6 | tom   | m    |   22 |
		|  7 | tom   | m    |   23 |
		|  8 | alice | f    |   22 |
		| 11 | tom   | m    |   21 |
		+----+-------+------+------+
		7 rows in set (0.00 sec)

		mysql> select * from tab111;
		+----+------+------+------+
		| id | name | sex  | age  |
		+----+------+------+------+
		|  1 | tom  | m    |   18 |
		|  3 | tom  | m    |   19 |
		|  4 | tom  | m    |   20 |
		| 11 | tom  | m    |   21 |
		|  6 | tom  | m    |   22 |
		|  7 | tom  | m    |   23 |
		|  9 | tom  | m    |   23 |
		+----+------+------+------+
		7 rows in set (0.00 sec)

		mysql> 

	
关于old和new：
	delete from tab1 where name='yy';	
		手动删除tab1表中的数据
	delete from tab2 where name=old.name;	
		触发器触发删除tab2表中的数据 此时删除的数据对于tab1表的name来说就是旧的数据值
		
	insert into tab1(name) values('tom');
		手动向tab1表中插入数据
	insert into tab2(name) values(new.name);
		触发器触发插入tab2表中的数据 此时插入的值对于tab1表中的name而言是新的数据
		
	update tab1 set name='cc111' where name='cc';
		手动更改tab1中name为cc的name为cc111 此时cc就是old值 cc111就是new值
	update tab2 set name=new.name where name=old.name;
		触发器触发更新tab2表中的数据
		

	创建触发器时的old和new始终是针对tab1表而言的  但触发是tab2表
	old.name ：旧值 指的是被删除或被更新之前的那个值 在被触发的表中执行相应的操作  
	new.name ：新值 指的是新插入的值 在被触发的表中执行相应的插入操作 
	
	old 和 new都是相对于tab1而言的 即都是针对手动执行操作而非被动触发操作的表
		
	
	
		
		
		
	


	
MySQL存储过程与函数
====================================================================================
一、概述
	存储过程和函数都是事先经过编译并存储在数据库中的一段SQL语句的集合。
	存储过程和函数的区别：
		函数必须要有返回值，而存储过程没有。
		存储过程的参数可以是in、out或是input，而函数的参数类型只能是in，且不用标明，而存储过程一定
	要标明参数类型。
	
	优点：
		存储过程只在创建时进行编译；而SQL语句每执行一次就会编译一次，所以利用存储过程可以提高数据库
	执行速度；
		简化复杂操作，结合事务一起封装；
		复用性好，安全性高，可以指定存储过程的使用权。
	说明：
		并发量少的情况下，很少使用存储过程；
		并发量高的情况下，为了提高效率，用存储过程比较多。

		
二、创建与调用
语法;
	create procedure sp_name(参数列表)
		[特性...] 过程体
	
	存储过程的参数形式：([参数类型] 参数变量名 参数变量数据类型)
		其中参数类型有in、out、inout
		in 		输入参数 			只能输入
		out		输出参数			只能输出
		inout	输入输出参数		即可以输入也可以输出
		若不设置参数类型 则默认为none类型 即没有参数类型
	
	\d $$
	create procedure 过程名(形式参数列表)
	begin
		过程体：sql语句
	end$$
	\d ;
	
	调用过程：
		call 过程名(实参列表);
		
		
存储过程三种参数类型：in、out、inout
===================================none===========================	
	mysql> \d $$
	
	mysql> create procedure p1()	#没有参数类型 也即没有形参
		-> begin
		->     select count(*) from mysql.user;
		-> end$$
	Query OK, 0 rows affected (0.41 sec)

	mysql> \d ;
	
	mysql> call p1();
	+----------+
	| count(*) |
	+----------+
	|        3 |
	+----------+
	1 row in set (0.00 sec)

	Query OK, 0 rows affected (0.00 sec)

	mysql> 
	
	
	
===================================in===========================
	mysql> create procedure autoinsert2(in a int)	#autoinsert2这个过程是在company库中创建的          
		-> begin
		->     declare i int default 1;
		->     while(i<=a)do
		->     insert into school.t2 values(i,md5(i));	#md5()一个函数 得到一个128位的哈希值
		->     set i=i+1;
		->     end while;
		-> end$$
	Query OK, 0 rows affected (0.00 sec)

	mysql> desc t2;
	+-------+--------------+------+-----+---------+-------+
	| Field | Type         | Null | Key | Default | Extra |
	+-------+--------------+------+-----+---------+-------+
	| id    | int(11)      | YES  | MUL | NULL    |       |
	| name  | varchar(128) | YES  |     | NULL    |       |
	+-------+--------------+------+-----+---------+-------+
	2 rows in set (0.00 sec)

	mysql> call autoinsert2(20);
	Query OK, 1 row affected (0.05 sec)

	mysql> use school;
	Reading table information for completion of table and column names
	You can turn off this feature to get a quicker startup with -A

	Database changed
	
	mysql> select * from t2;	#但t2表却在school库中
	+------+----------------------------------+
	| id   | name                             |
	+------+----------------------------------+
	|    1 | c4ca4238a0b923820dcc509a6f75849b |
	|    2 | c81e728d9d4c2f636f067f89cc14862c |
	|    3 | eccbc87e4b5ce2fe28308fd9f2a7baf3 |
	|    4 | a87ff679a2f3e71d9181a67b7542122c |
	|    5 | e4da3b7fbbce2345d7772b0674a318d5 |
	|    6 | 1679091c5a880faf6fb5e6087eb1b2dc |
	|    7 | 8f14e45fceea167a5a36dedd4bea2543 |
	|    8 | c9f0f895fb98ab9159f51fd0297e236d |
	|    9 | 45c48cce2e2d7fbdea1afc51c7c6ad26 |
	|   10 | d3d9446802a44259755d38e6d163e820 |
	|   11 | 6512bd43d9caa6e02c990b0a82652dca |
	|   12 | c20ad4d76fe97759aa27a0c99bff6710 |
	|   13 | c51ce410c124a10e0db5e4b97fc2af39 |
	|   14 | aab3238922bcc25a6f606eb525ffdc56 |
	|   15 | 9bf31c7ff062936a96d3c8bd1f8f2ff3 |
	|   16 | c74d97b01eae257e44aa9d5bade97baf |
	|   17 | 70efdf2ec9b086079795c442636b55fb |
	|   18 | 6f4922f45568161a8cdf4ad2299f6d23 |
	|   19 | 1f0e3dad99908345f7439f8ffabdffc4 |
	|   20 | 98f13708210194c475687be6106a3b84 |
	+------+----------------------------------+
	20 rows in set (0.00 sec)

	mysql> select @num;				#使用select查看某一个变量的值
	+------+
	| @num |
	+------+
	| NULL |
	+------+
	1 row in set (0.07 sec)

	mysql> set @num=40;				#给局部变量赋值：set @变量名=数值;
	Query OK, 0 rows affected (0.00 sec)


	mysql> call company.autoinsert2(@num);	
		#因为此时在school库中 所以调用autoinsert2要使用绝对路径
		#调用过程时也使用变量则要使用@
	Query OK, 1 row affected (0.13 sec)

	mysql> select * from t2;
	+------+----------------------------------+
	| id   | name                             |
	+------+----------------------------------+
	|    1 | c4ca4238a0b923820dcc509a6f75849b |
	|    2 | c81e728d9d4c2f636f067f89cc14862c |
	|    3 | eccbc87e4b5ce2fe28308fd9f2a7baf3 |
	|    4 | a87ff679a2f3e71d9181a67b7542122c |
	|    5 | e4da3b7fbbce2345d7772b0674a318d5 |
	|    6 | 1679091c5a880faf6fb5e6087eb1b2dc |
	|    7 | 8f14e45fceea167a5a36dedd4bea2543 |
	|    8 | c9f0f895fb98ab9159f51fd0297e236d |
	|    9 | 45c48cce2e2d7fbdea1afc51c7c6ad26 |
	|   10 | d3d9446802a44259755d38e6d163e820 |
	|   11 | 6512bd43d9caa6e02c990b0a82652dca |
	|   12 | c20ad4d76fe97759aa27a0c99bff6710 |
	|   13 | c51ce410c124a10e0db5e4b97fc2af39 |
	|   14 | aab3238922bcc25a6f606eb525ffdc56 |
	|   15 | 9bf31c7ff062936a96d3c8bd1f8f2ff3 |
	|   16 | c74d97b01eae257e44aa9d5bade97baf |
	|   17 | 70efdf2ec9b086079795c442636b55fb |
	|   18 | 6f4922f45568161a8cdf4ad2299f6d23 |
	|   19 | 1f0e3dad99908345f7439f8ffabdffc4 |
	|   20 | 98f13708210194c475687be6106a3b84 |
	|    1 | c4ca4238a0b923820dcc509a6f75849b |
	|    2 | c81e728d9d4c2f636f067f89cc14862c |
	|    3 | eccbc87e4b5ce2fe28308fd9f2a7baf3 |
	|    4 | a87ff679a2f3e71d9181a67b7542122c |
	|    5 | e4da3b7fbbce2345d7772b0674a318d5 |
	|    6 | 1679091c5a880faf6fb5e6087eb1b2dc |
	|    7 | 8f14e45fceea167a5a36dedd4bea2543 |
	|    8 | c9f0f895fb98ab9159f51fd0297e236d |
	|    9 | 45c48cce2e2d7fbdea1afc51c7c6ad26 |
	|   10 | d3d9446802a44259755d38e6d163e820 |
	|   11 | 6512bd43d9caa6e02c990b0a82652dca |
	|   12 | c20ad4d76fe97759aa27a0c99bff6710 |
	|   13 | c51ce410c124a10e0db5e4b97fc2af39 |
	|   14 | aab3238922bcc25a6f606eb525ffdc56 |
	|   15 | 9bf31c7ff062936a96d3c8bd1f8f2ff3 |
	|   16 | c74d97b01eae257e44aa9d5bade97baf |
	|   17 | 70efdf2ec9b086079795c442636b55fb |
	|   18 | 6f4922f45568161a8cdf4ad2299f6d23 |
	|   19 | 1f0e3dad99908345f7439f8ffabdffc4 |
	|   20 | 98f13708210194c475687be6106a3b84 |
	|   21 | 3c59dc048e8850243be8079a5c74d079 |
	|   22 | b6d767d2f8ed5d21a44b0e5886680cb9 |
	|   23 | 37693cfc748049e45d87b8c7d8b9aacd |
	|   24 | 1ff1de774005f8da13f42943881c655f |
	|   25 | 8e296a067a37563370ded05f5a3bf3ec |
	|   26 | 4e732ced3463d06de0ca9a15b6153677 |
	|   27 | 02e74f10e0327ad868d138f2b4fdd6f0 |
	|   28 | 33e75ff09dd601bbe69f351039152189 |
	|   29 | 6ea9ab1baa0efb9e19094440c317e21b |
	|   30 | 34173cb38f07f89ddbebc2ac9128303f |
	|   31 | c16a5320fa475530d9583c34fd356ef5 |
	|   32 | 6364d3f0f495b6ab9dcf8d3b5c6e0b01 |
	|   33 | 182be0c5cdcd5072bb1864cdee4d3d6e |
	|   34 | e369853df766fa44e1ed0ff613f563bd |
	|   35 | 1c383cd30b7c298ab50293adfecb7b18 |
	|   36 | 19ca14e7ea6328a42e0eb13d585e4c22 |
	|   37 | a5bfc9e07964f8dddeb95fc584cd965d |
	|   38 | a5771bce93e200c36f7cd9dfd0e5deaa |
	|   39 | d67d8ab4f4c10bf22aa353e27879133c |
	|   40 | d645920e395fedad7bbbed0eca3fe2e0 |
	+------+----------------------------------+
	60 rows in set (0.00 sec)

	mysql> 

	
	
===================================out===========================
	mysql> \d $$
	mysql> create procedure p2(out param1 int)		#输出参数param1
		-> begin 
		->     select count(*) into param1 from mysql.user;
		-> end$$
	Query OK, 0 rows affected (0.02 sec)

	mysql> \d ;
	mysql> select @a;
	+------+
	| @a   |
	+------+
	| NULL |
	+------+
	1 row in set (0.00 sec)

	mysql> call p2(@a);
	Query OK, 1 row affected (0.01 sec)

	mysql> select @a;
	+------+
	| @a   |
	+------+
	|    3 |
	+------+
	1 row in set (0.00 sec)

	mysql> 

===================================in和out===========================	
	mysql> \d $$
	
	mysql> create procedure count_num(in p1 varchar(50),out p2 int)
		-> bgein
		->      select count(*) into p2 from employee5
		->     where post=p1;
		-> end$$

	mysql> \d ;
	
	mysql> call count_num('hr',@a);

	mysql> \d $$
	
	mysql> create procedure count_num2(in p1 varchar(50),in p2 float(10,2),out p3 int)
		-> bgein
		->   select count(*) into p3 from employee5 where post=p1 and salary>=p2;
		-> end$$

	mysql> call count_num2('hr',5000,@a);


===================================inout===========================
	mysql> create procedure param_inout(inout ppp int) 
		-> begin   
		-> 		if(ppp is not null)then      
		->		set ppp=ppp+1;  
		->		else      
		->		set ppp=100;   
		->		end if; 
		->end$$
	Query OK, 0 rows affected (0.02 sec)


	mysql> \d ;
	
	mysql> select @h;
	+------+
	| @h   |
	+------+
	| NULL |
	+------+
	1 row in set (0.00 sec)


	mysql> call param_inout(@h);
	Query OK, 0 rows affected (0.00 sec)

	mysql> select @h;
	+------+
	| @h   |
	+------+
	|  100 |
	+------+
	1 row in set (0.00 sec)

	mysql> call param_inout(@h);
	Query OK, 0 rows affected (0.00 sec)

	mysql> select @h;
	+------+
	| @h   |
	+------+
	|  101 |
	+------+
	1 row in set (0.00 sec)

	mysql>	
		
说明：	
	@ 定义局部变量	@@就是全局变量	set @变量名;
	
	存储过程也是数据库的对象 
	
	select @变量名; 
		就相当于echo一个变量 即查看一个变量 
		还可以在查看变量时给变量取别名：
			select @变量名 as 别名; #as有没有都可以
	
	select 数值 into 变量名; 
		给变量赋值
	也可以直接用 
		set 变量名=数值;
	
	
	


	
	
FUNCTION函数
==================================================

	mysql> \d ##
	
	mysql> create function hello(s char(20))
		-> returns char(50)
		->    return concat('hello,',s,'!');
		-> ##
	Query OK, 0 rows affected (0.57 sec)

	mysql> \d ;
	
	mysql> select hello('world');
	+----------------+
	| hello('world') |
	+----------------+
	| hello,world!   |
	+----------------+
	1 row in set (0.00 sec)

	mysql> select hello('world') hello;	#给返回值取别名
	+--------------+
	| hello        |
	+--------------+
	| hello,world! |
	+--------------+
	1 row in set (0.00 sec)
	
	mysql> \d ##

	mysql> create function name_from_emp(x int)                                                             
		-> returns varchar(50)
		-> begin
		->     return(select name from employee5
		->            where id=x);
		-> end##
	Query OK, 0 rows affected (0.08 sec)

	
	mysql> \d ;
	

	mysql> select name_from_emp(13);
	+-------------------+
	| name_from_emp(13) |
	+-------------------+
	| jack              |
	+-------------------+
	1 row in set (0.00 sec)

	mysql> select * from employee5 where name=name_from_emp(15);
	+----+-------+-----+------------+------+---------+---------+--------+--------+
	| id | name  | sex | hire_data  | post | jobdesc | salary  | office | dep_id |
	+----+-------+-----+------------+------+---------+---------+--------+--------+
	| 15 | robin | m   | 2018-02-02 | ins  | teach   | 8000.00 |    501 |    101 |
	+----+-------+-----+------------+------+---------+---------+--------+--------+
	1 row in set (0.00 sec)

	mysql> 

	
	
	
创建函数的语法：
	create function 函数名(参数列表) returns 返回值类型
		[特性...] 函数体
	
	函数的参数列表形式：
		参数名	数据类型
		
	函数一定要有返回值 函数没有参数类型  使用returns 关键字定义返回值类型 
	定义参数的数据类型 与 定义返回值的数据类型是两个不同的语句 
	故并不是定义什么数据类型就会返回什么数据类型 返回值类型是根据需求自行设定的

	select 函数名(实参);  即可以得到函数的返回值
		
	\d ##
	create function 函数名(参数列表) returns 返回值类型
	begin
		有效的SQL语句
	end##
	\d ;
	
	调用函数：
		select 函数名(实参列表);
		
存储过程与函数的维护：
	show create procedure 过程名\G
	show create function  函数名\G
	
	show procedure|function status [like 'parttem'];
	show procedure|function [if exists] sp_name;
	
mysql变量的术语分类：
	1.用户变量：
		以@开始，形式为：@变量名，是由客户端定义的变量
		用户变量跟mysql客户端是绑定的，设置的变量只对当前用户使用的客户端生效，当用户断开连接时，
	所有变量会自动释放。
	
	2.全局变量：
		定义全局变量的两种形式：set global 变量名; 或者 set @@global.变量名
		全局变量对所有客户端都生效，但是只用具有super权限才可以设置全局变量
	
	3.会话变量：只对连接的客户端有效
	
	4.局部变量：
		设置并作用于begin...end语句块之间的变量
		declare语句专门用于定义局部变量，而set语句是设置不同类型的变量，包括会话变量和全局变量。
		语法:
			declare 变量名[...] 变量类型 [default 默认值]
			declare定义的变量必须写在复合语句的开头，并且在任何其他语句的前面。

变量的赋值：
	直接赋值：set 变量名=表达式值或常量值[....];
	

用户变量的赋值：
	1.set 变量名=表达式或常量值;
	
	2.也可以将查询结果赋值给变量（但要求查询结果只有一行）
		如：set 列名 into 变量名 from 表名 where 条件;
	
	3.select 值 into @变量名;
		但客户端变量不能共享







