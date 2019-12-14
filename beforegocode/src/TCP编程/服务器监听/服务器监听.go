package main

import (
	"fmt"
	"net"
)

func process(con net.Conn) {

	defer con.Close()

	for {
		buf := make([]byte, 1024)
		fmt.Printf("服务器等待客户端 %s 发送信息\n", con.RemoteAddr().String())
		n, err := con.Read(buf)
		if err != nil {
			fmt.Println("客户端退出 err = ", err)
			return
			//！！！这里一定要return 因为如果不在发现err时return
			//则服务器会因为客户端异常退出而一直阻塞在此 会一直读取客户端

		}
		fmt.Println(string(buf[:n]))
		//一定是buf[:n] 即客户端写入多少服务器就读多少 如果多读 会出现不可预期的错误
	}

}

func main() {
	fmt.Println("服务器开始监听...")

	//tcp表示使用的网络协议是tcp 0.0.0.0:8888表示在本地监听8888端口
	listen, err := net.Listen("tcp", "0.0.0.0:8888")
	//127.0.0.1 仅在ipv4上有效 而0.0.0.0则在ipv4和ipv6上均有效

	if err != nil {
		fmt.Println("listen err:", err)
		return
	}
	defer listen.Close() //延时关闭listen

	for {
		fmt.Println("等待客户端连接...")
		con, err := listen.Accept()
		if err != nil {
			fmt.Println("Accept() err=", err)
		} else {
			fmt.Println("Accept() suc con = ", con)
			fmt.Println("客户端ip=", con.RemoteAddr().String())
		}

		go process(con)
		//开启协程为客户端服务 每来一个客户端就开启一个协程为客户端进行服务
		//避免当多个客户端向服务器发起连接时 产生阻塞的问题

	}

	// fmt.Println(listen)

}
