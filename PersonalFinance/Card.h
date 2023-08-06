#ifndef PERSONALFINANCE_CARD_H
#define PERSONALFINANCE_CARD_H

#include "Wallet.h"

class Card : public Account {
public:
    Card(const std::string& type);

    void deduct(double amount) override;
};


#endif
