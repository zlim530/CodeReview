<?php

error_reporting(0);

// error_reporting(E_ERROR | E_WARNING | E_PARSE);
// 报告E_NOTICE：包括未初始化的变量或者捕获变量名的错误拼写
// error_reporting(E_ERROR | E_WARNING | E_PARSE | E_NOTICE);

// 除了E_NOTICE，报告其他所有错误
error_reporting(E_ALL ^ E_NOTICE);

// 报告所有PHP错误（参见changelog）
error_reporting(E_ALL);

// 报错所有PHP错误
error_reporting(-1);

// 和 error_reporting(E_ALL);一样
ini_set('error_reporting', E_ALL);

var_dump($a);	// E_NOTICE级别错误