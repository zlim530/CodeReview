<?php
// 常量：不可改变的量
// PHP中常量的定义：
define('PI', 3.1415926);	// 一盘常量名都使用大写

$number = PI * 3 * 3;
var_dump(PI);// 使用常量的地方不再需要引号
var_dump($number);

PI = 101010;	//Parse error: syntax error, unexpected '=' in C:\wamp64\www\local\demo17.php on line 10 如果试图修改常量 将会抛出一个错误