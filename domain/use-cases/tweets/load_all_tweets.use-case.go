package tweets

import (
	dtos "twitter-clone-backend/domain/dtos/tweets"
	"twitter-clone-backend/domain/repositories/tweets"
	"twitter-clone-backend/infraestructure/security/jwt"

	"github.com/go-playground/validator/v10"
)

type LoadAllTweetsUseCaseImpl struct {
	jwtAdapter jwt.JwtAdapter
	repository tweets.TweetsRepository
	validator  *validator.Validate
}

type LoadAllTweetsUseCase interface {
	Execute() (TweetResponse, error)
}
type TweetResponse struct {
	Ok      bool
	Message string
	Tweets  []dtos.GetTweetDto
}

func NewLoadAllTweetsUseCase(repository tweets.TweetsRepository) *LoadAllTweetsUseCaseImpl {
	return &LoadAllTweetsUseCaseImpl{
		repository: repository,
		jwtAdapter: jwt.NewJwtAdapter(),
		validator:  validator.New(),
	}
}

func (useCase *LoadAllTweetsUseCaseImpl) Execute() (TweetResponse, error) {

	tweets, err := useCase.repository.GetAllTweets()
	if err != nil {
		return TweetResponse{}, err
	}

	getTweetDtos := make([]dtos.GetTweetDto, 0)
	for _, tweet := range tweets {
		getTweetDtos = append(getTweetDtos, dtos.GetTweetDto{
			ID:        tweet.ID,
			UserID:    tweet.UserID,
			Content:   tweet.Content,
			Images:    tweet.Images,
			Likes:     tweet.Likes,
			Retweets:  tweet.Retweets,
			Replies:   tweet.Replies,
			Create_at: tweet.Create_at,
		})
	}

	return TweetResponse{
		Ok:      true,
		Message: "Tweets loaded successfully",
		Tweets:  getTweetDtos,
	}, nil
}
