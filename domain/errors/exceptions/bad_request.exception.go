package exceptions

type BadRequest struct {
	Errors map[string]string
}

func (e BadRequest) Error() string {
	return "Bad Request"
}
