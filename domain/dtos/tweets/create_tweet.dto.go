package tweets

import (
	"time"
)

type CreateTweetDTO struct {
	UserID   string    `json:"user_id"`
	Content  string    `json:"content"`
	Images   []string  `json:"images"`
	Likes    []string  `json:"likes"`
	Retweets []string  `json:"retweets"`
	Replies  []string  `json:"replies"`
	CreateAt time.Time `json:"create_at"`
}
