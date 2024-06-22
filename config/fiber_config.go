package config

import (
	"twitter-clone-backend/domain/errors"

	"github.com/gofiber/fiber/v2"
)

func NewFiberConfig() fiber.Config {
	return fiber.Config{
		ErrorHandler: errors.ErrorHandler,
	}
}
