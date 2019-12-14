package main

import (
	"fmt"
	"unsafe"
)

func main() {
	str := "hello"
	fmt.Println(unsafe.Sizeof(str)) //16
	fmt.Println("sizeof(str) = ", unsafe.Sizeof(str))
}
