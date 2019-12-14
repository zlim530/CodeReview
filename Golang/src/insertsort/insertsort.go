package main

import (
	"fmt"
)

func main() {
	// var arr = [6]int{8, 3, 6, 2, 7, 9}
	var num = [6]int{8, 3, 6, 2, 7, 9}
	// var i int
	// var j int
	// n := len(arr)

	// for i := 1; i < n; i++ {
	// 	needSort := arr[i]
	// 	for j := i + 1; j > 0; j-- {
	// 		if needSort < arr[j] {
	// 			arr[j+1] = arr[j]
	// 		} else {
	// 			break
	// 		}

	// 	}
	// 	arr[j] = needSort
	// }

	for i := 1; i < len(num); i++ {
		for j := i; j > 0; j-- {
			if num[j] < num[j-1] {
				num[j], num[j-1] = num[j-1], num[j]
			} else {
				break
			}
		}
	}

	for _, v := range num {
		fmt.Println(v)
	}
}
