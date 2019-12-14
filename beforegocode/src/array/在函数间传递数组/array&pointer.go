package main

import (
	"fmt"
)

func main() {
	array := [...]int{1, 2, 3, 4, 5}
	modify(&array)

	fmt.Println("in main() array values is :", array)

}

func modify(array1 *[5]int) {
	(*array1)[0] = 10
	fmt.Println("in modify() array1 is :", array1)
	fmt.Println("in modify() array1 values is :", *array1)
}

// func modify(array1 [5]int) {
// 	array1[0] = 10
// 	fmt.Println("in modify() array1 values is :", array1)
// }
