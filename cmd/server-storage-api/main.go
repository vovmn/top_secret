package main

import (
	"fmt"
	"log"
	"net/http"
	"server-storage/config"
	"server-storage/internal/database"
	"server-storage/internal/handlers/file"
	repo "server-storage/internal/repositories/file"
)

func main() {
	cfg, err := config.Load()
	fmt.Println(cfg)
	if err != nil {
		log.Println("ERROR load config:", err)
	}
	db := database.NewCon(cfg.DatabasePath)
	r := repo.New(db)
	app := file.New(r)
	http.HandleFunc("/storage/upload", app.UploadFileHandler(cfg.UploadPath))
	http.HandleFunc("/storage/get/", app.GetFileHandler(cfg.UploadPath))
	http.HandleFunc("/storage/delete/", app.DeleteFileHandler(cfg.UploadPath))

	http.ListenAndServe(cfg.Http.Port, nil)
}
