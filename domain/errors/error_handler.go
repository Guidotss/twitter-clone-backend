package errors

import (
	"twitter-clone-backend/domain/errors/exceptions"

	"github.com/gofiber/fiber/v2"
)

func ErrorHandler(ctx *fiber.Ctx, err error) error {
	_, unauthorizeError := err.(exceptions.UnauthorizeError)
	if unauthorizeError {
		return ctx.Status(fiber.StatusUnauthorized).JSON(fiber.Map{
			"Ok":      false,
			"Code":    fiber.StatusUnauthorized,
			"Message": "Invalid credentials",
			"Data":    err.Error(),
		})
	}

	_, badRequest := err.(exceptions.BadRequest)
	if badRequest {
		err := err.(exceptions.BadRequest)
		return ctx.Status(fiber.StatusBadRequest).JSON(fiber.Map{
			"Ok":      false,
			"Code":    fiber.StatusBadRequest,
			"Message": err.Error(),
			"Data":    err.Errors,
		})

	}

	return ctx.Status(fiber.StatusInternalServerError).JSON(fiber.Map{
		"Ok":      false,
		"Code":    fiber.StatusInternalServerError,
		"Message": err.Error(),
		"Data":    err.Error(),
	})
}
