<?php

// 删除Cookie：要删除一个cookie应该设置过期时间为过去
setcookie("TestCookie","",time()-3600);
setcookie("TestCookie","",time()-3600,"/","localhost",1);