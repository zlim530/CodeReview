package main

import (
	"fmt"
)

/*
	go语言中有一个comma-ok断言可以判断某一接口中存放了什么类型的数据
	断言语法格式：
	value,bool1 = 某接口变量.(待确定数据类型)
	如果这个接口变量中存放的数据是待确定数据类型
	那么ok会返回true 否则返回false
	而value则是接口变量中存储的数据
*/

type inter interface{}

//定义了一个空接口类型 类型名为inter 即使用type给空接口取一个别名
//因为空接口不包含方法 故所有类型都实现了空接口 也即所有类型都可以给空接口变量赋值
//空接口就是一个万能类型，它可以保存任意类型的值

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

	// fmt.Println(slice[3])

	//range有两个返回值 前者是index索引号 后者是value也即被遍历切片的值
	//在这里value依次等于slice[0] slice[1] slice[2] slice[3]
	//也即上述我们赋值的1 "hello world" person{"mike", 1} false（bool类型）
	for ind, val := range slice {

		//而val.(类型)也有两个返回值 前者是数据变量本身也即1 "hello world" person{"mike", 1} false
		//而后者ok则是一个判断真假的bool类型
		if comma, ok := val.(int); ok == true {
			fmt.Printf("slice[%d] 的类型为int 内容为%d\n", ind, comma)
		} else if comma, ok := val.(string); ok == true {
			fmt.Printf("slice[%d] 的类型为string 内容为%s\n", ind, comma)
		} else if comma, ok := val.(person); ok == true {
			fmt.Printf("slice[%d] 的类型为person(type person struct) 内容为%s %d\n", ind, comma.name, comma.age)
		} else if comma, ok := val.(bool); ok == true {
			fmt.Printf("slice[%d] 的类型为bool 内容为%v\n", ind, comma)
		}
	}

}
