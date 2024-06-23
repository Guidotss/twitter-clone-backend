package jwt

import (
	"time"
	"twitter-clone-backend/config"

	"github.com/golang-jwt/jwt/v5"
	"go.mongodb.org/mongo-driver/bson/primitive"
)

type JwtAdapterImpl struct {
	secretKey string
}

type JwtAdapter interface {
	GenerateToken(id primitive.ObjectID) string
	ValidateToken(token string) bool
}

func NewJwtAdapter() *JwtAdapterImpl {
	return &JwtAdapterImpl{
		secretKey: config.LoadEnvs().GetEnv("JWT_SECRET_KEY"),
	}
}

func (adapter *JwtAdapterImpl) GenerateToken(id primitive.ObjectID) string {
	claims := jwt.NewWithClaims(jwt.SigningMethodHS256, jwt.MapClaims{
		"authorized": true,
		"id":         id,
		"exp":        time.Now().Add(time.Hour * 2).Unix(),
	})

	token, err := claims.SignedString([]byte(adapter.secretKey))
	if err != nil {
		panic(err)
	}

	return token
}

func (adapter *JwtAdapterImpl) ValidateToken(token string) bool {
	claims := jwt.MapClaims{}
	_, err := jwt.ParseWithClaims(token, claims, func(token *jwt.Token) (interface{}, error) {
		return []byte(adapter.secretKey), nil
	})

	if err != nil {
		return false
	}

	return true
}
