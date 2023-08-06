#include "Transaction.h"

Transaction::Transaction(double amount, const std::string &category, const std::string &accountType,
                         const std::string &date): amount(amount), category(category), accountType(accountType), date(date) {}

double Transaction::getAmount() const {
    return amount;
}

string Transaction::getCategory() const {
    return category;
}

string Transaction::getAccountType() const {
    return accountType;
}

string Transaction::getDate() const {
    return date;
}