package main

import "fmt"

//定义接口类型
type Humaner interface {
	//方法，只有声明，没有实现，由别的类型（自定义类型）实现
	sayhi()
}

type Student struct {
	name string
	id   int
}

//Student实现了sayhi()方法
func (tmp *Student) sayhi() {
	fmt.Printf("Student[%s, %d] sayhi\n", tmp.name, tmp.id)
}

type Teacher struct {
	addr  string
	group string
}

//Teacher实现了sayhi()方法
func (tmp *Teacher) sayhi() {
	fmt.Printf("Teacher[%s, %s] sayhi\n", tmp.addr, tmp.group)
}

type MyStr string //给内置类型取别名

//MyStr实现了sayhi()方法
func (tmp *MyStr) sayhi() {
	fmt.Printf("MyStr[%s] sayhi\n", *tmp)
}

//定义一个普通函数，函数的参数为接口类型
//只有一个函数，可以有不同表现，多态
func WhoSayHi(i Humaner) {
	fmt.Println("in who say hi functions:")
	i.sayhi()
}

func main() {
	//定义接口类型的变量
	var i Humaner

	//只要有类型是实现了此接口定义中声明的方法，那么这个类型（也即方法中的接收者类型）就可以给i（这个接口的变量）赋值
	// s := &Student{"mike", 666}
	// i = s
	i = &Student{"a", 111}
	i.sayhi()

	// t := &Teacher{"bj", "go"}
	// i = t
	i1 := Teacher{"b", "beego"} //并且可以采用:=赋值 让go自动推导类型
	i1.sayhi()                  //虽然方法中的接受者类型是指针型  但是我们在定义接口时可以不使用指针型的接受者类型
	//因为go语言中有很强大的地址自动转换机制 会自动将值引用转换为地址引用

	// var str MyStr = "hello mike"
	// i = &str
	i2 := MyStr("hello c") //而对于内置类型而言 要想使用:=让go自动推导类型 则需要强制将数据转换为我们自定义的内置类型
	i2.sayhi()

	// s := &Student{"i33333", 333}
	// t := &Teacher{"i3tttt", "go3"}
	str := MyStr("33333i333333")
	i3 := make([]Humaner, 3)
	//当我们定义一个切片 长度为3 切片中的元素均为humaner接口类型
	//则此时需要严格的按照方法中的接受者类型进行赋值
	i3[0] = &Student{"i33333", 333}
	i3[1] = &Teacher{"i3tttt", "go3"}
	i3[2] = &str

	for _, va := range i3 {
		va.sayhi()
		WhoSayHi(va)
	}

}
