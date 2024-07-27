package tweets

import "time"

type GetTweetDto struct {
	ID        string    `json:"id"`
	UserID    string    `json:"user_id"`
	Content   string    `json:"content"`
	Images    []string  `json:"images"`
	Likes     []string  `json:"likes"`
	Retweets  []string  `json:"retweets"`
	Replies   []string  `json:"replies"`
	Create_at time.Time `json:"create_at"`
}
