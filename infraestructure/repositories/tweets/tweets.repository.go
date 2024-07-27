package tweets

import (
	"twitter-clone-backend/domain/datasources/tweets"
	dtos "twitter-clone-backend/domain/dtos/tweets"
	"twitter-clone-backend/domain/entities"
	repositories "twitter-clone-backend/domain/repositories/tweets"
)

type TweetsRepositoryImpl struct {
	datasource tweets.TweetsDataSource
}

func NewTweetsRepository(datasource tweets.TweetsDataSource) repositories.TweetsRepository {
	return &TweetsRepositoryImpl{
		datasource: datasource,
	}
}

func (r *TweetsRepositoryImpl) CreateTweet(createTweetDTO dtos.CreateTweetDTO) (entities.Tweet, error) {
	return r.datasource.CreateTweet(createTweetDTO)
}

func (r *TweetsRepositoryImpl) GetAllTweets() ([]entities.Tweet, error) {
	return r.datasource.GetAllTweets()
}
