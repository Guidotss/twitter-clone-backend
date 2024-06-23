package bcrypt

import "golang.org/x/crypto/bcrypt"

type BcryptAdapterImpl struct{}

type BcryptAdapter interface {
	HashPassword(password string) (string, error)
	ComparePassword(password, hashedPassword string) error
}

func NewBcryptAdapter() *BcryptAdapterImpl {
	return &BcryptAdapterImpl{}
}

func (adapter *BcryptAdapterImpl) HashPassword(password string) (string, error) {
	hashedPassword, err := bcrypt.GenerateFromPassword([]byte(password), bcrypt.DefaultCost)
	if err != nil {
		return "", err
	}
	return string(hashedPassword), nil
}

func (adapter *BcryptAdapterImpl) ComparePassword(password, hashedPassword string) error {
	err := bcrypt.CompareHashAndPassword([]byte(hashedPassword), []byte(password))
	if err != nil {
		return err
	}
	return nil
}
