<?php

/**
 * class 类：是一类事物的统称 有属性与方法
 */
class User
{
	protected	$username = 'cc';
	public function info()
	{
		echo 'hello world!';		
	}
}

$user = new User();
var_dump($user);// object(User)[1]
 //protected 'username' => string 'cc' (length=2)