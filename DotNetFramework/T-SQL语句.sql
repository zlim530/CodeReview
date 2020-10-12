-- 在这里编写SQl命令
-- 1.创建一个数据库（直接创建一个数据库，没有设置任何特殊选项，都是使用默认）

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
	filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MyFirstDatabase.mdf',
	-- 主数据文件的实际保存路径
	size = 5MB,
	maxsize = 150MB,
	filegrowth = 20%
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

create table Department
(
	Id int  identity(1,1) primary key,
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
	DeptId int not null --外键列，对应 Department 表中的主键列
)

-- 删除表
use master;

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
insert into Class values('.net blackhorse one');
select * from Class

-- 如果向表中的所有列（除自动编号以外的所有列）都要插入值，可以省略列名，同时必须保证后面的值列表中的顺序与表中对应列的顺序一致
insert into TblStudent(tName,tGender,tAge,tBirthday,tCardId,tClassId) 
values ('Tom','男',23,'1998-05-06','12345678987653212',1);
select * from TblStudent

-- 向自动编号列插入值
-- 启动某个表的“自动编号列”手动插入值的功能
set identity_insert Class on
insert into Class(Id,ClassName) values(5, 'Java Two')
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
-- 自动编号不会恢复默认值，仍然继续编号
delete from TblStudent
insert into TblStudent
values ('Tom','男',23,'1998-05-06','12345678987653212',1);
select * from TblStudent

-- truncate table 表名
-- 删除表中的所有行而不记录单个行删除操作，使用的系统资源和事务日志资源更少
truncate table TblStudent;
-- 如果确实是要删除表中的所有数据，建议使用 truncate 命令
-- 特定：
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
	EmpAddress nvarchar(300),
	EmpPhone varchar(100),
	EmpEmail varchar(100),
	DeptId int 
)

-- 手动增加约束：通过 T-SQL 语句
-- 删除某一列
alter table Employee drop column EmpAddress
-- 手动增加一列：不需要关键字 column
alter table Employee add EmpAddr nvarchar(1000)

--修改EmpEmail字段
alter table Employee alter column EmpEmail varchar(200)

--给Employee表中的EmpId字段添加主键约束
alter table Employee add constraint PK_Employee_EmpId
primary key(EmpId)

--给Employee 表中的 EmpName 字段添加非空约束：通过修改表字段属性来实现
alter table Employee alter column EmpName varchar(50) not null

--给Employee 表中的EmpName 添加唯一约束
alter table Employee add constraint UQ_Employee_EmpName 
unique(EmpName)

--给Employee 表中的EmpGender 字段添加默认约束
alter table Employee add constraint DF_Employee_EmpGedner
default('女') for EmpGender

--给Employee 表中的EmpGender 字段添加检查约束
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
alter table Employee add constraint FK_Employee_Department
foreign key(DeptId) references Department(Id)

--删除约束
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
	EmpGender bit default(1) ,-- 注意bit类型在进行insert 插入值时只能写1 或 0
	EmpJoinDate datetime,
	EmpAge int check(EmpAge >= 0 and EmpAge <= 120),
	EmpAddress nvarchar(300),
	EmpPhone varchar(100),
	EmpEmail varchar(100) not null unique,
	DeptId int foreign key references Department(Id) on delete cascade -- 级联删除：即如果删除Department表中的数据，那么Employee表中引用到Department表中对应Id主键的数据也会被删除
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



