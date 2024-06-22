package auth

import (
	"twitter-clone-backend/domain/dtos/auth"
)

type AuthRepository interface {
	Login(loginDTO auth.LoginDTO) (string, error)
	Register(registerDTO auth.RegisterDTO) (string, error)
	GetUserByEmail(email string) (string, error)
	GetUserByID(id string) (string, error)
}
