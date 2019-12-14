package user

import (
	DB "uway_api/model"
)

// usr/user表中的所有字段
type Model struct {
	ID              string
	CreateTime      int64
	UpdateTime      int64
	UniUniversityID string
	IsEnable        int
	IsVerify        int
	Sex             int
	Age             int
	ExpireTime      int
	Score           int
	Password        string
	Email           string
	Mobile          string
	Name            string
	Avatar          string
	Remark          string
}

//登录
func List(where string) []Model {
	var models []Model

	sql := "SELECT `email`, `password`"
	// sql := "SELECT `id`, `name`, `email`, `password`, `uri` FROM sys_slide "
	sql += "WHERE 1=1 "
	sql += where
	sql += " ORDER BY sort"

	rows := DB.GetRows(sql, "ryanswoo")
	defer rows.Close()
	for rows.Next() {
		var model Model
		rows.Scan(&model.Email, &model.Password)
		//rows.Scan(&model.ID, &model.Name, &model.Email, &model.Password /*, &model.Uri*/)
		models = append(models, model)
	}

	return models
}

//注册
func Create(where string) []Model {
	var models []Model
	sql := "SELECT `id`, `name`, `email`, `password`"
	sql += "WHERE 1=1 "
	sql += where
	sql += " ORDER BY sort"

	rows := DB.GetRows(sql, "ryanswoo")
	defer rows.Close()
	for rows.Next() {
		var model Model
		rows.Scan(&model.ID, &model.Name, &model.Email, &model.Password)
		models = append(models, model)
	}

	return models

}
