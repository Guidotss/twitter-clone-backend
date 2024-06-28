package auth

import (
	dtos "twitter-clone-backend/domain/dtos/auth"
	"twitter-clone-backend/domain/errors/exceptions"
	"twitter-clone-backend/domain/repositories/auth"
	"twitter-clone-backend/infraestructure/security/jwt"

	"github.com/go-playground/validator/v10"
)

type RefreshTokenUseCaseImpl struct {
	jwtAdapter jwt.JwtAdapter
	repository auth.AuthRepository
	validator  *validator.Validate
}

type RefreshTokenUseCase interface {
	Execute(token string) (string, error)
}

type RefreshTokenUseCaseResponse struct {
	Ok      bool
	Token   string
	Message string
	User    dtos.UserResponse
}

func NewRefreshTokenUseCase(repository auth.AuthRepository) *RefreshTokenUseCaseImpl {
	return &RefreshTokenUseCaseImpl{
		repository: repository,
		validator:  validator.New(),
		jwtAdapter: jwt.NewJwtAdapter(),
	}

}

func (useCase *RefreshTokenUseCaseImpl) Execute(token string) (RefreshTokenUseCaseResponse, error) {
	if !useCase.jwtAdapter.ValidateToken(token) {
		return RefreshTokenUseCaseResponse{}, exceptions.UnauthorizeError{
			Message: "Invalid token",
		}
	}
	claims, err := useCase.jwtAdapter.GetClaims(token)
	if err != nil {
		return RefreshTokenUseCaseResponse{}, err
	}

	user, err := useCase.repository.GetUserByID(claims["id"].(string))
	if err != nil {
		return RefreshTokenUseCaseResponse{}, err
	}

	newToken := useCase.jwtAdapter.GenerateToken(user.ID)

	return RefreshTokenUseCaseResponse{
		Ok:      true,
		Token:   newToken,
		Message: "Token refreshed successfully",
		User: dtos.UserResponse{
			ID:     user.ID.Hex(),
			Name:   user.Name,
			Email:  user.Email,
			Avatar: user.Avatar,
		},
	}, nil
}
