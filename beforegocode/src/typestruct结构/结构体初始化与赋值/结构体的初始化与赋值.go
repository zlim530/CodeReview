package main

import (
	"fmt"
)

type Student struct {
	id   int
	name string
	sex  byte
	age  int
	addr string
}

func main() {

	//1.按顺序初始化  每个成员都必须初始化
	var s1 Student = Student{1, "mike", 'm', 18, "newyork"}
	fmt.Println(s1)

	//2.省略var关键字按顺序初始化
	s2 := Student{2, "yoyo", 'f', 17, "los"}
	fmt.Println(s2)

	//3.指定初始化某些成员，未初始化的成员为零值（类型默认值）
	var s3 Student = Student{id: 3, name: "lily", sex: 'f'}
	fmt.Println(s3)

	//4.指定初始化某些成员 也可以使用:=缺省var关键字的方法
	s4 := Student{id: 4, name: "zhou", age: 19}
	fmt.Println(s4)

	fmt.Println("============================")

	//1.声明Student类型变量 再使用.运算符进行赋值
	//  这种方式可以不按顺序赋值
	var s5 Student
	s5.id = 5
	s5.name = "chen"
	s5.sex = 'f'
	s5.addr = "SH"
	s5.age = 20
	fmt.Println(s5)

}
