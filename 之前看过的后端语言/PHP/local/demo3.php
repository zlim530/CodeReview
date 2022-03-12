<?php

$str = 'ab中文';

var_dump(substr($str, 0,3));

var_dump(mb_substr($str, 0,3,'utf-8'));

var_dump( strlen($str));//8 

var_dump( mb_strlen($str)); // 4

var_dump( mb_detect_encoding($str));// UTF-8 查看字符串的编码
