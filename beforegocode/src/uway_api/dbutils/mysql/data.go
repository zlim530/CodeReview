package mysql

import (
	"database/sql"

	"uway_api/common"
	"uway_api/dbutils"

	_ "github.com/go-sql-driver/mysql"
)

func conn(conf dbutils.Conf) *sql.DB {
	dsn := conf.User + ":" + conf.Pass + "@tcp(" + conf.Host + ":" + conf.Port + ")/" + conf.Name + "?charset=" + conf.Charset

	db, err := sql.Open("mysql", dsn)
	common.CheckErr(err)
	return db
}

func GetRows(conf dbutils.Conf, sql string) *sql.Rows {
	db := conn(conf)
	defer db.Close()
	rows, err := db.Query(sql)
	common.CheckErr(err)
	return rows
}

func Execute(conf dbutils.Conf, sql string) int {
	db := conn(conf)
	defer db.Close()
	res, err := db.Exec(sql)
	common.CheckErr(err)
	num, _ := res.RowsAffected()
	return int(num)
}

func Create(conf dbutils.Conf, sql string) int {
	db := conn(conf)
	defer db.Close()
	res, err := db.Exec(sql)
	common.CheckErr(err)
	id, _ := res.LastInsertId()
	return int(id)
}
