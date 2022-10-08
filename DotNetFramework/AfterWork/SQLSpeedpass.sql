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



--007.002-数据的比较
/*
数据类型
Data Type Group				Big Group	Data Types
Exact numeric				数值			bigint,numeric,bit,smallint,decimal,smallmoney,int,tinyint,money
Approximate numeric			数值			float,real
Date and time				日期/时间	date,datetimeoffset,datetime2,smalldatetime,datetime,time
Character strings			字符串		char,varchar,text
Unicode character strings	字符串		nchar,varchar,ntext  
										只要需要存储非英语之外的字符就需要用 Unicode 字符，否则会乱码
Binary strings				其他			binary,varbinary,image
Other data types			其他			cursor,rowversion,hierarchyid,uniqueidentifier,sql_variant,xml,table...

比较操作符
Operator						Meaning
=(Equals)						Equal to
>(Greater Than)					Greater than
<(Less Than)					Less than
>=(Greater Than or Equal To)	Greater than or equal to
<=(Less Than or Equal To)		Less than or equal to
<>(Not Equal To)				Not equal to
!=(Not Equal to)				Not equal to(not ISO standard)：
								表示不是 SQL 通用的，而是为了兼容 C-like language programmer 的编程习惯
!<(Not Less Than) = >=			Not less than(not ISO standard)
!>(Not Greater Than) = <=		Not greater than(not ISO standard)

同类比较（即同种数据类型的数据进行比较）
--CAST Syntax:
CAST(expression AS data_type[(length)])

--CONVERT Syntax:
CONVERT(data_type[(length)], expression[, style])
*/
--当我们尝试比较不同的数据类型时，SQL Server 会获取等号左边的数据类型，而后获取等号右边的数据类型，当它发现两边的数据类型不一致时，则尝试将右边的数据隐式的转换为左边的数据类型，在这里失败的原因时100.1无法转换为 int 类型，但是可以通过显式转换为 float 类型而后再与 int 类型的100进行比较
--if 100 = '100.1'--在将 varchar 值 '100.1' 转换成数据类型 int 时失败。因为 SQL Server 隐式类型转换是这样的：100 = CAST('100.1' AS int) 所以肯定失败了，因为100.1有小数点，所以无法转换为整数
if 100 = CAST('100.1' AS float)-- correct
	-- 当我们尝试比较 100 = '100' 时之所以能成功，其实 SQL Server 为我们做的操作就是 100 = CAST('100' AS int) 
	select 'True'
else 
	select 'False'

--科学计数法：e-1 表示10的负一次方，国际上是可以使用科学计数法记录浮点数的，因此也支持两者的比较 => 数值类型的比较
--if 100.1 = 1001e-1
	--select 'True'
--if 100.1 = '1001e-1'-- 从数据类型 varchar 转换为 numeric 时出错。当给出比较复杂的科学计数法组成的字符串时，智能的隐式转换会出错，此时则需要手写显式的类型转换
	--select 'True'
if 100.1 = CAST('1001e-1' AS float)
	select 'True'
else
	select 'False'

------------------字符串的比较------------------
if 'tim' = 'TIm' -- 注意：SQL Server 中的字符串比较是忽略大小写的
	select 'True'
else
	select 'False'


------------------实操演练------------------
select * from Sales.SalesOrderHeader
where OrderDate < '2012-01-01'
-- 这里的 OrderDate 是 datetime 类型的，由于创造一个 datetime 类型的数据比较麻烦，所以我们这里使用字符串，让 SQL Server 智能的隐式转换为 datetime 类型 => OrderDate < CAST('2012-01-01' AS datetime)

declare @a as real = 1.0, @b as real = 3.0
declare @x as float = 1.0, @y as float = 3.0

--为什么说浮点数的运行不准确，因为在计算机中存储数据的内存是有限的，在这里由于 float 的精度比 real 类型的精度高，所以导致同是 1.0/3.0 却会算出不一样的结果，如果此时需要对这两个结果进行数值比较，我们以为它们应该相同，但结果其实是不同的
select @a/@b, -- 0.3333333
		@x/@y -- 0.333333333333333

if @a/@b = @x/@y
	select 'Yes'
else
	select 'No'




--007.003-字符串的筛选
select firstName, LastName from person.Person
where FirstName = 'timothy' -- 执行此查询语句发现 SQL Server 是大小写不敏感的 

select name, collation_name from sys.databases
/*
查询当前不同数据库的 collation 勘验集，发现 Person.Person 所在的 AdventureWork2019 数据库是 
SQL_Latin1_General_CP1_CI_AS，而这个校正集是不区分大小写的，所以上述 SQL 语句可以搜索到名字为
'Timothy'或'timothy'
*/

select FirstName, LastName
from Person.Person
where FirstName = 'timothy' collate Latin1_General_CS_AS 
							-- 如果此时在 SQL 语句的后面加上 collate 的约束
							-- 则可以发现查询结果为0条，因为指定的这个 collation 的约束是大小写敏感的

-- LIKE 关键字与 pattern（模式匹配）通配符的使用：注意 LIKE 并不是一个字句不像 WHERE 或者 FROM 子句，而是一个逻辑运算符，与 <、> 等运算符一样
--1.%：匹配“0到任意多个字符”
select FirstName,LastName 
from Person.Person
--where FirstName LIKE 'T%' -- %：0或者任意多个字符：可以为0，也即 T 或者 t 也是匹配的
where FirstName LIKE '%T%T%'-- 查看出现2次 T/t 的名字，T/t 可以出现在任意位置，只要有两个就行

--2._：匹配“一个字符”
select distinct FirstName 
from Person.Person
--where FirstName LIKE '_im' -- _：代表任意一个字符：必须有一个，不能多也不能少，有且只有一个
where FirstName LIKE '___' -- 查找名字长度为3的所有名字

--3.[]：匹配方括号“其中任意一个字符”，[^]：不匹配方括号中的“任何字符”
select FirstName, LastName 
from Person.Person
--where FirstName LIKE '[abc]%'-- 查找所有由 a/A、b/B、c/C 这三个字符开头的任意名字
							 -- 其中 a/A、b/B、c/C 这三个字母不管大小写也匹配，因为 % 可以匹配0个字符
--where FirstName LIKE '[a-e]%'-- a-e：指 abcde
where FirstName LIKE '[^xyz]%' -- 除了 x、y、z 以外的任何字符

--4.转义符：
--select * from Drink
--where Description LIKE '%5%%' -- 想要查询包含 5% 的数据，但是发现只要有5的数据就查询到了
							  -- 这是因为 % 是模式匹配通配符，当查询数据源中存在 %，又需要
							  -- 准确匹配 % 这个字符时，可以将 % 放 [] 中
--where Description LIKE '%5[%]%' -- 查询所有包含 5% 这个字符串的数据
--where Description LIKE '%5/%%' ESCAPE '/' -- 或者可以使用 ESCAPE 关键字指定转义符
										  -- 这里需要先确认数据源表中绝对不会出现的
										  -- 字符是什么，这里假设是 /，表示如果遇到
										  -- /，则 / 后面的字符将不再是通配符，而是
										  -- 普通的字符

