<?php
// example1：对为定义的变量使用引用
function foo(&$var){

}
foo($a);
$b = [];
foo($b['b']);
var_dump(array_key_exists('b', $b));
$c = new StdClass;
foo($c->d);
var_dump(property_exists($c, 'd'));
// example2：在函数内引用全局变量
$var1 = "Example variable";
$var2 = "";
function global_references($use_globals){
	global $var1,$var2;
	if ( ! $use_globals) {
		$var2 = &$var1;
	}else  {
		$GLOBALS["var2"] = &$var1;
	}
}
global_references(false);
echo "var2 is set to '$var2'\n";
global_references(true);
echo "var2 is set to '$var2'\n";