package file

import (
	"server-storage/internal/entities"
	"server-storage/internal/repositories/file"
)

type FileRepository interface {
	Create(*entities.File) error
	Get(string) (*file.File, error)
	Delete(string) error
}
