<?php

// function QuickSort($arr,$low,$hight)
// {
// 	if ( $low < $hight) {
// 		$pivotKey = Partition($arr,$low,$hight);
// 		QuickSort($arr,$low,$pivotKey-1);
// 		QuickSort($arr,$pivotKey+1,$hight);
// 	}
// }

// function Partition($arr,$low,$hight)
// {
// 	$pivot = $arr[$low];
// 	while ( $low < $hight) {
// 		while ( $low<$hight && $arr[$hight] >= $pivot) {
// 			$hight--;
// 		}
// 		swap($arr,$low,$hight);
// 		while ( $low < $hight && $arr[$low] <= $pivot) {
// 			$low--;
// 		}
// 		swap($arr,$low,$hight);
// 	}
// 	return $low;
// }

// function swap($arr,$low,$hight)
// {
// 	if ( $low == $hight) return;
// 	$temp = $arr[$low];
// 	$arr[$low] = $arr[$hight];
// 	$arr[$hight] = $temp;

// }

// $arr = [5,3,4,6,7,9,8,2];
// var_dump($arr);
// QuickSort($arr,0,count($arr)-1);
// echo "<hr>";
// echo "After sorting:";
// var_dump($arr);
 
$arr = array(25,133,452,364,5876,293,607,365,8745,534,18,33);
 
function quick_sort($arr)
{
    // 判断是否需要继续
    if (count($arr) <= 1) {
        return $arr;
    }
 
    $middle = $arr[0]; // 中间值
 
    $left = array(); // 小于中间值
    $right = array();// 大于中间值
 
    // 循环比较
    for ($i=1; $i < count($arr); $i++) { 
        if ($middle < $arr[$i]) {
            // 大于中间值
            $right[] = $arr[$i];
        } else {
 
            // 小于中间值
            $left[] = $arr[$i];
        }
    }
 
    // 递归排序两边
    $left = quick_sort($left);
    $right = quick_sort($right);
 
    // 合并排序后的数据，别忘了合并中间值
    return array_merge($left, array($middle), $right);
}
 
var_dump($arr);
var_dump(quick_sort($arr));