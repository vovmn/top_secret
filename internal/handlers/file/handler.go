package file

type FileHandler struct {
	FileRepo FileRepository
}

func New(FileRepo FileRepository) *FileHandler {
	return &FileHandler{FileRepo: FileRepo}
}
