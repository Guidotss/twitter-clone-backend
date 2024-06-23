package auth

import (
	"context"
	"fmt"
	"time"
	"twitter-clone-backend/domain/datasources/auth"
	domain "twitter-clone-backend/domain/dtos/auth"
	"twitter-clone-backend/domain/entities"
	"twitter-clone-backend/domain/errors/exceptions"
	"twitter-clone-backend/infraestructure/security/bcrypt"

	"go.mongodb.org/mongo-driver/bson/primitive"
	"go.mongodb.org/mongo-driver/mongo"
)

type AuthDataSourceImpl struct {
	client *mongo.Collection
}

func NewAuthDataSourceImpl(client *mongo.Client) auth.AuthDataSource {
	return &AuthDataSourceImpl{
		client: client.Database("twitter-clone").Collection("users"),
	}
}

func (ds *AuthDataSourceImpl) Login(loginDTO domain.LoginDTO) (entities.User, error) {
	checkUser, err := ds.GetUserByEmail(loginDTO.Email)
	if err != nil {
		return entities.User{}, err
	}
	if checkUser.Email == "" {
		return entities.User{}, exceptions.UnauthorizeError{
			Message: "Email or password is incorrect",
		}
	}

	bcrypt := bcrypt.NewBcryptAdapter()
	err = bcrypt.ComparePassword(loginDTO.Password, checkUser.Password)

	if err != nil {
		return entities.User{}, exceptions.UnauthorizeError{
			Message: "Email or password is incorrect",
		}
	}

	fmt.Println(checkUser)

	return entities.User{
		ID:       checkUser.ID,
		Email:    checkUser.Email,
		Password: checkUser.Password,
		Name:     checkUser.Name,
	}, nil
}

func (ds *AuthDataSourceImpl) Register(registerDTO domain.RegisterDTO) (entities.User, error) {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()
	checkUser, err := ds.GetUserByEmail(registerDTO.Email)
	if err != nil {
		return entities.User{}, err
	}

	if checkUser.Email != "" {
		return entities.User{}, exceptions.BadRequest{
			Errors: map[string]string{
				"email": "Email already exists",
			},
		}
	}

	bcrypt := bcrypt.NewBcryptAdapter()
	hashedPassword, err := bcrypt.HashPassword(registerDTO.Password)
	if err != nil {
		return entities.User{}, err
	}

	result, err := ds.client.InsertOne(ctx, domain.RegisterDTO{
		Email:    registerDTO.Email,
		Password: hashedPassword,
		Name:     registerDTO.Name,
	})

	if err != nil {
		return entities.User{}, err
	}

	return entities.User{
		ID:       result.InsertedID.(primitive.ObjectID),
		Email:    registerDTO.Email,
		Password: hashedPassword,
		Name:     registerDTO.Name,
	}, nil

}

func (ds *AuthDataSourceImpl) GetUserByEmail(email string) (entities.User, error) {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	checkUser := ds.client.FindOne(ctx, map[string]string{"email": email})

	userDecoded := entities.User{}
	checkUser.Decode(&userDecoded)

	return entities.User{
		ID:       userDecoded.ID,
		Email:    userDecoded.Email,
		Password: userDecoded.Password,
		Name:     userDecoded.Name,
	}, nil
}

func (ds *AuthDataSourceImpl) GetUserByID(id string) (entities.User, error) {
	return entities.User{}, nil
}
