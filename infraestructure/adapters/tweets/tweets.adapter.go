package tweets

import (
	datasources "twitter-clone-backend/infraestructure/datasources/tweets"
	repository "twitter-clone-backend/infraestructure/repositories/tweets"
	controllers "twitter-clone-backend/presentation/tweets"

	"go.mongodb.org/mongo-driver/mongo"
)

func TweetsAdapter(db *mongo.Client) *controllers.TweetsControllerImpl {
	datasource := datasources.NewTweetsDatasource(db)
	repository := repository.NewTweetsRepository(datasource)
	return controllers.NewTweetsController(repository)
}
