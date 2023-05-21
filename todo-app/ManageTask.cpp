#include "ManageTask.h"


bool validateInput(const char* input) {
    // Check if the input contains a comma
    if (strchr(input, ',') != nullptr) {
        cout << "Invalid input. Comma (',') is not allowed.\n";
        return false;
    }
    return true;
}

void addTask(Task tasks[], int& taskCount) {
    if (taskCount >= MAX_TASKS) {
        cout << "Task list is full. Cannot add more tasks.\n";
        return;
    }

    Task newTask{};

    cout << "Enter task title: ";
    cin.getline(newTask.title, sizeof(newTask.title));
    if (!validateInput(newTask.title)) {
        return;
    }
    cin.ignore(numeric_limits<streamsize>::max(), '\n');

    while (true) {
        cout << "Enter task priority: ";
        cin >> newTask.priority;
        cin.ignore(numeric_limits<streamsize>::max(), '\n');
        if (cin.fail()) {
            cout << "Invalid input. Please enter a number.\n";
            cin.clear();
            cin.ignore(numeric_limits<streamsize>::max(), '\n');
            return;
        }
        else break;
    }

    cout << "Enter task description: ";
    cin.getline(newTask.description, sizeof(newTask.description));
    if (!validateInput(newTask.description)) {
        return;
    }
    cin.ignore(numeric_limits<streamsize>::max(), '\n');

    cout << "Enter task date and time: ";
    cin.getline(newTask.datetime, sizeof(newTask.datetime));
    if (!validateInput(newTask.datetime)) {
        return;
    }
    cin.ignore(numeric_limits<streamsize>::max(), '\n');


    tasks[taskCount++] = newTask;

    // Save the tasks to the file
    ofstream outputFile("tasks.txt", ios::app);
    if (outputFile.is_open()) {
        outputFile << newTask.title << "," << newTask.priority << ","
                   << newTask.description << "," << newTask.datetime << "\n";
        outputFile.close();
    }
    else {
        cout << "Error opening file for writing.\n";
    }
}

void deleteTask(Task tasks[], int& taskCount) {
    if (taskCount == 0) {
        cout << "No tasks to delete.\n";
        return;
    }

    int index;
    cout << "Enter the index of the task to delete (0-" << taskCount - 1 << "): ";
    cin >> index;
    cin.ignore();

    if (index < 0 || index >= taskCount) {
        cout << "Invalid task index.\n";
        return;
    }

    for (int i = index; i < taskCount - 1; ++i) {
        tasks[i] = tasks[i + 1];
    }

    --taskCount;

    // Save the updated tasks to the file
    ofstream outputFile("tasks.txt");
    if (outputFile.is_open()) {
        for (int i = 0; i < taskCount; ++i) {
            outputFile << tasks[i].title << "," << tasks[i].priority << ","
                       << tasks[i].description << "," << tasks[i].datetime << "\n";
        }
        outputFile.close();
    }
    else {
        cout << "Error opening file for writing.\n";
    }
}

void editTask(Task tasks[], int taskCount) {
    if (taskCount == 0) {
        cout << "No tasks to edit.\n";
        return;
    }

    int index;
    cout << "Enter the index of the task to edit (0-" << taskCount - 1 << "): ";
    cin >> index;
    cin.ignore();

    if (index < 0 || index >= taskCount) {
        cout << "Invalid task index.\n";
        return;
    }

    Task& task = tasks[index];

    cout << "Enter new task title: ";
    cin.getline(task.title, sizeof(task.title));

    cout << "Enter new task priority: ";
    cin >> task.priority;
    cin.ignore();

    cout << "Enter new task description: ";
    cin.getline(task.description, sizeof(task.description));

    cout << "Enter new task date and time: ";
    cin.getline(task.datetime, sizeof(task.datetime));

    // Save the updated tasks to the file
    ofstream outputFile("tasks.txt");
    if (outputFile.is_open()) {
        for (int i = 0; i < taskCount; ++i) {
            outputFile << tasks[i].title << "," << tasks[i].priority << ","
                       << tasks[i].description << "," << tasks[i].datetime << "\n";
        }
        outputFile.close();
    }
    else {
        cout << "Error opening file for writing.\n";
    }
}

void searchTasks(const Task tasks[], int taskCount) {
    char keyword[100];

    cout << "Enter the keyword to search: ";
    cin.ignore();
    cin.getline(keyword, sizeof(keyword));

    bool found = false;

    cout << "Search results:\n";
    for (int i = 0; i < taskCount; ++i) {
        if (strstr(tasks[i].title, keyword) != nullptr ||
            strstr(tasks[i].description, keyword) != nullptr ||
            strstr(tasks[i].datetime, keyword) != nullptr) {
            cout << "Task " << i << ":\n";
            cout << "Title: " << tasks[i].title << "\n";
            cout << "Priority: " << tasks[i].priority << "\n";
            cout << "Description: " << tasks[i].description << "\n";
            cout << "Date and Time: " << tasks[i].datetime << "\n";
            cout << "\n";
            found = true;
        }
    }

    if (!found) {
        cout << "No tasks found.\n";
    }
}

void displayTasks(const Task tasks[], int taskCount) {
    if (taskCount == 0) {
        cout << "No tasks to display.\n";
        return;
    }

    cout << "Task list:\n";
    for (int i = 0; i < taskCount; ++i) {
        cout << "Task " << i << ":\n";
        cout << "Title: " << tasks[i].title << "\n";
        cout << "Priority: " << tasks[i].priority << "\n";
        cout << "Description: " << tasks[i].description << "\n";
        cout << "Date and Time: " << tasks[i].datetime << "\n";
        cout << "\n";
    }
}