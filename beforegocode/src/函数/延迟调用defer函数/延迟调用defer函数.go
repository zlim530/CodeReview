package main

import (
	"fmt"
)

func test(x int) {
	fmt.Println(100 / x) //x为0时，产生异常
}

func main() {
	defer fmt.Println("aaaaaaaa")
	defer fmt.Println("bbbbbbbb")

	defer test(0) //引起panic

	defer fmt.Println("cccccccc")
}

func main1() {
	fmt.Println("this is a test")
	defer fmt.Println("this is a defer1 ")
	defer fmt.Println("this is a defer2 ")
	/*
		运行结果
		this is a test
		this is a defer2
		this is a defer1
		延迟调用：
		如果一个函数中有多个defer语句，它们会以LIFO（后进先出）
		的顺序执行。哪怕函数或某个延迟调用发生错误，
		这些调用依旧会被执性.
		但如果不是defer语句 则没有此特点

	*/
}
