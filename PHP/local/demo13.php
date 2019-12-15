<?php

$number = 12;

$number1 = 34;

$min = min($number1,$number);

var_dump($min);

$max = max($number,$number1);

var_dump($max);

echo "<hr>";

var_dump(rand());	//在指定的第一个参数到第二参数之间产生一个随机数，不指定参数则默认从0到PHP_INT_MAX之间产生一个随机数:1999526046

var_dump(PHP_INT_MAX);	//	 9223372036854775807

var_dump(PHP_INT_SIZE);	// 8 在php中一个整型占8个字节

echo('<hr>');

var_dump(mt_rand(1,123));

$number1 = 3.4;

var_dump(ceil($number1));	//向上取整

var_dump(floor($number1));	// 向下取整

echo "<hr>";

$number2 = 3.1415926;

var_dump(round($number2,2));// 第二个参数代表获取几位有效小数 这里为保留2位小数