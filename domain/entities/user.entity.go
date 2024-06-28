package entities

import "go.mongodb.org/mongo-driver/bson/primitive"

type User struct {
	ID        primitive.ObjectID   `json:"id" bson:"_id"`
	Email     string               `json:"email" bson:"email"`
	Password  string               `json:"password" bson:"password"`
	Profile   Profile              `json:"profile" bson:"profile"`
	Followers []primitive.ObjectID `json:"followers" bson:"followers"`
	Following []primitive.ObjectID `json:"following" bson:"following"`
}
type Profile struct {
	Name     string `json:"name" bson:"name"`
	Bio      string `json:"bio" bson:"bio"`
	Location string `json:"location" bson:"location"`
	Website  string `json:"website" bson:"website"`
	Avatar   string `json:"avatar" bson:"avatar"`
}
