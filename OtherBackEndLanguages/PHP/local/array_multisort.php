<?php
// $ar1 = array(10,101,100,0);
// $ar2 = array(1,2,3,4);

// array_multisort($ar1,$ar2);

// var_dump($ar1);
// var_dump($ar2);
// $ar = array(
// 	array("10",11,100,101,"a"),
// 	array(-1,2,"2",3,1),
// );

// array_multisort($ar[0],SORT_ASC,SORT_STRING,$ar[1],SORT_NUMERIC,SORT_DESC);

// var_dump($ar);

$arrUsers = array(
    array(
            'id'   => 1,
            'name' => '张三',
            'age'  => 25,
    ),
    array(
            'id'   => 2,
            'name' => '李四',
            'age'  => 23,
    ),
    array(
            'id'   => 3,
            'name' => '王五',
            'age'  => 40,
    ),
    array(
            'id'   => 4,
            'name' => '赵六',
            'age'  => 31,
    ),
    array(
            'id'   => 5,
            'name' => '黄七',
            'age'  => 20,
    ),
);

$last_ages = array_column($arrUsers,'age');
var_dump($last_ages);
//array (size=5)
  // 0 => int 25
  // 1 => int 23
  // 2 => int 40
  // 3 => int 31
  // 4 => int 20
array_multisort($last_ages ,SORT_ASC,$arrUsers);
 
var_dump($arrUsers);