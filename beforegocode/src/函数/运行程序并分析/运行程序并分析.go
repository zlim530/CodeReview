package main

import (
	"fmt"
)

// func Test(args ...int) {
// 	for _, n := range args { //遍历参数列表
// 		fmt.Println(n)
// 	}
// }

//形如...type格式的类型只能作为函数的参数类型存在，并且必须是最后一个参数
func MyFunc01(args ...int) {
	fmt.Println("MyFunc01")
	for _, n := range args { //遍历参数列表 此时的参数列表被当做了切片
		//通过range可以得到切片的索引与元素的值
		fmt.Println(n)
	}
}

func MyFunc02(args ...int) {
	fmt.Println("MyFunc02")
	for _, n := range args { //遍历参数列表
		fmt.Println(n)
	}
}

func Test(args ...int) {
	MyFunc01(args...)     //按原样传递, Test()的参数原封不动传递给MyFunc01
	MyFunc02(args[1:]...) //Test()参数列表中，第1个参数及以后的参数传递给MyFunc02
}

func main() {
	Test(1, 2, 3) //函数调用
}

func main2() {
	//函数调用，可传0到多个参数
	Test() //不会输出任何值
	Test(1)
	Test(1, 2, 3, 4)
}

func main1() {
	var fs = [4]func(){}
	//定义了一个数组 数组成员的类型是函数类型

	for i := 0; i < 4; i++ {
		defer fmt.Println("defer i = ", i)
		defer func() {
			fmt.Println("defer_closure i = ", i)
		}()
		fs[i] = func() {
			fmt.Println("closure i = ", i)
		}
	}

	for _, f := range fs {
		f()
	}

}
