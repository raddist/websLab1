// server header file for LAB1 SETI I COMMUNIC
// author Rodin Andrey <andreyrodin405@yandex.ru>
#define TESTING // uncomment this for additional info output

#include <string>
//#include <iostream>

using namespace std;
enum Message_type { MSG, PVT, NEW, DCT, THR, ERN};

struct client_node {
    string client_name;
    int port;
};

typedef struct client_node CLIENT_NODE;



struct dashboard {
    Message_type TYP;
    bool trig_sender;
    string client_name;
    string message;
}dashboard;
