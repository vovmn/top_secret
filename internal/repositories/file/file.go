package file

import (
	"database/sql"
	"log"
	"server-storage/internal/entities"
)

type File struct {
	Name     string
	HashName string
	Format   string
	MimeType string
	Size     int
}

type repository struct {
	db *sql.DB
}

func New(db *sql.DB) *repository {
	return &repository{db: db}
}

func (r *repository) Create(file *entities.File) error {
	_, err := r.db.Exec(CreateRow, file.Name, file.HashName, file.Format, file.MimeType, file.Size)
	if err != nil {
		log.Println("Error creating record in database:", err)
		return err
	}
	return nil
}

func (r *repository) Get(hashname string) (*File, error) {
	row := r.db.QueryRow(GetRow, hashname)
	file := &File{}
	err := row.Scan(&file.Name, &file.HashName, &file.Format, &file.MimeType, &file.Size)
	if err != nil {
		log.Println("Error scan row")
		return nil, err
	}
	return file, nil

}

func (r *repository) Delete(hashname string) error {
	_, err := r.db.Exec(DeleteRow, hashname)
	if err != nil {
		log.Println("Failed to DELETE row", "ERROR:", err)
		return err
	}
	return nil
}
