package auth

import (
	"twitter-clone-backend/domain/datasources/auth"
	domain "twitter-clone-backend/domain/dtos/auth"
	"twitter-clone-backend/domain/entities"

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

func (ds *AuthDataSourceImpl) Login(loginDTO domain.LoginDTO) (entities.User, error) {
	return entities.User{}, nil
}

func (ds *AuthDataSourceImpl) Register(registerDTO domain.RegisterDTO) (entities.User, error) {
	return entities.User{}, nil
}

func (ds *AuthDataSourceImpl) GetUserByEmail(email string) (entities.User, error) {
	return entities.User{}, nil
}

func (ds *AuthDataSourceImpl) GetUserByID(id string) (entities.User, error) {
	return entities.User{}, nil
}
