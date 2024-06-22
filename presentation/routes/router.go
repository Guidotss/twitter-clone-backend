package routes

import (
	"twitter-clone-backend/presentation/routes/health"

	"github.com/gofiber/fiber/v2"
	"go.mongodb.org/mongo-driver/mongo"
)

func AppRoutes(router fiber.Router, db *mongo.Client) {
	health.HealthRoute(router)
}
