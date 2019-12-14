package main

import (
	"encoding/json"
	"fmt"
)

type Monster struct {
	NAME     string  `json:"name"` //实际是一种反射机制
	AGE      int     `json:"age"`
	BIRTHDAY string  `json:"birth"`
	SAL      float64 `json:"sal"`
	SKILL    string  `json:"skill"`
}

func unmarshalstruck() {
	str := "{\"name\":\"mike\",\"age\":18,\"birth\":\"2001-5-30\",\"sal\":1800,\"skill\":\"coding\"}"
	var monster Monster
	err := json.Unmarshal([]byte(str), &monster)
	if err != nil {
		fmt.Println("unmarshar err is ", err)
		return
	}
	fmt.Printf("反序列化 monster = %v，monster.name = %v\n", monster, monster.NAME)
}

func main() {
	// var a int = 20
	// fmt.Printf("type a is %T,a = %d\n", a, a)
	unmarshalstruck()
}
