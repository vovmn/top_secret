package middleware

import (
	"bytes"
	"encoding/json"
	"io"
	"net/http"
)

type MinimalPayload struct {
	UserName string `json:"userName,omitempty"`
	Role     string `json:"role,omitempty"`
}

func ForwardRequest(w http.ResponseWriter, r *http.Request, target string) {
	client := &http.Client{}

	accessToken := r.Header.Get("Authorization")
	if len(accessToken) > 7 && accessToken[:7] == "Bearer " {
		accessToken = accessToken[7:]
	}

	refreshResp, err := RefreshTokenIfNeeded(accessToken)
	if err != nil {
		http.Error(w, "failed to refresh token: "+err.Error(), http.StatusUnauthorized)
		return
	}

	bodyBytes, _ := io.ReadAll(r.Body)
	r.Body.Close()

	var fullBody map[string]interface{}
	json.Unmarshal(bodyBytes, &fullBody)

	minPayload := MinimalPayload{
		UserName: refreshResp.UserName,
		Role:     refreshResp.Role,
	}
	minJSON, _ := json.Marshal(minPayload)

	req, err := http.NewRequest(r.Method, target+r.URL.Path, bytes.NewReader(minJSON))
	if err != nil {
		http.Error(w, "failed to create request", http.StatusInternalServerError)
		return
	}

	for key, values := range r.Header {
		for _, v := range values {
			req.Header.Add(key, v)
		}
	}
	req.Header.Set("Content-Type", "application/json")

	resp, err := client.Do(req)
	if err != nil {
		http.Error(w, "failed to forward request", http.StatusBadGateway)
		return
	}
	defer resp.Body.Close()

	w.Header().Set("X-Access-Token", refreshResp.AccessToken)
	w.Header().Set("X-Refresh-Token", refreshResp.RefreshToken)

	for key, values := range resp.Header {
		for _, v := range values {
			w.Header().Add(key, v)
		}
	}
	w.WriteHeader(resp.StatusCode)
	io.Copy(w, resp.Body)
}
