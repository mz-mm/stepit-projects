#include "Card.h"

Card::Card(const std::string &type): Account(type) {}

void Card::deduct(double amount) {
    balance -= amount;
}