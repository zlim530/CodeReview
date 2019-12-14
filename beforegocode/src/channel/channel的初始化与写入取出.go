package main

import (
	"fmt"
)

func main() {

	var c chan int //管道使用关键字chan声明
	c = make(chan int, 3)

	fmt.Println("c 的值 = ", c, "c 的地址 = ", &c)
	//管道变量中的值为0xc000082000一串地址 故管道是引用类型
	//也即当我们将管道传给函数时  不需要使用取&进行地址传递  管道会自定将自己地址的拷贝传递给函数
	//故在函数中修改或是操作此管道时  main函数中的管道也会发生相应的变化

	// m := make(map[int]int)
	// fmt.Printf("m 的值 = %d m 的地址 = %p\n", m, &m)

	// sli := make([]int, 3)
	// fmt.Println("sli 的值 = ", sli, "sli 的地址 = ", &sli)
	// fmt.Printf("sli 的值 = %d sli 的地址 = %p\n", sli, &sli)

	c <- 10
	c <- 20
	c <- 30
	fmt.Println("len(c) = ", len(c), "cap(c) = ", cap(c))
	<-c
	c <- 40
	fmt.Println("len(c) = ", len(c), "cap(c) = ", cap(c))

	a := <-c
	b := <-c
	d := <-c
	fmt.Println(a, b, d)

}
