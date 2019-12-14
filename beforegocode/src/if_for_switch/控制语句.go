package main

import (
	"fmt"
)

func main() {
	s := [...]int{'a', 'b', 'c', 'd', 'e'}
	//range 遍历数组
	for i := range s { //支持 string/array/slice/map。
		fmt.Printf("%c\n", s[i])
	}

	for _, c := range s { // 忽略 index
		fmt.Printf("%c\n", c)
	}

	for i, c := range s {
		fmt.Printf("%d, %c\n", i, c)
	}
	//关键字 range 会返回两个值，第一个返回值是元素的数组下标，第二个返回值是元素的值：

}

func main2() {
	if a := 3; a > 3 {
		fmt.Println("a>3")
	} else if a < 3 {
		fmt.Println("a<3")
	} else if a == 3 {
		fmt.Println("a=3")
	} else {
		fmt.Println("error")
	}
}

func main1() {
LABEL1:
	for i := 0; i < 10; i++ {
		for {
			fmt.Println(i)
			continue LABEL1

		}
	}
	fmt.Println("over")
}
