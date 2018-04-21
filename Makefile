NAME = interpreter.exe
MAIN = Interpreter.cs

make:
	mcs -out:$(NAME) $(MAIN)
clean:
	$(RM) $(NAME)