#ifndef PERSONALFINANCE_TRANSACTION_H
#define PERSONALFINANCE_TRANSACTION_H

#include <string>
using namespace std;

class Transaction {
public:
    Transaction(double amount, const string& category, const string& accountType, const string& date);

    double getAmount() const;

    string getCategory() const;

    string getAccountType() const;

    string getDate() const;

private:
    double amount;
    string category;
    string accountType;
    string date;
};



#endif
