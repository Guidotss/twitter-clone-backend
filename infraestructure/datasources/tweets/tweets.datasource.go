package tweets

import (
	"twitter-clone-backend/domain/datasources/tweets"
	dtos "twitter-clone-backend/domain/dtos/tweets"
	"twitter-clone-backend/domain/entities"

	"go.mongodb.org/mongo-driver/mongo"
)

type TweetsRepositoryImpl struct {
	client *mongo.Collection
}

func NewTweetsRepository(client *mongo.Client) tweets.TweetsDataSource {
	return &TweetsRepositoryImpl{
		client: client.Database("twitter-clone").Collection("tweets"),
	}
}

func (ds *TweetsRepositoryImpl) CreateTweet(createTweetDto dtos.CreateTweetDTO) (entities.Tweet, error) {
	return entities.Tweet{}, nil
}
