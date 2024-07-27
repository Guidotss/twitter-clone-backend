package tweets

import (
	"twitter-clone-backend/domain/repositories/tweets"
	useCase "twitter-clone-backend/domain/use-cases/tweets"

	"github.com/go-playground/validator/v10"
	"github.com/gofiber/fiber/v2"
)

type TweetsController interface {
	GetAllTweets(ctx *fiber.Ctx) error
}

type TweetsControllerImpl struct {
	repository tweets.TweetsRepository
	validator  *validator.Validate
}

func NewTweetsController(repository tweets.TweetsRepository) *TweetsControllerImpl {
	return &TweetsControllerImpl{
		repository: repository,
		validator:  validator.New(),
	}
}

func (controller *TweetsControllerImpl) GetAllTweets(ctx *fiber.Ctx) error {
	response, err := useCase.NewLoadAllTweetsUseCase(controller.repository).Execute()
	if err != nil {
		return err
	}

	return ctx.Status(fiber.StatusOK).JSON(response)

}
