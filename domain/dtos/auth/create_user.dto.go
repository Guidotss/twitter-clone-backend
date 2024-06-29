package auth

import "go.mongodb.org/mongo-driver/bson/primitive"

type CreateUserDTO struct {
	Email     string               `json:"email" validate:"required,email"`
	Password  string               `json:"password" validate:"required,min=6"`
	Profile   ProfileDTO           `json:"profile"`
	Followers []primitive.ObjectID `json:"followers"`
	Following []primitive.ObjectID `json:"following"`
}
