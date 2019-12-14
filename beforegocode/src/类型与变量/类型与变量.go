package main

import (
	"fmt"
	"strconv"
)

//全局变量可以采用var()组的形式一次声明多个变量
//函数体内也可以使用这种格式声明 但不推荐使用
var (
	a1 = 9
	b2 = 0
	c3 = 78
)

func main() {
	var a int = 65

	b := string(a)

	fmt.Println(b)
	//会输出大写字母A 因为计算机只能存储数字 字符本身是不能存储的
	//故计算机会使用特定的数字来表示字符 故这里计算机就将数字65
	//理所当然的转换成字符"A"

	//如果想输出字符串的65 可以使用strconv包中的Itoa函数

	c := strconv.Itoa(a)

	a, _ = strconv.Atoi(c)
	//将string转换为int

	fmt.Println(c, a)
	fmt.Printf("a= %T  b = %T  c=%T\n", a, b, c)
}

func main1() {

	// var a int = 97

	// b := string(a)

	// var e int = 4
	/*
	   这种声明格式一般用在全局变量的声明中
	   因为全局变量的声明不能使用 := 这种形式
	   必须显示的声明
	*/

	// var c = 5

	// d := 6
	var (
		a4 = 95
		b5 = 90
		c6 = 78
	)
	fmt.Println(a4, b5, c6)

	fmt.Println(a1, b2, c3)
}
