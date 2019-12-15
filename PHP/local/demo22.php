<?php


//字符串转换成16进制
function str2hex($str){
    $hex = '';
    for($i=0,$length=mb_strlen($str); $i<$length; $i++){
        $hex .= dechex(ord($str{$i}));
    }
    return $hex;
}
//16进制转换成字符串
function hex2str($hex){
    $str = '';
    $arr = str_split($hex, 2);
    foreach($arr as $bit){
        $str .= chr(hexdec($bit));
    }
    return $str;
}
function test(){
    //utf8字符测试
    $str = '我';
    echo mb_strlen($str);
    echo '';
    //字符串转换成16进制     
    $hex = str2hex($str);
    echo $hex;
    echo '';
    echo "<hr>";		
    //16进制转换成字符串        
    $dec = hex2str($hex);
    echo $dec;
    echo "";
    echo "<hr>";
    //gbk测试
    $gbkstr = mb_convert_encoding($str,'GBK','UTF-8');
    echo mb_strlen($gbkstr);
    echo '';
    echo "<hr>";
            
    $hex = str2hex($gbkstr);
    echo $hex;
    echo '';
    echo "<hr>";
        
    $dec = mb_convert_encoding(hex2str($hex), 'UTF-8', 'GBK');
    echo $dec;
    echo "";
    echo "<hr>";
}
test();

