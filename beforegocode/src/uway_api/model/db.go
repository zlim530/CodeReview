package db

import (
	"database/sql"

	// "../common"
	// "../dbutils"
	// "../dbutils/mysql"

	"uway_api/common"
	"uway_api/dbutils"
	"uway_api/dbutils/mysql"
	// "/common"
	// "/dbutils"
	// "/dbutils/mysql"
	// ----> cannot import absolute path
)

func initConf(object string) dbutils.Conf {
	var conf dbutils.Conf
	conf.Type = common.Config("database", object, "type")
	conf.Host = common.Config("database", object, "host")
	conf.Name = common.Config("database", object, "name")
	conf.User = common.Config("database", object, "user")
	conf.Pass = common.Config("database", object, "pass")
	conf.Port = common.Config("database", object, "port")
	conf.Charset = common.Config("database", object, "charset")

	return conf
}

func GetRows(sql string, object string) *sql.Rows {
	conf := initConf(object)

	return mysql.GetRows(conf, sql)
}

func Execute(sql string, object string) int {
	conf := initConf(object)

	return mysql.Execute(conf, sql)
}

func Create(sql string, object string) int {
	conf := initConf(object)

	return mysql.Create(conf, sql)
}
