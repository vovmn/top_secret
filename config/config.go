package config

import (
	"os"

	"github.com/joho/godotenv"
)

type Config struct {
	Https HttpsConfig
}

type HttpsConfig struct {
	Port        string
	Certpath    string
	Certkeypath string
}

func Load() *Config {
	godotenv.Load()
	httpscfg := &HttpsConfig{
		Port:        os.Getenv("PORT"),
		Certpath:    os.Getenv("CERT_PATH"),
		Certkeypath: os.Getenv("CERT_KEY_PATH"),
	}

	return &Config{
		Https: *httpscfg,
	}
}
