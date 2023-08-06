#include "FinanceManager.h"

void FinanceManager::addAccount(Account *account) {
    accounts.push_back(account);
}

void FinanceManager::depositToAccount(int accountIndex, double amount) {
    if (accountIndex >= 0 && accountIndex < accounts.size()) {
        accounts[accountIndex]->deposit(amount);
    } else {
        throw runtime_error("Invalid account index");
    }
}

void FinanceManager::addExpense(int accountIndex, double amount, const string& category, int cardIndex, const string& date) {
    if (accountIndex >= 0 && accountIndex < accounts.size() && cardIndex >= 0 && cardIndex < accounts.size()) {
        if (amount <= 0) {
            throw invalid_argument("Expense amount must be greater than 0");
        }
        Transaction transaction(amount, category, accounts[cardIndex]->getType(), date);
        accounts[cardIndex]->deduct(amount);
        expenses.push_back(transaction);
    } else {
        throw runtime_error("Invalid account or card index");
    }
}
void FinanceManager::saveReportsToFile(const string &filename) {
    ofstream file(filename);
    if (file.is_open()) {
        file << "Expense Report:\n";
        for (const Transaction& expense : expenses) {
            file << "Category: " << expense.getCategory() << "\tAmount: " << expense.getAmount() << "\tAccount Type: " << expense.getAccountType() << "\tDate: " << expense.getDate() << "\n";
        }

        file.close();
        cout << "Reports saved to " << filename << " successfully.\n";
    } else {
        cerr << "Error: Unable to open file " << filename << " for writing.\n";
    }
}

void FinanceManager::generateDailyReport(const string& targetDate, const string& filename) {
    ofstream file(filename, ios::app); // Open the file in append mode
    if (file.is_open()) {
        file << "Daily Report (" << targetDate << "):\n";

        for (const Transaction& expense : expenses) {
            if (targetDate == expense.getDate()) {
                file << "Category: " << expense.getCategory() << "\tAmount: " << expense.getAmount() << "\n";
            }
        }

        file.close();
        cout << "Daily report for " << targetDate << " saved to " << filename << " successfully.\n";
    } else {
        cerr << "Error: Unable to open file " << filename << " for writing.\n";
    }
}

void FinanceManager::generateWeeklyReport(const string& startDate, const string& endDate, const string& filename) {
    ofstream file(filename, ios::app);
    if (file.is_open()) {
        file << "Weekly Report (" << startDate << " - " << endDate << "):\n";

        for (const Transaction& expense : expenses) {
            if (expense.getDate() >= startDate && expense.getDate() <= endDate) {
                file << "Category: " << expense.getCategory() << "\tAmount: " << expense.getAmount() << "\n";
            }
        }

        file.close();
        cout << "Weekly report for " << startDate << " to " << endDate << " saved to " << filename << " successfully.\n";
    } else {
        cerr << "Error: Unable to open file " << filename << " for writing.\n";
    }
}

void FinanceManager::generateMonthlyReport(const string& targetMonth, const string& targetYear, const string& filename) {
    ofstream file(filename, ios::app);
    if (file.is_open()) {
        file << "Monthly Report (" << targetMonth << "/" << targetYear << "):\n";

        for (const Transaction& expense : expenses) {
            if (expense.getDate().substr(3, 7) == targetMonth + "/" + targetYear) {
                file << "Category: " << expense.getCategory() << "\tAmount: " << expense.getAmount() << "\n";
            }
        }

        file.close();
        cout << "Monthly report for " << targetMonth << "/" << targetYear << " saved to " << filename << " successfully.\n";
    } else {
        cerr << "Error: Unable to open file " << filename << " for writing.\n";
    }
}

void FinanceManager::calculateTop3(const string& filename) {
    ofstream file(filename);
    if (!file.is_open()) {
        cerr << "Error: Unable to open file " << filename << " for writing.\n";
        return;
    }

    vector<pair<string, double>> categoriesTotal;

    for (const Transaction& expense : expenses) {
        bool found = false;
        for (auto& pair : categoriesTotal) {
            if (pair.first == expense.getCategory()) {
                pair.second += expense.getAmount();
                found = true;
                break;
            }
        }
        if (!found) {
            categoriesTotal.emplace_back(expense.getCategory(), expense.getAmount());
        }
    }

   sort(categoriesTotal.begin(), categoriesTotal.end(),
              [](const pair<string, double>& a, const pair<string, double>& b) {
                  return a.second > b.second;
              });

    cout << "\nTOP 3 Expenses/Categories:\n";
    file << "\nTOP 3 Expenses/Categories:\n";

    for (size_t i = 0; i < categoriesTotal.size() && i < 3; ++i) {
        cout << "Category: " << categoriesTotal[i].first << "\tTotal Amount: " << categoriesTotal[i].second << "\n";
        file << "Category: " << categoriesTotal[i].first << "\tTotal Amount: " << categoriesTotal[i].second << "\n";
    }

    file.close();
}

void FinanceManager::calculateTop3Daily(const string& filename, const string& targetDate) {
    ofstream file(filename);
    if (!file.is_open()) {
        cerr << "Error: Unable to open file " << filename << " for writing.\n";
        return;
    }

    vector<pair<string, double>> categoriesTotal;

    for (const Transaction& expense : expenses) {
        if (targetDate == expense.getDate()) {
            bool found = false;
            for (auto& pair : categoriesTotal) {
                if (pair.first == expense.getCategory()) {
                    pair.second += expense.getAmount();
                    found = true;
                    break;
                }
            }
            if (!found) {
                categoriesTotal.emplace_back(expense.getCategory(), expense.getAmount());
            }
        }
    }

    sort(categoriesTotal.begin(), categoriesTotal.end(),
         [](const pair<string, double>& a, const pair<string, double>& b) {
             return a.second > b.second;
         });

    cout << "\nTOP 3 Expenses/Categories (Daily):\n";
    file << "\nTOP 3 Expenses/Categories (Daily):\n";

    for (size_t i = 0; i < categoriesTotal.size() && i < 3; ++i) {
        cout << "Category: " << categoriesTotal[i].first << "\tTotal Amount: " << categoriesTotal[i].second << "\n";
        file << "Category: " << categoriesTotal[i].first << "\tTotal Amount: " << categoriesTotal[i].second << "\n";
    }

    file.close();
}

void FinanceManager::calculateTop3Weekly(const string& filename, const string& startDate, const string& endDate) {
    ofstream file(filename);
    if (!file.is_open()) {
        cerr << "Error: Unable to open file " << filename << " for writing.\n";
        return;
    }

    vector<pair<string, double>> categoriesTotal;

    for (const Transaction& expense : expenses) {
        if (expense.getDate() >= startDate && expense.getDate() <= endDate) {
            bool found = false;
            for (auto& pair : categoriesTotal) {
                if (pair.first == expense.getCategory()) {
                    pair.second += expense.getAmount();
                    found = true;
                    break;
                }
            }
            if (!found) {
                categoriesTotal.emplace_back(expense.getCategory(), expense.getAmount());
            }
        }
    }

    sort(categoriesTotal.begin(), categoriesTotal.end(),
         [](const pair<string, double>& a, const pair<string, double>& b) {
             return a.second > b.second;
         });

    cout << "\nTOP 3 Expenses/Categories (Weekly):\n";
    file << "\nTOP 3 Expenses/Categories (Weekly):\n";

    for (size_t i = 0; i < categoriesTotal.size() && i < 3; ++i) {
        cout << "Category: " << categoriesTotal[i].first << "\tTotal Amount: " << categoriesTotal[i].second << "\n";
        file << "Category: " << categoriesTotal[i].first << "\tTotal Amount: " << categoriesTotal[i].second << "\n";
    }

    file.close();
}

void FinanceManager::calculateTop3Monthly(const string& filename, const string& targetMonth, const string& targetYear) {
    ofstream file(filename);
    if (!file.is_open()) {
        cerr << "Error: Unable to open file " << filename << " for writing.\n";
        return;
    }

    vector<pair<string, double>> categoriesTotal;

    for (const Transaction& expense : expenses) {
        if (expense.getDate().substr(3, 7) == targetMonth + "/" + targetYear) {
            bool found = false;
            for (auto& pair : categoriesTotal) {
                if (pair.first == expense.getCategory()) {
                    pair.second += expense.getAmount();
                    found = true;
                    break;
                }
            }
            if (!found) {
                categoriesTotal.emplace_back(expense.getCategory(), expense.getAmount());
            }
        }
    }

    sort(categoriesTotal.begin(), categoriesTotal.end(),
         [](const pair<string, double>& a, const pair<string, double>& b) {
             return a.second > b.second;
         });

    cout << "\nTOP 3 Expenses/Categories (Monthly):\n";
    file << "\nTOP 3 Expenses/Categories (Monthly):\n";

    for (size_t i = 0; i < categoriesTotal.size() && i < 3; ++i) {
        cout << "Category: " << categoriesTotal[i].first << "\tTotal Amount: " << categoriesTotal[i].second << "\n";
        file << "Category: " << categoriesTotal[i].first << "\tTotal Amount: " << categoriesTotal[i].second << "\n";
    }

    file.close();
}
