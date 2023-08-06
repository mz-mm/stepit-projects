#include "Wallet.h"

Wallet::Wallet(const std::string &type): Account(type) {}

void Wallet::deduct(double amount) {
    if (amount <= getBalance()) {
        balance -= amount;
    } else {
        throw out_of_range("Insufficient funds");
    }
}