package middleware

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io"
	"net/http"
)

// Структуры для работы с IAM
type RefreshRequest struct {
	AccessToken string `json:"accessToken"`
}

type RefreshResponse struct {
	AccessToken  string `json:"accessToken"`
	RefreshToken string `json:"refreshToken"`
	UserName     string `json:"userName"`
	Role         string `json:"role"`
	ExpiresIn    int    `json:"expiresIn"`
}

// Middleware skeleton (мы проверяем токен в ForwardRequest, поэтому здесь пусто)
func AuthMiddleware(next http.Handler) http.Handler {
	return http.HandlerFunc(func(w http.ResponseWriter, r *http.Request) {
		next.ServeHTTP(w, r)
	})
}

// Обновление токена через IAM
func RefreshTokenIfNeeded(token string) (*RefreshResponse, error) {
	reqBody := RefreshRequest{AccessToken: token}
	bodyBytes, _ := json.Marshal(reqBody)

	resp, err := http.Post("http://localhost:5129/Account/RefreshToken", "application/json", bytes.NewReader(bodyBytes))
	if err != nil {
		return nil, err
	}
	defer resp.Body.Close()

	respBytes, _ := io.ReadAll(resp.Body)
	if resp.StatusCode != http.StatusOK {
		return nil, fmt.Errorf("IAM returned status %d: %s", resp.StatusCode, string(respBytes))
	}

	var refreshResp RefreshResponse
	if err := json.Unmarshal(respBytes, &refreshResp); err != nil {
		return nil, err
	}

	return &refreshResp, nil
}
