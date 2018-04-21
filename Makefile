NAME = interpreter.exe

make:
	mcs -out:$(NAME) src/interpreter.cs
clean:
	$(RM) $(NAME)