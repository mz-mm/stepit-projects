#include "Account.h"

Account::Account(const string &type): type(type), balance(0.0) {}

void Account::deposit(double amount) {
    balance += amount;
}

double Account::getBalance() const {
    return balance;
}

string Account::getType() const {
    return type;
}