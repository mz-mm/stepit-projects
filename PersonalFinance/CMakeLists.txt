cmake_minimum_required(VERSION 3.26)
project(PersonalFinance)

set(CMAKE_CXX_STANDARD 20)

add_executable(PersonalFinance main.cpp
        Transaction.cpp
        Transaction.h
        Account.cpp
        Account.h
        Wallet.cpp
        Wallet.h
        Card.cpp
        Card.h
        FinanceManager.cpp
        FinanceManager.h
)
