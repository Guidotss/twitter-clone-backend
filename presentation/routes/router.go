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
	auth.AuthRoutes(router, adapters.AuthAdapter(db))
}
