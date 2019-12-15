<?php

$string = 'abc';

file_put_contents('1.txt', $string);// 第一个参数表示要存储数据的文件名 如果不存在 则会在当前路径下创建 如果存在 则直接写入第二参数对应的数据

echo "ok!";

$string2 = file_get_contents('1.txt');	// 表示读取1.txt文件中的内容

var_dump('read in file:' . $string2);



