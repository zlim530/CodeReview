package main

import (
	"encoding/json"

	"github.com/gin-gonic/gin"
)

//定义服务器返回给程序/前端的结构体
type DataPackage struct {
	Code  int    `json:"code"`
	Msg   string `json:"msg"`
	Count int    `json:"count"`
	//每次返回多少条信息给前端
	Data string `json:"data"`
	//返回的具体数据信息存储在data中 它是json字符串
}

func main() {

	r := gin.Default()

	//c 引擎框架
	r.POST("/login", func(c *gin.Context) {

		inputdata := jsonDataInterface(c.PostForm("inputdata"))

		//c.PostForm即获取post过来的formdata参数
		// inputdata := c.PostForm("inputdata")
		// data := jsonDataInterface(inputdata)

		if inputdata["email"] == nil {
			c.JSON(200, gin.H{
				"message": "no email param",
			})
			return
		}

		if inputdata["password"] == nil {
			c.JSON(200, gin.H{
				"message": "no password param",
			})
			return
		}

		c.JSON(200, gin.H{
			"code": 200,
			"msg":  "successful",
			"data": gin.H{
				"email":             "go.edu",
				"uni_university_id": "1001",
				"sex":               1,
				"age":               20,
				"mobile":            "182-xxxx-xxxx",
				"name":              "jack",
			},
			"count": 1,
		})

	})

	r.POST("/register", func(c *gin.Context) {

		inputdata := jsonDataInterface(c.PostForm("inputdata"))

		if inputdata["name"] == nil {
			c.JSON(200, gin.H{
				"message": "no username param",
			})
			return
		}

		if inputdata["email"] == nil {
			c.JSON(200, gin.H{
				"message": "no email param",
			})
			return
		}

		if inputdata["password"] == nil {
			c.JSON(200, gin.H{
				"message": "no password param",
			})
			return
		}

		c.JSON(200, gin.H{
			"code": 200,
			"msg":  "successful",
			"data": gin.H{
				`\"email\"`:         `\"collage/xxx/.edu\"`,
				"uni_university_id": "1002",
				"`\"sex\"":          "2`",
				"`\"age\"":          "21`",
				"`\"mobile\"":       "\"+86-xxxx-xxxx\"`",
				"`\"name\"":         "\"lily\"`",
			},
			"count": 1,
		})

	})

	r.Run(":30000")

}

//将前端传来的数据转化为map[string]interface{}的map 即其key为string 而其value为任意类型
//value就是前端传来的具体数据的值 而这是不可预见的 所以使用这种方式将其转为为任意类型的数据并存放在data中
func jsonDataInterface(jsonString string) map[string]interface{} {
	var data map[string]interface{}
	_ = json.Unmarshal([]byte(jsonString), &data)
	return data
}
