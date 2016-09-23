server: server.cpp
	g++ $^ -o $@ -pthread -std=c++11

client: client.cpp
	g++ $^ -o $@

clean:
	rm -f server client *~
