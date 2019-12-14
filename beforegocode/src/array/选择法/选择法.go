package main

import (
	"fmt"
)

func main() {
	a := [...]int{6, 7, 9, 4, 3, 1}
	fmt.Println(a)

	n := len(a)
	for i := 0; i < n; i++ {
		for j := i + 1; j < n; j++ {
			if a[i] > a[j] {
				a[i], a[j] = a[j], a[i]
			}
		}
	}
	fmt.Println(a)

}
