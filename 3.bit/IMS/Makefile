CXX = g++
CXXFLAGS = -Wall -Werror -Wextra -pedantic -std=c++9

SRC = src/*cpp
OBJ = $(SRC:.cc=.o)
EXEC = simulator

all: $(EXEC)

$(EXEC): $(OBJ)
	$(CXX) $(LDFLAGS) -o $@ $(OBJ) $(LBLIBS)

run:
	./simulator

.PHONY: all run
