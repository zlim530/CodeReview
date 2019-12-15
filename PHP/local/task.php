<?php

$string = 'abc';
var_dump(strlen($string));
$string1 = 'abc中文';
var_dump(strlen($string1));

echo '<hr>';
var_dump('hello world');

$s = '      sad   dsajd a        ';

var_dump('(' . $s . ')');

var_dump('(' . trim($s) . ')');

var_dump($s); // 即对字符串的处理不会影响原始字符串 

$s = '##sv##';
var_dump(trim($s,'#'));

$ss = '1,2,3,4';
$arr = explode(',', $ss);
var_dump($arr);
