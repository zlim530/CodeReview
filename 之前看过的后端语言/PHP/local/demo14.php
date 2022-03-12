<?php
// 数组：是一类数据的集合，可以将多个数据放置在一起 并且数组中的每个值被称为数组元素 数组元素可以是其他任何基本数据类型 也可以是复合数据类型


$a = array(1,2,3,);

$a1 = array('hi','workd','pip');

$a2 = array(true,false);

$a3 = array(12,'ll',true,12.34); // 在php中数组元素可以混合存放 即一个数组中可以存放不同的数据类型的数据 

$a4 = [12,56,7];	// 在php5.4之后的写法 建议使用 因为更加通用

echo $a4;//Notice: Array to string conversion in C:\wamp64\www\local\demo14.php on line 13 ehco函数只能进行基本数据类型的输出 不能输出复合数据类型

echo("\r\n");	// 输出一个换行	其实际是回车换行  这与古老的打字机有关 最开始使用的打字机当打到一行的行时 需要先使用\n进行换行 此时光标位于新一行的行尾 这时使用\r回车时光标回到行首 在windows下 \r\n 表示换行 而在Linux下 \n表示换行 n：new line r：return

var_dump($a4);	// var_dump函数可以输出任何类型的变量 并且在输出时会返回其相应的数据类型

echo("\r\n");	

print_r($a4);// 专门用于输出数组的函数