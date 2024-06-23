package auth

import (
	dtos "twitter-clone-backend/domain/dtos/auth"
	"twitter-clone-backend/domain/errors"
	"twitter-clone-backend/domain/errors/exceptions"
	"twitter-clone-backend/domain/repositories/auth"

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
	return ctx.JSON(fiber.Map{
		"message": "login",
	})
}

func (controller *AuthControllerImpl) Register(ctx *fiber.Ctx) error {

	var registerDTO dtos.RegisterDTO
	err := ctx.BodyParser(&registerDTO)
	errors.PanicLogging(err)

	err = controller.validator.Struct(registerDTO)
	if err != nil {
		validationErrors := make(map[string]string)
		for _, err := range err.(validator.ValidationErrors) {
			validationErrors[err.Field()] = "Invalid " + err.Field()
		}
		return exceptions.BadRequest{
			Errors: validationErrors,
		}
	}

	newUser, err := controller.repository.Register(registerDTO)
	if err != nil {
		return err
	}

	return ctx.JSON(fiber.Map{
		"message": "register",
		"data":    newUser,
	})
}

func (controller *AuthControllerImpl) RefreshToken(ctx *fiber.Ctx) error {
	return ctx.JSON(fiber.Map{
		"message": "refresh token",
	})
}
