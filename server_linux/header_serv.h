// server header file for LAB1 SETI I COMMUNIC
// author Rodin Andrey <andreyrodin405@yandex.ru>
#define TESTING // uncomment this for additional info output

#include <string>
//#include <iostream>

using namespace std;
enum Message_type { MSG,// - message                                  ( serv <> client)
                    PVT,// - private message                          ( serv <> client)
                    NEW,// - new member                               ( serv <> client)
                    DCT,// - member disconnect                        ( serv <> client)
                    LST,// - list of users already in chat            ( serv -> client)
                    ERN,// - error name ( name already in use)        ( serv -> client)
                    PRT // - port number for client to reconnect to   ( serv -> client)
                    };

struct client_node {
    string client_name; // obvious what that is
    int port; // port on which this user is served
    int rw_descriptor; // descriptor for write/read function to send/receive to/from client.
};

typedef struct client_node CLIENT_NODE;



struct dashboard {
    Message_type TYP;
    bool trig_sender;
    string client_name;
    string message;
}dashboard;
