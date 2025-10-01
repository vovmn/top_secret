package entities

import (
	"errors"
	"fmt"
	"io"
	"log"
	"os"
	"strings"

	"github.com/google/uuid"
)

type File struct {
	Name     string
	HashName string
	Format   string
	MimeType string
	Size     int
	Content  []byte
}

func New(name, mimeType string, size int, content []byte) *File {
	subs := strings.Split(name, ".")
	hashName := uuid.New().String()
	fmt.Println(subs)
	return &File{
		Name:     subs[0],
		HashName: hashName,
		Format:   subs[len(subs)-1],
		MimeType: mimeType,
		Size:     size,
		Content:  content,
	}
}

func (f *File) Save(uploadPath string) error {

	_, err := os.Stat(uploadPath + "/")
	if os.IsNotExist(err) {
		err = os.Mkdir(uploadPath, 0777)
		if err != nil {
			log.Println("Error creating uploads folder:", err)
			return err
		}
	}

	file, err := os.Create(uploadPath + "/" + f.HashName + "." + f.Format)
	if err != nil {
		return err
	}
	defer file.Close()

	_, err = file.Write(f.Content)

	if err != nil {
		return err
	}
	return nil
}

func Delete(deletePath, filename string, format string) error {
	name := filename + "." + format
	_, err := os.Stat(deletePath + "/" + name)
	if err != nil {
		log.Println("File not found", err)
		return err
	} else {
		err = os.Remove(deletePath + "/" + name)
		if err != nil {
			return errors.New("ERROR while deleting")
		}
	}
	return nil
}

func Open(openPath, fileName string, format string) ([]byte, error) {

	file, err := os.Open(openPath + "/" + fileName + "." + format)
	if err != nil {
		log.Println("Error open file:", err)
		return nil, err
	}
	defer file.Close()
	buf, err := io.ReadAll(file)
	if err != nil {
		log.Println("Error read file:", err)
		return nil, err
	}
	return buf, nil

}
