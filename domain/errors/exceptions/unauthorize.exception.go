package exceptions

type UnauthorizeError struct {
	Message string
}

func (e *UnauthorizeError) Error() string {
	return e.Message
}
