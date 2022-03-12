<?php

$arr = [12,34,45,6];

var_dump(implode('+', $arr));// 将数组以指定的连接符转化为字符串

var_dump(implode('&', $arr));

var_dump(explode('&', '21314&2354325&3452'));

