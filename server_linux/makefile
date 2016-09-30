all: server client

server: server.cpp
	g++ $^ -o $@ -pthread -std=c++11

client: client2.cpp
	g++ $^ -o $@ -std=c++11

clean:
	rm -f server client *~
