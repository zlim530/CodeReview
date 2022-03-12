<?php

var_dump ("==Alohabets==" . PHP_EOL);

$s = 'w';
$count = 0;
for ($i=0; $i < 6; $i++) { 
	echo ++$s . PHP_EOL;
	$count ++;
}
//x y z aa ab ac
// var_dump($count);
var_dump ("== Digits ==" . PHP_EOL);
$d = 'A8';
for ($i=0; $i < 6; $i++) { 
	echo ++$d . PHP_EOL;
}
//A9 B0 B1 B2 B3 B4
$d = 'A08';
for ($i=0; $i < 6; $i++) { 
	echo ++$d . PHP_EOL;
}
//A09 A10 A11 A12 A13 A14