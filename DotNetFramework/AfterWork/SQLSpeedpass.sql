/*
Syntax for SQL Sever and Azure SQL Database

<SELECT statement> ::=
	[WITH { [ XMLNAMESPACES,] [ <common_table_expreesion> [,...n]]}]
	<query_expreesion>
	[ORDER BY <order_by_expreesion>]
	[<FOR Clause>]
	[OPTION(<query_hint>[,...n])]
<query_expreesion> ::=
	{<query_specification> |(<query_expreesion>)}
	[ {UNION [ALL] | EXCEPT | INTERSECT}
		<query_sepecification> | (<query_expression>)[...n]]
<query_sepcification> ::=
SELECT [ ALL | DISTINCT ]
	[TOP (expression) [PERCENT] [WITH TIES]]
	<select_list>
	[INTO new table]
	[FROM {<table_source>}[,...n]]
	[WHERE <search_condition>]
	[<GROUP BY>]
	[HAVING <search_condition>]

�﷨Ԫ��				����
<...>				���������
::=					������Ľ��͡����㿪չ
[...]				��ѡ��Ԫ�أ������ű�ʾ��Ԫ�ؿ�ѡҲ�����п���
{...|...}			��ѡ��һ�������б�ѡһ���������ű�ʾԪ�ر�����
[...n]				��ǰ��Ԫ�ص�0������ظ�
[,...n]				��ǰ��Ԫ�ص�0������ظ����ö��Ÿ���

SELECT ��������ֵ�Ԫ�أ������ʽ��
<SELECT statement> ::=
	SELECT <select_list>

*/
--DECLARE @N INT = 3 -- ����һ������ n ����Ϊ int������ֵ 3
--SELECT POWER(324, @N) -- POWER() SqlServer ���õĺ�������ѯ����ֵ�� n �η�

-- ͨ�� UNION ����Ѱ������ʵ�ʹ����в�������ôʹ�ã������ SQL ���û���κ����壬������ʾ UNION ���÷�
--SELECT FirstName FROM Person.Person
--UNION
--SELECT Name FROM Production.Product
--UNION 
--SELECT PhoneNumber FROM Person.PersonPhone

--SELECT FirstName FROM
--(SELECT FirstName, LastName FROM Person.Person ) AS FullName


/*
FROM �Ӿ���﷨��

-- Syntax for SQL Server and Azure SQL Database  
  
[ FROM { <table_source> } [ ,...n ] ]   
<table_source> ::=   
{  
    table_or_view_name [ FOR SYSTEM_TIME <system_time> ] [ [ AS ] table_alias ]   
        [ <tablesample_clause> ]   
        [ WITH ( < table_hint > [ [ , ]...n ] ) ]   
    | rowset_function [ [ AS ] table_alias ]   
        [ ( bulk_column_alias [ ,...n ] ) ]   
    | user_defined_function [ [ AS ] table_alias ]  
    | OPENXML <openxml_clause>   
    | derived_table [ [ AS ] table_alias ] [ ( column_alias [ ,...n ] ) ]   
    | <joined_table>   
    | <pivoted_table>   
    | <unpivoted_table>  
    | @variable [ [ AS ] table_alias ]  
    | @variable.function_call ( expression [ ,...n ] )   
        [ [ AS ] table_alias ] [ (column_alias [ ,...n ] ) ]     
}  

��������Ϊʲô���󡱣�
	-Velocity�����٣�
	-Volume��������
	-Variety����������
������Ҫ�� <table_source>
	-table_or_view_name
	-derived_table
	-<joined_table>
	-<pivoted_table>
��<table_source> �����ж���������
��alias������������Ҫ

������Ҫ�ĸ���
��What is a table?
	-Table are database objects that contain all the data in a database.
	 In tables, data is logically organized in a row-and-column format.
	-Each row represents a unique record.
	-Each column represents a field in the record.
		�еġ�Ψһ�ԡ���������primary key������֤

��What is a view?
	-A view is a virtual table whose contents are defined by a query.
	-A view acts as a filter on the underlying tables.

��table_or_view_name
	-Is the name of a table or view.

��derived_table��������
	-Is a subquery that retrieves rows from the database.
	-derived_table is used as input to the other query.
*/

--select P.FirstName + ' ' + P.LastName AS FullName
--from Person.Person AS P

--select FirstName + ' ' + LastName AS FullName,
--SUBSTRING(FirstName, 1, 1 ) + SUBSTRING(LastName, 1, 1) AS Iniital
--from Person.Person
/*
�����У�SUBSTRING(LastName,1,1)����˼�ǣ���ȡLastName���е��ַ�������Ϊ���ӵ�һ����ĸ��ʼ����ȡһ���ַ�
*/



-- 006-������������Ħ
SELECT FirstName FROM Person.Person				-- 19972
SELECT DISTINCT FirstName FROM Person.Person	-- 1018
SELECT DISTINCT FirstName, LastName FROM Person.Person	-- 19516
-- ȥ������FirstName��LastName���ظ��У�ֻ����1�� 


--ѡ�����������п�ǰ���У���ͨ�����ʽ�Ȳ���ѡ������ǰ��������С�������Ĭ�ϵģ���Ҳ����ORDER�����������������.
SELECT TOP (7) FirstName, LastName
FROM Person.Person
--ORDER BY FirstName
--��Person.Person��ѡ��FirstName��LastName���У�����ֻѡ��ǰ7��������FirstName����ĸ���С�

--�ɼ��Ͽ�ѡ�İٷֱȣ� 
SELECT TOP(7) PERCENT FirstName, LastName
FROM Person.Person
ORDER BY FirstName
--��Person.Person��ѡ��FirstName��LastName���У�����ѡ���е�ǰ7%.



--007-Դ���ݵ�ɸѡ��CTE:
SELECT * FROM Person.Person
WHERE FirstName = 'Timothy'

--����WHERE�Ӿ䣬��ѯ����InitialΪTL���ˣ���ʾ�����ǵ�ȫ����������β��������������β��ƴ��������Initial
SELECT FirstName + ' ' + LastName AS FullName,
SUBSTRING(FirstName, 1, 1) + SUBSTRING(LastName, 1, 1) AS Initial
FROM Person.Person

--ʹ�� CONCAT ����������д������������
SELECT CONCAT(FirstName , ' ' , LastName) AS FullName,
CONCAT(SUBSTRING(FirstName, 1, 1) , SUBSTRING(LastName, 1, 1)) AS Initial
FROM Person.Person 
--WHERE Initial = 'TL'-- ���� 'Initial' ��Ч��
--������д�����Ϸ�����ΪWHERE���ֻ�ܹ�ɸѡ����Դ�е����ݡ���FROM���������ԴPerson.Person���в�û��Initial��һ��.
WHERE CONCAT(SUBSTRING(FirstName, 1, 1) , SUBSTRING(LastName, 1, 1)) = 'TL' -- ������ôд

--Ҳ���Խ�������ѯ�����Ϊһ������Դ����ʱ Initial �оʹ���������Դ��
SELECT * FROM
(SELECT CONCAT(FirstName , ' ' , LastName) AS FullName,
CONCAT(SUBSTRING(FirstName, 1, 1) , SUBSTRING(LastName, 1, 1)) AS Initial
FROM Person.Person) AS PersonName
WHERE Initial = 'TL'
-- ��������д�����׷�����Ҫһ�����ģ������γ� SELECT һ���Ƕ�� SELECT �����

-- �ռ�д����Common Table Expression(CTE)������SELECTǶ�׵�SELECT��ѯ�����ǰ����������Ҫʹ�õ�WHERE�Ӿ�����ѯʱ�Ϳ���ֱ�ӷ����ʹ�����������ı��ˣ�
WITH PersonName AS
(SELECT CONCAT(FirstName , ' ' , LastName) AS FullName,
CONCAT(SUBSTRING(FirstName, 1, 1) , SUBSTRING(LastName, 1, 1)) AS Initial
FROM Person.Person) 

SELECT * 
FROM PersonName
WHERE Initial = 'TL'



--007.002-���ݵıȽ�
/*
��������
Data Type Group				Big Group	Data Types
Exact numeric				��ֵ			bigint,numeric,bit,smallint,decimal,smallmoney,int,tinyint,money
Approximate numeric			��ֵ			float,real
Date and time				����/ʱ��	date,datetimeoffset,datetime2,smalldatetime,datetime,time
Character strings			�ַ���		char,varchar,text
Unicode character strings	�ַ���		nchar,varchar,ntext  
										ֻҪ��Ҫ�洢��Ӣ��֮����ַ�����Ҫ�� Unicode �ַ������������
Binary strings				����			binary,varbinary,image
Other data types			����			cursor,rowversion,hierarchyid,uniqueidentifier,sql_variant,xml,table...

�Ƚϲ�����
Operator						Meaning
=(Equals)						Equal to
>(Greater Than)					Greater than
<(Less Than)					Less than
>=(Greater Than or Equal To)	Greater than or equal to
<=(Less Than or Equal To)		Less than or equal to
<>(Not Equal To)				Not equal to
!=(Not Equal to)				Not equal to(not ISO standard)��
								��ʾ���� SQL ͨ�õģ�����Ϊ�˼��� C-like language programmer �ı��ϰ��
!<(Not Less Than) = >=			Not less than(not ISO standard)
!>(Not Greater Than) = <=		Not greater than(not ISO standard)

ͬ��Ƚϣ���ͬ���������͵����ݽ��бȽϣ�
--CAST Syntax:
CAST(expression AS data_type[(length)])

--CONVERT Syntax:
CONVERT(data_type[(length)], expression[, style])
*/
--�����ǳ��ԱȽϲ�ͬ����������ʱ��SQL Server ���ȡ�Ⱥ���ߵ��������ͣ������ȡ�Ⱥ��ұߵ��������ͣ������������ߵ��������Ͳ�һ��ʱ�����Խ��ұߵ�������ʽ��ת��Ϊ��ߵ��������ͣ�������ʧ�ܵ�ԭ��ʱ100.1�޷�ת��Ϊ int ���ͣ����ǿ���ͨ����ʽת��Ϊ float ���Ͷ������� int ���͵�100���бȽ�
--if 100 = '100.1'--�ڽ� varchar ֵ '100.1' ת������������ int ʱʧ�ܡ���Ϊ SQL Server ��ʽ����ת���������ģ�100 = CAST('100.1' AS int) ���Կ϶�ʧ���ˣ���Ϊ100.1��С���㣬�����޷�ת��Ϊ����
if 100 = CAST('100.1' AS float)-- correct
	-- �����ǳ��ԱȽ� 100 = '100' ʱ֮�����ܳɹ�����ʵ SQL Server Ϊ�������Ĳ������� 100 = CAST('100' AS int) 
	select 'True'
else 
	select 'False'

--��ѧ��������e-1 ��ʾ10�ĸ�һ�η����������ǿ���ʹ�ÿ�ѧ��������¼�������ģ����Ҳ֧�����ߵıȽ� => ��ֵ���͵ıȽ�
--if 100.1 = 1001e-1
	--select 'True'
--if 100.1 = '1001e-1'-- ���������� varchar ת��Ϊ numeric ʱ�����������Ƚϸ��ӵĿ�ѧ��������ɵ��ַ���ʱ�����ܵ���ʽת���������ʱ����Ҫ��д��ʽ������ת��
	--select 'True'
if 100.1 = CAST('1001e-1' AS float)
	select 'True'
else
	select 'False'

------------------�ַ����ıȽ�------------------
if 'tim' = 'TIm' -- ע�⣺SQL Server �е��ַ����Ƚ��Ǻ��Դ�Сд��
	select 'True'
else
	select 'False'


------------------ʵ������------------------
select * from Sales.SalesOrderHeader
where OrderDate < '2012-01-01'
-- ����� OrderDate �� datetime ���͵ģ����ڴ���һ�� datetime ���͵����ݱȽ��鷳��������������ʹ���ַ������� SQL Server ���ܵ���ʽת��Ϊ datetime ���� => OrderDate < CAST('2012-01-01' AS datetime)

declare @a as real = 1.0, @b as real = 3.0
declare @x as float = 1.0, @y as float = 3.0

--Ϊʲô˵�����������в�׼ȷ����Ϊ�ڼ�����д洢���ݵ��ڴ������޵ģ����������� float �ľ��ȱ� real ���͵ľ��ȸߣ����Ե���ͬ�� 1.0/3.0 ȴ�������һ���Ľ���������ʱ��Ҫ�����������������ֵ�Ƚϣ�������Ϊ����Ӧ����ͬ���������ʵ�ǲ�ͬ��
select @a/@b, -- 0.3333333
		@x/@y -- 0.333333333333333

if @a/@b = @x/@y
	select 'Yes'
else
	select 'No'




--007.003-�ַ�����ɸѡ
select firstName, LastName from person.Person
where FirstName = 'timothy' -- ִ�д˲�ѯ��䷢�� SQL Server �Ǵ�Сд�����е� 

select name, collation_name from sys.databases
/*
��ѯ��ǰ��ͬ���ݿ�� collation ���鼯������ Person.Person ���ڵ� AdventureWork2019 ���ݿ��� 
SQL_Latin1_General_CP1_CI_AS�������У�����ǲ����ִ�Сд�ģ��������� SQL ����������������Ϊ
'Timothy'��'timothy'
*/

select FirstName, LastName
from Person.Person
where FirstName = 'timothy' collate Latin1_General_CS_AS 
							-- �����ʱ�� SQL ���ĺ������ collate ��Լ��
							-- ����Է��ֲ�ѯ���Ϊ0������Ϊָ������� collation ��Լ���Ǵ�Сд���е�

-- LIKE �ؼ����� pattern��ģʽƥ�䣩ͨ�����ʹ�ã�ע�� LIKE ������һ���־䲻�� WHERE ���� FROM �Ӿ䣬����һ���߼���������� <��> �������һ��
--1.%��ƥ�䡰0���������ַ���
select FirstName,LastName 
from Person.Person
--where FirstName LIKE 'T%' -- %��0�����������ַ�������Ϊ0��Ҳ�� T ���� t Ҳ��ƥ���
where FirstName LIKE '%T%T%'-- �鿴����2�� T/t �����֣�T/t ���Գ���������λ�ã�ֻҪ����������

--2._��ƥ�䡰һ���ַ���
select distinct FirstName 
from Person.Person
--where FirstName LIKE '_im' -- _����������һ���ַ���������һ�������ܶ�Ҳ�����٣�����ֻ��һ��
where FirstName LIKE '___' -- �������ֳ���Ϊ3����������

--3.[]��ƥ�䷽���š���������һ���ַ�����[^]����ƥ�䷽�����еġ��κ��ַ���
select FirstName, LastName 
from Person.Person
--where FirstName LIKE '[abc]%'-- ���������� a/A��b/B��c/C �������ַ���ͷ����������
							 -- ���� a/A��b/B��c/C ��������ĸ���ܴ�СдҲƥ�䣬��Ϊ % ����ƥ��0���ַ�
--where FirstName LIKE '[a-e]%'-- a-e��ָ abcde
where FirstName LIKE '[^xyz]%' -- ���� x��y��z ������κ��ַ�

--4.ת�����
--select * from Drink
--where Description LIKE '%5%%' -- ��Ҫ��ѯ���� 5% �����ݣ����Ƿ���ֻҪ��5�����ݾͲ�ѯ����
							  -- ������Ϊ % ��ģʽƥ��ͨ���������ѯ����Դ�д��� %������Ҫ
							  -- ׼ȷƥ�� % ����ַ�ʱ�����Խ� % �� [] ��
--where Description LIKE '%5[%]%' -- ��ѯ���а��� 5% ����ַ���������
--where Description LIKE '%5/%%' ESCAPE '/' -- ���߿���ʹ�� ESCAPE �ؼ���ָ��ת���
										  -- ������Ҫ��ȷ������Դ���о��Բ�����ֵ�
										  -- �ַ���ʲô����������� /����ʾ�������
										  -- /���� / ������ַ���������ͨ���������
										  -- ��ͨ���ַ�

