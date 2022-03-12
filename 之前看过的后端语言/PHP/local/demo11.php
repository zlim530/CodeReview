<?php
// 在编程的时候 我们偶尔有时候会希望代码能暂停一会 一盘在做数据采集的时候可能用到
$data = 12;

// sleep(3); // 代表睡眠3秒	在睡眠过程中下面的代码将不会执行 俗称被阻塞了
// usleep(1000);	// 精度是毫秒 1s = 1000ms

var_dump($data);

unset($data);	// 删除一个变量

var_dump( isset($data));	// 判断一个变量时候存在 不存在则返回false

if ( isset($data)) {
	var_dump($data);		
}	else{
	echo('do not exists');
}	

// empty()判断变量是否为空 如果为空 则返回true 在php中 以下情况均认为是空：''(空字符串)、0(数字0)、false(boolean值false)、array()(一个空数组)、[](在php5.4以后这个写法也表示一个空数组)
$data = '0';
var_dump( empty($data));	