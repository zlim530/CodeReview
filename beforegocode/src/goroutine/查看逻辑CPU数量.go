package main

import (
	"fmt"
	"time"
)

func test() {

	for i := 0; i < 10; i++ {
		fmt.Println("hello world")
		time.Sleep(time.Second)
		//设置每隔1s输出
	}

}

func main() {
	// num := runtime.NumCPU()
	// // num := 1
	// // runtime.GOMAXPROCS(num)
	// fmt.Println("num = ", num)
	go test()
	for i := 0; i < 10; i++ {
		fmt.Println("hello golang")
		time.Sleep(time.Second)
	}

}
