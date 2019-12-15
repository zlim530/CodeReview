<?php

$stri = '<div> 
	<h1>hello</h1>
	<p>lorem ipsum dolor sit amet,consectetur.</p>		
	
</div>';

echo htmlspecialchars($stri);// 将所有的HTML标签转化为实体字符 即不对html标签解析


echo htmlspecialchars_decode('
&lt;div&gt; 
	&lt;h1&gt;hello&lt;/h1&gt;
	&lt;p&gt;lorem ipsum dolor sit amet,consectetur.&lt;/p&gt;		
	
&lt;/div&gt;gt;');	// 将实体化标签转化为正常的HTML代码
$string2 = "it's a body.";
echo '<hr>';
echo $string2;
echo '<hr>';

echo addslashes($string2);// 对字符串中的引号做转义 防止sql注入
//it\'s a body.