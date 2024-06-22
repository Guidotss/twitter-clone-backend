package errors

import (
	"twitter-clone-backend/domain/errors/exceptions"

	"github.com/gofiber/fiber/v2"
)

func ErrorHandler(ctx *fiber.Ctx, err error) error {
	_, unauthorizeError := err.(*exceptions.UnauthorizeError)
	if unauthorizeError {
		return ctx.Status(fiber.StatusUnauthorized).JSON(fiber.Map{
			"Code":    fiber.StatusUnauthorized,
			"Message": err.Error(),
			"Data":    err.Error(),
		})
	}

	return ctx.Status(fiber.StatusInternalServerError).JSON(fiber.Map{
		"Code":    fiber.StatusInternalServerError,
		"Message": err.Error(),
		"Data":    err.Error(),
	})
}
