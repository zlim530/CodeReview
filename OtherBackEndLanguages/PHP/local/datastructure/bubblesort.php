<?php

function BubbleSort($array)
{
	for($i = 0;$i < count($array)-1;$i++)
	{
		for($j = 0;$j < count($array)-1-$i;$j++)
		{
			if ( $array[$j] > $array[$j+1] ) {
				$temp = $array[$j];
				$array[$j] = $array[$j+1];
				$array[$j+1] = $temp;
				var_dump("发生交换");
				var_dump($array);
			}

		}
		var_dump("============" ."第"."$i+1"."次遍历：");
		var_dump($array);
	}

}

$a = [5,6,1,7,2,4,3];
var_dump(count($a));
var_dump( "初始状态：");
var_dump($a);
BubbleSort($a);

function BubbleSortPro($arr)
{
	for($i = 0;$i < count($arr) - 1 ;$i ++)
	{
		$flag = true;
		for($j = 0;$j< count($arr) -1- $i;$j++)
		{
			if ( $arr[$j] > $arr[$j+1]) {
				$temp = $arr[$j];
				$arr[$j] = $arr[$j+1];
				$arr[$j+1] = $temp;
				var_dump("发生交换");
				var_dump($arr);
				$flag = false;
				$lastPostion = $j;
			}
		}
		if ( $flag) break;
		var_dump("============" ."第"."($i+1)"."次遍历：");
		var_dump($arr);
	}

}

var_dump("==============================================人工分界线========================================");
var_dump( "初始状态：");
var_dump($a);
BubbleSortPro($a);

function BubbleSortProSed($a)
{
	$len = count($a) - 1;
	for($i = 0;$i < count($a)-1;$i++)
	{
		$flag = true;
		for($j = 0;$j< $len;$j++)
		{
			if ( $a[$j] > $a[$j+1]) {
				$temp = $a[$j];
				$a[$j] = $a[$j+1];
				$a[$j+1] = $temp;
				$flag = false;
				$lastPostion = $j;
				var_dump("发生交换");
				var_dump($a);
			}
			var_dump("你在浪费计算吗？");
		}
		if ( $flag) break;
		$len = $lastPostion;
		var_dump("============" ."第"."($i+1)"."次遍历：");
		var_dump($a);
	}

}

var_dump("==============================================人工分界线========================================");
$a = [4,3,2,1,5,6,7];
var_dump( "初始状态：");
var_dump($a);
BubbleSortProSed($a);