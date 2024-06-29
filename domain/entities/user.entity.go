package entities

import (
	dtos "twitter-clone-backend/domain/dtos/auth"
)

type User struct {
	ID        string          `json:"id" bson:"_id"`
	Email     string          `json:"email" bson:"email"`
	Password  string          `json:"password" bson:"password"`
	Profile   dtos.ProfileDTO `json:"profile" bson:"profile"`
	Followers []string        `json:"followers" bson:"followers"`
	Following []string        `json:"following" bson:"following"`
}
