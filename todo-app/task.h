#ifndef TODO_APP_TASK_H
#define TODO_APP_TASK_H

const int MAX_TASKS = 100;

struct Task {
    char title[100];
    int priority;
    char description[100];
    char datetime[100];
};
#endif
