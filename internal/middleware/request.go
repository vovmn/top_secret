package middleware

import (
	"io"
	"net/http"
	"strings"
)

func ForwardRequest(w http.ResponseWriter, r *http.Request, target string) {
	client := &http.Client{}

	// читаем тело
	bodyBytes, _ := io.ReadAll(r.Body)
	r.Body.Close()

	// создаём новый запрос
	req, err := http.NewRequest(r.Method, target+r.URL.Path, strings.NewReader(string(bodyBytes)))
	if err != nil {
		http.Error(w, "failed to create request", http.StatusInternalServerError)
		return
	}

	// копируем заголовки
	for key, values := range r.Header {
		for _, v := range values {
			req.Header.Add(key, v)
		}
	}

	// выполняем
	resp, err := client.Do(req)
	if err != nil {
		http.Error(w, "failed to forward request", http.StatusBadGateway)
		return
	}
	defer resp.Body.Close()

	// копируем статус и заголовки
	for key, values := range resp.Header {
		for _, v := range values {
			w.Header().Add(key, v)
		}
	}
	w.WriteHeader(resp.StatusCode)

	// копируем тело ответа
	io.Copy(w, resp.Body)
}
