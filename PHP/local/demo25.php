<?php

setcookie("cookie[three]","cookiethree");
setcookie("cookie[tow]","cookietwo");
setcookie("cookie[one]","cookieone");

if ( isset($_COOKIE['cookie'])) {
	foreach ($_COOKIE['cookie'] as $name => $value) {
		$name = htmlspecialchars($name);
		$value = htmlspecialchars($value);
		echo "$name : $value <br> \n" ;			
	}
}