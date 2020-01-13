<?php

$f = array("lemon","orange","banana","apple");

sort($f);

$res = sort($f);

var_dump($res);

foreach ($f as $key => $value) {
    echo "fruits[" . $key . "] = " . $value . "\n";
}
// fruits[0] = apple fruits[1] = banana fruits[2] = lemon fruits[3] = orange

// $a = array(
//     10 => 'a',
//     4 => 'b',
//     1 => 'c',
//     'd'
// );
// var_dump($a);
//array (size=4)
// 10 => string 'a' (length=1)
// 4 => string 'b' (length=1)
// 1 => string 'c' (length=1)
// 11 => string 'd' (length=1)
