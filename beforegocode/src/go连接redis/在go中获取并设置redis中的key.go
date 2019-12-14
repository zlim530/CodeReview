package main

import (
	"fmt"

	"github.com/garyburd/redigo/redis"
)

func main() {

	c, err := redis.Dial("tcp", "192.168.12.31:6379")
	if err != nil {
		fmt.Println("conn redis failed err = ", err)
		return
	}

	fmt.Println("conn succ ...", c)

	defer c.Close()

	_, err = c.Do("set", "key1", 1)
	if err != nil {
		fmt.Println("conn do err = ", err)
		return
	}

	r, err := redis.Int(c.Do("get", "key1"))
	if err != nil {
		fmt.Println("redis do get err = ", err)
		return
	}

	fmt.Println(r)

}
