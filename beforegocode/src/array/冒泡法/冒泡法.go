package main

import (
	"fmt"
)

func main() {
	a := [...]int{6, 7, 9, 4, 3, 1}
	fmt.Println(a)

	n := len(a)

	for i := 0; i < n; i++ {
		for j := 0; j < n-1; j++ {
			if a[i] < a[j] {
				a[i], a[j] = a[j], a[i]

			}
		}
	}
	fmt.Println(a)
}
