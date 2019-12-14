package main

import (
	"bufio"
	"fmt"
	"net"
	"os"
	"strings"
)

func main() {

	con, err := net.Dial("tcp", "10.1.41.86:8888")
	if err != nil {
		fmt.Println("dial err = ", err)
		return
	}

	for {
		//接收标准输入也即终端中的数据
		reader := bufio.NewReader(os.Stdin)

		//将接收到的终端中的数据读取出来 并发送给服务器
		// \n 设定结束符
		line, err := reader.ReadString('\n')
		if err != nil {
			fmt.Println("read string err = ", err)
		}

		//去掉line中的空格 \r \n
		line = strings.Trim(line, " \r\n")
		if line == "exit" {
			fmt.Println("客户端退出...")
			break
		}

		//将line通过conn（连接）发送给服务器
		_, err = con.Write([]byte(line + "\n"))
		if err != nil {
			fmt.Println("write err = ", err)

		}

		fmt.Println("con suc", con)

		// fmt.Printf("客户端发送了 %d 个字节的数据，并退出\n", n)
	}
}
