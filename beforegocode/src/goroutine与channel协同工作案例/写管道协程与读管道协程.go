package main

import (
	"fmt"
)

func writetest(ch chan int) {
	for i := 0; i < 50; i++ {
		ch <- i
		fmt.Println("write data is ", i)
	}

	close(ch)
	//在写完管道后一定要进行关闭 否则后面读取管道对管道进行遍历时会报错

}

func readtest(ch chan int, exit chan bool) {
	for {
		v, ok := <-ch
		if !ok {
			break
		}
		fmt.Println("read channel data is ", v)
	}

	//当读完管道中的数据后 将true写入exit管道 告诉main主函数 协程执行完毕
	exit <- true
	close(exit)
	//写入exit管道后要及时关闭 因为main函数中要对exit管道进行遍历 以确定协程readtest的任务是否完毕

}

func main() {

	ch := make(chan int, 20)
	exit := make(chan bool, 1)

	//因为管道是引用类型 所以作为参数传递给函数时 会自动传递地址拷贝
	//则在函数中的修改会同步到主函数中
	go writetest(ch)
	go readtest(ch, exit)

	for {
		v, _ := <-exit
		if v == true {
			break
		}
	}

}
