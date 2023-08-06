#include "Wallet.h"
#include "Card.h"
#include "FinanceManager.h"
#include <exception>

int main() {
    FinanceManager manager;

    Wallet wallet("Debit Wallet");
    Card card("Credit Card");

    manager.addAccount(&wallet);
    manager.addAccount(&card);

    manager.depositToAccount(0, 1000.0);
    manager.depositToAccount(1, 500.0);

    try {
        manager.addExpense(0, 200.0, "Groceries", 1, "01/08/2023");
        manager.addExpense(0, 20.0, "Entertainment", 0, "01/09/2023");
        manager.addExpense(0, 50.0, "Food", 0, "01/09/2023");
        manager.addExpense(0, 10.0, "Entertainment", 0, "01/09/2023");

        manager.saveReportsToFile("finance_reports.txt");
        manager.generateDailyReport("01/08/2023", "daily_report.txt");
        manager.calculateTop3("top_3_expense.txt");
        manager.calculateTop3Daily("top_3_daily_expense", "01/09/2023");

    } catch (const exception& e) {
        cerr << "An error occurred: " << e.what() << endl;
    }

    return 0;
}