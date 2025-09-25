package main

import (
	"fmt"
	"log"
	"net/http"

	"gogw-service/config"
	mid "gogw-service/internal/middleware"

	"github.com/go-chi/chi/v5"
	"github.com/go-chi/chi/v5/middleware"
)

func main() {
	cfg := config.Load()
	r := chi.NewRouter()
	r.Use(middleware.Logger)

	r.Group(func(pr chi.Router) {
		pr.Use(mid.AuthMiddleware)

		pr.HandleFunc("/service1/*", func(w http.ResponseWriter, r *http.Request) {
			mid.ForwardRequest(w, r, "http://localhost:8081")
		})

		pr.HandleFunc("/service2/*", func(w http.ResponseWriter, r *http.Request) {
			mid.ForwardRequest(w, r, "http://localhost:8082")
		})
	})
	fmt.Println("Gateway running on :3000")
	err := http.ListenAndServeTLS(cfg.Https.Port, cfg.Https.Certpath, cfg.Https.Certkeypath, r)
	if err != nil {
		log.Println("server start error:", err)
	}
}
