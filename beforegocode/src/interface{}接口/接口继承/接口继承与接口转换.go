package main

import (
	"fmt"
)

//humaner相当于子集
type humaner interface {
	sayhi()
}

//person相当于父集
type person interface {
	humaner //使用匿名字段嵌入的方式来达到继承的效果 此时person接口也拥有了humaner接口中声明的方法
	//而实际上go语言中是没有继承的
	sing(song string)
}

type student struct {
	name string
	id   int
}

func (tmp student) sayhi() {
	fmt.Printf("student[%s %d] say hi\n", tmp.name, tmp.id)
}

func (tmp student) sing(song string) {
	fmt.Println("student ", tmp.name, "is sing the ", song)
}

func main() {
	var i person
	i = student{"mike", 1}
	i.sayhi() //接口的继承
	i.sing("the way you love me")

	var ipro person
	ipro = student{"ipro student", 2}

	var ihuman humaner

	// ipro = ihuman :
	// cannot use ihuman (type humaner) as type person in assignment:
	// humaner does not implement person (missing sing method)

	ihuman = ipro //直接将ipro中的值赋给ihuman
	ihuman.sayhi()
	// ihuman.sing("i love you"):
	//ihuman.sing undefined (type humaner has no field or method sing)
	ipro.sing("i love you")

}
