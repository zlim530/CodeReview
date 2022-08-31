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



-- 007.001-�����Ĳ�������