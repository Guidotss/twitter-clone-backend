package routes

import (
	authAdapter "twitter-clone-backend/infraestructure/adapters/auth"
	tweetsAdapter "twitter-clone-backend/infraestructure/adapters/tweets"
	"twitter-clone-backend/presentation/auth"
	"twitter-clone-backend/presentation/health"
	"twitter-clone-backend/presentation/tweets"

	"github.com/gofiber/fiber/v2"
	"go.mongodb.org/mongo-driver/mongo"
)

func AppRoutes(router fiber.Router, db *mongo.Client) {

	health.HealthRoute(router)
	auth.AuthRoutes(router.Group("/auth"), authAdapter.AuthAdapter(db))
	tweets.TweetsRoutes(router.Group("/tweets"), tweetsAdapter.TweetsAdapter(db))

	router.Use(func(c *fiber.Ctx) error {
		return c.Status(fiber.StatusNotFound).JSON(fiber.Map{
			"ok":      false,
			"message": "Resource not found",
		})
	})

}
