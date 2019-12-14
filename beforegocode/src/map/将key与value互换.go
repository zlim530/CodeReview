package main

import (
	"fmt"
)

func main1() {

	var m5 map[int]string
	fmt.Println(m5)

	var mm map[int]string = map[int]string{1: "a"}
	fmt.Println(mm)

	m3 := map[int]string{}
	fmt.Println(m3)

	m6 := map[int]string{2: "b"}
	fmt.Println(m6)

	m9 := make(map[int]string)
	fmt.Println(m9)
	m9[3] = "c"
	fmt.Println(m9)

	var m4 map[int]string = make(map[int]string)
	fmt.Println(m4)
	m4[4] = "d"
	fmt.Println(m4)

}

func main() {
	m1 := map[int]string{8: "h", 3: "c", 9: "i", 5: "e", 7: "g", 4: "d", 2: "b", 1: "a"}
	m2 := make(map[string]int)
	for k, v := range m1 {
		m2[v] = k
	}

	fmt.Println(m1)
	fmt.Println(m2)
}
