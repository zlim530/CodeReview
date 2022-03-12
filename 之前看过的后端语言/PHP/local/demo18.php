<?php
// 关联数组 ：下标可以是字符串 一般我们把关联数组叫做map(字典)或是hash(哈希)
$userInfo = ['username'=>'cc','gender'=>'female','age'=>22,];

var_dump($userInfo['username']);

var_dump($userInfo['age']);

var_dump($userInfo[gender]);//Warning: Use of undefined constant gender - assumed 'gender' (this will throw an Error in a future version of PHP) in C:\wamp64\www\local\demo18.php on line 9 当我们想要访问关联数组的数组成员时  我们使用定义关联数组时对应值所指定的key 并将key放在单引号或双引号中  如果不加引号 则PHP解释器会认为我们输入的索引key值是一个常量 如果此常量存在 则PHP会进行替换而后在关联数组中查看key值为此常量值的数组元素 如果找不到 则返回null
// 如果此常量不存在，在PHP解释器会去对应的关联数组中查找对应key的value值 
// 即在PHP中，如果我们想要在关联数组中获取某个元素的值，需要通过其key值来获取，如果key不适用引号包裹，PHP也会尝试去数组中进行查找 
// 而在去数组中查找之前，PHP解释器会先在当前脚本中查看对应的常量是否存在，如果存在则会根据对应的常量值前往关联数组中查找

