
#include <sys/socket.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <netinet/in.h>
#include <arpa/inet.h>

const int SERV_PORT=18990; // Номер порта сервера по умолчанию
const int SIZE_SOCKADDR=sizeof(struct sockaddr_in);
int main()
{
    const int BUFSIZE=400;
    int lfd;
    int cfd;
    int nread; // кол-во прочтённых байт
    int clilen;
    char buf[BUFSIZE]; // буфер для чтения
    sockaddr_in servaddr; // Для адреса сервера
    sockaddr_in cliaddr; // Для адреса клиента
    if ((lfd = socket(AF_INET, SOCK_STREAM, 0))<0) // Создать сокет типа SOCK_STREAM
        perror("socket"), exit(1);
        
    memset(&servaddr, 0, SIZE_SOCKADDR); // Обнулить структуру для адреса сервера
    servaddr.sin_family = AF_INET;
    servaddr.sin_addr.s_addr = htonl(INADDR_ANY); // Адрес сервера - любой
    //IP адрес этого компьютера, преобразовать его в сетевой порядок
    servaddr.sin_port = htons(SERV_PORT); // Порт сервера преобразуется в сетевой вид
    // Привязать сокет к адресу и порту сервера
    if (bind(lfd, (sockaddr *) &servaddr, SIZE_SOCKADDR)<0)
    perror("bind"), exit(1);
    // Сделать сокет lfd прослушиваемым
    if (listen(lfd, 5) < 0)
    perror("listen"), exit(1);
    for(;;)// ГЛАВНЫЙ ЦИКЛ СЕРВЕРА
    {
        clilen =SIZE_SOCKADDR;
        // Ожидаем соединения, в cfd получим дескриптор присоединенного сокета,
        // в cliaddr - адрес клиента, в clilen - число байт адреса
        if ((cfd = accept(lfd, (sockaddr *)&cliaddr, (socklen_t*)&clilen)) < 0)
        perror("accept"), exit(1);
        printf("connection established\n");
        // Прочитать сообщение от клиента и вывести его в stdout
        while ((nread = read(cfd, buf, BUFSIZE))> 0)
        write(1, &buf, nread);
        if (nread == -1)
        perror("read"), exit(1);
        close(cfd); // закрыть присоединенный сокет
    }
}
