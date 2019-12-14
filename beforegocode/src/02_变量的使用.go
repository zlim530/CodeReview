package main

import "fmt"

/*

单个变量的声明与赋值：
变量的声明格式：
	var 变量名称 变量类型
赋值格式：
	变量名称 = 表达式
初始化格式：即声明的同时进行赋值
	var 变量名称 变量类型 = 表达式
	或者
	var 变量名称 = 表达式
	或者
	变量名称 := 表达式

*/

func main() {

	a := 10
	var b int = 20
	var c int
	c = 30

	var d = 123
	fmt.Println(a, b, c, d)

	f1 := 3.14

	var f2 float64 = 3.141

	var f3 float64
	f3 = 3.1415

	var f4 = 3.14159
	fmt.Println(f1, f2, f3, f4)

	b1 := true

	var b2 bool = false

	var b3 bool
	b3 = true

	var b4 = false
	fmt.Println(b1, b2, b3, b4)

	arr := [2]int{1, 2}

	var arr1 [2]int = [2]int{3, 4}

	var arr2 [2]int
	// arr2 = [5,6]	不可以使用这种方式赋值 错误
	arr2[0] = 5
	arr2[1] = 6

	var arr3 = [2]int{7, 8}
	fmt.Println(arr, arr1, arr2, arr3)

	sli := []int{1, 2}

	var sli1 []int = []int{3, 4}

	// var sli2 []int	切片不支持这种定义之后再赋值
	//会引发panic --> panic: runtime error: index out of range
	// sli2[0] = 5
	// sli2[1] = 6
	sli2 := make([]int, 2)
	sli2[0] = 5
	sli2[1] = 6

	var sli3 = []int{7, 8}

	var sli4 []int = make([]int, 2)
	sli4[0] = 5
	sli4[1] = 6

	var sli5 = make([]int, 2)
	sli5[0] = 5
	sli5[1] = 6

	fmt.Println(sli, sli1, sli2, sli3, sli4)

}
