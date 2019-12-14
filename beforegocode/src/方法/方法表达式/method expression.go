package main

import (
	"fmt"
)

/*
	Method Expression的格式为：
		变量名 := (类型名).方法名
	当变量名想要调用类型中的方法时 必须显示传递接受者这个参数
	且如果类型中定义方法时的接受者类型为指针 则格式为：
	变量名 := (*类型名).方法名
	调用方式为：变量名(接受者)	//接受者按照方法中接受者类型判断要不要加&
*/

type person struct {
	name string
	age  int
}

func (tmp *person) setpersoninfo(n string, i int) {
	tmp.name = n
	tmp.age = i
	fmt.Println("in setpersoninfo:", tmp)
}

func (tmp person) funcsetvalue(n string, i int) {
	tmp.name = n
	tmp.age = i
	fmt.Println("in funcsetvalue:", tmp)
}

func main() {
	p := person{"adia", 999}
	p.funcsetvalue("adiaa", 500)
	fmt.Println("in main:", p)
	p.setpersoninfo("adiaas", 550)
	fmt.Println("in main:", p)

	p1 := (*person).funcsetvalue
	p1(&p, "b", 6)
	fmt.Println("in main:", p)
	p2 := (*person).setpersoninfo
	p2(&p, "b", 6)
	fmt.Println("in main:", p)

}
