package auth

import (
	dtos "twitter-clone-backend/domain/dtos/auth"
	"twitter-clone-backend/domain/errors"
	"twitter-clone-backend/domain/repositories/auth"
	useCases "twitter-clone-backend/domain/use-cases/auth"

	"github.com/go-playground/validator/v10"
	"github.com/gofiber/fiber/v2"
)

type AuthControllerImpl struct {
	repository auth.AuthRepository
	validator  *validator.Validate
}

type AuthController interface {
	Login(ctx *fiber.Ctx) error
	Register(ctx *fiber.Ctx) error
	RefreshToken(ctx *fiber.Ctx) error
}

func NewAuthController(repository auth.AuthRepository) *AuthControllerImpl {
	return &AuthControllerImpl{
		repository: repository,
		validator:  validator.New(),
	}
}

func (controller *AuthControllerImpl) Login(ctx *fiber.Ctx) error {
	var loginDTO dtos.LoginDTO
	err := ctx.BodyParser(&loginDTO)
	errors.PanicLogging(err)
	response, err := useCases.NewLoginUseCase(controller.repository).Execute(loginDTO)

	if err != nil {
		return err
	}

	return ctx.Status(fiber.StatusOK).JSON(response)

}

func (controller *AuthControllerImpl) Register(ctx *fiber.Ctx) error {

	var registerDTO dtos.RegisterDTO
	err := ctx.BodyParser(&registerDTO)
	errors.PanicLogging(err)

	response, err := useCases.NewRegisterUseCase(controller.repository).Execute(registerDTO)

	if err != nil {
		return err
	}

	return ctx.Status(fiber.StatusCreated).JSON(response)
}

func (controller *AuthControllerImpl) RefreshToken(ctx *fiber.Ctx) error {
	authHeader := ctx.Get("Authorization")
	token := authHeader[7:]

	response, err := useCases.NewRefreshTokenUseCase(controller.repository).Execute(token)

	if err != nil {
		return err
	}

	return ctx.Status(fiber.StatusOK).JSON(response)
}
