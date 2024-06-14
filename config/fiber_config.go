package config

import (
	"drive-backend/domain/errors"

	"github.com/gofiber/fiber/v2"
)

func NewFiberConfig() fiber.Config {
	return fiber.Config{
		ErrorHandler: errors.ErrorHandler,
	}
}
