package auth

import (
	datasources "twitter-clone-backend/domain/datasources/auth"
	dtos "twitter-clone-backend/domain/dtos/auth"
	respositories "twitter-clone-backend/domain/repositories/auth"
)

type AuthRepositoryImpl struct {
	datasource datasources.AuthDataSource
}

func NewAuthRepositoryImpl(datasource datasources.AuthDataSource) respositories.AuthRepository {
	return &AuthRepositoryImpl{
		datasource: datasource,
	}
}

func (r *AuthRepositoryImpl) Login(loginDTO dtos.LoginDTO) (string, error) {
	return r.datasource.Login(loginDTO)
}

func (r *AuthRepositoryImpl) Register(registerDTO dtos.RegisterDTO) (string, error) {
	return r.datasource.Register(registerDTO)
}

func (r *AuthRepositoryImpl) GetUserByEmail(email string) (string, error) {
	return r.datasource.GetUserByEmail(email)
}

func (r *AuthRepositoryImpl) GetUserByID(id string) (string, error) {
	return r.datasource.GetUserByID(id)
}
