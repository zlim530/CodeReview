package main

import (
	"fmt"
)

type inter interface{}

type person struct {
	name string
	age  int
}

func main() {
	slice := make([]inter, 4)
	//定义一个切片 长度为4 类型为空接口类型
	slice[0] = 1                 //int
	slice[1] = "hello world"     //string
	slice[2] = person{"mike", 1} //struct
	slice[3] = bool(false)       //bool

	for index, value := range slice {

		//type是switch结构的一个内置变量
		switch data := value.(type) {
		case int:
			fmt.Printf("slice[%d] 的类型为int 内容为%d\n", index, data)
		case string:
			fmt.Printf("slice[%d] 的类型为string 内容为%s\n", index, data)
		case person:
			fmt.Printf("slice[%d] 的类型为person(type person struct) 内容为%s %d\n", index, data.name, data.age)
		case bool:
			fmt.Printf("slice[%d] 的类型为bool 内容为%v\n", index, data)
		}
	}

}
