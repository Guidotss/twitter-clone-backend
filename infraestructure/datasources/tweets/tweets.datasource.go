package tweets

import (
	"context"
	"fmt"
	"time"
	"twitter-clone-backend/domain/datasources/tweets"
	dtos "twitter-clone-backend/domain/dtos/tweets"
	"twitter-clone-backend/domain/entities"

	"go.mongodb.org/mongo-driver/bson/primitive"
	"go.mongodb.org/mongo-driver/mongo"
)

type TweetsDataSource struct {
	client *mongo.Collection
}

func NewTweetsDatasource(client *mongo.Client) tweets.TweetsDataSource {
	return &TweetsDataSource{
		client: client.Database("twitter-clone").Collection("tweets"),
	}
}

func (ds *TweetsDataSource) GetAllTweets() ([]entities.Tweet, error) {
	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	fmt.Println("GetAllTweets")
	cursor, err := ds.client.Find(ctx, nil)
	if err != nil {
		if err.Error() == mongo.ErrNilDocument.Error() {
			return nil, nil
		}
		return nil, err
	}

	var tweets []entities.Tweet
	for cursor.Next(ctx) {
		var tweet dtos.GetTweetDto
		if err = cursor.Decode(&tweet); err != nil {
			return nil, err
		}

		tweets = append(tweets, entities.Tweet{
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

	return tweets, nil
}

func (ds *TweetsDataSource) CreateTweet(createTweetDto dtos.CreateTweetDTO) (entities.Tweet, error) {
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
