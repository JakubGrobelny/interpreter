NAME = interpreter.exe
FILES = src/*.cs

make:
	mcs -out:$(NAME) -pkg:dotnet $(FILES) 
clean:
	$(RM) $(NAME)