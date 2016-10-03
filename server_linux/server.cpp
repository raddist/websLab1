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
#include <termios.h>

#include <iostream>
#include <string>
#include <sstream>
#include <list>     // подключаем заголовок списка
#include <iterator> // заголовок итераторов
#include <fcntl.h> // non blocking read
#include <thread>
#include <mutex>
#include <ctime>

int client_routine(int port); // будем обслуживать конкретного клиента
int writing_head(int); // функция потока-писаря ( пишет всем кто в чате разную инфу)
void show_list(); // showing list of clients
bool is_bad_name(string name); // check presence of name in list
void write_do_desk(string s, int descriptor);// пишем в открытый порт
void show_time();

using std::list;
using namespace std;

list< CLIENT_NODE > CLIENT_LIST;

const int SERV_PORT=13990; // Номер порта сервера по умолчанию
const int SIZE_SOCKADDR=sizeof(struct sockaddr_in);
int main(int argc, char ** argv)
{
    dashboard.trig_sender = false;


    thread *wr_head = new thread(writing_head,0);
    thread *thread_mass[500]; // массив дескрипторов дочерних потоков
    int thread_mass_count = 0; // счетчик дескрипторов дочерних потоков
    show_time();
    cout << "Starting chat server ..." << endl;
    int port_counter = 14000;
    int port_num;
    if (argc > 1)
        port_num = atoi(argv[1]);
    else
        port_num = SERV_PORT;

    const int BUFSIZE=1004096;
    int lfd; // дескриптор сокета
    int cfd; // дескриптор присоединенного сокета
    int nread; // кол-во прочтённых байт
    int clilen; //  число байт адреса клиента
    char buf[BUFSIZE]; // буфер для чтения
    sockaddr_in servaddr; // Для адреса сервера
    sockaddr_in cliaddr; // Для адреса клиента
    if ((lfd = socket(AF_INET, SOCK_STREAM, 0))<0) // Создать сокет типа SOCK_STREAM
    {
        show_time();
        perror("Socket error: "), exit(1);
    }


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
            show_time();
            cerr << "Cannot bind server to port " << port_num << " : ";
            perror(NULL);
            exit(1);
        }
        else
        {
            show_time();
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
                    show_time();
                    cout << "Server listening on port " << port_num + counter << endl;
                    break;
                }

        }
        if (counter >= 10)
        {
            show_time();
            cerr << "Cannot bind server to ports " << SERV_PORT << "-"<< SERV_PORT +9<< " by default : ";
            perror(NULL);
            exit(1);
        }
    }

    // Сделать сокет lfd прослушиваемым
    if (listen(lfd, 5) < 0)
    {
        show_time();
        perror("listen"), exit(1);
    }


    for(;;)// ГЛАВНЫЙ ЦИКЛ СЕРВЕРА
    {
        clilen =SIZE_SOCKADDR;
        // Ожидаем соединения, в cfd получим дескриптор присоединенного сокета,
        // в cliaddr - адрес клиента, в clilen - число байт адреса
        if ((cfd = accept(lfd, (sockaddr *)&cliaddr, (socklen_t*)&clilen)) < 0)
        {
            show_time();
            perror("Accept error: ");
            continue;
        }
        show_time();
        cout <<"Coming new connection ..." << endl;

        struct termios termios;

        tcgetattr(cfd, &termios);
        termios.c_lflag &= ~ICANON; /* Set non-canonical mode */
        termios.c_cc[VTIME] = 10; /* Set timeout of 1.0 seconds */
        tcsetattr(cfd, TCSANOW, &termios);

        while ((nread = read(cfd, buf, BUFSIZE))> 0)
        {
            buf[nread] = '\0';
            std::string s(buf);
            std::istringstream ss(s);
            std::string CMD;
            ss >> CMD;
            if (CMD == "NEW")
            {
                show_time();
                cout << "New connection is initializing ..." << endl;
                // check here name unickness

                string nme;
                ss >> nme;
                if( is_bad_name(nme))
                {
                    show_time();
                    cerr << "Client entered existing name."<< endl;
                    string tmpstr = "ERN ";
                    const char *sddd = tmpstr.c_str();
                    write(cfd,sddd ,15);
                    break;
                }
                CLIENT_NODE *new_node = new CLIENT_NODE;
                new_node->client_name = nme;
                new_node->port = port_counter;
                port_counter++;
                CLIENT_LIST.push_back(*new_node);

                thread_mass[thread_mass_count++] = new thread(client_routine,new_node->port);

                string tmpstr = "PRT ";
                tmpstr += std::to_string(new_node->port);
                const char *sddd = tmpstr.c_str();


                write(cfd,sddd ,15);
                show_list();

            }
            else
            {
                show_time();
                cout << "Someone different (not client) wants to connect to server"<< endl;
                //cout << CMD << endl;
                break;
            }

            //cout << CMD << endl;
            //co0ut << s;

        }
#ifdef TESTING
        show_time();
        cout<< "Did one loop" << endl;
#endif
        if (nread == -1)
        {
            show_time();
            perror("Error reading socket: ");
        }
        close(cfd); // закрыть присоединенный сокет
    }
}


int client_routine(int port)// будем обслуживать конкретного клиента
{
    const int BUFSIZE=4096;
    int lfd; // дескриптор сокета
    int cfd; // дескриптор присоединенного сокета
    int nread; // кол-во прочтённых байт
    int clilen; //  число байт адреса клиента
    char buf[BUFSIZE]; // буфер для чтения
    sockaddr_in servaddr; // Для адреса сервера
    sockaddr_in cliaddr; // Для адреса клиента
    if ((lfd = socket(AF_INET, SOCK_STREAM, 0))<0) // Создать сокет типа SOCK_STREAM
    {
        show_time();
        perror("Socket error in thread : ");
        return -1;
    }

    memset(&servaddr, 0, SIZE_SOCKADDR); // Обнулить структуру для адреса сервера
    servaddr.sin_family = AF_INET;
    servaddr.sin_addr.s_addr = htonl(INADDR_ANY); // Адрес сервера - любой
    //IP адрес этого компьютера, преобразовать его в сетевой порядок
    servaddr.sin_port = htons(port); // Порт сервера преобразуется в сетевой вид

    if (bind(lfd, (sockaddr *) &servaddr, SIZE_SOCKADDR)<0)
    {
        show_time();
        cerr << "Cannot bind thread to port " << port << " : ";
        perror(NULL);
        return -1;
    }

    if (listen(lfd, 5) < 0)
    {
        perror("listen");
        return -1;
    }

    show_time();
    cout << "Im daughter thread on port " << port << endl;

    clilen =SIZE_SOCKADDR;
    // Ожидаем соединения, в cfd получим дескриптор присоединенного сокета,
    // в cliaddr - адрес клиента, в clilen - число байт адреса
    if ((cfd = accept(lfd, (sockaddr *)&cliaddr, (socklen_t*)&clilen)) < 0)
    {
        show_time();
        perror("Accept error: ");
        return -1;
    }

    list< CLIENT_NODE >::iterator iter = CLIENT_LIST.begin();

    string tmps = "";
    int mems = 0;
    for ( ; iter != CLIENT_LIST.end() ; iter++) //
    {
        mems++;
        tmps+= iter->client_name + " ";
    }

    for (iter = CLIENT_LIST.begin() ; iter != CLIENT_LIST.end() ; iter++) // находим себя в списке
    {
        if(iter->port == port)
            break;
    }
    iter->rw_descriptor = cfd; // кладем в список дескриптор для обращения

    dashboard.TYP = NEW;
    dashboard.client_name = iter->client_name;
    dashboard.message = "";
    dashboard.trig_sender = true; // всем отсылаем нового пользователя

    usleep(500000);
    //for(int i = 0; i < 1000000; i++);// delay
    string v = to_string(mems) +" "+ tmps;
    dashboard.TYP = LST;
    dashboard.client_name = iter->client_name;
    dashboard.message = v;
    dashboard.trig_sender = true;

    show_time();
    cout <<"Connection with " << iter->client_name<< " established." << endl;

//////// главный цикл потока
    while ((nread = read(cfd, buf, BUFSIZE))> 0)
    {
        buf[nread] = '\0';
        std::string s(buf);
        std::istringstream ss(s);
        std::string CMD;
        ss >> CMD;
        if (CMD == "MSG")
        {
            dashboard.TYP = MSG;
            dashboard.client_name = iter->client_name;
            string strr = ss.str();
            strr.erase(0, 4);
            dashboard.message = strr;
            dashboard.trig_sender = true; // всем отсылаем сообщение
        }
        else if (CMD == "DCT")
        {
            show_time();
            cout << "Client " << iter->client_name << " disconnected." << endl;
            dashboard.TYP = DCT;
            dashboard.client_name = iter->client_name;
            string strr = ss.str();
            strr.erase(0, 3);
            dashboard.message = strr;

            CLIENT_LIST.erase(iter);
            //delete iter;
            show_list();

            dashboard.trig_sender = true; // всем отсылаем отключившегося пользователя
            close(cfd);
            return 0;
        }
        else if (CMD == "PVT")
        {

            dashboard.TYP = PVT;
            ss >> dashboard.client_name; // имя получателя
            show_time();
            cout << " User " << iter->client_name << " sent private message to " << dashboard.client_name << endl;
            string strr = ss.str();
            strr.erase(0, dashboard.client_name.length() + 4);
            strr = iter->client_name  + strr;
            dashboard.message = strr;
            dashboard.trig_sender = true; //отсылаем приватное сообщение
        }
        else
        {
            show_time();
            cout << "Client " << iter->client_name << " sent unproper format message"<< endl;
        }
        //cout << CMD << endl;
    }
    show_time();
    cerr << "Tcp connection with user " << iter->client_name << " failed. Closing port." << endl;
    dashboard.TYP = DCT;
    dashboard.client_name = iter->client_name;
    CLIENT_LIST.erase(iter);
    dashboard.trig_sender = true;
    close(cfd);
    //delete iter;
    return 0;
}

int writing_head(int param)
{
    string s;
    list< CLIENT_NODE >::iterator iter;
    while(true)
    {// cannot break this loop
        if(dashboard.trig_sender)
        {
            dashboard.trig_sender = false;

            switch(dashboard.TYP)
            {
                case NEW:
                    s = "NEW ";
                    s+= dashboard.client_name;
                    for(iter = CLIENT_LIST.begin(); iter != CLIENT_LIST.end(); iter++)
                    {
                        if(iter->client_name == dashboard.client_name)
                            continue;
                        else
                        {
                            write_do_desk(s, iter->rw_descriptor);
                        }
                    }
                    break;
                case MSG:
                    s = "MSG ";
                    s+= dashboard.client_name + " " + dashboard.message;
                    for(iter = CLIENT_LIST.begin(); iter != CLIENT_LIST.end(); iter++)
                    {
                            write_do_desk(s, iter->rw_descriptor);
                    }
                    break;
                case DCT:
                    s = "DCT ";
                    s+= dashboard.client_name;
                    for(iter = CLIENT_LIST.begin(); iter != CLIENT_LIST.end(); iter++)
                    {
                        if(iter->client_name == dashboard.client_name)
                            continue;
                        else
                        {
                            write_do_desk(s, iter->rw_descriptor);
                        }
                    }
                    break;
                case PVT:
                    s = "PVT " +dashboard.message;
                    //s+= dashboard.client_name + " " + dashboard.message;
                    for(iter = CLIENT_LIST.begin(); iter != CLIENT_LIST.end(); iter++)
                    {
                        if(iter->client_name == dashboard.client_name)
                        {
                            write_do_desk(s, iter->rw_descriptor);
                            break;
                        }
                        else
                            continue;
                    }
                    break;
#ifdef LIST_OUTPUT // in "header_serv.h"
                case LST:
                    s = "LST " + dashboard.message;
                    for(iter = CLIENT_LIST.begin(); iter != CLIENT_LIST.end(); iter++)
                    {
                        if(iter->client_name == dashboard.client_name)
                        {
                            write_do_desk(s, iter->rw_descriptor);
                            break;
                        }
                        else
                            continue;
                    }
                    break;
#endif
                }

            }

        }
    return 0;
}

void show_time()
{
    char buffer[80];
    time_t seconds = time(NULL);
    tm* timeinfo = localtime(&seconds);
    char format[] = "[ %Y %H:%M:%S ] ";
    strftime(buffer, 80, format, timeinfo);
    cerr << buffer;
    ///////////////////////////////////////////////////////////////////////////////////////////////////
}

void show_list()// showing list of clients
{
    show_time();
    cout << "List of users:" << endl;

    list< CLIENT_NODE >::iterator iter = CLIENT_LIST.begin();
    for ( ; iter != CLIENT_LIST.end() ; iter++)
    {
        cout << iter->client_name << endl;
    }

    std::cout << std::endl;
}

void write_do_desk(string s, int descriptor)// пишем в открытый порт
{
    const char *sddd = s.c_str();
    write(descriptor,sddd, s.length()+1);

}

bool is_bad_name(string name) // check presence of name in list
{
    list< CLIENT_NODE >::iterator iter;
    for ( iter = CLIENT_LIST.begin(); iter != CLIENT_LIST.end() ; iter++)
    {
        //cout <<"Iterr ";
        if(iter->client_name == name)
            return true;

    }
    return false;
    //cout << endl;

}
