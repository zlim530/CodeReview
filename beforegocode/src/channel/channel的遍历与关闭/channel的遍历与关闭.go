package main

import (
	"fmt"
)

func main() {

	ch := make(chan int, 3)
	ch <- 100
	ch <- 200
	ch <- 300
	close(ch)

	fmt.Println("the channel ch is closed")

	n1 := <-ch
	fmt.Println(n1)
	//管道关闭后仍可以读数据 但是不能再向管道变量写入数据

	/*
		管道可以使用for range的方式进行遍历 但是管道在进行遍历
		取出其中的数据前 一定要先close()管道 否则会产生deadlock的panic

	*/

	ch1 := make(chan int, 100)
	for i := 0; i < 100; i++ {
		ch1 <- i
	}

	//不能使用普通的for循环遍历管道 因为管道再每取出一个数据后 其长度会发生改变 会减1
	// for i:= 0;i<len(ch2);i++{
	// 	fmt.Println(i)
	// }

	close(ch1)
	//且使用for range遍历管道 range函数仅有一个返回值 即管道中的数据
	for val := range ch1 {
		fmt.Println("ch1 = ", val)
	}

}
