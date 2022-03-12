<?php

//数组的操作：增删改查	即curd

$arr5 = [123,432,5,6,7,];

var_dump($arr5);

var_dump($arr5[2]);
var_dump($arr5[10]);// Notice: Undefined offset: 10 in C:\wamp64\www\local\demo15.php on line 10 下标不存在 即对数据越界操作

$arr5[] = 90;

var_dump($arr5);

var_dump(count($arr5));

unset($arr5[3]);

var_dump($arr5);

$arr5[1] = 80;
var_dump($arr5);
