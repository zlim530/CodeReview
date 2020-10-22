-- �������дSQl����
-- 1.����һ�����ݿ⣨ֱ�Ӵ���һ�����ݿ⣬û�������κ�����ѡ�����ʹ��Ĭ�ϣ�

use MyFirstDatabase;

create database MySecondDatabse;

-- 2.ɾ�����ݿ�
drop database MySecondDatabase;

-- 3.�������ݿ��ʱ������һЩ����ѡ��
create database MyFirstDatabase
on primary
(
	-- �����������ļ���ѡ��
	name = 'MyFirstDatabase', -- �������ļ����߼����ƣ�һ�������ݿ�����һ��
	filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MyFirstDatabase.mdf',
	-- �������ļ���ʵ�ʱ���·��
	size = 5MB,
	maxsize = 150MB,
	filegrowth = 20%
)
log on
(
	-- ������־�ļ���ѡ��
	name = 'MyFirstDatabase_log',
	filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MyFirstDatabase_log.ldf',
	size = 5mb, -- ��־�ļ��ĳ�ʼ��С
	filegrowth = 5mb
)

------------------�����ݿ��д���һ����----------------
-- �����뻷���л��� MyFirstDatabase �£�
use MyFirstDatabase;

create table Department
(
	Id int  identity(1,1) primary key,
	DepartmentName nvarchar(50) not null
)

-- ����һ��Ա����Ա��Id�����֤�ţ��������Ա���ְ���ڣ����䣬��ַ���绰���������ţ�Email
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
	DeptId int not null --����У���Ӧ Department ���е�������
)

-- ɾ����
use master;

drop table Department;
drop table DepartmentTable;

-- SQL ʹ�õ����ű�ʾ�ַ����������Ҫת��Ҳʹ�õ����� '��Ĭ�ϲ����ִ�Сд
-- [] ��������Ϊ�˷�ֹ�����������ݿ�����ؼ��������������������пո�����
create database ['i''m so hot !! '];

--Go����T-SQL���������͵����ݿ�ʵ��ִ��

-- 2020��10��3�գ�
---------------------------insert �������---------------------------

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
-- ��༶���в���һ����¼��
-- insert into ����(��1,��2,��3 ... ) values(ֵ1,ֵ2,ֵ3 ... )
-- �Զ������Ĭ�Ͼͻ��Զ��������ʲ���ҪĬ�������Ҳ���������Զ�����в���ֵ��
insert into Class values('.net blackhorse one');
select * from Class

-- �������е������У����Զ��������������У���Ҫ����ֵ������ʡ��������ͬʱ���뱣֤�����ֵ�б��е�˳������ж�Ӧ�е�˳��һ��
insert into TblStudent(tName,tGender,tAge,tBirthday,tCardId,tClassId) 
values ('Tom','��',23,'1998-05-06','12345678987653212',1);
select * from TblStudent

-- ���Զ�����в���ֵ
-- ����ĳ����ġ��Զ�����С��ֶ�����ֵ�Ĺ���
set identity_insert Class on
insert into Class(Id,ClassName) values(5, 'Java Two')
set identity_insert Class off
select * from Class

-- �� SQL �����ֱ��д���ַ����У�����������ģ�һ��Ҫ���ַ���ǰ�����N
insert into Class values(N'�����')

-- �򿪺͹رղ�ѯ������ڵĿ�ݼ���ctrl+r

---------------------------update �������---------------------------
-- update ���� set ��=��ֵ,��2=��ֵ ... where ����;
-- ����������������ô��ʾ�Ա������е����ݶ������޸�
update TblStudent set tAge=tAge-1,tName = tName+'(��)' where tGender = '��'
select * from TblStudent


---------------------------ɾ�����---------------------------
-- ɾ�� TblStudent ���е���������
-- �Զ���Ų���ָ�Ĭ��ֵ����Ȼ�������
delete from TblStudent
insert into TblStudent
values ('Tom','��',23,'1998-05-06','12345678987653212',1);
select * from TblStudent

-- truncate table ����
-- ɾ�����е������ж�����¼������ɾ��������ʹ�õ�ϵͳ��Դ��������־��Դ����
truncate table TblStudent;
-- ���ȷʵ��Ҫɾ�����е��������ݣ�����ʹ�� truncate ����
-- �ض���
	--1.truncate ��䲻�ܸ� where ���������޷�����������ɾ����ֻ��ɾ��ȫ������
	--2.ͬʱ�Զ���Żָ�Ϊ��ʼֵ
	--3.ʹ��truncate ɾ�����е��������ݱ�delete Ч�ʸ���
	--4.truncate ɾ�����ݲ��ᴥ��delete ������


---------------------------Լ��---------------------------
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

-- �ֶ�����Լ����ͨ�� T-SQL ���
-- ɾ��ĳһ��
alter table Employee drop column EmpAddress
-- �ֶ�����һ�У�����Ҫ�ؼ��� column
alter table Employee add EmpAddr nvarchar(1000)

--�޸�EmpEmail�ֶ�
alter table Employee alter column EmpEmail varchar(200)

--��Employee���е�EmpId�ֶ��������Լ��
alter table Employee add constraint PK_Employee_EmpId
primary key(EmpId)

--��Employee ���е� EmpName �ֶ���ӷǿ�Լ����ͨ���޸ı��ֶ�������ʵ��
alter table Employee alter column EmpName varchar(50) not null

--��Employee ���е�EmpName ���ΨһԼ��
alter table Employee add constraint UQ_Employee_EmpName 
unique(EmpName)

--��Employee ���е�EmpGender �ֶ����Ĭ��Լ��
alter table Employee add constraint DF_Employee_EmpGedner
default('Ů') for EmpGender

--��Employee ���е�EmpGender �ֶ���Ӽ��Լ��
alter table Employee add constraint CK_Employee_EmpGender 
check(EmpGender = '��' or EmpGender = 'Ů')

--��Employee ���е� EmpAge �ֶ���Ӽ��Լ��
alter table Employee add constraint CK_Employee_EmpAge 
check(EmpAge >= 2 and EmpAge <= 120) 

--��Employee ���е� EmpName �ֶ���Ӽ��Լ��
alter table Employee add constraint CK_Employee_EmpName
check(len(EmpName) >= 2 and len(EmpName) <= 20)

--��Department���е�Id�ֶ��������Լ��
alter table Department add constraint PK_Department_Id 
primary key(Id)

--��Employee ���е� DeptId �ֶ���ӷǿ�Լ����ͨ���޸ı��ֶ�������ʵ��
alter table Employee alter column DeptId int not null

--��Employee ���е� DeptId �ֶ�������Լ��
alter table Employee add constraint FK_Employee_Department
foreign key(DeptId) references Department(Id)

--ɾ��Լ��
alter table Employee drop constraint FK_Employee_Department,UQ_Employee_EmpName
,DF_Employee_EmpGender

-- ͨ��һ�д�������Ӷ��Լ��
alter table Employee add 
constraint FK_Employee_Department foreign key(DeptId) references Department(Id),
constraint CK_Employee_EmpAge check(EmpAge >= 2 and EmpAge <= 120) 

-- �ڴ�����ʱ�����Լ��
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
	EmpGender bit default(1) ,-- ע��bit�����ڽ���insert ����ֵʱֻ��д1 �� 0
	EmpJoinDate datetime,
	EmpAge int check(EmpAge >= 0 and EmpAge <= 120),
	EmpAddress nvarchar(300),
	EmpPhone varchar(100),
	EmpEmail varchar(100) not null unique,
	DeptId int foreign key references Department(Id) on delete cascade -- ����ɾ���������ɾ��Department���е����ݣ���ôEmployee�������õ�Department���ж�ӦId����������Ҳ�ᱻɾ��
)



------------------------------------------------------------------------
-- 2020��10��8�գ�
use MyFirstDatabase;
-- * ��ʾ��ʾ�����У���ѯ�����û�м� where ��������ʾ��ѯ������
select * from Class; 

-- ֻ��ѯ���еĲ�����
select ClassName from Class;

--����������ѯ������
select ClassName from Class where Id = 1;

-- Ϊ��ѯ������е���ȡ������������ʽ
select ClassName as �γ����� from Class;

select ClassName �γ����� from Class;

select �γ����� = ClassName from Class;

-- �����������Ҫ�пո�������ַ�����ʹ�õ�����������
select 
	'�γ�  ����' = ClassName,
	�Ƿ������Ͽ� = '��' -- Ҳ�����Լ����һ��
from Class;

-- ������˵ select ������� from ʹ�ã�Ҳ���Ե���ʹ�� select 
select ��ǰϵͳʱ�� = GETDATE()



---------------------------- Top �� distinct -----------------------
select * from Class
-- distinct �ؼ�������Ѿ���ѯ���Ľ��Ȼ��ȡ���ظ���������ظ����������������
select distinct * from Class -- ��ѯ�����������ѯ������������һ������Ϊ�� Class ���� Id �������������ǲ����ظ��ģ����ÿһ�����ݶ�һ��

select distinct ClassName from Class -- ��ѯ���������������Ϊ��ʱ����ѯ ClassName �У���ClassName �д����ظ�������ڴ˲�ѯ��������ʹ�� distinct ����ȥ���ظ�

---------------------����---------------
select * from Class

select * from Class order by Id desc -- ���� Id ��������

select * from Class order by ClassName asc -- ���� ClassName ��������Ĭ��ʲô����д���ǰ�����������


select top 5 * from TblScore order by tMath desc -- ��ѯ���ݳɼ���ߵ�ǰ5��

select top 5 * from TblScore order by tMath asc -- ��ѯ���ݳɼ���͵�ǰ5��

select top 5 percent * from TblScore order by tMath asc -- ������ʹ�ðٷֱȣ������ѯ����޷������趨�İٷֱȣ��������ȡ����1.5 => 2��

select top (2 * 2) * from TblScore order by tMath asc -- ���top������Ĳ������ֶ��Ǳ��ʽ��һ��Ҫ��()С���Ž����ʽ������



---------------------------- �ۺϺ��� -----------------------
select * from Class

-- ͳ�Ƶ�ǰǰ��һ���ж�������¼�������հ�null�У�
select count(*) '�������������հ��У�' from Class;

select sum(Id) * 1.0 as 'Sum(Id)' from Class

-- ����Id ƽ��ֵ��ע������д����д from Class ���������� 21 �е� 12
select 
	average = (select sum(Id) from Class) / (select count(*) from Class) * 1.0


select 
	average = avg(Id) * 1.0 
from Class

--------------------�ۺϺ�����һЩ�������⣺
-- 1.�ۺϺ�����ͳ�ƿ�ֵ������ count() ֮��
select * from Class
-- sum() ����nullֵ��Ϊ��0
-- 2.���ʹ�þۺϺ�����ʱ��û���ֶ� group by ���飬��ô�ۺϺ�������������е�������Ϊһ����ͳ��



-----------------------------2020��10��12��(Monday)-----------------------------
-- �������Ĳ�ѯ��
/*
select 
	����
from ����
where ����
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

-- ����������ѯ��䣬����д�ɣ�
select 
	Name,
	Age,
from Stduent 
where ClassId >= 1 and ClassId <= 3
-- ��Ϊ1,2,3��������ֵ��ʹ������д��Ч�ʸ���

--��ѯ������20-30��֮�����ѧ��������20��30��
select 
	*
from Student
where Age >= 20 and Age <= 30 and Gender = '��'

select * from TblStudent where Age between 20 and 30 and Gender = '��'
-- between ... and .. ��...֮�䣨�����䣬���������˵�ֵ��

select * from TblStudent where tClassId = 3 or tClassId = 4 or tClassId = 5
select * from TblStudent where tClassId in (3,4,5)
-- ����in ����or ��ѯ�������ѯ�е������������ļ������֣����ʹ�� >=��<= ���� between ... and ...����Ҫʹ��or����in���Ե���Ч��
select * from TblStudent where tClassId >= 3 and tClassId <= 5



-------------------------------2020��10��21��-----------------------
/*
ģ����ѯ��
	ͨ���: _ �� % ��[] ��^
*/
-- _ ��ʾ���ⵥ���ַ�����ƥ�䵥�����ֵ��ַ�
-- �������ţ�����Ϊ�����ֵ�
select * from TblStudent where tName like '��_'

-- �������ţ�����Ϊ�����ֵ�
select * from TblStudent where tName like '��__'


-- % ��ʾƥ���������ַ�����ƥ���������������������ֵ������ַ�
-- ��������������ֻҪ��һ���ַ���'��'����ƥ��
select * from TblStudent where tName like '��%'

-- ����һ���ַ����滻���ֵ�����ָ���ַ���ֵ
replace(string _expression, string _pattern, string replacement)
-- string _expression Ҫ�������ַ������ʽ���������ַ����������������
-- string _pattern Ҫ���ҵ����ַ������������ַ���������������ͣ������ǿ��ַ���('')
-- string replacement �滻�ַ������������ַ����������������
update TblStudent set tName = REPLACE(tName,'(Ů)','')


-- [] ��ʾɸѡ��Χ��ֻƥ��һ���ַ�����������ַ������� [] ��Χ�ڵ�
select * from TblStudent where tName like '��[0-9]��'
select * from TblStudent where tName like '��[a-z]��'
select * from TblStudent where tName like '��[a-z0-9]��'
-- [^] ����ָ����Χ�ڵ��κε����ַ���^ ֻ��MSSQL Server ֧�֣�����DBMS�� not like
select * from TblStudent where tName like '��[^0-9]��'
select * from TblStudent where tName not like '��[0-9]��'


-- ��ѯ�������а��� % ����
-- ͨ����ŵ� [] �о�ת����������Ϊ��ͨ���
select * from TblStudent where tName like '%[%]%'

-- where columnA like '%5/%%' ESCAPE '/'
-- �Զ���ת���
select * from TblStudent where tName like '%/]%' escape '/' -- ��ת���ָ��Ϊ / Ҳ���� ] ���Ž���ת��
select * from TblStudent where tName like '%/[%' escape '/'
select * from TblStudent where tName like '%/[%/]%' escape '/'


/*
��ֵ����
�����ݿ��У�һ�������û��ָ��ֵ����ôֵ��Ϊnull�����ݿ��е�null��ʾ����֪�����������Ǳ�ʾû�С�
���select null + 1 �Ľ������null����Ϊ����֪������1�Ľ�����ǡ���֪����
��select * from score where English = null; select * from socre where English != null
��û���κη��ؽ������Ϊ���ݿ�Ҳ��֪��ʲô���ڡ���֪��������ʲô�����ڡ���֪����
��SQL ��ʹ�� is null ���� is not null�����п�ֵ�ж�
select * from score where English is null; select * from score where English is not null
ISNULL(check_expression,replacement_)
*/
select null + 200-- �κ�ֵ��null���м��㣬�õ��Ľ������null



-------------------------------2020��10��22��-----------------------
-------------------------------��������-----------------------
/*
��order by�ֽ�λ��select ���ĩβ��������ָ������һ���л��߶���н������򣬻�����ָ�����������򣨴�С�������У�asc�����ǽ��򣨴Ӵ�С���У�desc��
��order by ���һ��Ҫ�����������ĺ��棬��ʾ��������������ɸѡ����ȫ��ɸѡ��ɺ�����������
�����������Ǽ��ϣ�������û��˳��ġ�order by ���ص���������˳��ģ��ʴ����ǰ�order by�Ժ󷵻ص����ݼ��ϽС��αꡱ
*/
-- ����Ӣ��ɼ��������������
select * from TblScore order by tEnglish asc
-- �Ȱ���Ӣ��ɼ��Ĵ�С��������������Ӣ��ɼ���ͬ������ѧ�ɼ��Ӵ�С����
select * from TblScore order by tEnglish asc,tMath desc

--order by�־�Ҫ����where �־�֮��
select * from TblScore where tEnglish >=60 and tMath >=60 order by tEnglish asc,tMath desc

-- order by ������һ��Ҫ��������sql�������
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



-------------------------------���ݷ���-----------------------
/*
����ʹ��select ��ѯ��ʱ����ʱ����Ҫ�����ݽ��з�����ܣ����������е����ݰ���ĳ��������ͳ�ƣ�����ʱ����Ҫ�õ�group by ��䣬select ����п���ʹ��group by�־佫�л��ֳɽ�С���飬Ȼ��ʹ�þۺϺ�������ÿһ��Ļ�����Ϣ������һ�㶼�;ۺϺ�������
��group by �־����ŵ�where ���֮��group by �� order by ���Ƕ�ɸѡ������ݽ��д�����where ������ɸѡ���ݵģ�
��û�г�����group by �־��е����ǲ��ܷŵ�select ����������б��еģ��ۺϺ����г��⣩
	select ClassId,count(sName),sAge from Student group by ClassId => ����
	select ClassId,count(sName),avg(sAge) from Student group by ClassId => ��ȷ��
*/

--��ѧ�����в��ÿ����İ༶Id�Ͱ༶����
select 
	tClassId as �༶Id,
	�༶���� = count(*)
from TblStudent
group by tClassId

-- ͳ�Ƴ��༶����ͬѧ��Ůͬѧ������
select 
	Gender = tGender,
	GenderCount = COUNT(*)
from TblStudent
group by tGender



-------------------------------having��䣨�����ɸѡ����Щ����ʾ��Щ�鲻��ʾ��-----------------------

