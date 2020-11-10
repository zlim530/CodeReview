-- 在这里编写SQl命令
-- 1.创建一个数据库（直接创建一个数据库，没有设置任何特殊选项，都是使用默认）

print @@version --查看当前sqlserver版本信息
--Microsoft SQL Server 2012 (SP3) (KB3072779) - 11.0.6020.0 (X64) 
--	Oct 20 2015 15:36:27 
--	Copyright (c) Microsoft Corporation
--	Standard Edition (64-bit) on Windows NT 6.3 <X64> (Build 9600: )

use MyFirstDatabase;

create database MySecondDatabse;

-- 2.删除数据库
drop database MySecondDatabase;

-- 3.创建数据库的时候设置一些参数选项
create database MyFirstDatabase
on primary
(
	-- 配置主数据文件的选项
	name = 'MyFirstDatabase', -- 主数据文件的逻辑名称，一般与数据库名称一致
	-- 主数据文件的实际保存路径
	filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MyFirstDatabase.mdf',
	size = 5MB,
	maxsize = 150MB,
	filegrowth = 20% -- 既可以写百分比也可以写每次增长的文件大小
)
log on
(
	-- 配置日志文件的选项
	name = 'MyFirstDatabase_log',
	filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MyFirstDatabase_log.ldf',
	size = 5mb, -- 日志文件的初始大小
	filegrowth = 5mb
)

------------------在数据库中创建一个表----------------
-- 将代码环境切换到 MyFirstDatabase 下：
use MyFirstDatabase;
-- 如果在创建表之前不选择数据库环境，则默认为 master 数据库，创建的表也默认在 master 数据库中
create table Department
(
	Id int identity(1,1) primary key,
	DepartmentName nvarchar(50) not null
)

-- 创建一个员工表：员工Id，身份证号，姓名，性别，入职日期，年龄，地址，电话，所属部门，Email
create table Employee
(
	EmpId int identity(1,1) primary key,
	EmpIdCard varchar(18) not null,
	EmpName nvarchar(50) not null,
	EmpGender bit not null,
	EmpJoinDate datetime,
	EmpAge int ,
	EmpAddress nvarchar(300),
	EmpPhone varchar(100),
	EmpEmail varchar(100),
	DeptId int not null --外键列，对应 Department 表中的主键列也即 Id 列
)

use master;

-- 删除表
drop table Department;
drop table DepartmentTable;

-- SQL 使用单引号表示字符串，如果需要转义也使用单引号 '，默认不区分大小写
-- [] 中括号是为了防止表名或者数据库名与关键字重名，并且名称中有空格的情况
create database ['i''m so hot !! '];

--Go：将T-SQL语句分批发送到数据库实例执行

-- 2020年10月3日：
---------------------------insert 插入语句---------------------------

use MyFirstDatabase;

create table TblScore(
	tScoreId int identity(1,1) primary key,
	tSId int not null,
	tEnglish float,
	tMath float
)

create table TblStudent(
	tId int identity(1,1) primary key,
	tName nvarchar(50) not null,
	tGender nchar(1),
	tAge int,
	tBirthday datetime,
	tCardId varchar(18) ,
	tClassId int not null
)

create table TblTeacher(
	tId int identity(1,1) primary key,
	tName nvarchar(50) not null,
	tGender bit,
	tSalary money,
	tAge int,
	tBirthday datetime
)

create table Class(
	Id int identity(1,1) primary key,
	ClassName nvarchar(50) not null
)
-- 向班级表中插入一条记录：
-- insert into 表名(列1,列2,列3 ... ) values(值1,值2,值3 ... )
-- 自动编号列默认就会自动增长，故不需要默认情况下也不允许向自动编号列插入值：
insert into Class(ClassName) values('.net blackhorse one');
select * from Class

-- 如果向表中的所有列（除自动编号以外的所有列）都要插入值，可以省略列名，同时必须保证后面的值列表中的顺序与表中对应列的顺序一致
insert into TblStudent(tName,tGender,tAge,tBirthday,tCardId,tClassId) 
values ('Tom','男',23,'1998-05-06','12345678987653212',1);
select * from TblStudent

-- 向自动编号列插入值
-- 启动某个表的“自动编号列”手动插入值的功能
set identity_insert Class on

insert into Class(Id,ClassName) values(5, 'Java Two')-- 手动向自动增长的Id列插入值

set identity_insert Class off
select * from Class

-- 在 SQL 语句中直接写的字符串中，如果包含中文，一定要在字符串前面加上N
insert into Class values(N'物理课')

-- 打开和关闭查询结果窗口的快捷键：ctrl+r

---------------------------update 更新语句---------------------------
-- update 表名 set 列=新值,列2=新值 ... where 条件;
-- 如果不加条件语句那么表示对表中所有的数据都进行修改
update TblStudent set tAge=tAge-1,tName = tName+'(男)' where tGender = '男'
select * from TblStudent


---------------------------删除语句---------------------------
-- 删除 TblStudent 表中的所有数据
-- 自动编号不会恢复默认值，仍然会继续(累加1)编号
delete from TblStudent
insert into TblStudent
values ('Tom','男',23,'1998-05-06','12345678987653212',1);
select * from TblStudent

-- truncate table 表名
-- 删除表中的所有行而不记录单个行删除操作，使用的系统资源和事务日志资源更少
truncate table TblStudent;
-- 如果确实是要删除表中的所有数据，建议使用 truncate 命令
-- 特点：
	--1.truncate 语句不能跟 where 条件：即无法根据条件来删除，只能删除全部数据
	--2.同时自动编号恢复为初始值
	--3.使用truncate 删除表中的所有数据比delete 效率更高
	--4.truncate 删除数据不会触发delete 触发器


---------------------------约束---------------------------
use MyFirstDatabase

drop table Department
drop table Employee

create table Department
(
	Id int  identity(1,1),
	DepartmentName nvarchar(50) 
)

create table Employee
(
	EmpId int identity(1,1) ,
	EmpIdCard varchar(18) ,
	EmpName nvarchar(50) ,
	EmpGender bit,
	EmpJoinDate datetime,
	EmpAge int ,
	EmpAddress nvarchar(300),-- 不带n的数据类型长度最长可以设置为8000，带n的则为4000
	EmpPhone varchar(100),
	EmpEmail varchar(100),
	DeptId int 
)

-- 手动增加约束：通过 T-SQL 语句
-- 删除某一列：
-- alter table tableName drop column columnName
alter table Employee drop column EmpAddress
-- 手动增加一列：不需要关键字 column:
-- alter table tableName add columnName ... 
alter table Employee add EmpAddr nvarchar(1000)

--修改EmpEmail字段
-- alter table tableName alter column columnName ... 
alter table Employee alter column EmpEmail varchar(200)

--给Employee表中的EmpId字段添加主键约束
-- alter table tableName add constraint primaryKeyName primary key(columName)
alter table Employee add constraint PK_Employee_EmpId
primary key(EmpId)

--给Employee 表中的 EmpName 字段添加非空约束：通过修改表字段属性来实现
--非空约束 not null 不需要加关键字 constraint
alter table Employee alter column EmpName varchar(50) not null

--给Employee 表中的EmpName 添加唯一约束
-- alter table tableName add constraint uniqueName unique(columName)
alter table Employee add constraint UQ_Employee_EmpName 
unique(EmpName)

--给Employee 表中的EmpGender 字段添加默认约束
-- alter table tableName add constraint defaultName default('defaultValue') for columName
alter table Employee add constraint DF_Employee_EmpGedner
default('女') for EmpGender

--给Employee 表中的EmpGender 字段添加检查约束
-- alter table tableName add constraint checkName check(expression)
alter table Employee add constraint CK_Employee_EmpGender 
check(EmpGender = '男' or EmpGender = '女')

--给Employee 表中的 EmpAge 字段添加检查约束
alter table Employee add constraint CK_Employee_EmpAge 
check(EmpAge >= 2 and EmpAge <= 120) 

--给Employee 表中的 EmpName 字段添加检查约束
alter table Employee add constraint CK_Employee_EmpName
check(len(EmpName) >= 2 and len(EmpName) <= 20)

--给Department表中的Id字段添加主键约束
alter table Department add constraint PK_Department_Id 
primary key(Id)

--给Employee 表中的 DeptId 字段添加非空约束：通过修改表字段属性来实现
alter table Employee alter column DeptId int not null

--给Employee 表中的 DeptId 字段添加外键约束
-- alter table tableName add constraint foreignKeyName foreign key(columnName) references anotherTableName(primaryKeyColumnName)
alter table Employee add constraint FK_Employee_Department
foreign key(DeptId) references Department(Id)

--删除约束：通过约束名来删除
-- alter table tableName drop constraint constraintName1,constraintName2, ...
alter table Employee drop constraint FK_Employee_Department,UQ_Employee_EmpName
,DF_Employee_EmpGender


-- 通过一行代码来添加多个约束
alter table Employee add 
constraint FK_Employee_Department foreign key(DeptId) references Department(Id),
constraint CK_Employee_EmpAge check(EmpAge >= 2 and EmpAge <= 120) 

-- 在创建表时就添加约束
drop table Department
drop table Employee

create table Department
(
	Id int  identity(1,1) primary key,
	DepartmentName nvarchar(50) not null unique
)

create table Employee
(
	EmpId int identity(1,1) primary key ,
	EmpIdCard varchar(18) not null unique,
	EmpName nvarchar(50) not null check(len(EmpName) >= 2),
	EmpGender bit default(1) ,-- 注意bit类型在进行insert 插入值时只能写1 或 0：但在代码中筛选判断时应该写 true(1) 或 false(0)
	EmpJoinDate datetime,
	EmpAge int check(EmpAge >= 0 and EmpAge <= 120),
	EmpAddress nvarchar(300),
	EmpPhone varchar(100),
	EmpEmail varchar(100) not null unique,
	DeptId int foreign key references Department(Id) on delete cascade 
	-- 级联删除：即如果删除Department表中的数据，那么Employee表中引用到Department表中对应Id主键的数据也会被删除
	-- 在实际项目中并不会设置级联删除，一般主外键关系均为弱关系/依赖
)



------------------------------------------------------------------------
-- 2020年10月8日：
use MyFirstDatabase;
-- * 表示显示所有列，查询语句中没有加 where 条件即表示查询所有行
select * from Class; 

-- 只查询表中的部分列
select ClassName from Class;

--根据条件查询部分行
select ClassName from Class where Id = 1;

-- 为查询结果集中的列取别名：三种形式
select ClassName as 课程名称 from Class;

select ClassName 课程名称 from Class;

select 课程名称 = ClassName from Class;

-- 如果别名中需要有空格等特殊字符可以使用单引号引起来
select 
	'课程  名称' = ClassName,
	是否正常上课 = '是' -- 也可以自己添加一列
from Class;

-- 并不是说 select 必须配合 from 使用，也可以单独使用 select 
select 当前系统时间 = GETDATE()



---------------------------- Top 和 distinct -----------------------
select * from Class
-- distinct 关键字针对已经查询出的结果然后取出重复，这里的重复数据是针对所有列
select distinct * from Class -- 查询结果与上述查询所有行所有列一样，因为在 Class 表中 Id 是主键，主键是不可重复的，因此每一条数据都一样

select distinct ClassName from Class -- 查询结果仅有三条，因为此时仅查询 ClassName 列，而ClassName 列存在重复，因此在此查询基础上再使用 distinct 即可去除重复

---------------------排序---------------
select * from Class

select * from Class order by Id desc -- 按照 Id 降序排序

select * from Class order by ClassName asc -- 按照 ClassName 升序排序，默认什么都不写就是按照升序排序


select top 5 * from TblScore order by tMath desc -- 查询数据成绩最高的前5名

select top 5 * from TblScore order by tMath asc -- 查询数据成绩最低的前5名

select top 5 percent * from TblScore order by tMath asc -- 还可以使用百分比，如果查询结果无法整除设定的百分比，则会向上取整（1.5 => 2）

select top (2 * 2) * from TblScore order by tMath asc -- 如果top后面跟的不是数字而是表达式则一定要用()小括号将表达式括起来



---------------------------- 聚合函数 -----------------------
select * from Class

-- 统计当前前中一共有多少条记录（包括空白null行）
select count(*) '总行数（包括空白行）' from Class;

select sum(Id) * 1.0 as 'Sum(Id)' from Class

-- 计算Id 平均值：注意这样写不用写 from Class 否则结果会是 21 列的 12
select 
	average = (select sum(Id) from Class) / (select count(*) from Class) * 1.0


select 
	average = avg(Id) * 1.0 
from Class

--------------------聚合函数的一些其他问题：
-- 1.聚合函数不统计空值：除了 count() 之外
select * from Class
-- sum() 对于null值认为是0
-- 2.如果使用聚合函数的时候没有手动 group by 分组，那么聚合函数会把整个表中的数据作为一组来统计



-----------------------------2020年10月12日(Monday)-----------------------------
-- 带条件的查询：
/*
select 
	列名
from 表名
where 条件
*/
select 
	Name,
	Age,
from Stduent 
where ClassId = 1 or ClassId = 2 or ClassId = 3

select 
	Name,
	Age,
from Stduent 
where ClassId in (1,2,3)

-- 对于上述查询语句，可以写成：
select 
	Name,
	Age,
from Stduent 
where ClassId >= 1 and ClassId <= 3
-- 因为1,2,3是连续的值，使用这种写法效率更高

--查询年龄在20-30岁之间的男学生（包含20和30）
select 
	*
from Student
where Age >= 20 and Age <= 30 and Gender = '男'

select * from TblStudent where Age between 20 and 30 and Gender = '男'
-- between ... and .. 在...之间（闭区间，包含两个端点值）

select * from TblStudent where tClassId = 3 or tClassId = 4 or tClassId = 5
select * from TblStudent where tClassId in (3,4,5)
-- 对于in 或者or 查询，如果查询中的条件是连续的几个数字，最好使用 >=、<= 或者 between ... and ...，不要使用or或者in，以调高效率
select * from TblStudent where tClassId >= 3 and tClassId <= 5



-------------------------------2020年10月21日-----------------------
/*
模糊查询：
	通配符: _ 、 % 、[] 、^
*/
-- _ 表示任意单个字符，它匹配单个出现的字符
-- 所有姓张，名字为两个字的
select * from TblStudent where tName like '张_'

-- 所有姓张，名字为三个字的
select * from TblStudent where tName like '张__'


-- % 表示匹配任意多个字符，它匹配任意次数（零或多个）出现的任意字符
-- 无论姓名字数，只要第一个字符是'张'即可匹配
select * from TblStudent where tName like '张%'

-- 用另一个字符串替换出现的所有指定字符串值
replace(string _expression, string _pattern, string replacement)
-- string _expression 要搜索的字符串表达式，可以是字符或二进制数据类型
-- string _pattern 要查找的子字符串，可以是字符或二进制数据类型，不能是空字符串('')
-- string replacement 替换字符串，可以是字符或二进制数据类型
update TblStudent set tName = REPLACE(tName,'(女)','')


-- [] 表示筛选范围，只匹配一个字符，并且这个字符必须是 [] 范围内的
select * from TblStudent where tName like '张[0-9]豪'
select * from TblStudent where tName like '张[a-z]豪'
select * from TblStudent where tName like '张[a-z0-9]豪'
-- [^] 不在指定范围内的任何单个字符：^ 只有MSSQL Server 支持，其他DBMS用 not like
select * from TblStudent where tName like '张[^0-9]豪'
select * from TblStudent where tName not like '张[0-9]豪'


-- 查询出姓名中包含 % 的人
-- 通配符放到 [] 中就转义了则不再认为是通配符
select * from TblStudent where tName like '%[%]%'

-- where columnA like '%5/%%' ESCAPE '/'
-- 自定义转义符
select * from TblStudent where tName like '%/]%' escape '/' -- 将转义符指定为 / 也即对 ] 符号进行转义
select * from TblStudent where tName like '%/[%' escape '/'
select * from TblStudent where tName like '%/[%/]%' escape '/'


/*
空值处理：
·数据库中，一个列如果没有指定值，那么值就为null，数据库中的null表示“不知道”，而不是表示没有。
因此select null + 1 的结果还是null，因为“不知道”加1的结果还是“不知道”
·select * from score where English = null; select * from socre where English != null
都没有任何返回结果，因为数据库也不知道什么等于“不知道”或者什么不等于“不知道”
·SQL 中使用 is null 或者 is not null来进行空值判断
select * from score where English is null; select * from score where English is not null
ISNULL(check_expression,replacement_)
*/
select null + 200-- 任何值与null进行计算，得到的结果还是null



-------------------------------2020年10月22日-----------------------
-------------------------------数据排序-----------------------
/*
·order by字节位于select 语句末尾，它允许指定按照一个列或者多个列进行排序，还可以指定排序是升序（从小到大排列，asc）还是降序（从大到小排列，desc）
·order by 语句一般要放在所有语句的后面，表示先让其他语句进行筛选，待全部筛选完成后，最后进行排序
·表中数据是集合，集合是没有顺序的。order by 返回的数据是有顺序的，故此我们把order by以后返回的数据集合叫“游标”
*/
-- 按照英语成绩的升序进行排序
select * from TblScore order by tEnglish asc
-- 先按照英语成绩的从小到大进行排序，如果英语成绩相同则按照数学成绩从大到小排序
select * from TblScore order by tEnglish asc,tMath desc

--order by字句要放在where 字句之后
select * from TblScore where tEnglish >=60 and tMath >=60 order by tEnglish asc,tMath desc

-- order by 语句必须一定要放在整个sql语句的最后
select * from TableName
left join ...
where ...
group by ... 
having ...
order by ...


select 
	*,
	AvgScore = (tEnglish + tMath) * 1.0 / 2
from TblScore
order by AvgScore desc

select 
	*
from TblScore
order by(tEnglish + tMath) * 1.0 / 2 desc

select * -- 3
from TblScore -- 1
where tEnglish >= 60 and tMath >=60 -- 2
order by tEnglish desc,tMath desc -- 4



-------------------------------数据分组-----------------------
/*
·在使用select 查询的时候，有时候需要对数据进行分组汇总（即：将现有的数据按照某列来汇总统计），这时就需要用到group by 语句，select 语句中可以使用group by字句将行划分成较小的组，然后使用聚合函数返回每一组的汇总信息；分组一般都和聚合函数连用
·group by 字句必须放到where 语句之后，group by 与 order by 都是对筛选后的数据进行处理，而where 是用来筛选数据的；
·没有出现在group by 字句中的列是不能放到select 语句后的列名列表中的（聚合函数中除外）
	select ClassId,count(sName),sAge from Student group by ClassId => 错误！
	select ClassId,count(sName),avg(sAge) from Student group by ClassId => 正确！
*/

--从学生表中查出每个班的班级Id和班级人数
select 
	tClassId as 班级Id,
	班级人数 = count(*)
from TblStudent
group by tClassId

-- 统计出班级中男同学与女同学的人数
select 
	Gender = tGender,
	GenderCount = COUNT(*)
from TblStudent
group by tGender



---------------------------------2020年10月30日-------------------------------------
/*
					having语句（对组的筛选，哪些组显示哪些组不显示）
对表中的数据分组后，会得到一个分组后的结果集，如何对该结果集在进行筛选：having
注意Having中不能使用未参与分组的列，Having不能替代where。
作用不一样，Having是对组进行过滤，where是对每条记录进行过滤的
Having是Group By的条件对分组后的数据进行筛选，与where类似，都是筛选数据，只不过having是用来筛选分组后的组的
在where中不能使用聚合函数，必须使用Having，Having要位于Group by之后
Having的使用几乎与where一样，也可以用in
	·Having count(*) in (5,8,10)
*/
select 
	 班级Id = tClassId,
	 男生人数 = count(*)
from TblStudent
where tGender = 'Male'
group by tClassId

------------------------------
select 
	 sum(tAge),
	 Gender = tGender,
	 'count' = COUNT(*)
from TblStudent
group by tGender
-- 当使用了分组语句（group by）或者是聚合函数的时候，在select查询列表中不能再包含其他的列名，除非该列同时也出现在了group by子句中，或者改列也包含在了某个聚合函数中

-- 对分组以后的数据进行筛选，使用having
-- having与where都是对数据进行筛选，where是对分组前的每一行数据进行筛选，而having是对分组后的每一行数据进行筛选
select				    --4
	tClassId as 班级Id,
	班级人数 = count(*)
from TblStudent			--1
group by tClassId		--2
having COUNT(*) > 0		--3
order by 班级人数 desc	--5



SQL语句的执行顺序：
5.select 
	5.1选择列名
	5.2distinct
	5.3top（应用top选项最后计算）
1.from 表
2.where 条件
3.group by 列
4.having 筛选条件
6.order by 列

SELECT语句的处理顺序：
以下步骤显示SELECT语句的处理顺序
	1.FROM
	2.ON
	3.JOIN
	4.WHERE
	5.GROUP BY
	6.WITH CUBE 或 WITH ROLLIP
	7.HAVING
	8.SELECT
	9.DISTINCT
	10.ORDER BY
	11.TOP


--热销商品排名表，即按照每种商品的总销售数量排序
select
	商品名称
	sum(销售数量) as 销售数量
from MyOrders
group by 商品名称
order by 销售数量 desc


--统计销售总价超过3000元的商品名称和销售总价，并按销售总价降序排序
select 
	商品数量,
	销售总价 = sum(销售数量 * 销售价格)
from MyOrders
group by 商品名称
having sum(销售数量 * 销售价格) > 3000
order by 销售总价 desc


--统计各个客户对“可口可乐”的喜爱度（即统计每个购买人对“可口可乐”的购买量）
select 
	购买人,
	购买数量 = sum(销售数量)
from MyOrders
where 商品数量 = '可口可乐'
group by 购买人
order by 购买数量 desc



/*
类型转换函数：
	·cast(_expression,as data_type)
	·convert(data_type,expression,[style])
		select 'Number' + 1; 语句错误，因为这里的+是数学运算符
*/



------------------------------------------2020年11月4日----------------------------
/*
					联合结果集union（集合运算符）
·集合运算符是对两个集合操作的，两个集合必须具有相同的列数，列具有相同的数据结构（至少能隐式转换的），最终输出的集合的列名由一个集合的列名来确定。（可以用来连接多个结果）
·联合（union）与连接（join）不一样
·简单的结果集联合：
	select tName,tSex from techer union
	select sName,sSex from student
·基本的原则：每个结果集必须有相同的列数；每个结果集的列必须类型相容
·联合：将多个结果集合并成一个结果集。
·union（默认去除重复，相当于默认应用了distinct）
·union all
·常见应用：底部汇总。使用 union all（即不默认去重）
*/
-- 使用union联合结果集
select 
	tName,tGender,tAge
from TblStudent
union all
select
	fName,fGender,fAge
from MySdtuent


--使用union和union all都能进行联合，区别在于：使用union联合会去除重复、重新排列数据，而union all不会去除重复也不会重新排列
select tName,tGender,tAge from TblStudent
union
select fName,fGender,fAge from MyStudent
--union 合并两个查询结果集，并且将其中完全重复的数据合并为一条
--union因为要进行重复值扫描,所以效率低,因此如果不是确定要去除重复行,就使用union all

--大多数情况下，联合的时候不需要去除重复，同时要保持数据的顺序，所以一般建议使用union all


--从MyOrder表中统计每种商品的销售总价，并且在底部做汇总
select
	商品名称,
	销售总价 = sum(销售价格 * 销售数量)
from MyOrders
group by 商品名称
union all
select '总销售价格',sum(销售价格 * 销售数量) from MyOrders
order by 销售总价 asc


--查询成绩表中的:最高分,最低分,平均分
select 
	max(tMath) as 最高分,
	min(tMath) as 最低分,
	avg(tMath) as 平均分
from TblScore

select 
	最高分 = (select max(tMath) from TblScore),	
	最低分 = (select min(tMath) from TblScore),	
	平均分 = (select avg(tMath) from TblScore)
from TblScore

select 名称='最高分', 分数 = max(tMath) from TblScore
union all
select 名称='最低分', 分数 = min(tMath) from TblScore
union all
select 名称='平均分', 分数 = avg(tMath) from TblScore


--使用union all向表中插入多条数据
insert into Class
select 'Python'
union all
select 'Python'
union all
select 'Java'
union all
select 'C'
union all
select 'CPlusPlus'


--使用union向表中插入多条数据：会自动去除重复
insert into Class
select 'Python'
union  
select 'Python'
union  
select 'Java'
union  
select 'C'
union  
select 'CPlusPlus'


--一次插入多条数据
--把现有的表的数据插入到新表(表不能存在),为表建备份
--newStudent表示在执行select into语句时创建的
--select into 语句不能重复执行，因为每次执行都会创建一个newStudent表
--TblStudent 表结构包括自动编号列都会在newStudent中创建，但是TblStudent表中的约束并不会出现在newStudent表中
select * into newStudent from TblStudent 
--(newStudent表在select 查询时同时自动建立)
--把现有表的数据复制到一个已存在的表，通过这种方式复制，只能复制表结构，以及列的名字和数据类型，对于约束，不会复制过来
--只拷贝表结构，不拷贝数据
select * into newTbl from oldTbl where 1 != 1
--这样做可以只复制表结构，但效率不高，建议：
select top 0 * into newTbl from oldTbl

--如果表已经存在了
insert into backupStudent select * from TblStudent
--(backupStudent表必须提前建好)


select top 0 * into newStudent from TblStudent
select * from newStudent




-----------------------------2020年11月10日-----------------------------
use Test

create table MartialArtsMaster(
	Id int identity(1,1) primary key,
	Name nvarchar(100) not null,
	Age int not null,
	Menpai nvarchar(100) not null,
	Kungfu nvarchar(100) not null,
	Level int not null
)

select * from MartialArtsMaster

create table Kongfu(
	KongfuId int identity(1,1) primary key,
	KongfuName nvarchar(100) not null,
	Lethality int not null
)

select * from Kongfu

insert into MartialArtsMaster values('黄蓉',18,'丐帮','打狗棒法',9)

