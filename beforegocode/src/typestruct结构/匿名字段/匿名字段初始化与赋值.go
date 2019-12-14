package main

import (
	"fmt"
)

//在定义结构体时 只提供类型 而不写字段名的方式 称为匿名字段 也叫嵌入字段

type person struct {
	name string
	sex  byte
	age  int
}

type student struct {
	person //匿名字段 默认student中包含了person中的所有字段
	id     int
	addr   string
}

func main() {

	//1.使用顺序初始化时 此处的匿名字段是一个结构体
	//要像正常结构体那样初始化 只不过是嵌入在另一个结构体中
	s1 := student{person{"nike", 'm', 19}, 1, "newyork"}
	fmt.Println(s1)
	fmt.Printf("s1 = %v\n", s1)

	//2.对部分成员初始化 虽然person是一个结构体 但它嵌入在studen中
	//  就相当一个字段 故对遵循结构体内字段指定赋值的语法
	//	语法：结构体名{字段名:字段的值,...}
	s2 := student{person: person{"mike", 'm', 18}, id: 2}
	fmt.Printf("s2 = %+v\n", s2)
	//%+v格式：显示更详细

	//  部分成员初始化2：对匿名字段中的字段也进行部分成员初始化操作
	s3 := student{person: person{name: "lal", age: 20}, addr: "sz"}
	fmt.Println("s3 = ", s3)

	fmt.Println("=================================")

	//1.匿名字段为结构体时的赋值 采用 变量名.字段名 的方式
	var s4 student
	s4.name = "nini" //等价于 s4.person.name="nini"
	s4.sex = 'f'
	s4.age = 16
	s4.id = 6
	fmt.Println("s4 = ", s4)

	var s5 student
	// 匿名字段为结构体时的赋值方法2：
	s5.person = person{"han", 'm', 25}
	fmt.Println("s5 = ", s5)

}
