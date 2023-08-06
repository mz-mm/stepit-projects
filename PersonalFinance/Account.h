#ifndef PERSONALFINANCE_ACCOUNT_H
#define PERSONALFINANCE_ACCOUNT_H

#include <string>
using namespace std;

class Account {
public:

    explicit Account(const string& type);

    void deposit(double amount);

    double getBalance() const;

    virtual void deduct(double amount) = 0;

    string getType() const;

protected:
    string type;
    double balance;
};

#endif
