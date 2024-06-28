package auth

import (
	dtos "twitter-clone-backend/domain/dtos/auth"
	"twitter-clone-backend/domain/errors/exceptions"
	"twitter-clone-backend/domain/repositories/auth"
	"twitter-clone-backend/infraestructure/security/jwt"

	"github.com/go-playground/validator/v10"
)

type RegisterUseCaseResponse struct {
	Ok      bool
	Message string
	Token   string
	User    dtos.UserResponse
}

type RegisterUseCase interface {
	Execute(registerDTO dtos.RegisterDTO) (RegisterUseCaseResponse, error)
}

type RegisterUseCaseImpl struct {
	repository auth.AuthRepository
	validator  *validator.Validate
	jwtAdapter jwt.JwtAdapter
}

func NewRegisterUseCase(repository auth.AuthRepository) *RegisterUseCaseImpl {
	return &RegisterUseCaseImpl{
		repository: repository,
		validator:  validator.New(),
		jwtAdapter: jwt.NewJwtAdapter(),
	}
}

func (useCase *RegisterUseCaseImpl) Execute(registerDTO dtos.RegisterDTO) (RegisterUseCaseResponse, error) {

	err := useCase.validator.Struct(registerDTO)
	if err != nil {
		validatorErros := make(map[string]string)
		for _, err := range err.(validator.ValidationErrors) {
			validatorErros[err.Field()] = "Invalid " + err.Field()
		}
		return RegisterUseCaseResponse{}, exceptions.BadRequest{
			Errors: validatorErros,
		}
	}

	user, err := useCase.repository.Register(registerDTO)
	if err != nil {
		return RegisterUseCaseResponse{}, err
	}

	token := useCase.jwtAdapter.GenerateToken(user.ID)

	return RegisterUseCaseResponse{
		Ok:      true,
		Message: "User registered successfully",
		Token:   token,
		User: dtos.UserResponse{
			ID:     user.ID.Hex(),
			Name:   user.Name,
			Email:  user.Email,
			Avatar: user.Avatar,
		},
	}, nil
}
