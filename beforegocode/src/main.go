package main

import (
	"fmt"
	"test"
)

func main() {
	var s3 test.Student02
	s3.Id = 1
	// s3.name = "mike"
	//无法给s3的name赋值 因为name在test包中的Student02结构体中定义时其字段名为小写
	//故仅能在包内使用
	fmt.Println(s3)

	s4 := test.Student03{2, "nike"}
	fmt.Println(s4)
}
