package main

import (
	"fmt"
)

func main() {

	mySlice := make([]int, 5, 10)

	fmt.Println("len(mySlice):", len(mySlice))
	fmt.Println("cap(mySlice):", cap(mySlice))
}

// 如果需要往上例中mySlice已包含的5个元素后面继续新增元素，可以使用append()函数。
// 下面的代码可以从尾端给mySlice加上3个元素，从而生成一个新的数组切片：
// mySlice = append(mySlice,1,2,3)
// 函数append()的第二参数其实是一个不定参数，我们可以按自己需求添加若干个元素，
// 甚至可以直接将一个数组切片追加到另一个数组切片的末尾：
// mySlice2 := []int(8,9,10)
// mySlice = append(mySlice,mySlice2...)
// 需要注意的是，我们在第二个参数mySlice2后面加了三个点，即一个省略号，
// 如果没有这个省略号的话，会有编译错误，因为按append()的语义，从第二个参数起的
// 所有参数都是待附加的元素。因为mySlice中的元素类型为int，所以直接传递mySlice2是
// 行不通的。加上省略号相当于把mySlice2包好的所有元素打散后传入。
// 上述调用等同于：
// mySlice = append(mySlice,8,9,10)
// 数组切片会自动处理存储空间不足的问题。如果追加的内容长度超过当前已分配的存储空间
// （即cap()调用返回的信息），数组切片会自动分配一块足够大的内存。
