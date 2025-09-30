package file

const (
	CreateRow = "INSERT INTO meta (Name, HashName, Format, MimeType, Size) values (?, ?, ?, ?, ?)"
	GetRow    = "SELECT Name, HashName, Format, MimeType, Size FROM meta WHERE HashName=?"
	DeleteRow = "DELETE FROM meta WHERE HashName=?"
)
