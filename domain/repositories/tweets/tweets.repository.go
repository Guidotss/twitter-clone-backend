package tweets

import (
	dtos "twitter-clone-backend/domain/dtos/tweets"
	"twitter-clone-backend/domain/entities"
)

type TweetsRepository interface {
	CreateTweet(createTweetDTO dtos.CreateTweetDTO) (entities.Tweet, error)
	GetAllTweets() ([]entities.Tweet, error)
}
