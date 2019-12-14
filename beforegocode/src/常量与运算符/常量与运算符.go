package main

import (
	"fmt"
)

const (
	a       = 0
	b       = iota
	c, d, e = iota, iota, iota
	//c=d=e=2 iota在同一行值相同
)

func main() {
	fmt.Println(b<<10<<10<<10, "=1G")
	fmt.Println(b<<10<<10, "=1MB")
	fmt.Println(b<<10, "=1KB")
	fmt.Println(b, "=1B")
	fmt.Println(c, d, e)

}
