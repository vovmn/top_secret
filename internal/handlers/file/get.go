package file

import (
	"log"
	"net/http"
	"server-storage/internal/entities"
	"strings"
)

func (fh *FileHandler) GetFileHandler(fileFolder string) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		if r.Method != http.MethodGet {
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
		content, err := entities.Open(fileFolder, hashname, f.Format)
		if err != nil {
			log.Println("Error open file:", err)
			return
		}
		w.Header().Set("Content-Disposition", "attachment; filename=\""+f.Name+"\\."+f.Format+"\"")
		w.Header().Set("Content-Type", f.MimeType)
		w.Write(content)

	}
}
