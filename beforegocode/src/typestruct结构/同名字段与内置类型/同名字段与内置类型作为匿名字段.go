package main

import (
	"fmt"
)

type person struct {
	name string
	sex  string
	age  int
}

type student struct {
	person
	id   int
	name string //与person匿名字段中的name同名
}

func main() {
	var s student
	//当存在同名字段时  默认给最外层的字段赋值 也即最外层优先级更高
	s.name = "zxiong"
	fmt.Printf("%+v\n", s)

	//如果想给匿名字段中的同名字段赋值 需要显示声明
	s.person.name = "yoyo"
	fmt.Printf("%+v\n", s)

	s1 := student{person: person{name: "lala"}, name: "lalalalal"}
	fmt.Printf("%+v\n", s1)

	fmt.Println("===========================")

	/*
		其他类型的匿名字段：
		所有内置类型与自定义类型都可以作为匿名字段
	*/

	type mystr string //自定义类型

	type stu1 struct {
		person
		int //内置类型int
		mystr
	}

	s2 := stu1{person{"neinei", "mf", 19}, 1, "bj"}
	fmt.Printf("%+v\n", s2)

	var s3 stu1
	s3.name = "chenchen"
	s3.sex = "m"
	s3.age = 19
	s3.int = 2
	s3.mystr = "jing"
	fmt.Printf("%+v\n", s3)

}
