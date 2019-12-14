package main

import (
	"fmt"
)

func main1() {
	var a int = 10              //声明一个变量，同时初始化
	fmt.Printf("&a = %p\n", &a) //操作符 "&" 取变量地址

	var p *int = nil //声明一个变量p, 类型为 *int, 指针类型
	p = &a
	fmt.Printf("p = %p\n", p)
	fmt.Printf("a = %d, *p = %d\n", a, *p)

	*p = 111 //*p操作指针所指向的内存，即为a 故这样即修改了变量a的值
	fmt.Printf("a = %d, *p = %d\n", a, *p)

}

func main() {
	a := [...]int{9: 1}
	var p *[10]int = &a
	//p是指向数组的指针 要保证指针的长度和数组的长度是一致的 否则会报错
	fmt.Println(a)
	fmt.Println(p)
	fmt.Printf("a = %T,p = %T\n", a, p)

	fmt.Println("========================")

	/*
		指向数组的指针还可以使用关键字new进行定义
		且不管是数组本身还是指向数组的指针 都可以使用数组/指针名加[]的方式来对单个元素进行赋值
		即都支持索引操作
	*/

	c := [10]int{}
	c[2] = 3
	fmt.Println(c)
	po := new([10]int)
	po[2] = 3
	fmt.Println(po)

	fmt.Println("========================")

	x, y := 1, 2
	b := [...]*int{&x, &y}
	//b是数组 它存放了指向int的指针
	fmt.Println(b)
	fmt.Printf("b = %T\n", b)
}

/*
关于new()函数：
	表达式new(T)将创建一个T类型的匿名变量，
	所做的是为T类型的新值分配并清零一块内存空间，
	然后将这块内存空间的地址作为结果返回，
	而这个结果就是指向这个新的T类型值的指针值，返回的指针类型为*T。
*/
