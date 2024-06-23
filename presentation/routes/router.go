package routes

import (
	adapters "twitter-clone-backend/infraestructure/adapters/auth"
	"twitter-clone-backend/presentation/auth"
	"twitter-clone-backend/presentation/health"

	"github.com/gofiber/fiber/v2"
	"go.mongodb.org/mongo-driver/mongo"
)

func AppRoutes(router fiber.Router, db *mongo.Client) {

	health.HealthRoute(router)
	auth.AuthRoutes(router.Group("/auth"), adapters.AuthAdapter(db))

	router.Use(func(c *fiber.Ctx) error {
		return c.Status(fiber.StatusNotFound).JSON(fiber.Map{
			"ok":      false,
			"message": "Resource not found",
		})
	})

}
