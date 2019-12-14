package main

import (
	"fmt"
	"os"
)

func main() {

	list := os.Args
	n := len(list)
	//注意.go文件也是一个参数
	//执行.go文件的命令也是一个参数
	fmt.Println("n = ", n)

	for i := 0; i < n; i++ {
		fmt.Printf("list[%d] = %s\n", i, list[i])
	}

	fmt.Println("range 方法实现打印参数:")

	for i, data := range list {
		fmt.Printf("list[%d] = %s\n", i, data)
	}

	// args := os.Args
	// if args == nil || len(args) < 2 {
	// 	fmt.Println("err:xxx ip port")
	// 	return
	// }
	// ip := args[1]
	// port := args[2]
	// fmt.Printf("ip = %s,port = %s\n", ip, port)

}
