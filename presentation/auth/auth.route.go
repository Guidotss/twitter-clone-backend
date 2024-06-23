package auth

import (
	"github.com/gofiber/fiber/v2"
)

func AuthRoutes(router fiber.Router, controller AuthController) {

	router.Post("/login", controller.Login)
	router.Post("/register", controller.Register)
}
