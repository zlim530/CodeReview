<?php
// var_dump('shdjsahd' == 0);// true

// echo "Negative numbers\n";
// $Negative = array('-5','-4','3','-2','0','-1000','9','1');
// var_dump($Negative);
// var_dump("Natsort:");	
// natsort($Negative);
// var_dump($Negative);

// var_dump("Sort:");
// $Negative = array('-5','-4','3','-2','0','-1000','9','1');
// sort($Negative);
// var_dump($Negative);
var_dump('8'<'09');	// true
var_dump('09'=='009');	//true 
echo "Zero padding\n";
$Zero = array('09','8','10','009','001','0');
var_dump($Zero);
var_dump("Matsort:");
natsort($Zero);
var_dump($Zero);

var_dump("Sort:");
$Zero = array('09','8','10','009','001','0');
sort($Zero);
var_dump($Zero);

