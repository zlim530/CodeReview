package main

import (
	"fmt"
)

//结构体也是一种值类型 在函数的参数传递时遵循值类型传递的准则

type Student struct {
	id   int
	name string
	sex  byte
	age  int
	addr string
}

func printStudentValue(tmp Student) {
	tmp.id = 250
	//printStudentValue tmp =  {250 mike 109 18 sz}
	fmt.Println("printStudentValue tmp = ", tmp)
}

func main() {
	var s Student = Student{1, "mike", 'm', 18, "sz"}

	printStudentValue(s) //值传递，形参的修改不会影响到实参
	fmt.Println("main s = ", s)

	var s2 Student = Student{2, "mike2", 'm', 18, "sz2"}

	printStudentPointer2(&s2) //引用(地址)传递，形参的修改会影响到实参
	fmt.Println("main s2 = ", s2)
}

func printStudentPointer2(p *Student) {
	p.id = 105
	//printStudentPointer p =  &{250 mike 109 18 sz}
	fmt.Println("printStudentPointer2 p = ", p)
}

// type s struct {
// 	id   int
// 	name string
// }

// func changes(ss s) {
// 	ss.id = 10
// 	fmt.Println("in changs() function ss = :", ss)
// }

// func changes2(tmp *s) {
// 	tmp.id = 20
// 	fmt.Println("in changes2() tmp = ", tmp)
// }

// func main() {
// 	s1 := s{1, "lim"}
// 	s2 := s{2, "qi"}
// 	fmt.Println(s1, s2)
// 	changes(s1)
// 	changes2(&s2)
// 	fmt.Println(s1, s2)

// }
