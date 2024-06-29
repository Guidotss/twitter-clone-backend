package auth

type UserResponse struct {
	ID        string     `json:"id"`
	Email     string     `json:"email"`
	Profile   ProfileDTO `json:"profile"`
	Followers []string   `json:"followers"`
	Following []string   `json:"following"`
}
