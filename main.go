package main

import (
	"twitter-clone-backend/config"
	"twitter-clone-backend/presentation/routes"

	"github.com/gofiber/fiber/v2"
	"github.com/gofiber/fiber/v2/middleware/cors"
	"github.com/gofiber/fiber/v2/middleware/recover"
)

func main() {
	// Load .env file
	envs := config.LoadEnvs(".env")

	// Fiber instance
	app := fiber.New(config.NewFiberConfig())

	// Middlewares
	app.Use(cors.New())
	app.Use(recover.New())

	// Connect to MongoDB
	client := config.NewDataBase(envs.GetEnv("MONGO_URL"))

	// Routes
	group := app.Group("/api/v1")
	routes.AppRoutes(group, client)

	app.Listen(":" + envs.GetEnv("PORT"))
}
