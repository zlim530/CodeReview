-- �������дSQl����
-- 1.����һ�����ݿ⣨ֱ�Ӵ���һ�����ݿ⣬û�������κ�����ѡ�����ʹ��Ĭ�ϣ�

print @@version --�鿴��ǰsqlserver�汾��Ϣ
--Microsoft SQL Server 2012 (SP3) (KB3072779) - 11.0.6020.0 (X64) 
--	Oct 20 2015 15:36:27 
--	Copyright (c) Microsoft Corporation
--	Standard Edition (64-bit) on Windows NT 6.3 <X64> (Build 9600: )

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
	-- �������ļ���ʵ�ʱ���·��
	filename = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\MyFirstDatabase.mdf',
	size = 5MB,
	maxsize = 150MB,
	filegrowth = 20% -- �ȿ���д�ٷֱ�Ҳ����дÿ���������ļ���С
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
-- ����ڴ�����֮ǰ��ѡ�����ݿ⻷������Ĭ��Ϊ master ���ݿ⣬�����ı�ҲĬ���� master ���ݿ���
create table Department
(
	Id int identity(1,1) primary key,
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
	DeptId int not null --����У���Ӧ Department ���е�������Ҳ�� Id ��
)

use master;

-- ɾ����
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
insert into Class(ClassName) values('.net blackhorse one');
select * from Class

-- �������е������У����Զ��������������У���Ҫ����ֵ������ʡ��������ͬʱ���뱣֤�����ֵ�б��е�˳������ж�Ӧ�е�˳��һ��
insert into TblStudent(tName,tGender,tAge,tBirthday,tCardId,tClassId) 
values ('Tom','��',23,'1998-05-06','12345678987653212',1);
select * from TblStudent

-- ���Զ�����в���ֵ
-- set identity_insert tableName on
-- ����ĳ����ġ��Զ�����С��ֶ�����ֵ�Ĺ���
set identity_insert Class on

insert into Class(Id,ClassName) values(5, 'Java Two')-- �ֶ����Զ�������Id�в���ֵ

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
-- �Զ���Ų���ָ�Ĭ��ֵ����Ȼ�����(�ۼ�1)���
delete from TblStudent
insert into TblStudent
values ('Tom','��',23,'1998-05-06','12345678987653212',1);
select * from TblStudent

-- truncate table ����
-- ɾ�����е������ж�����¼������ɾ��������ʹ�õ�ϵͳ��Դ��������־��Դ����
truncate table TblStudent;
-- ���ȷʵ��Ҫɾ�����е��������ݣ�����ʹ�� truncate ����
-- �ص㣺
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
	EmpAddress nvarchar(300),-- ����n���������ͳ������������Ϊ8000����n����Ϊ4000
	EmpPhone varchar(100),
	EmpEmail varchar(100),
	DeptId int 
)

-- �ֶ�����Լ����ͨ�� T-SQL ���
-- ɾ��ĳһ�У�
-- alter table tableName drop column columnName
alter table Employee drop column EmpAddress
-- �ֶ�����һ�У�����Ҫ�ؼ��� column:
-- alter table tableName add columnName ... 
alter table Employee add EmpAddr nvarchar(1000)

--�޸�EmpEmail�ֶ�
-- alter table tableName alter column columnName ... 
alter table Employee alter column EmpEmail varchar(200)

--��Employee���е�EmpId�ֶ��������Լ��
-- alter table tableName add constraint primaryKeyName primary key(columName)
alter table Employee add constraint PK_Employee_EmpId
primary key(EmpId)

--��Employee ���е� EmpName �ֶ���ӷǿ�Լ����ͨ���޸ı��ֶ�������ʵ��
--�ǿ�Լ�� not null ����Ҫ�ӹؼ��� constraint
alter table Employee alter column EmpName varchar(50) not null

--��Employee ���е�EmpName ���ΨһԼ��
-- alter table tableName add constraint uniqueName unique(columName)
alter table Employee add constraint UQ_Employee_EmpName 
unique(EmpName)

--��Employee ���е�EmpGender �ֶ����Ĭ��Լ��
-- alter table tableName add constraint defaultName default('defaultValue') for columName
alter table Employee add constraint DF_Employee_EmpGedner
default('Ů') for EmpGender

--��Employee ���е�EmpGender �ֶ���Ӽ��Լ��
-- alter table tableName add constraint checkName check(expression)
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
-- alter table tableName add constraint foreignKeyName foreign key(columnName) references anotherTableName(primaryKeyColumnName)
alter table Employee add constraint FK_Employee_Department
foreign key(DeptId) references Department(Id)

--ɾ��Լ����ͨ��Լ������ɾ��
-- alter table tableName drop constraint constraintName1,constraintName2, ...
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
	EmpGender bit default(1) ,-- ע��bit�����ڽ���insert ����ֵʱֻ��д1 �� 0�����ڴ�����ɸѡ�ж�ʱӦ��д true(1) �� false(0)
	EmpJoinDate datetime,
	EmpAge int check(EmpAge >= 0 and EmpAge <= 120),
	EmpAddress nvarchar(300),
	EmpPhone varchar(100),
	EmpEmail varchar(100) not null unique,
	DeptId int foreign key references Department(Id) on delete cascade 
	-- ����ɾ���������ɾ��Department���е����ݣ���ôEmployee�������õ�Department���ж�ӦId����������Ҳ�ᱻɾ��
	-- ��ʵ����Ŀ�в��������ü���ɾ����һ���������ϵ��Ϊ����ϵ/����
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



---------------------------------2020��10��30��-------------------------------------
/*
					having��䣨�����ɸѡ����Щ����ʾ��Щ�鲻��ʾ��
�Ա��е����ݷ���󣬻�õ�һ�������Ľ��������ζԸý�����ڽ���ɸѡ��having
ע��Having�в���ʹ��δ���������У�Having�������where��
���ò�һ����Having�Ƕ�����й��ˣ�where�Ƕ�ÿ����¼���й��˵�
Having��Group By�������Է��������ݽ���ɸѡ����where���ƣ�����ɸѡ���ݣ�ֻ����having������ɸѡ���������
��where�в���ʹ�þۺϺ���������ʹ��Having��HavingҪλ��Group by֮��
Having��ʹ�ü�����whereһ����Ҳ������in
	��Having count(*) in (5,8,10)
*/
select 
	 �༶Id = tClassId,
	 �������� = count(*)
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
-- ��ʹ���˷�����䣨group by�������ǾۺϺ�����ʱ����select��ѯ�б��в����ٰ������������������Ǹ���ͬʱҲ��������group by�Ӿ��У����߸���Ҳ��������ĳ���ۺϺ�����

-- �Է����Ժ�����ݽ���ɸѡ��ʹ��having
-- having��where���Ƕ����ݽ���ɸѡ��where�ǶԷ���ǰ��ÿһ�����ݽ���ɸѡ����having�ǶԷ�����ÿһ�����ݽ���ɸѡ
select				    --4
	tClassId as �༶Id,
	�༶���� = count(*)
from TblStudent			--1
group by tClassId		--2
having COUNT(*) > 0		--3
order by �༶���� desc	--5



SQL����ִ��˳��
5.select 
	5.1ѡ������
	5.2distinct
	5.3top��Ӧ��topѡ�������㣩
1.from ��
2.where ����
3.group by ��
4.having ɸѡ����
6.order by ��

SELECT���Ĵ���˳��
���²�����ʾSELECT���Ĵ���˳��
	1.FROM
	2.ON
	3.JOIN
	4.WHERE
	5.GROUP BY
	6.WITH CUBE �� WITH ROLLIP
	7.HAVING
	8.SELECT
	9.DISTINCT
	10.ORDER BY
	11.TOP


--������Ʒ������������ÿ����Ʒ����������������
select
	��Ʒ����
	sum(��������) as ��������
from MyOrders
group by ��Ʒ����
order by �������� desc


--ͳ�������ܼ۳���3000Ԫ����Ʒ���ƺ������ܼۣ����������ܼ۽�������
select 
	��Ʒ����,
	�����ܼ� = sum(�������� * ���ۼ۸�)
from MyOrders
group by ��Ʒ����
having sum(�������� * ���ۼ۸�) > 3000
order by �����ܼ� desc


--ͳ�Ƹ����ͻ��ԡ��ɿڿ��֡���ϲ���ȣ���ͳ��ÿ�������˶ԡ��ɿڿ��֡��Ĺ�������
select 
	������,
	�������� = sum(��������)
from MyOrders
where ��Ʒ���� = '�ɿڿ���'
group by ������
order by �������� desc



/*
����ת��������
	��cast(_expression,as data_type)
	��convert(data_type,expression,[style])
		select 'Number' + 1; ��������Ϊ�����+����ѧ�����
*/



------------------------------------------2020��11��4��----------------------------
/*
					���Ͻ����union�������������
������������Ƕ��������ϲ����ģ��������ϱ��������ͬ���������о�����ͬ�����ݽṹ����������ʽת���ģ�����������ļ��ϵ�������һ�����ϵ�������ȷ�����������������Ӷ�������
�����ϣ�union�������ӣ�join����һ��
���򵥵Ľ�������ϣ�
	select tName,tSex from techer union
	select sName,sSex from student
��������ԭ��ÿ���������������ͬ��������ÿ����������б�����������
�����ϣ������������ϲ���һ���������
��union��Ĭ��ȥ���ظ����൱��Ĭ��Ӧ����distinct��
��union all
������Ӧ�ã��ײ����ܡ�ʹ�� union all������Ĭ��ȥ�أ�
*/
-- ʹ��union���Ͻ����
select 
	tName,tGender,tAge
from TblStudent
union all
select
	fName,fGender,fAge
from MySdtuent


--ʹ��union��union all���ܽ������ϣ��������ڣ�ʹ��union���ϻ�ȥ���ظ��������������ݣ���union all����ȥ���ظ�Ҳ������������
select tName,tGender,tAge from TblStudent
union
select fName,fGender,fAge from MyStudent
--union �ϲ�������ѯ����������ҽ�������ȫ�ظ������ݺϲ�Ϊһ��
--union��ΪҪ�����ظ�ֵɨ��,����Ч�ʵ�,����������ȷ��Ҫȥ���ظ���,��ʹ��union all

--���������£����ϵ�ʱ����Ҫȥ���ظ���ͬʱҪ�������ݵ�˳������һ�㽨��ʹ��union all


--��MyOrder����ͳ��ÿ����Ʒ�������ܼۣ������ڵײ�������
select
	��Ʒ����,
	�����ܼ� = sum(���ۼ۸� * ��������)
from MyOrders
group by ��Ʒ����
union all
select '�����ۼ۸�',sum(���ۼ۸� * ��������) from MyOrders
order by �����ܼ� asc


--��ѯ�ɼ����е�:��߷�,��ͷ�,ƽ����
select 
	max(tMath) as ��߷�,
	min(tMath) as ��ͷ�,
	avg(tMath) as ƽ����
from TblScore

select 
	��߷� = (select max(tMath) from TblScore),	
	��ͷ� = (select min(tMath) from TblScore),	
	ƽ���� = (select avg(tMath) from TblScore)
from TblScore

select ����='��߷�', ���� = max(tMath) from TblScore
union all
select ����='��ͷ�', ���� = min(tMath) from TblScore
union all
select ����='ƽ����', ���� = avg(tMath) from TblScore


--ʹ��union all����в����������
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


--ʹ��union����в���������ݣ����Զ�ȥ���ظ�
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


--һ�β����������
--�����еı�����ݲ��뵽�±�(���ܴ���),Ϊ������
--newStudent��ʾ��ִ��select into���ʱ������
--select into ��䲻���ظ�ִ�У���Ϊÿ��ִ�ж��ᴴ��һ��newStudent��
--TblStudent ��ṹ�����Զ�����ж�����newStudent�д���������TblStudent���е�Լ�������������newStudent����
select * into newStudent from TblStudent 

--(newStudent����select ��ѯʱͬʱ�Զ�����)
--�����б�����ݸ��Ƶ�һ���Ѵ��ڵı�ͨ�����ַ�ʽ���ƣ�ֻ�ܸ��Ʊ�ṹ���Լ��е����ֺ��������ͣ�����Լ�������Ḵ�ƹ���
--ֻ������ṹ������������
select * into newTbl from oldTbl where 1 != 1
--����������ֻ���Ʊ�ṹ����Ч�ʲ��ߣ����飺
select top 0 * into newTbl from oldTbl

--������Ѿ�������
insert into backupStudent select * from TblStudent
--(backupStudent�������ǰ����)


select top 0 * into newStudent from TblStudent
select * from newStudent




-----------------------------2020��11��10��-----------------------------
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


insert into MartialArtsMaster values('���߹�',70,'ؤ��','�򹷰���',10)
,('����',22,'ؤ��','����ʮ����',10)
,('������',50,'����','���Ǵ�',1)
,('��������',35,'����','��������',10)
,('��ƽ֮',23,'��ɽ','��������',7)
,('����Ⱥ',50,'��ɽ','��������',8)

insert into Kongfu values('����ʮ����',95)
,('��������',100)
,('���Ǵ�',10)


select * from MartialArtsMaster as m
where m.Level > 8 and m.Menpai = 'ؤ��'
-- LINQ query
--var list = from m in MartialArtsMaster
--			where m.Level > 8 and m.Menpai == "ؤ��"
--LINQ method
--var lis2t = MartialArtsMaster.Where(m => m.Level > 8 && m.Menpai == "ؤ��");


select 
	m.Id,m.Name,m.Age,m.Menpai,m.Kungfu,m.Level
from MartialArtsMaster as m 
left join Kongfu as k on k.KongfuName = m.Kungfu
where k.Lethality > 90 
order by m.Level
--LINQ query
--var list = from m in MartialArtsMaster 
--			from k in Kongfu
--			where k.Lethality > 90 && m.Kungfu == k.KongfuName
--			order by m.Level
--			select m.Id + m.Name + m.Age + m.Menpai + m.Kungfu + m.Level




-----------------------------2020��12��21��-----------------------------
declare @name nvarchar(50),@age int

-- Ϊ������ֵ
set @name = 'Tim'

select @age = 10

-- ���
select 'name',@name
select 'age',@age


-- ����
--print 'fs',@name

--whileѭ��
declare @i int = 1-- ����������ͬʱ��ֵ

while @i <= 10
begin
	print 'Hello'
	set @i = @i + 1
end


declare @i int = 1
declare @sum int = 0
while @i <= 100
begin
	set @sum = @sum + @i
	set @i += 1
end
select 'sum',@sum


declare @n int = 10
if @n > 10
	begin
		print '@n > 10'
	end
else if @n > 5
	begin
		print '@n > 5'
	end
else
	begin
		print '@n <= 5'	
	end


declare @sum int = 0,@i int = 1
while @i <= 100
	begin
		if @i % 2 = 0
			begin
				set @sum = @sum + @i
			end
		set @i += 1
	end
print @sum


-- ����@@���ſ�ͷ��һ�㶼��ϵͳ����
print @@version -- sqlserver�汾��Ϣ
/*
Microsoft SQL Server 2019 (RTM) - 15.0.2000.5 (X64) 
	Sep 24 2019 13:48:23 
	Copyright (C) 2019 Microsoft Corporation
	Developer Edition (64-bit) on Windows 10 Pro 10.0 <X64> (Build 18363: )
*/

print @@error -- ���һ��T-SQL������Ĵ����

print @@language -- ��ǰʹ�õ����Ե�����

print @@max_connections -- ���Դ���ͬʱ���ӵ������Ŀ

print @@rowcount --����һ��SQL���Ӱ�������

print @@servername -- ���ط�����������



create table bank
(
	cId int,
	balance int check(balance > 0)
)

insert into bank values
(
	1,1000
),
(
	2,90
)

select * from bank

-- ��ô��֤����SQL���ͬʱִ�гɹ�����ͬʱִ��ʧ���أ�
--ʹ����������֤
update bank set balance -= 10 where cId = 2
update bank set balance += 10 where cId = 1


--��һ������
begin transaction
declare @sum int = 0
	update bank set balance -= 100 where cId = 1
	set @sum += @@ERROR
	
	update bank set balance += 100 where cId = 2
	set @sum += @@ERROR
	--ֻҪ���κ�һ��SQL���ִ�г�����ô���@sum������ֵ�Ͳ�Ϊ0

	if @sum != 0
		begin
			--��ʾ����ִ�г�����
			rollback -- �ع�
		end
	else
		begin
			--���û�г���,���ύ����
			commit -- �ύ
		end


/*
ʲô������(Transaction)
`����:ͬ������
`ָ���ʲ����ܸ������ݿ��и����������һ������ִ�е�Ԫ(unit)--������ɶ��SQL������,������Ϊһ������ִ��
`��ЩSQL��������Ϊһ������һ����ϵͳ�ύ,Ҫô��ִ��,Ҫô����ִ��
�﷨����:
	1.��ʼ����:begin transaction
	2.�����ύ:commit transaction
	3.����ع�:rollback transaction
�ж�ĳ�����ִ���Ƿ����:
	`ȫ�ֱ���@@error
	`@@errorֻ���жϵ�ǰһ��T-SQL���ִ���Ƿ�����,Ϊ���ж�����������T-SQL����Ƿ��д�,������Ҫ�Դ�������ۼ�
	����:set @errorCount = @errorCount + @@error
set implicit_transactions{ on | off} ��ʽ����|�ر�����
*/


--�Զ��ύ����
--��ִ��һ��sql���ʱ�����ݿ���Զ������Ǵ�һ�����񣬵����ִ�гɹ������ݿ��Զ��ύ����ִ��ʧ�ܣ����ݿ��Զ��ع�����

--��ʽ����
--ÿ��ִ��һ��SQL���ʱ�����ݿ��Զ������Ǵ�һ�����񣬵�����Ҫ�����ֶ��ύ���߻ع�����
set implicit_transactions { on | off} -- ��ʽ����
set implicit_transactions on
--�������Ϊ on������������Ϊ��ʽ����ģʽ���������Ϊ off�������ӻָ�Ϊ�Զ��ύ����ģʽ
insert into bank values(3,1000000)
commit 
set implicit_transactions off
select * from bank

--��ʽ����:��Ҫ�ֶ�������,�ֹ��ύ���߻ع�����
begin tran -- transaction ����д

commit tran

rollback tran


/*
����ACID���ԣ�
��������Ϊ�����߼�������Ԫִ�е�һϵ�в�����һ���߼�������Ԫ�������ĸ����ԣ���Ϊԭ���ԡ�һ���ԡ������Ժͳ־��ԣ�ACID�����ԣ�ֻ���������ܳ�Ϊһ������
ԭ���� Actomicity:������һ�������Ĳ���,����ĸ��������ǲ��ɷֵ�,Ҫôִ��,Ҫô��ִ��
	���������ԭ�ӹ�����Ԫ�������������޸ģ�Ҫôȫ��ִ�У�Ҫôȫ����ִ��

һ���� Consistency:���������ʱ,���ݱ��봦��һ��״̬
	���������ʱ������ʹ���е����ݶ�����һ��״̬����������ݿ��У����й��򶼱���Ӧ����������޸ģ��Ա����������ݵ������ԡ�
	�������ʱ�����е��ڲ����ݽṹ����B��������˫����������������ȷ��

������ Isolation:�����ݽ����޸ĵ����в�������˴˸���,�������������Ƕ�����,����Ӧ���κη�ʽ�����ڻ�Ӱ����������
	�ɲ��������������޸ı������κ��������������������޸ĸ��롣����ʶ������ʱ����������״̬��Ҫô����һ���������޸���֮ǰ��
	״̬��Ҫô�ǵڶ����޸���֮���״̬�����񲻻�ʶ���м�״̬�����ݡ����Ϊ�ɴ����ԣ���Ϊ���ܹ�����װ����ʼ���ݣ������ز�һ
	ϵ��������ʹ���ݽ���ʱ��״̬��ԭʼ����ִ�е�״̬��ͬ��

�־��� Durability:������ɺ�,�������ݿ���޸ı����ñ���,������־�ܹ����������������
	�������֮��������ϵͳ��Ӱ���������Եġ����޸ļ�ʹ����ϵͳ����Ҳ��һֱ���֡�
*/




-----------------------------2020��12��22��-----------------------------
/*
�洢���̣�
	�����ݿ��б���Ĵ洢������䶼�Ǳ�����ģ�ִ���ٶȸ���
ϵͳ�洢���̣�
	��ϵͳ���壬�����master���ݿ���
	�����ɡ�sp_�����ߡ�xp_����ͷ���Զ���Ĵ洢���̿����� usp_ ��ͷ
ϵͳ�洢���̣�
	sp_databases���г��������ϵ��������ݿ�
	sp_helpdb�������й�ָ�����ݿ���������ݿ����Ϣ
	sp_renamedb���������ݿ������
	sp_tables�����ص�ǰ�����¿ɲ�ѯ�Ķ�����б�
	sp_columns������ĳ�����е���Ϣ
	sp_help���鿴ĳ�����������Ϣ
	sp_helpconstraint���鿴ĳ�����Լ��
	sp_helpindex���鿴ĳ���������
	sp_stored_procedures���г���ǰ�����е����д洢����
	sp_password����ӻ��޸ĵ�¼�˻�������
*/

-----------------------------ϵͳ�洢����-----------------------------
--1.���ص�ǰʵ���е����е����ݿ�Ļ�����Ϣ
exec sp_databases
--2.���ص�ǰ���ݿ��µ����еı�
exec sp_tables
--3.����ĳ�ű��µ����е���
exec sp_columns 'bank'

--4.�鿴ĳ���洢���̵�Դ��
exec sp_helptext 'sp_databases'


create procedure sys.sp_databases  
as  
    set nocount on  

    select  
        DATABASE_NAME   = db_name(s_mf.database_id),  
        DATABASE_SIZE   = convert(int,  
                                    case -- more than 2TB(maxint) worth of pages (by 8K each) can not fit an int...  
                                    when sum(convert(bigint,s_mf.size)) >= 268435456  
                                    then null  
                                    else sum(convert(bigint,s_mf.size))*8 -- Convert from 8192 byte pages to Kb  
                                    end),  
        REMARKS         = convert(varchar(254),null)  
    from  
        sys.master_files s_mf  
    where  
        s_mf.state = 0 and -- ONLINE  
        has_dbaccess(db_name(s_mf.database_id)) = 1 -- Only look at databases to which we have access  
    group by s_mf.database_id  
    order by 1  

-----------------------------�����Լ��Ĵ洢����
create proc usp_say_hello
as 
begin 
	print 'HELLO WORLD'
end

exec usp_say_hello

drop proc usp_select_tblStudent

alter proc usp_select_tblStudent
as 
begin
	select * from TblStudent where tGender = '��'
end

--���ô洢����
exec usp_select_tblStudent


--����һ�������������Ĵ洢����
create proc usp_add_number
@n1 int,
@n2 int
as 
begin
	select @n1 + @n2
end

exec usp_add_number

create proc usp_select_tblstudent_by_condition
@gender char(2),
@age int
as
begin
	select * from TblStudent where tAge >= @age and tGender = @gender
end 

exec usp_select_tblstudent_by_condition @gender = '��',@age = 15

/* 
�����洢����
������洢���̵��﷨
	CREATE PROC[EDURE] �洢������
	@����1 �������� = Ĭ��ֵ OUTPUT,
	@����n �������� = Ĭ��ֵ OUTPUT
	AS
		SQL ���
������˵����
	������ѡ
	������Ϊ����������������
	���������Ϫ��Ĭ��ֵ
��EXEC �������� [����]

*/

drop proc usp_add_number
create proc usp_add_number
@n1 int,
@n2 int = 50
as
begin
	select @n1 + @n2
end
exec usp_add_number 80
-- ���ô洢���̵Ĳ�����Ĭ��ֵ



-----------------------------2021��9��17��-----------------------------
/* 
case��ѯ
*/
--1.Ҫ�� then ������������ͱ���һ��
--��������д���൱�� C# �е� if-else��
select 
	*
	title = case
				when [level] = 1 then '����'
				when [level] = 2 then '����'
				when [level] = 3 then '��ʦ'
				else '�ǻҼ���ʦ'
			end
from user

--�൱�� C# �е� switch��
select 
	*
	title = case [level]
				when 1 then ''
				when 2 then ''
				when 3 then ''
				else ''
			end 
from user

--��ϰ��
select 
	*,
	title = case 
				when tEnglish >= 95 then '��'
				when tEnglish >= 80 then '��'
				when tEnglish >= 70 then '��'
				else '��'
			end
from TblScore

select 
	*,
	isPass = case
				when tEnglish >= 60 and tMath >= 60 then 'pass'
				else 'not pass'
			 end
from TblScore

select 
	x = case 
			when A > B then A
			else B
		end,
	Y = case 
			when B > C then B
			else C
		end
from TestA

--�ڶ������У�ͳ��ÿ������Ա�������۽��г�����Ա���������۽��ƺţ�>6000���ƣ�>5500���ƣ�>4500ͭ�ƣ�������ͨ��
select * from MyOrders
select 
	salesman,
	total = sum(price * number),
	title = case
				when sum(price * number) > 6000 then 'gold'
				when sum(price * number) > 5500 then 'silver'
				when sum(price * number) > 4500 then 'copper'
				else 'general'
			end
from MyOrders
group by salesman

--way one
select
	teamName,
	win = sum(case 
			when gameResult = 'win' then 1
			else 0
		  end),
	loose = sum(case
			  when gameResult = 'loose' then 1
			  else 0
			end)
from TeamScore
group by teamName

--way two
select
	teamName,
	win = count(case 
			when gameResult = 'win' then 'win'
			else null
		  end),
	loose = count(case
			  when gameResult = 'loose' then 'loose'
			  else null
			end)
from TeamScore
group by teamName

--create table NBAScore (
--	[autoId] int null,
--	[teamName] nvarchar(100) null,
--	[seasonName] nvarchar(100) null,
--	[Score] int null
--)

--insert into NBAScore(autoId,teamName,seasonName,Score) values
--	(2,'ɭ����','��2����',20),
--	(3,'ɭ����','��3����',15),
--	(4,'�촬','��1����',7),
--	(5,'�촬','��2����',12),
--	(6,'�촬','��3����',11),
--	(7,'������','��1����',17),
--	(8,'������','��2����',12),
--	(9,'������','��3����',15),
--	(10,'���˹','��1����',6),
--	(11,'���˹','��2����',18),
--	(12,'���˹','��3����',7);

SELECT *
FROM [Test].[dbo].[NBAScore]

select 
	teamName,
	��1���� = max(case
				when seasonName = '��1����' then score 
				else null
			end),
	��2���� = max(case
				when seasonName = '��2����' then score
				else null
			end),
	��3���� = max(case
				when seasonName = '��3����' then score
			end)
from [NBAScore]
group by teamName