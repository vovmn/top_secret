package config

import (
	"log"
	"os"

	"github.com/joho/godotenv"
)

type Config struct {
	Http         HttpConfig
	UploadPath   string
	DatabasePath string
}

type HttpConfig struct {
	Port string
}

func Load() (*Config, error) {
	if err := godotenv.Load(); err != nil {
		log.Println("Error loading .env file. Loading default values")
		return nil, err
	}
	httpPort := os.Getenv("PORT")
	httpCfg := &HttpConfig{Port: httpPort}
	return &Config{
		Http:         *httpCfg,
		UploadPath:   os.Getenv("UPLOAD_PATH"),
		DatabasePath: os.Getenv("DATABASE_PATH"),
	}, nil
}
