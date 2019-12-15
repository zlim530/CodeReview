<?php

$value = 'something from somewhere';

setcookie("TestCookie",$value);
setcookie("TestCookie",$value,time()+3600);
setcookie("TestCookie",$value,time()+3600,"/","localhost",1);

echo $_COOKIE["TestCookie"];// 仅打印cookie的value => something from somewhere
echo "<hr>";
print_r($_COOKIE);// 不仅打印cookie的value还会打印cookie的PHPSESSID => Array ( [TestCookie] => something from somewhere [PHPSESSID] => bqomvmtij1416ukttiorjmp1ot )