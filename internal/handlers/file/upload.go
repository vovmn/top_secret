package file

import (
	"io"
	"log"
	"net/http"
	"server-storage/internal/entities"
)

func (fh *FileHandler) UploadFileHandler() http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodPost {
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
		}
		file, header, err := r.FormFile("file")
		if err != nil {
			log.Println("Error parse FormFile", err)
			return
		}
		defer file.Close()

		mimeType := header.Header.Get("Content-Type")
		buf, _ := io.ReadAll(file)
		f := entities.New(header.Filename, mimeType, int(header.Size), buf)
		err = f.Save()

		if err != nil {
			log.Println("Error while uploading:", err)
			return
		}

		err = fh.FileRepo.Create(f)
		if err != nil {
			log.Println("Error while uploading:", err)
			return
		}
		w.WriteHeader(http.StatusOK)
		w.Write([]byte("File uploaded"))
		log.Println("Uploaded file: ", f.Name, f.Format, f.HashName, f.Size, f.MimeType)

	}
}
