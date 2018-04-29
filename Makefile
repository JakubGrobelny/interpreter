NAME = interpreter.exe
FILES = Interpreter/src/*.cs

make:
	mcs -out:$(NAME) -pkg:dotnet $(FILES) 
clean:
	$(RM) $(NAME)
