
#include <sys/socket.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <netinet/in.h>
#include <arpa/inet.h>

int main(int argc, char *argv[])
{
    const int BUFSIZE=100, SIZE=sizeof(sockaddr_in);
    int fd, nread;
    char buf[BUFSIZE];
    sockaddr_in servaddr;
    if (argc < 3)
    printf("Too few arguments \n"), exit(1);
    if ((fd = socket(AF_INET, SOCK_STREAM, 0))<0) // Создать сокет типа SOCK_STREAM
    perror("socket creating"), exit(1);
    // Сформировать структуру с адресом сервера
    memset(&servaddr, 0, SIZE); // Обнулить структуру для адреса
    servaddr.sin_family = AF_INET;
    // Преобразовать адрес сервера в двоичное число в сетевом порядке
    if(inet_pton(AF_INET, argv[1], &servaddr.sin_addr) <= 0)
    perror("bad address"), exit(1);// Преобразовать номер порта сервера в сетевой порядок
    servaddr.sin_port = htons(atoi(argv[2]));
    // Установить соединение с сервером, указанным в servaddr
    if (connect(fd, (sockaddr *)&servaddr, SIZE)<0)
    perror("connect"), exit(1);
    write(1, "Input message to send\n", 22);
    // Вывод приглашения на экран
    while ((nread = read(0, buf, BUFSIZE)) > 0) // Чтение строки из stdin
    if (write(fd, buf, nread)<0) // Посылка строки серверу
    perror("write"), exit(1);
    close(fd); // Закрыть сокет
    exit(0);
}
