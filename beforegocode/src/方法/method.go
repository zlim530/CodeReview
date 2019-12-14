package main

import (
	"fmt"
)

type person struct {
	name string
	id   int
}

func (tmp person) printf() {
	fmt.Println(tmp)
}

func main() {
	p := &person{"mike", 666}
	p.printf()
}

// type TZ int

// func (tz *TZ) Increase(a int) {
// 	*tz += TZ(a)
// }

// func main() {
// 	var a TZ = 100
// 	a.print()
// 	fmt.Println(a)
// 	a.Increase(100)
// 	fmt.Println(a)
// }

// func (a *TZ) print() {
// 	fmt.Println("TZ")
// }
