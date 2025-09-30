package main

import (
	"net/http"
	"server-storage/internal/database"
	"server-storage/internal/handlers/file"
	repo "server-storage/internal/repositories/file"
)

func main() {

	db := database.NewCon("database.db")
	r := repo.New(db)
	app := file.New(r)
	http.HandleFunc("/storage/upload", app.UploadFileHandler())
	http.HandleFunc("/storage/get/", app.GetFileHandler())
	http.HandleFunc("/storage/delete/", app.DeleteFileHandler())

	http.ListenAndServe(":3020", nil)
}
