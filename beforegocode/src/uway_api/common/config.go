package common

import (
	"bufio"
	"io"
	"os"
	"strings"
)

const folder = "config/"

// common.Config("database", "erp", "hostname")
func Config(objectName string, className string, titleName string) string {
	path := folder + objectName + ".conf"
	file, err := os.Open(path)
	CheckErr(err)
	defer file.Close()
	locateClass := false
	value := ""

	read := bufio.NewReader(file)
	for {
		line, _, err := read.ReadLine()
		if err != nil {
			if err == io.EOF {
				break
			}
			panic(err)
		}
		content := strings.TrimSpace(string(line))
		if strings.Index(content, "#") == 0 {
			continue
		}

		if !locateClass {
			if strings.Index(content, "[") != 0 {
				continue
			}
			s1 := strings.Index(content, "[")
			s2 := strings.LastIndex(content, "]")
			class := strings.TrimSpace(content[s1+1 : s2])
			if class == className {
				locateClass = true
			}
			continue
		}

		index := strings.Index(content, "=")
		if index < 0 {
			continue
		}

		title := strings.TrimSpace(content[:index])
		if title == titleName {
			value = strings.TrimSpace(content[index+1:])
			break
		}
	}

	return value
}
