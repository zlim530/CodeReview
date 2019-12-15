<?php


 /**
     * 上传测试接口(不需要任何参数)
     * @param  {string} addr:地址，longitude:经度，latitude:纬度
     * @return {json} 成功返回图片路径及参数
    */
    function addReport()
    {
        $tmp = file_get_contents("php://input");
        $arr = json_decode($tmp,true);

        $picList = $this->_upload();//调用图片上传函数
        if(!$picList){
            $this->error('图片上传失败');
        }

        $i = 1;
        foreach ($picList as $value) {
            $key = 'src'.$i;
            $data[$key] = $value;
            $i++;
        }

        // $data['addr'] = $arr['addr']?$arr['addr']:I('addr');
        // $data['longitude'] = $arr['longitude']?$arr['longitude']:I('longitude');
        // $data['latitude'] = $arr['latitude']?$arr['latitude']:I('latitude');
        if(M('Report')->add($data)){
            $this->success($data,'成功');
        }else{
            $this->error('存储失败');
        }

    }

    /**
     * 多个图像上传
     * @param {string} $type: 存储类型 report:投诉 ,avatar:头像，logo：厂家logo，ad：广告图片
     * @return {array} $arr 图片web地址列表
     */
    function _upload($type = 'report'){
        $config = array(
            'maxSize'    =>    3145728,
            'rootPath'   =>    __DIR__ .$type.'/',
            'savePath'   =>    '',
            'saveName'   =>    array('uniqid',''),
            'exts'       =>    array('jpg', 'gif', 'png', 'jpeg'),
            'autoSub'    =>    true,
            'subName'    =>    array('date','Ymd'),
        );
        $baseUrl = C('SITE_URL'). __DIR__ .$type.'/';//例如：http://www.baidu.com/up/report/
        $upload = new \Think\Upload($config);// 实例化上传类
        // 上传文件 
        $info = $upload->upload();

        $arr = array();
        $i = 0;
        if(!$info) {// 上传错误提示错误信息
            $this->error($upload->getError());
        }else{// 上传成功
            foreach($info as $file){ //循环存储图片到服务器
                $subUrl = $file['savepath'].$file['savename'];//例如：20140125/dfuosi203.jpg
                $arr[$i++] = $baseUrl.$subUrl;
            }
            return $arr;
        }

    }
    

    // 用来处理上传的数据流代码

    function imgApp(){

        //方式一：电脑上传文件

        $image = $_FILES["photo"]["tmp_name"];

        $fp = fopen($image, "r");

        $file = fread($fp, $_FILES["photo"]["size"]); //二进制数据流

        //保存地址

        $imgDir = './Uploads/';

        //要生成的图片名字

        $filename = date("Ym")."/".md5(time().mt_rand(10, 99)).".png"; //新图片名称

        $newFilePath = $imgDir.$filename;

        $data = $file;

        $newFile = fopen($newFilePath,"w"); //打开文件准备写入

        fwrite($newFile,$data); //写入二进制流到文件

        fclose($newFile); //关闭文件

        //写入数据库

        $arr = array(

                "uid" => 1, //用户id

                "cid" => 1, //分类id

                "a_title" => $_POST["a_title"], //标题

                "a_content" => $_POST["a_content"], //内容

                "photo" => $filename, //图片路径

                "a_urgent" => $_POST['status'] == 'on' ? 0 : 1, //是否急需

                "add_time" => time(), //创建时间

            );

        if(empty($arr["a_title"])){

            $this->error("标题不为空");

        } else if(empty($arr["a_content"])) {

            $this->error("内容不为空");

        }

        if($db = M("answer")->add($arr)){

            $this->success("保存成功", "demo2");

        } else {

            $this->error("失败");

        }

    }

 /** 二进制流生成文件
    * $_POST 无法解释二进制流，需要用到 $GLOBALS['HTTP_RAW_POST_DATA'] 或 php://input
    * $GLOBALS['HTTP_RAW_POST_DATA'] 和 php://input 都不能用于 enctype=multipart/form-data
    * @param    String  $file   要生成的文件路径
    * @return   boolean
    */
    function binary_to_file($file){
        $content = $GLOBALS['HTTP_RAW_POST_DATA'];  // 需要php.ini设置
        if(empty($content)){
            $content = file_get_contents('php://input');    // 不需要php.ini设置，内存压力小
        }
        $ret = file_put_contents($file, $content, true);
        return $ret;
    }
    
    // demo
    binary_to_file('photo/test.png');


/** php 发送流文件
* @param  String  $url  接收的路径
* @param  String  $file 要发送的文件
* @return boolean
*/
function sendStreamFile($url, $file){
 
    if(file_exists($file)){
 
        $opts = array(
            'http' => array(
                'method' => 'POST',
                'header' => 'content-type:application/x-www-form-urlencoded',
                'content' => file_get_contents($file)
            )
        );
 
        $context = stream_context_create($opts);
        $response = file_get_contents($url, false, $context);
        $ret = json_decode($response, true);
        return $ret['success'];
 
    }else{
        return false;
    }
 
}
 
$ret = sendStreamFile('http://localhost/fdipzone/receiveStreamFile.php', 'send.txt');
var_dump($ret);


/** php 接收流文件
* @param  String  $file 接收后保存的文件名
* @return boolean
*/
function receiveStreamFile($receiveFile){
 
    $streamData = isset($GLOBALS['HTTP_RAW_POST_DATA'])? $GLOBALS['HTTP_RAW_POST_DATA'] : '';
 
    if(empty($streamData)){
        $streamData = file_get_contents('php://input');
    }
 
    if($streamData!=''){
        $ret = file_put_contents($receiveFile, $streamData, true);
    }else{
        $ret = false;
    }
 
    return $ret;
 
}
 
$receiveFile = 'receive.txt';
$ret = receiveStreamFile($receiveFile);
echo json_encode(array('success'=>(bool)$ret));

