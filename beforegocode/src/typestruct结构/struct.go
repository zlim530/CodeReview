package main

import (
	"fmt"
)

type A struct {
	name string
	B
}

type B struct {
	name string
}

//结构体C只有一个嵌入结构B
type C struct {
	B
}

type D struct {
	name string
}

type E struct {
	B
	D
}

type P struct {
	J
	name string
}

type J struct {
	name string
}

type F struct {
	P
}

func main() {
	a := A{name: "A", B: B{name: "B"}}
	fmt.Println(a.name, a.B.name)

	b := C{B: B{name: "B"}}
	fmt.Println(b.name, b.B.name)
	////结构体C只有一个嵌入结构B  则此时b.name, b.B.name都可以表示B中的name字段

	c := E{B: B{name: "B"}, D: D{name: "C"}}
	fmt.Println(c.D.name, c.B.name)
	//此时使用c.name就会报错 因为c中的结构体类型E包含两个嵌入字段B 、D 它们两是同级的关系
	//而它们又有相同的字段名 故编译器无法知道c.name到底是哪个嵌入结构的name字段

	// d := F{P: P{name: "P"}, J: J{name: "J"}}
	// fmt.Println(d.name, d.P.name, d.J.name)

}
