package main

import (
	"fmt"
)

func putnum(ch chan int) {

	for i := 0; i < 8000; i++ {
		ch <- i
	}

	close(ch)
}

func calculate(ch chan int, result chan int, exit chan bool) {
	var flag bool
	for {
		num, ok := <-ch
		if !ok {
			break
		}
		flag = true
		for i := 2; i < num; i++ {
			if num%i == 0 {
				flag = false
				break
			}

		}

		if flag {
			result <- num
		}
	}

	fmt.Println("此协程因为再也读不到数据 退出")
	exit <- true

}

func main() {
	ch := make(chan int, 1000)
	result := make(chan int, 2000)

	exit := make(chan bool, 4)

	go putnum(ch)

	for i := 1; i <= 4; i++ {
		go calculate(ch, result, exit)

	}

	go func() {
		for i := 0; i < 4; i++ {
			<-exit

		}
		close(result)
	}() //在主线程中使用匿名函数开启一个协程 用于判断4个协程是否执行完毕
	//避免协程还没有执行完但主线程已经执行完毕而导致的协程的不能成功执行

	for {
		res, ok := <-result
		if !ok {
			break
		}
		fmt.Println("素数为：", res)
	}

}
