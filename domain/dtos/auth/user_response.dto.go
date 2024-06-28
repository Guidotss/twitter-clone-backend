package auth

import "go.mongodb.org/mongo-driver/bson/primitive"

type UserResponse struct {
	ID        string               `json:"id"`
	Email     string               `json:"email"`
	Profile   ProfileResponseDTO   `json:"profile"`
	Followers []primitive.ObjectID `json:"followers"`
	Following []primitive.ObjectID `json:"following"`
}
