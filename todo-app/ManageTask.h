#ifndef TODO_APP_MANAGETASK_H
#define TODO_APP_MANAGETASK_H

#include "iostream"
#include "fstream"
#include "task.h"
using namespace std;

bool validateInput(const char* input);

void addTask(Task tasks[], int& taskCount);

void deleteTask(Task tasks[], int& taskCount);

void editTask(Task tasks[], int taskCount);

void searchTasks(const Task tasks[], int taskCount);


void displayTasks(const Task tasks[], int taskCount);

#endif
