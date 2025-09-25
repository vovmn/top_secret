package middleware

import (
	"encoding/json"
	"fmt"
	"io"
	"net/http"
	"strings"
)

type ValidateResponse struct {
	Valid  bool   `json:"valid"`
	UserID string `json:"user_id,omitempty"`
	Error  string `json:"error,omitempty"`
}

// Middleware для проверки токена
func AuthMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		token := extractToken(r)
		if token == "" {
			http.Error(w, "missing token", http.StatusUnauthorized)
			return
		}

		if !validateToken(token) {
			http.Error(w, "invalid token", http.StatusUnauthorized)
			return
		}

		// токен валидный пускаем дальше
		next.ServeHTTP(w, r)
	})
}

func extractToken(r *http.Request) string {
	authHeader := r.Header.Get("Authorization")
	if strings.HasPrefix(authHeader, "Bearer ") {
		return strings.TrimPrefix(authHeader, "Bearer ")
	}
	return ""
}

func validateToken(token string) bool {
	reqBody := strings.NewReader(fmt.Sprintf(`{"token":"%s"}`, token))
	resp, err := http.Post("http://localhost:9000/validate", "application/json", reqBody)
	if err != nil {
		fmt.Println("IAM request error:", err)
		return false
	}
	defer resp.Body.Close()

	body, _ := io.ReadAll(resp.Body)
	if resp.StatusCode != http.StatusOK {
		fmt.Println("IAM response error:", string(body))
		return false
	}

	var vr ValidateResponse
	if err := json.Unmarshal(body, &vr); err != nil {
		fmt.Println("IAM parse error:", err)
		return false
	}
	return vr.Valid
}
