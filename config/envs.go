package config

import (
	"os"

	"github.com/joho/godotenv"
)

type Envs interface {
	GetEnv(key string) string
}

type envsImpl struct{}

func (envsImpl) GetEnv(key string) string {
	return os.Getenv(key)
}

func LoadEnvs(filename ...string) Envs {
	err := godotenv.Load(filename...)
	if err != nil {
		panic(err)
	}
	return &envsImpl{}
}
