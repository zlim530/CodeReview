package main

import (
	"encoding/json"
	"fmt"
)

type Monster struct {
	Name     string
	Age      int
	Birthday string
	Sal      float64
	Skill    string
}

func testStruck() {
	monster := Monster{
		Name:     "牛魔王",
		Age:      500,
		Birthday: "2011-11-11",
		Sal:      8000.0,
		Skill:    "牛魔拳",
	}

	data, err := json.Marshal(&monster)
	if err != nil {
		fmt.Println("err is ", err)
	}
	fmt.Println("mosnter 序列化后 = ", string(data))

}

func main() {
	testStruck()
}
