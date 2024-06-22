package auth

import (
	datasources "twitter-clone-backend/infraestructure/datasources/auth"
	respositories "twitter-clone-backend/infraestructure/repositories/auth"
	controllers "twitter-clone-backend/presentation/auth"

	"go.mongodb.org/mongo-driver/mongo"
)

func AuthAdapter(db *mongo.Client) *controllers.AuthControllerImpl {
	datasource := datasources.NewAuthDataSourceImpl(db)
	repository := respositories.NewAuthRepositoryImpl(datasource)
	return controllers.NewAuthController(repository)
}
