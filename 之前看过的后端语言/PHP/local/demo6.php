<?php

$userData = [
	['id' => 1,'name' => 'ana','score' => 45],
	['id' => 2,'name' => 'lls','score' => 65],
	['id' => 3,'name' => 'luc','score' => 85],
	['id' => 4,'name' => 'mai','score' => 95],
];

usort($userData, function($a,$b)
{
	return $b['score'] - $a['score'];
});
// usort()用于给二维数组排序（会根据二维数组中的某个key值进行排序）这里就是取出数据后 根据分值进行排序 其中$a 和 $b 都代表数组中的元素 如果返回的结果大于0则表示交换 否则不交换 
//即 第一个参数 - 第二个参数 	：升序
//   第二个参数 - 第二个参数	：降序
                                                                                                   
echo '<hr>';

var_dump($userData);