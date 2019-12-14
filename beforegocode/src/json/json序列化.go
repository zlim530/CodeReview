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

func teststruck() {
	// monster := Monster{NAME: "牛魔王", AGE: 500, BIRTHDAY: "2011-11-11", SAL: 8000.0, SKILL: "牛魔拳"}
	monster := Monster{"mike", 18, "2001-5-30", 1800.0, "coding"}
	data, err := json.Marshal(monster)
	if err != nil {
		fmt.Println("序列化错误 err = ", err)
	}

	fmt.Println("Monster 序列化后 =", string(data))

}

func testmap() {
	var m map[string]interface{} = make(map[string]interface{})
	m["name"] = "jack"
	m["age"] = 20
	m["birthday"] = "1999-01-01"
	m["hobby"] = "watching film"
	data, err := json.Marshal(m)
	if err != nil {
		fmt.Println("序列化错误 err = ", err)
	}

	fmt.Println("map 序列化后 =", string(data))

}

func testslice() {
	var sli []map[string]interface{}
	m1 := make(map[string]interface{})
	m1["name"] = "like"
	m1["age"] = 21
	m1["birth"] = "1998.04.28"
	m1["hobby"] = "listen music"

	sli = append(sli, m1)
	fmt.Println("len(sli) = ", len(sli))
	m2 := make(map[string]interface{})
	m2["name"] = "roby"
	m2["age"] = 16
	m2["birth"] = "1998.05.28"
	m2["hobby"] = "social"
	sli = append(sli, m2)
	fmt.Println("len(sli) = ", len(sli))

	data, err := json.Marshal(sli)
	if err != nil {
		fmt.Println("序列化错误 err = ", err)
	}

	fmt.Println("slice 序列化后 =", string(data))
	//序列化后会变成一个json字串的数组

}

func main() {
	teststruck()
	testmap()
	testslice()
}
