#include "SortTask.h"

void sortTasksByPriority(Task tasks[], int taskCount) {
    for (int i = 0; i < taskCount - 1; ++i) {
        for (int j = i + 1; j < taskCount; ++j) {
            if (tasks[i].priority > tasks[j].priority) {
                Task temp = tasks[i];
                tasks[i] = tasks[j];
                tasks[j] = temp;
            }
        }
    }
}

void sortTasksByDatetime(Task tasks[], int taskCount) {
    for (int i = 0; i < taskCount - 1; ++i) {
        for (int j = i + 1; j < taskCount; ++j) {
            if (strcmp(tasks[i].datetime, tasks[j].datetime) > 0) {
                Task temp = tasks[i];
                tasks[i] = tasks[j];
                tasks[j] = temp;
            }
        }
    }
}
