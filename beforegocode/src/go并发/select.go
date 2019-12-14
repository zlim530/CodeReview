package main

import (
	"fmt"
	"time"
)

func fibonacci(c, quit chan int) {
	s, y := 1, 1
	for {
		select {
		case c <- s:
			s, y = y, s+y
		case <-quit:
			fmt.Println("quit")
			return
		default: //当s阻塞时执行default语句
			fmt.Println("channel is blocking. ")

		}

	}
}

func timeout() {

	c := make(chan int)
	o := make(chan bool)
	go func() {
		for {
			select {
			case v := <-c:
				fmt.Println(v)
			case <-time.After(5 * time.Second):
				fmt.Println("timeout")
				o <- true
				break
			}

		}
	}()
	<-o
}

func main() {

	c := make(chan int) //非缓存类型的channel->默认情况下这种channel的接收和发送数据都是阻塞的
	quit := make(chan int)

	go func() {
		for i := 0; i < 10; i++ {
			fmt.Println(<-c)

		}
		quit <- 0
	}()

	fibonacci(c, quit)
	timeout()
}
