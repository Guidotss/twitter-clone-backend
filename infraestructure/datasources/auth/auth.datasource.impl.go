package auth

import (
	"twitter-clone-backend/domain/datasources/auth"
	domain "twitter-clone-backend/domain/dtos/auth"

	"go.mongodb.org/mongo-driver/mongo"
)

type AuthDataSourceImpl struct {
	client *mongo.Client
}

func NewAuthDataSourceImpl(client *mongo.Client) auth.AuthDataSource {
	return &AuthDataSourceImpl{
		client: client,
	}
}

func (ds *AuthDataSourceImpl) Login(loginDTO domain.LoginDTO) (string, error) {
	return "", nil
}

func (ds *AuthDataSourceImpl) Register(registerDTO domain.RegisterDTO) (string, error) {
	return "", nil
}

func (ds *AuthDataSourceImpl) GetUserByEmail(email string) (string, error) {
	return "", nil
}

func (ds *AuthDataSourceImpl) GetUserByID(id string) (string, error) {
	return "", nil
}
