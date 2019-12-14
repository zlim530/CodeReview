package main

import (
	"fmt"
)

func mdslice(s []int) {
	s[0] = 1
	s[1] = 2
}

func mdslice2(s []int) {
	s = make([]int, 2)
	s[0] = 1
	s[1] = 2
	fmt.Println("In mdslice2", s)
}

func mdslice3(s *[]int) {
	*s = append(*s, 1)
	*s = append(*s, 2)
}

//在 Go 中不存在引用传递，所有的参数传递都是值传递。
func main() {
	s := make([]int, 2)
	//当调用mdslice方法时重新开辟了内存，将s的内容，也就是slice的地址拷贝入了s'(即mdslice函数的形参s)
	//所以此时当操作slice时，s 和 s' 所指向的内存为同一块，就导致 s 的 slice 发生了改变
	//即这仍是一种值传递 只不过传递过去的值是数据地址的拷贝
	mdslice(s)
	fmt.Println(s)

	var s2 []int
	mdslice2(s2)
	fmt.Println(s2)
	//而在 s2 中，在调用mdslice2之前 s2并未分配内存 也就是说并未指向任何的slice内存区域
	//从而导致 s' 的 slice 修改不能反馈到 s2 上。

	s3 := make([]int, 2)
	mdslice3(&s3)
	fmt.Println("s3 = ", s3, "cap(s3) = ", cap(s3))
}
