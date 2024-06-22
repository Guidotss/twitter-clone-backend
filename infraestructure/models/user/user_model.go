package user

type User struct {
	ID       string `json:"id" bson:"_id"`
	Name     string `json:"name" bson:"name" validate:"required"`
	Email    string `json:"email" bson:"email" validate:"required,email"`
	Password string `json:"password" bson:"password" validate:"required"`
	Image    string `json:"image" bson:"image"`
}
