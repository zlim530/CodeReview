package main

import (
	"fmt"
)

type person struct {
	name string
	age  int
}

type student struct {
	*person
	id int
}

func main() {

	s1 := student{&person{"asks", 20}, 1}
	fmt.Printf("%+v\n", s1)
	//会打印出person的地址信息 {person:0xc00005c420 id:1}

	fmt.Printf("%s,%d,%d\n", s1.name, s1.age, s1.id)
	//打印出person中的字段信息

	var s2 student
	s2.person = new(person)
	s2.name = "haohao"
	s2.age = 21
	s2.id = 3
	fmt.Printf("%+v\n", s2)
	fmt.Printf("%s,%d,%d\n", s2.name, s2.age, s2.id)
	/*
		关于new()函数：
			表达式new(T)将创建一个T类型的匿名变量，
			所做的是为T类型的新值分配并清零一块内存空间，
			然后将这块内存空间的地址作为结果返回，
			而这个结果就是指向这个新的T类型值的指针值，返回的指针类型为*T。
	*/

}
