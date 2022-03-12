<?php

$fr = array("lemon","orange","banana","apple");

rsort($fr);

foreach ($fr as $key => $val) {
	echo "$key = $val\n";
}