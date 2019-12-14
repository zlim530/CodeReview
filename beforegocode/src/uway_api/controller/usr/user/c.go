package user

import (
	"encoding/json"
	// "uway_api/common"

	M "uway_api/model/usr/user"
)

//定义登录时返给程序/前端的字段
type Response struct {
	// ID         string `json:"id"`
	// CreateTime int64  `json:"create_time"`
	// IsVerify   int    `json:"is_verify"`

	// ExpireTime int    `json:"expire_time"`
	UniUniversityID string `json:"uni_university_id"`
	Sex             int    `json:"sex"`
	Age             int    `json:"age"`
	Email           string `json:"email"`
	Mobile          string `json:"moble"`
	Name            string `json:"name"`
	// Avatar          string `json:"avatar"`
	// Remark          string `json:"remark"`
}

//定义注册时返给程序/前端的字段
type RpsRegister struct {
	// ID              string `json:"id"`
	// CreateTime      int64  `json:"create_time"`
	// UniUniversityID string `json:"uni_university_id"`
	// IsVerify        int    `json:"is_verify"`
	// Sex             int    `json:"sex"`
	// Age             int    `json:"age"`
	UniUniversityID string `json:"uni_university_id"`
	Sex             int    `json:"sex"`
	Age             int    `json:"age"`
	Email           string `json:"email"`
	Mobile          string `json:"moble"`
	Name            string `json:"name"`
	// Avatar          string `json:"avatar"`
}

//登录的接口
func List(email string, password string) (int, string, int) {
	var response Response
	var returnString string
	where := "email and password"
	models := M.List(where)
	if len(models) == 0 {
		return 20101, "[]", 0
	}
	for _, model := range models {
		// var response Response
		response.ID = model.ID
		response.CreateTime = model.CreateTime

		response.IsVerify = model.IsVerify
		response.Sex = model.Sex
		response.Age = model.Age
		response.ExpireTime = model.ExpireTime
		response.Email = model.Email
		response.Mobile = model.Mobile
		response.Name = model.Name
		response.Remark = model.Remark

		jsonObject, _ := json.Marshal(response)
		returnString = string(jsonObject)

		if returnString == "null" {
			returnString = "[]"
		}

		return 200, returnString, len(models)
	}
	return 200, returnString, len(models)
}

//注册的接口
func Create(email string, password string, name string) (int, string, int) {
	var response RpsRegister
	var returnString string
	where := "name and email and password"
	models := M.Create(where)
	if len(models) == 0 {
		return 20101, "[]", 0
	}
	for _, model := range models {
		// var response Response

		response.ID = model.ID
		response.CreateTime = model.CreateTime
		response.UniUniversityID = model.UniUniversityID
		response.IsVerify = model.IsVerify
		response.Sex = model.Sex
		response.Age = model.Age
		response.Email = model.Email
		response.Mobile = model.Mobile
		response.Name = model.Name

		jsonObject, _ := json.Marshal(response)
		returnString = string(jsonObject)

		if returnString == "null" {
			returnString = "[]"
		}

		return 200, returnString, len(models)
	}
	return 200, returnString, len(models)
}
