<?php

$ar  = [1,2,3,];

echo '<hr>';

var_dump($ar);

$a = array_unshift($ar, 1);	// 在数组的最前面添加一个元素

var_dump($a);

var_dump($ar);

$rs = array_shift($ar);	// 弹出数组的第一个元素
$rs = array_shift($ar);
$rs = array_shift($ar);
$rs = array_shift($ar);
var_dump($rs);
var_dump($ar);
