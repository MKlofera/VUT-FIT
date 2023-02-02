/**
 * IPK 1. project
 * @author Marek Klofera, xklofe01@stud.fit.vutbr.cz
 */

#include "main.h"

const int BUFFER_SIZE = 1024;
const int SMALL_BUFFER_SIZE = 256;

int main(int argc, char *argv[]) {

    // get port number from argv
    int port = atoi(argv[1]);

    //HTTP header
    string httpHeader = "HTTP/1.1 200 OK\r\nContent-Type: text/plain;\r\n\r\n";

    //set network
    struct sockaddr_in server_address;
    server_address.sin_family = AF_INET;
    server_address.sin_port = htons(port);
    server_address.sin_addr.s_addr = INADDR_ANY;

    // creating socket
    int welcome_socket = CreateSocket();
    int server_socket = CreateSocket();

    // binding socket
    if(bind(welcome_socket, (struct sockaddr *) &server_address, sizeof (server_address)) < 0){
        perror("ERROR: bind");
        exit (EXIT_FAILURE);
    }

    // listen
    if ((listen(welcome_socket, 1)) < 0){
        perror("ERROR: listen");
        exit (EXIT_FAILURE);
    }

    while(1){
        int server_address_len = sizeof (server_address);
        // creates communication socket
        int comm_socket = accept(welcome_socket, (struct sockaddr *) &server_address,
                                 reinterpret_cast<socklen_t *>(&server_address_len));
        if (comm_socket <= 0){
            perror("ERROR: accept");
            exit (EXIT_FAILURE);
        }

        char buffer[BUFFER_SIZE];
        int request = read(comm_socket , buffer, 1024);

        string httpRequest = buffer;

        if (strncmp(buffer, "GET /load ", 10) == 0){
            string cpuUsage = to_string(GetCPU_usage());
            string messageHTTP = httpHeader + cpuUsage + "%";
            send(comm_socket , messageHTTP.c_str(), messageHTTP.length() , 0 );

        }else if((httpRequest.compare(0, 14, "GET /hostname ")) == 0){

            string hostname = GetHostname();
            string messageHTTP = httpHeader + hostname;
            send(comm_socket , messageHTTP.c_str(), messageHTTP.length() , 0 );

        } else if ((httpRequest.compare(0, 14, "GET /cpu-name ")) == 0){

            string cpuName = GetCPU_name();
            string messageHTTP = httpHeader + cpuName;
            send(comm_socket , messageHTTP.c_str(), messageHTTP.length() , 0 );

        } else{

            string messageHTTP = httpHeader + "400 Bad Request";
            send(comm_socket , messageHTTP.c_str(), messageHTTP.length() , 0 );
        }

        close(comm_socket);
    }

    return 0;
}

int CreateSocket(){
    int new_socket = socket(AF_INET, SOCK_STREAM, 0);
    if (new_socket <= 0){
        perror("ERROR: creating socket");
        exit (EXIT_FAILURE);
    }
    return new_socket;
}

string ShellExec(const char *shellCommand){
    string shellResult = "";
    FILE *file = popen(shellCommand, "r");
    if (file == NULL){
        perror("ERROR: executing shell command in function: ShellExec()\n");
        exit (EXIT_FAILURE);
    }
    char buffer[SMALL_BUFFER_SIZE];
    while (fgets(buffer, SMALL_BUFFER_SIZE , file) != NULL){
        shellResult += buffer;
    }
    pclose(file);

    // delete's end of line from shellResult
    if (!shellResult.empty() && shellResult[shellResult.length()-1] == '\n') {
        shellResult.erase(shellResult.length()-1);
    }
    return shellResult;
}

string GetCPU_name(){
    return ShellExec("cat /proc/cpuinfo | grep 'name' | uniq | grep -oP '(?<=: ).*'");
}
string GetHostname(){
    return ShellExec("cat /proc/sys/kernel/hostname");
}
void GetCPU_stats(long long int *cpuStats){
    string shellResult = ShellExec("cat /proc/stat | grep -w cpu");


    string delimiter = " ";
    size_t pos = 0;
    string token;

    //parsing shellResult to get cpu data into array for calculating cpu usage
    int counter = 0;
    while ((pos = shellResult.find(delimiter)) != string::npos) {
        token = shellResult.substr(0, pos);
        switch (counter) {
            case 2:
                cpuStats[0] = stoll(token, nullptr, 10);
                break;
            case 3:
                cpuStats[1] = stoll(token, nullptr, 10);
                break;
            case 4:
                cpuStats[2] = stoll(token, nullptr, 10);
                break;
            case 5:
                cpuStats[3] = stoll(token, nullptr, 10);
                break;
            case 6:
                cpuStats[4] = stoll(token, nullptr, 10);
                break;
            case 7:
                cpuStats[5] = stoll(token, nullptr, 10);
                break;
            case 8:
                cpuStats[6] = stoll(token, nullptr, 10);
                break;
            case 9:
                cpuStats[7] = stoll(token, nullptr, 10);
                break;
            case 10:
                cpuStats[8] = stoll(token, nullptr, 10);
                break;
        }
        counter += 1;
        shellResult.erase(0, pos + delimiter.length());
    }
}
int GetCPU_usage(){

//          user    nice   system  idle      iowait irq   softirq  steal  guest  guest_nice
//    cpu  74608   2520   24433   1117073   6176   4054  0        0      0      0

    long long int prevCPUStats [10] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    GetCPU_stats((prevCPUStats));

    sleep(1);

    long long int cpuStats [10] = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    GetCPU_stats((cpuStats));

    //previdle + previowait
    long long int PrevIdle = prevCPUStats[3] + prevCPUStats[4];
    //idle + iowait
    long long int Idle = cpuStats[3] + cpuStats[4];

    //prevuser + prevnice + prevsystem + previrq + prevsoftirq + prevsteal
    long long int PrevNonIdle = prevCPUStats[0] + prevCPUStats[1] + prevCPUStats[2] + prevCPUStats[5] + prevCPUStats[6] + prevCPUStats[7];
    //user + nice + system + irq + softirq + steal
    long long int NonIdle = cpuStats[0] + cpuStats[1] + cpuStats[2] + cpuStats[5] + cpuStats[6] + cpuStats[7];

    long long int PrevTotal = PrevIdle + PrevNonIdle;
    long long int Total = Idle + NonIdle;

    // differentiate: actual value minus the previous one
    int totald = Total - PrevTotal;
    int idled = Idle - PrevIdle;

    if (totald == 0){
        totald == 1;
    }
    int CPU_Percentage = ((totald - idled) * 100) / (totald * 1.0);
    return CPU_Percentage;
}