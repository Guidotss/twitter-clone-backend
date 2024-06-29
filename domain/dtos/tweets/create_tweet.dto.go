package tweets

import "go.mongodb.org/mongo-driver/bson/primitive"

type CreateTweetDTO struct {
	UserID   string               `json:"user_id"`
	Content  string               `json:"content"`
	Images   []string             `json:"images"`
	Likes    []primitive.ObjectID `json:"likes"`
	Retweets []primitive.ObjectID `json:"retweets"`
	Replies  []primitive.ObjectID `json:"replies"`
	CreateAt primitive.Timestamp  `json:"create_at"`
}
