<?php

$url = 'http://www.baidu.com';

sleep(1);

header("location:" . $url);// header("location:跳转地址") 页面跳转

// header('Content-Type:text/html;charset=utf-8') 设置页面编码