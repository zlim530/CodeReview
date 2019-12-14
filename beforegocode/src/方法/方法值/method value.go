package main

import (
	"fmt"
)

/*
	Method Value格式为：
	变量名 := 类型实例出来的变量.类型中的方法
	而后此变量即为一个函数类型 使用变量名()的方式即可调用类型所实现的方法
	这种调用方法隐藏了接受者   它隐式传递receiver.
	且不管接受者类型是不是指针 都可以采用上述格式定义
	因为go中有非常灵活与智能的指针转化机制

*/

type person struct {
	name string
	age  int
}

func (tmp *person) setpersoninfo(n string, i int) {
	tmp.name = n
	tmp.age = i
	fmt.Println(tmp)
}

func (tmp person) funcsetvalue(n string, i int) {
	tmp.name = n
	tmp.age = i
	fmt.Println("in funcsetvalue:", tmp)

}

func main() {
	var p person
	p.setpersoninfo("nike", 666)
	fmt.Printf("%s,%d\n", p.name, p.age)

	p1 := person{"mike", 999}
	fmt.Printf("%s,%d\n", p1.name, p1.age)
	p1.funcsetvalue("mikeee", 1000)
	fmt.Printf("%s,%d\n", p1.name, p1.age)

	//方法值：method value
	p3 := p.setpersoninfo
	p3("nikeee", 6666)

	v3 := p.funcsetvalue
	v3("mikev3", 333)
}
