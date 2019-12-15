<?php

$a = [12,23,34];

echo '<pre>';

var_dump($a);

$a[] = 65;	// 在数组的最后添加新的元素

var_dump($a);

$arr = [];

echo '<hr>';

var_dump($arr);

$a = array_push($arr, 12);

var_dump($a);// 1 

$B = array_push($arr, 23);	// 返回处理之后数组的元素个数。

var_dump($arr);

var_dump($B);// 1

echo '<hr>';

$rs = array_pop($arr);	// 返回数组中被弹出的最后一个元素
var_dump($rs); // 23
var_dump($arr);
// 数组是一种叫栈的数据结构  所谓栈就是 first in last out 即先进后出 后进先出 array_pop()永远是都是弹出最后一个元素 也叫出栈	而array_push()永远你都是将变量写入到数组的最后一个元素中 也叫入栈（压栈）

// 与栈相对的数据结构是队列 所谓队列就是 first in first our 即先进先出、后进后出