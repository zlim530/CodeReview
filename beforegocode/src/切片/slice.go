package main

import (
	"fmt"
)

func main() {
	/*
		var a1 []int
		a1[0] = 0
		是错误的
	*/

	a := make([]int, 2)
	a[0] = 1
	a[1] = 2

	fmt.Println(a)

	var b = []int{3, 4}
	fmt.Println(b)

	var c []int = make([]int, 2)
	c[0] = 5
	c[1] = 6
	fmt.Println(c)

	var d = []int{0: 7, 1: 8}
	fmt.Println(d)

}

func main2() {
	sarr := []int{0, 1, 2, 3, 4, 5, 6, 7, 8, 9}
	s := sarr[1:10:10]
	/*
		s[low:high:max]	从切片s的索引位置low到high处所获得的切片，len=high-low，cap=max-low
		又cap总是大于等于len 所以由len=high-low，cap=max-low可以得到
		max >= high
	*/
	fmt.Println(s)
	fmt.Printf("len(s) is %d , cap(s) is %d\n", len(s), cap(s))
}

func main1() {
	a := [...]int{1, 2, 3, 4, 5}
	sa := a[:]
	fmt.Println(sa)
	fmt.Print(sa, "\n")
	//Print不会自动换行 需要手动换行

	fmt.Printf("sa's type is %T\n", sa)

}
