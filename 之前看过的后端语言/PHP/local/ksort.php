<?php
$fr = array("d"=>"lemon","a"=>"orange","b"=>"banana","c"=>"apple");

krsort($fr);

foreach ($fr as $key => $value) {
	echo "$key = $value\n";
}

// To create a natural reverse sorting by keys, use the following function:
function natkrsort($array) 
{
    $keys = array_keys($array);
    //array_keys — 返回数组中部分的所或有的键名
    // array_keys ( array $array [, mixed $search_value = null [, bool $strict = false ]] ) : array
    //input
	// 一个数组，包含了要返回的键。

	// search_value
	// 如果指定了这个参数，只有包含这些值的键才会返回。

	// strict
	// 判断在搜索的时候是否该使用严格的比较（===）。
    natsort($keys);

    foreach ($keys as $k)
    {
        $new_array[$k] = $array[$k];
    }
   
    $new_array = array_reverse($new_array, true);

    return $new_array;
}

