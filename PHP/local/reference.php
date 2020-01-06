<?php
$a = "ABC";
$b = &$a;
echo "$a";
echo "$b";
$b = "EFG";
echo "$a";
echo "$a";
echo "<hr>";
function testbefore(&$a){
	$a = $a + 100;
}
$b = 1;
testbefore($b);
//testbefore(1);//Fatal error: Only variables can be passed by reference
echo "$b";
echo "<hr>";
function &test(){
	static $b = 0;
	$b = $b + 1;
	echo "$b";
	return $b;
}
$a = test();//1 
$a = 5;
$a = test();//2
$a = &test();// 相当于$a = &$b;
$a = 5;	// 所以 $b = $a = 5;
$a =test(); // 6
echo "<hr>";
/**
 * 
 */
class Test
{
	var $string = "ABC";
}
$b = new Test;
$c = $b;// 相当于 $c = &$b;
echo "$b->string";//ABC
echo "$c->string";//ABC
$b->string = "DEF";
echo "$c->string";//DEF
echo "<hr>";
$e = 1;
$f = &$e;
unset($e);
echo "$f";
// echo "$e";// Notice:Undefined variable:e
// $GLOBALS['var'] = $var;
// global $var;
// $var = &$GLOBALS["var"];
// unset($var);
// echo "$var"; //Notice: Undefined variable: var
$pointer1 = "ABC";
$pointer2 = $pointer1;
var_dump(memory_get_usage($pointer1));//2097152
var_dump(memory_get_usage($pointer2));//2097152
$pointer1 = "EFG";


