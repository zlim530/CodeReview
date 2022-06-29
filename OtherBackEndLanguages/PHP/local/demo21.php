<?php
// 方式一： 以绝对路径打开文件：fopem('C:\Users\gogery\Desktop\code\1.txt','r')
// 方式二：使用系统内置常量 也叫魔术常量 即__DIR__表示当前脚本的所在目录
var_dump(__DIR__);

$fp = fopen(__DIR__.'\1.txt', 'r');
// 以只读的方式打开当前路径下的1.txt . 表示拼接符，如果是做绝对路径的获取，建议使用__DIR__ 一般来说以 __(双下划线)开头的叫做魔术常量 是系统内置常量，我们自己定义的常量不建议使用__开头

var_dump($fp);// C:\wamp64\www\local\demo21.php:7:resource(2, stream) 返回值是一个资源 注意 如果是在命令行下打开一个文件 则要以绝对路径的方式打开

// null代表空，一般我们在初始化的时候使用
$user = null;	// 相当于定义了一个容器，用于存放信息，但是现在我们并不知道里面存储何种类型的数据，故可以先定义为null

var_dump($user);

$user = [213,32];

var_dump($user);