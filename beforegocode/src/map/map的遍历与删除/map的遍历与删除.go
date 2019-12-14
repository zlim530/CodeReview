package main

import (
	"fmt"
)

//在函数间传递映射并不会制造出该映射的一个副本，不是值传递，而是引用传递

func main() {
	m1 := map[int]string{1: "mike", 2: "yoyo"}

	for k, v := range m1 {
		fmt.Println(k, v)
	}

	for k := range m1 {
		fmt.Println("keys:", k, "values:", m1[k])
	}

	value, ok := m1[1]
	fmt.Println("value = ", value, ",ok = ", ok)

	value2, ok2 := m1[3]
	fmt.Println("value2 = ", value2, ",ok2 = ", ok2)

	m2 := map[int]string{1: "mike", 2: "yoyo", 3: "lily"}
	delete(m2, 2)
	//delete()函数需要两个参数 一个是待删除的map 一个是待删除map中的key值
	for k2, v2 := range m2 {
		fmt.Println(k2, v2)
	}

}
