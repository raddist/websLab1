
#include "header_serv.h"

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

void client_routine(int port); // будем обслуживать конкретного клиента


using namespace std;

const int SERV_PORT=13990; // Номер порта сервера по умолчанию
const int SIZE_SOCKADDR=sizeof(struct sockaddr_in);
int main(int argc, char ** argv)
{
    cout << "Starting chat server ..." << endl;
    int port_num;
    if (argc > 1)
        port_num = atoi(argv[1]);
    else
        port_num = SERV_PORT;

    const int BUFSIZE=4096;
    int lfd; // дескриптор сокета
    int cfd; // дескриптор присоединенного сокета
    int nread; // кол-во прочтённых байт
    int clilen; //  число байт адреса клиента
    char buf[BUFSIZE]; // буфер для чтения
    sockaddr_in servaddr; // Для адреса сервера
    sockaddr_in cliaddr; // Для адреса клиента
    if ((lfd = socket(AF_INET, SOCK_STREAM, 0))<0) // Создать сокет типа SOCK_STREAM
        perror("Socket error: "), exit(1);

    memset(&servaddr, 0, SIZE_SOCKADDR); // Обнулить структуру для адреса сервера
    servaddr.sin_family = AF_INET;
    servaddr.sin_addr.s_addr = htonl(INADDR_ANY); // Адрес сервера - любой
    //IP адрес этого компьютера, преобразовать его в сетевой порядок
    servaddr.sin_port = htons(port_num); // Порт сервера преобразуется в сетевой вид

    if(argc > 1) // Если указан порт - пытаемся привязаться к нему, если не указан -
                    // пытаемся привязаться к портам 13990-13999 ( до первого привязавшегося)
    {
        // Привязать сокет к адресу и порту сервера
        if (bind(lfd, (sockaddr *) &servaddr, SIZE_SOCKADDR)<0)
        {
            cerr << "Cannot bind server to port " << port_num << " : ";
            perror(NULL);
            exit(1);
        }
        else
        {
            cout << "Server listening on port " << port_num << endl;
        }
    }
    else
    {
        int counter = 0;
        while (counter < 10)
        {
                servaddr.sin_port = htons(port_num + counter); // Порт сервера преобразуется в сетевой вид
                if (bind(lfd, (sockaddr *) &servaddr, SIZE_SOCKADDR)<0)
                {
                    counter++;
                    continue;
                }
                else
                {
                    cout << "Server listening on port " << port_num + counter << endl;
                    break;
                }

        }
        if (counter >= 10)
        {
            cerr << "Cannot bind server to ports " << SERV_PORT << "-"<< SERV_PORT +9<< " by default : ";
            perror(NULL);
            exit(1);
        }
    }

    // Сделать сокет lfd прослушиваемым
    if (listen(lfd, 5) < 0)
    perror("listen"), exit(1);



    for(;;)// ГЛАВНЫЙ ЦИКЛ СЕРВЕРА
    {
        clilen =SIZE_SOCKADDR;
        // Ожидаем соединения, в cfd получим дескриптор присоединенного сокета,
        // в cliaddr - адрес клиента, в clilen - число байт адреса
        if ((cfd = accept(lfd, (sockaddr *)&cliaddr, (socklen_t*)&clilen)) < 0)
        perror("Accept error: "), exit(1);
        cout <<"Coming new connection ..." << endl;

        // Прочитать сообщение от клиента и вывести его в stdout
        while ((nread = read(cfd, buf, BUFSIZE))> 0)
        {
            buf[nread] = '\0';
            std::string s(buf);
            std::istringstream ss(s);
            std::string CMD;
            ss >> CMD;
            if (CMD == "NEW")
            {
                cout << "New connection is initializing ..." << endl;
                CLIENT_NODE *new_node = new CLIENT_NODE;
                ss >> new_node.client_name;
                // check here name unicness
            }
            else
            {
                cout << "Someone different (not client) wants to connect to server"<< endl;
                break;
            }

            cout << CMD << endl;
            //co0ut << s;

        }
#ifdef TESTING
        cout<< "Did one loop" << endl;
#endif
        if (nread == -1)
        perror("Error reading socket: ");
        close(cfd); // закрыть присоединенный сокет
    }
}
