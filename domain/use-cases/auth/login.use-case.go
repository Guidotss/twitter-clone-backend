package auth

import (
	dtos "twitter-clone-backend/domain/dtos/auth"
	"twitter-clone-backend/domain/errors/exceptions"
	"twitter-clone-backend/domain/repositories/auth"
	"twitter-clone-backend/infraestructure/security/jwt"

	"github.com/go-playground/validator/v10"
)

type LoginUseCaseImpl struct {
	repository auth.AuthRepository
	validator  *validator.Validate
	jwtAdapter jwt.JwtAdapter
}
type User struct {
	Name   string `json:"name"`
	Email  string `json:"email"`
	Avatar string `json:"avatar"`
}
type LoginUseCaseResponse struct {
	Ok      bool
	Message string
	Token   string
	User    User
}

type LoginUseCase interface {
	Execute(loginDTO dtos.LoginDTO) (LoginUseCaseResponse, error)
}

func NewLoginUseCase(repository auth.AuthRepository) *LoginUseCaseImpl {
	return &LoginUseCaseImpl{
		repository: repository,
		validator:  validator.New(),
		jwtAdapter: jwt.NewJwtAdapter(),
	}
}

func (useCase *LoginUseCaseImpl) Execute(loginDTO dtos.LoginDTO) (LoginUseCaseResponse, error) {

	err := useCase.validator.Struct(loginDTO)
	if err != nil {
		validatorErros := make(map[string]string)
		for _, err := range err.(validator.ValidationErrors) {
			validatorErros[err.Field()] = "Invalid " + err.Field()
		}
		return LoginUseCaseResponse{}, exceptions.BadRequest{
			Errors: validatorErros,
		}
	}

	user, err := useCase.repository.Login(loginDTO)
	if err != nil {
		return LoginUseCaseResponse{}, err
	}

	token := useCase.jwtAdapter.GenerateToken(user.ID)

	return LoginUseCaseResponse{
		Ok:      true,
		Message: "login",
		Token:   token,
		User: User{
			Name:   user.Name,
			Email:  user.Email,
			Avatar: user.Avatar,
		},
	}, nil

}
