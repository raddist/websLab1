// файл клиента для лабораторной работы 7 по осям
// автор: Родин Андрей <andreyrodin405@yandex.ru>
#include <sys/socket.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <iostream>
#include <fcntl.h> // non blocking read



////////
#include <sys/socket.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <netinet/in.h>
#include <arpa/inet.h>

#include <iostream>
#include <string>
#include <sstream>
#include <list>     // подключаем заголовок списка
#include <iterator> // заголовок итераторов
#include <fcntl.h> // non blocking read
#include <thread>
#include <mutex>
////////

#include "interconnection.h"
using namespace std;
int main(int argc, char ** argv)
{
	int port_num;
	int nfd, fd, nread;
	char buf[BUFSIZE];
	char username[BUFSIZE]; // как зовут юзера в чате
	int usr_size; // длина ника
	//char eexit[] = {'e','x','i','t'};
	char tbuf[4];


	sockaddr_in servaddr; // адрес сервера

	if (argc < 4)
		printf("Too few arguments: ip port username \n"), exit(1);
	if ((fd = socket(AF_INET, SOCK_STREAM, 0))<0) // Создать сокет типа SOCK_STREAM
		perror("socket creating"), exit(1);
	// Сформировать структуру с адресом сервера
		memset(&servaddr, 0, SOCKADDR_IN_SIZE); // Обнулить структуру для адреса
	servaddr.sin_family = AF_INET;
	// Преобразовать адрес сервера в двоичное число в сетевом порядке
		if(inet_pton(AF_INET, argv[1], &servaddr.sin_addr) <= 0)
			perror("bad address"), exit(1);// Преобразовать номер порта сервера в сетевой порядок
	servaddr.sin_port = htons(atoi(argv[2]));
	// Установить соединение с сервером, указанным в servaddr
		if (connect(fd, (sockaddr *)&servaddr, SOCKADDR_IN_SIZE)<0)
			perror("connect1"), exit(1);
	//cerr << "Введите желаемый ник в чате: ";
	//printf("enter\n");
	//usr_size = read(1,username,BUFSIZE);
	//cerr<< usr_size<<endl;;
	//cin >> username;
	char ttt[100] = "NEW ";
	strcat(ttt,argv[3]);
			if(write(fd,ttt,20) < 0)
				perror("write") , exit(1);

		while ((nread = read(fd, buf, BUFSIZE)) > 0) // Чтение c сервера
		{

			//if ((fd = socket(AF_INET, SOCK_STREAM, 0))<0) // Создать сокет типа SOCK_STREAM
			//	perror("socket creating"), exit(1);
			//if (connect(fd, (sockaddr *)&servaddr, SOCKADDR_IN_SIZE)<0)
			//				perror("connect"), exit(1);
			//if(nread==1)
			//	continue;
			buf[nread] = '\0';
			cout << "Before : " << buf << endl;
            std::string s(buf);
			cout << "String : " << s << endl;
            std::istringstream ss(s);
            std::string CMD;
            ss >> CMD;
			if( CMD == "PRT")
			{
				ss >> port_num;
				cout << port_num<< endl;
				break;
			}
			else if( CMD == "ERN")
			{
				cerr << "ERRor : Name already exists" << endl;
			}
			else cerr << "Unknown error" << endl;


			//if (write(1, buf, nread)<0) // Посылка строки серверу
			//	perror("write"), exit(1);
			//cout << buf;
			//close(fd);
		}
	close(fd); // Закрыть сокет
	if ((nfd = socket(AF_INET, SOCK_STREAM, 0))<0) // Создать сокет типа SOCK_STREAM
		perror("socket creating"), exit(1);
	// Сформировать структуру с адресом сервера
		memset(&servaddr, 0, SOCKADDR_IN_SIZE); // Обнулить структуру для адреса
	servaddr.sin_family = AF_INET;
	// Преобразовать адрес сервера в двоичное число в сетевом порядке
		if(inet_pton(AF_INET, argv[1], &servaddr.sin_addr) <= 0)
			perror("bad address"), exit(1);// Преобразовать номер порта сервера в сетевой порядок
	servaddr.sin_port = htons(port_num);
	// Установить соединение с сервером, указанным в servaddr
	sleep(1);
		if (connect(nfd, (sockaddr *)&servaddr, SOCKADDR_IN_SIZE)<0)
			perror("connect2"), exit(1);
	int flags = fcntl(0, F_GETFL, 0);
	fcntl(0, F_SETFL, flags | O_NONBLOCK); // for non-blocking read
	flags = fcntl(nfd, F_GETFL, 0);
	fcntl(nfd, F_SETFL, flags | O_NONBLOCK); // for non-blocking read
	for(;;)
	{
		if((nread = read(nfd, buf, BUFSIZE)) > 0)
		{
			//cerr << nread << endl;
			printf(buf);
			//write(1,buf,nread);
		}
		if((nread = read(0, buf, BUFSIZE))== 0) // Чтение c stdin
			break;
		//printf("Hahaha42\n");
		for (int i = 3; i >= 0; i--)
			tbuf[i]=buf[i];
		if(strcmp(tbuf,"exit") == 0)
			break;
		//cerr << "Hahahahaha" <<endl;
		if(nread ==1)
			continue;
		if(nread > 0)
		{
			//cerr << strcmp(tbuf,eexit) << endl;
			//write(1,buf,5);
			//strcat(buf,username);
			if(write(nfd,buf,nread) < 0)
				perror("write") , exit(1);
		//	cout <<"Afterrr writte" << endl;
		}
	}
	buf[0]='\0';
	strcat(buf,"[tech] Пользователь ");
	strcat(buf,username);
	strcat(buf," покинул чат.");
	write(nfd,buf,66+2*usr_size);
	sleep(1);
	exit(0);
}
