#include <iostream>
#include <fstream>
#include "task.h"
#include "ManageTask.h"
#include "SortTask.h"

using namespace std;

int main() {
    Task tasks[MAX_TASKS];
    int taskCount = 0;
    int choice{};

    // Load tasks from file
    ifstream inputFile("tasks.txt");
    if (inputFile.is_open()) {
        char line[400];
        while (inputFile.getline(line, sizeof(line))) {
            char* token = strtok(line, ",");
            if (token != nullptr) {
                strcpy(tasks[taskCount].title, token);
                token = strtok(nullptr, ",");
                if (token != nullptr) {
                    tasks[taskCount].priority = atoi(token); // convert string to int
                    token = strtok(nullptr, ",");
                    if (token != nullptr) {
                        strcpy(tasks[taskCount].description, token);
                        token = strtok(nullptr, ",");
                        if (token != nullptr) {
                            strcpy(tasks[taskCount].datetime, token);
                            ++taskCount;
                        }
                    }
                }
            }
        }
        inputFile.close();
    }
    else {
        cout << "Error opening file for reading.\n";
    }

    do {
        cout << "Task Manager Menu:\n";
        cout << "1. Add Task\n";
        cout << "2. Delete Task\n";
        cout << "3. Edit Task\n";
        cout << "4. Search Tasks\n";
        cout << "5. Display Tasks\n";
        cout << "6. Sort Tasks by Priority\n";
        cout << "7. Sort Tasks by Date and Time\n";
        cout << "8. Exit\n";
        cout << "Enter your choice (1-8): ";

        // Validate the input
        while (!(cin >> choice)) {
            cout << "Invalid input. Please enter a number.\n";
            cin.clear();
            cin.ignore(numeric_limits<streamsize>::max(), '\n');
        }
        cin.ignore();

        switch (choice) {
            case 1:
                addTask(tasks, taskCount);
                break;
            case 2:
                deleteTask(tasks, taskCount);
                break;
            case 3:
                editTask(tasks, taskCount);
                break;
            case 4:
                searchTasks(tasks, taskCount);
                break;
            case 5:
                displayTasks(tasks, taskCount);
                break;
            case 6:
                sortTasksByPriority(tasks, taskCount);
                displayTasks(tasks, taskCount);
                break;
            case 7:
                sortTasksByDatetime(tasks, taskCount);
                displayTasks(tasks, taskCount);
                break;
            case 8:
                cout << "Exiting Task Manager.\n";
                break;
            default:
                cout << "Invalid choice. Please try again.\n";
                break;
        }

        cout << "\n";
    } while (choice != 8);

    return 0;
}
