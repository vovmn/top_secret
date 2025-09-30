package file

import (
	"log"
	"net/http"
	"server-storage/internal/entities"
	"strings"
)

func (fh *FileHandler) DeleteFileHandler() http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodDelete {
			http.Error(w, "Method not allowed", http.StatusMethodNotAllowed)
			return
		}
		u := strings.Split(r.URL.String(), "/")
		hashname := u[len(u)-1]
		f, err := fh.FileRepo.Get(hashname)
		if err != nil {
			log.Println("Error get row", err)
			return
		}
		err = entities.Delete(f.HashName, f.Format)
		if err != nil {
			log.Println("Error delete file", err)
			return
		}
		fh.FileRepo.Delete(hashname)

	}
}
