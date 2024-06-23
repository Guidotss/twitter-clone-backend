package auth

import (
	"twitter-clone-backend/domain/repositories/auth"

	"github.com/gofiber/fiber/v2"
)

type AuthControllerImpl struct {
	repository auth.AuthRepository
}

type AuthController interface {
	Login(ctx *fiber.Ctx) error
	Register(ctx *fiber.Ctx) error
	RefreshToken(ctx *fiber.Ctx) error
}

func NewAuthController(repository auth.AuthRepository) *AuthControllerImpl {
	return &AuthControllerImpl{
		repository: repository,
	}
}

func (controller *AuthControllerImpl) Login(ctx *fiber.Ctx) error {
	return ctx.JSON(fiber.Map{
		"message": "login",
	})
}

func (controller *AuthControllerImpl) Register(ctx *fiber.Ctx) error {
	return ctx.JSON(fiber.Map{
		"message": "register",
	})
}

func (controller *AuthControllerImpl) RefreshToken(ctx *fiber.Ctx) error {
	return ctx.JSON(fiber.Map{
		"message": "refresh token",
	})
}
