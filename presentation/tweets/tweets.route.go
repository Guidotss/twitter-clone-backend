package tweets

import "github.com/gofiber/fiber/v2"

func TweetsRoutes(router fiber.Router, controller TweetsController) {
	router.Get("/", controller.GetAllTweets)
}
