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