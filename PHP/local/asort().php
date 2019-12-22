<?php
$fr = array("d" => "lemon","a" => "orange","b"=>"banana","c"=>"apple");

// asort($fr,SORT_NUMERIC );
asort($fr);
// arsort($fr);

foreach ($fr as $key => $val) {
	echo "$key = $val\n";
}
//c = apple b = banana d = lemon a = orangefruits 被按照字母顺序排序，并且单元的索引关系不变。