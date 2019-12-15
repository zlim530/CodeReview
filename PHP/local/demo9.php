<?php
// 把复杂数据类型（例如数组、对象）直接保存在文件中是不行的，因此我们需要先将复杂数据序列化成字符串进行保存，而后在文件中读取数据时进行反序列为相应的数据类型格式即可

$data = [12,23,34];

$s = serialize($data);	// 序列化：将数组转换成一个字符串的格式

var_dump($s);	// a:3:{i:0;i:12;i:1;i:23;i:2;i:34;}

file_put_contents('1.txt', $s);

echo "ok!";

$data1 = file_get_contents('1.txt');

$f = unserialize($data1);	// 反序列化

var_dump($f);

echo "<hr>";

var_dump($f[1]);