<?php

$arr = [12,34,[123,4]];

$arr1 = [12,456,[[12,5],[34,5]],];

var_dump($arr1[2]);
var_dump($arr1[2][0]);
var_dump($arr1[2][0][1]);

$arr2 = [12,78,54,2,];

$arr3 = [123,45,7,'hi'];

$arr4 = [[12,5],[0,23]];

var_dump($arr4[0]);

var_dump($arr4[0][1]);

$arr5 = ['key1'=>[12,45],'key2'=>[12,90]];

var_dump($arr5['key1'][1]);

$arr6 = ['key1'=>['name'=>'cc','age'=>22,],'key2'=>['name'=>'qq','age'=>21]];

var_dump($arr6['key2']['age']);

$userDate = [
	['id'=>1,'username'=>'cc','age'=>22],
	['id'=>2,'username'=>'qq','age'=>21],
	['id'=>3,'username'=>'z','age'=>21],

];

var_dump($userDate);
var_dump($userDate[2]['id']);

print_r($userDate);

foreach ($userDate as $value) {
	var_dump($value);
}

foreach ($userDate as $key => $value) {
	var_dump($key,$value);
}

foreach ($userDate as $k => $v) {
	var_dump($k,$v);
}