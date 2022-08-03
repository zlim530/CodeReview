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

语法元素				解释
<...>				术语，待解释
::=					对术语的解释、单层开展
[...]				可选的元素，方括号表示此元素可选也即可有可无
{...|...}			必选其一：两者中必选一个，花括号表示元素必须有
[...n]				对前面元素的0到多次重复
[,...n]				对前面元素的0到多次重复，用逗号隔开

SELECT 语句必须出现的元素：最简单形式：
<SELECT statement> ::=
	SELECT <select_list>

*/
--DECLARE @N INT = 3 -- 声明一个变量 n 类型为 int，并赋值 3
--SELECT POWER(324, @N) -- POWER() SqlServer 内置的函数：查询给定值的 n 次方

-- 通过 UNION 来找寻并集：实际工作中并不会这么使用，下面的 SQL 语句没有任何意义，仅是演示 UNION 的用法
--SELECT FirstName FROM Person.Person
--UNION
--SELECT Name FROM Production.Product
--UNION 
--SELECT PhoneNumber FROM Person.PersonPhone

--SELECT FirstName FROM
--(SELECT FirstName, LastName FROM Person.Person ) AS FullName