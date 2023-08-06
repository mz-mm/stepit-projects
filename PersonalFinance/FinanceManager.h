#ifndef PERSONALFINANCE_FINANCEMANAGER_H
#define PERSONALFINANCE_FINANCEMANAGER_H

#include <iostream>
#include <vector>
#include <fstream>

#include "Transaction.h"
#include "Account.h"
#include "Wallet.h"
#include "Card.h"

using namespace std;

class FinanceManager {
public:
    void addAccount(Account* account);

    void depositToAccount(int accountIndex, double amount);

    void addExpense(int accountIndex, double amount, const string& category, int cardIndex, const string& date);

    void saveReportsToFile(const string& filename);

    void generateDailyReport(const string& targetDate, const string& filename);

    void generateWeeklyReport(const string& startDate, const string& endDate, const string& filename);

    void generateMonthlyReport(const string& targetMonth, const string& targetYear, const string& filename);

    void calculateTop3(const string& filename);

    void calculateTop3Daily(const string& filename, const string& targetDate);
    void calculateTop3Weekly(const string& filename, const string& startDate, const string& endDate);
    void calculateTop3Monthly(const string& filename, const string& targetMonth, const string& targetYear);

private:
    vector<Account*> accounts;
    vector<Transaction> expenses;
};


#endif