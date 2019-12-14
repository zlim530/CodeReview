package main

import (
	"fmt"
)

func main() {

	for i := 0; i < 3; i++ {
		defer fmt.Println(i)
	}
	/*
		defer函数 逆序返回各函数调用的结果
		上述循环会输出
		2
		1
		0
		直接调用fmt.Println()函数的返回结果
		将局部变量i作为参数传递给 fmt.Println()函数
		即i是以值传递方式传给fmt.Println()
		故在运行defer语句时 就会对i进行值的拷贝 故会依次输出2 1 0
	*/

	for j := 0; j < 3; j++ {
		defer func() {
			fmt.Println(j)
		}()
	}
	/*
		defer配合匿名函数使用：
		此时fmt.Println()函数即为一个闭包 即它是外层匿名函数内部的一个函数
		并且用到了外层函数(这里是main函数)中的变量j 而这种使用其实是一种引用
		也即引用了局部变量j的地址 而当for循环结束时 j=3 而在这个main函数return
		的时候 开始执行defer语句 故此时就会打印三次3 因为此时j已经等于3了
		上述循环会输出
		3
		3
		3

	*/

}
