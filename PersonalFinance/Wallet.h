#ifndef PERSONALFINANCE_WALLET_H
#define PERSONALFINANCE_WALLET_H

#include "Account.h"
#include <exception>

class Wallet : public Account {
public:
    Wallet(const std::string& type);

    void deduct(double amount) override;
};


#endif
