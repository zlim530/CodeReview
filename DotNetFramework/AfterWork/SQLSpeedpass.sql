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


/*
FROM 子句的语法：

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

·大数据为什么“大”？
	-Velocity（高速）
	-Volume（大量）
	-Variety（多样化）
·最重要的 <table_source>
	-table_or_view_name
	-derived_table
	-<joined_table>
	-<pivoted_table>
·<table_source> 可以有多个正交组合
·alias（别名）很重要

几个重要的概念
·What is a table?
	-Table are database objects that contain all the data in a database.
	 In tables, data is logically organized in a row-and-column format.
	-Each row represents a unique record.
	-Each column represents a field in the record.
		行的“唯一性”由主键（primary key）来保证

·What is a view?
	-A view is a virtual table whose contents are defined by a query.
	-A view acts as a filter on the underlying tables.

·table_or_view_name
	-Is the name of a table or view.

·derived_table（派生表）
	-Is a subquery that retrieves rows from the database.
	-derived_table is used as input to the other query.
*/

--select P.FirstName + ' ' + P.LastName AS FullName
--from Person.Person AS P

--select FirstName + ' ' + LastName AS FullName,
--SUBSTRING(FirstName, 1, 1 ) + SUBSTRING(LastName, 1, 1) AS Iniital
--from Person.Person
/*
·其中，SUBSTRING(LastName,1,1)的意思是：抽取LastName列中的字符，规则为：从第一个字母开始，抽取一个字符
*/



-- 006-给数据做个按摩
SELECT FirstName FROM Person.Person				-- 19972
SELECT DISTINCT FirstName FROM Person.Person	-- 1018
SELECT DISTINCT FirstName, LastName FROM Person.Person	-- 19516
-- 去除所有FirstName和LastName的重复行，只留下1行 


--选择数据中排列靠前的行，可通过表达式等操作选择保留靠前具体多少行。排行是默认的，但也可用ORDER语句来调整控制排序.
SELECT TOP (7) FirstName, LastName
FROM Person.Person
--ORDER BY FirstName
--从Person.Person中选择FirstName和LastName两列，并且只选择前7个，改用FirstName的字母排列。

--可加上可选的百分比： 
SELECT TOP(7) PERCENT FirstName, LastName
FROM Person.Person
ORDER BY FirstName
--从Person.Person中选择FirstName和LastName两列，并且选择列的前7%.



--007-源数据的筛选及CTE:
SELECT * FROM Person.Person
WHERE FirstName = 'Timothy'

--运用WHERE子句，查询所有Initial为TL的人，显示出他们的全名（首名和尾名）和由首名和尾名拼接起来的Initial
SELECT FirstName + ' ' + LastName AS FullName,
SUBSTRING(FirstName, 1, 1) + SUBSTRING(LastName, 1, 1) AS Initial
FROM Person.Person

--使用 CONCAT 函数对上述写法进行升级：
SELECT CONCAT(FirstName , ' ' , LastName) AS FullName,
CONCAT(SUBSTRING(FirstName, 1, 1) , SUBSTRING(LastName, 1, 1)) AS Initial
FROM Person.Person 
--WHERE Initial = 'TL'-- 列名 'Initial' 无效。
--但这种写法不合法。因为WHERE语句只能够筛选数据源中的数据。由FROM引入的数据源Person.Person当中并没有Initial这一列.
WHERE CONCAT(SUBSTRING(FirstName, 1, 1) , SUBSTRING(LastName, 1, 1)) = 'TL' -- 可以这么写

--也可以将上述查询结果作为一个数据源，此时 Initial 列就存在于数据源中
SELECT * FROM
(SELECT CONCAT(FirstName , ' ' , LastName) AS FullName,
CONCAT(SUBSTRING(FirstName, 1, 1) , SUBSTRING(LastName, 1, 1)) AS Initial
FROM Person.Person) AS PersonName
WHERE Initial = 'TL'
-- 但是这种写法容易犯错，需要一旦更改，容易形成 SELECT 一层层嵌套 SELECT 的情况

-- 终极写法：Common Table Expression(CTE)，将被SELECT嵌套的SELECT查询语句提前表达出来，当要使用到WHERE子句对其查询时就可以直接方便地使用声明出来的表了：
WITH PersonName AS
(SELECT CONCAT(FirstName , ' ' , LastName) AS FullName,
CONCAT(SUBSTRING(FirstName, 1, 1) , SUBSTRING(LastName, 1, 1)) AS Initial
FROM Person.Person) 

SELECT * 
FROM PersonName
WHERE Initial = 'TL'



-- 007.001-神隐的布尔类型