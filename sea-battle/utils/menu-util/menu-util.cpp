#include <iostream>
#include "menu-util.h"
using namespace std;


bool menuUtil(char* optionOne, char* optionTwo) {
    int choice{};
    while (true) {
        cout << "1 - " << optionOne << endl
             << "2 - " << optionTwo << endl;
        cin >> choice;

        if (cin.fail()) {
            CLEAR_CONSOLE;
            cout << "Error, please enter a valid choice" << endl;
            cin.clear();
            cin.ignore(numeric_limits<streamsize>::max(), '\n');
        }
        else if (choice < 3 && choice > 0) {
            break;
        }
        else {
            CLEAR_CONSOLE;
            cout << "Error, please enter a valid choice" << endl;
            cin.ignore(numeric_limits<streamsize>::max(), '\n');
        }
    }
    CLEAR_CONSOLE;
    return --choice;
}