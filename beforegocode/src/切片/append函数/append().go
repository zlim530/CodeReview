package main

import (
	"fmt"
)

func main() {
	s := make([]int, 0, 1)
	c := cap(s)
	for i := 0; i < 50; i++ {
		s = append(s, i)

		if n := cap(s); n > c {
			fmt.Printf("第 %d 次 ：cap : %d --> %d \n", i, c, n)
		}
	}
}
