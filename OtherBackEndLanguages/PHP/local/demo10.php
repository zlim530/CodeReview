<?php

$data = ['name'=>'cc','age'=>23];

$r = json_encode($data);

var_dump($r);

file_put_contents('2.txt', $r);

$s = file_get_contents('2.txt');

$t = json_decode($s,true);	// 第二参数表示将返回array（数组）而非object（对象）

echo "<hr>";

var_dump($t);