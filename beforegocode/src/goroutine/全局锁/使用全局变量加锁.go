package main

import (
	"fmt"
	"sync"
	"time"
)

var (
	mymap = make(map[int]int, 10)
	lock  sync.Mutex
	/*
		声明一个全局变量的互斥锁也即写锁
		lock 是一个全局的互斥锁
		sync 即 synchornized 同步的 是一个包
		mutex 互斥的

	*/
)

func test(n int) {
	res := 1
	for i := 1; i <= n; i++ {
		res *= i
	}

	lock.Lock()
	mymap[n] = res
	//fatal error: concurrent map writes 即并发写入map错误
	lock.Unlock()
}

func main() {
	for i := 1; i <= 20; i++ {
		go test(i)
	}
	time.Sleep(time.Second * 10) //休眠10秒

	lock.Lock()
	for i, v := range mymap {
		fmt.Printf("map[%d] = %d\n", i, v)
	}
	lock.Unlock()
}
