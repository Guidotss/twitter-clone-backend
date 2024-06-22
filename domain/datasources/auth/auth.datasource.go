package auth

import (
	"twitter-clone-backend/domain/dtos/auth"
	"twitter-clone-backend/domain/entities"
)

type AuthDataSource interface {
	Login(loginDTO auth.LoginDTO) (entities.User, error)
	Register(registerDTO auth.RegisterDTO) (entities.User, error)
	GetUserByEmail(email string) (entities.User, error)
	GetUserByID(id string) (entities.User, error)
}
