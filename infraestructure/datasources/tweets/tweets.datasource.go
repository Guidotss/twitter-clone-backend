package tweets

import (
	"context"
	"time"
	"twitter-clone-backend/domain/datasources/tweets"
	dtos "twitter-clone-backend/domain/dtos/tweets"
	"twitter-clone-backend/domain/entities"

	"go.mongodb.org/mongo-driver/bson/primitive"
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
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	result, err := ds.client.InsertOne(ctx, dtos.CreateTweetDTO{
		UserID:   createTweetDto.UserID,
		Content:  createTweetDto.Content,
		Images:   createTweetDto.Images,
		Likes:    createTweetDto.Likes,
		Retweets: createTweetDto.Retweets,
		Replies:  createTweetDto.Replies,
		CreateAt: createTweetDto.CreateAt,
	})
	if err != nil {
		return entities.Tweet{}, err
	}

	return entities.Tweet{
		ID:        result.InsertedID.(primitive.ObjectID).Hex(),
		UserID:    createTweetDto.UserID,
		Content:   createTweetDto.Content,
		Images:    createTweetDto.Images,
		Likes:     createTweetDto.Likes,
		Retweets:  createTweetDto.Retweets,
		Replies:   createTweetDto.Replies,
		Create_at: createTweetDto.CreateAt,
	}, nil
}
