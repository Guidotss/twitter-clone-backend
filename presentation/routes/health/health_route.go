package health

import (
	"github.com/gofiber/fiber/v2"
)

func HealthRoute(router fiber.Router) {
	router.Get("/ping", func(ctx *fiber.Ctx) error {
		return ctx.JSON(fiber.Map{
			"message": "pong",
		})
	})
}
