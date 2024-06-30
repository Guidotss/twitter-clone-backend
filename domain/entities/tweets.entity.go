package entities

import (
	"time"
)

type Tweet struct {
	ID        string    `json:"id" bson:"_id"`
	UserID    string    `json:"user_id" bson:"user_id"`
	Content   string    `json:"content" bson:"content"`
	Images    []string  `json:"images" bson:"images"`
	Likes     []string  `json:"likes" bson:"likes"`
	Retweets  []string  `json:"retweets" bson:"retweets"`
	Replies   []string  `json:"replies" bson:"replies"`
	Create_at time.Time `json:"create_at" bson:"create_at"`
}
