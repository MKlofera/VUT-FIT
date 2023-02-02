/**
 * IPK 1. project
 * @author Marek Klofera, xklofe01@stud.fit.vutbr.cz
 */

#ifndef CPP_MAIN_H
#define CPP_MAIN_H



#include <iostream>
#include <stdio.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <netdb.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <string>
using namespace std;

int SetPort(char *argv[]);

/** Creates new socket
 * @return new socket
 * @author Marek Klofera, xklofe01@stud.fit.vutbr.cz
 */
int CreateSocket();
/**
 * execute shell command passed as argument
 * @param shellCommand
 * @return terminal response to entered command in string
 * @author Marek Klofera, xklofe01@stud.fit.vutbr.cz
 */
string ShellExec(const char *shellCommand);

/**
 * using terminal find hostname
 * @return hostname in string
 * @author Marek Klofera, xklofe01@stud.fit.vutbr.cz
 */
string GetHostname();

/**
 * using terminal command find's cpu name
 * @return cpu name in string
 * @author Marek Klofera, xklofe01@stud.fit.vutbr.cz
 */
string GetCPU_name();

/**
 * Save's information about cpu to referenced array
 * @param cpuStats reference to array of long long int to save cpu information
 * @author Marek Klofera, xklofe01@stud.fit.vutbr.cz
 */
void GetCPU_stats(long long int *cpuStats);

/**
 * calculate CPU usage
 * @return rounded cpu usage in int
 * @author Marek Klofera, xklofe01@stud.fit.vutbr.cz
 */
int GetCPU_usage();

#endif //CPP_MAIN_H
