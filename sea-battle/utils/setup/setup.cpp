#include "setup.h"
using namespace std;

bool isAdjacentOccupied(Game::GameMap* player, int x, int y) {
    // Check if any adjacent positions are occupied by a ship
    for (int i = -1; i <= 1; i++) {
        for (int j = -1; j <= 1; j++) {
            int posX = x + i;
            int posY = y + j;

            if (posX >= 0 && posX < s_BordWidth && posY >= 0 && posY < s_BordHeight) {
                if (player->map[posY][posX] == ShipPosition) {
                    return true;
                }
            }
        }
    }

    return false;
}

void gameSetup(Game &gameConfig) {
    srand(time(NULL));
    cout << "Welcome to the game!" << endl;
    gameConfig.mode = menuUtil("Human vs Computer", "Computer vs Computer");
    if (gameConfig.mode == 1) {
        gameConfig.setup = true;
        gameConfig.difficulty = true;
    }
    else {
        gameConfig.setup = menuUtil("Manual setup", "Auto Setup");
        gameConfig.difficulty = menuUtil("Easy mode", "Hard mode");
    }
}

void mapSetup(Game::GameMap* player, Ship* ships, int shipCount, bool autoSetupMap) {
    char yCoord = 'a';
    int xCoord{};
    Orientation shipOrientation{};
    int orientation{};
    bool shipInPosition{};
    int choice{};
    bool errorOccurred{};

    if (autoSetupMap) {
        for (int i = 0; i < shipCount; i++) {
            for (int j{}; j < ships[i].count; j++) {
                bool shipPlaced = false;

                while (!shipPlaced) {
                    xCoord = rand() % s_BordWidth;
                    yCoord = rand() % s_BordHeight;
                    orientation = rand() % 4;

                    switch (orientation) {
                        case 0:  // Up orientation
                            if (yCoord - ships[i].size >= 0 &&
                                !isAdjacentOccupied(player, xCoord, yCoord - ships[i].size)) {
                                bool validPlacement = true;

                                for (int n = 0; n < ships[i].size; n++) {
                                    if (player->map[yCoord - n][xCoord] == ShipPosition ||
                                        isAdjacentOccupied(player, xCoord, yCoord - n)) {
                                        validPlacement = false;
                                        break;
                                    }
                                }

                                if (validPlacement) {
                                    for (int n = 0; n < ships[i].size; n++) {
                                        player->map[yCoord - n][xCoord] = ShipPosition;
                                    }
                                    shipPlaced = true;
                                }
                            }
                            break;

                        case 1:  // Left orientation
                            if (xCoord - ships[i].size >= 0 &&
                                !isAdjacentOccupied(player, xCoord - ships[i].size, yCoord)) {
                                bool validPlacement = true;

                                for (int n = 0; n < ships[i].size; n++) {
                                    if (player->map[yCoord][xCoord - n] == ShipPosition ||
                                        isAdjacentOccupied(player, xCoord - n, yCoord)) {
                                        validPlacement = false;
                                        break;
                                    }
                                }

                                if (validPlacement) {
                                    for (int n = 0; n < ships[i].size; n++) {
                                        player->map[yCoord][xCoord - n] = ShipPosition;
                                    }
                                    shipPlaced = true;
                                }
                            }
                            break;

                        case 2:  // Right orientation
                            if (xCoord + ships[i].size <= s_BordWidth &&
                                !isAdjacentOccupied(player, xCoord + ships[i].size, yCoord)) {
                                bool validPlacement = true;

                                for (int n = 0; n < ships[i].size; n++) {
                                    if (player->map[yCoord][xCoord + n] == ShipPosition ||
                                        isAdjacentOccupied(player, xCoord + n, yCoord)) {
                                        validPlacement = false;
                                        break;
                                    }
                                }

                                if (validPlacement) {
                                    for (int n = 0; n < ships[i].size; n++) {
                                        player->map[yCoord][xCoord + n] = ShipPosition;
                                    }
                                    shipPlaced = true;
                                }
                            }
                            break;

                        case 3:  // Down orientation
                            if (yCoord + ships[i].size <= s_BordHeight &&
                                !isAdjacentOccupied(player, xCoord, yCoord + ships[i].size)) {
                                bool validPlacement = true;

                                for (int n = 0; n < ships[i].size; n++) {
                                    if (player->map[yCoord + n][xCoord] == ShipPosition ||
                                        isAdjacentOccupied(player, xCoord, yCoord + n)) {
                                        validPlacement = false;
                                        break;
                                    }
                                }

                                if (validPlacement) {
                                    for (int n = 0; n < ships[i].size; n++) {
                                        player->map[yCoord + n][xCoord] = ShipPosition;
                                    }
                                    shipPlaced = true;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }

    else {
        CLEAR_CONSOLE;
        for (int i{}; i < shipCount; i++) {
            for (int j{}; j < ships[i].count; j++) {
                while (true) {
                    errorOccurred = false;
                    shipInPosition = false;
                    player->displayMap(false);
                    cout << "Enter Y coordinate for " << ships[i].name << "(" << j + 1 << ")";
                    cin >> yCoord;

                    if (cin.fail()) {
                        CLEAR_CONSOLE;
                        cin.clear();
                        cin.ignore(numeric_limits<streamsize>::max(), '\n');
                        cout << "Please enter a valid value for coordinate: ";
                        continue;
                    }

                    yCoord = tolower(yCoord);
                    cin.ignore(numeric_limits<streamsize>::max(), '\n');

                    cout << "Enter X coordinate for " << ships[i].name << "(" << j + 1 << ")";
                    cin >> xCoord;

                    if (cin.fail()) {
                        CLEAR_CONSOLE;
                        cin.clear();
                        cin.ignore(numeric_limits<streamsize>::max(), '\n');
                        cout << "ERROR: Please enter a valid value for coordinate: " << endl;
                        continue;
                    }

                    if (yCoord > s_MaxYCoord || xCoord > s_BordWidth || yCoord < s_MinYCoord || xCoord < s_MinXCoord) {
                        CLEAR_CONSOLE;
                        cout << "ERROR: PLease enter a coordinate in range" << endl;
                        continue;
                    }

                    if (player->map[static_cast<int>(yCoord) - 97][xCoord - 1] == ShipPosition) {
                        CLEAR_CONSOLE;
                        cout << "ERROR: Ship already exist in that position" << endl;
                        continue;
                    }

                    // Check if any adjacent positions are occupied by a ship
                    bool adjacentOccupied = isAdjacentOccupied(player, xCoord - 1, static_cast<int>(yCoord) - 97);
                    if (adjacentOccupied) {
                        CLEAR_CONSOLE;
                        cout << "ERROR: Ships cannot be placed adjacent to each other" << endl;
                        continue;
                    }

                    else {
                        xCoord--;
                        while (true) {
                            if (ships[i].size > 1) {
                                cout << "Enter ship orientation:" << endl
                                     << "1 - Up" << endl
                                     << "2 - Left" << endl
                                     << "3 - Right" << endl
                                     << "4 - Down" << endl
                                     << "choice: ";
                                cin >> choice;
                            } else
                                choice = 1;

                            if (cin.fail()) {
                                CLEAR_CONSOLE;
                                cin.clear();
                                cin.ignore(numeric_limits<streamsize>::max(), '\n');
                                cout << "Enter a valid value for orientation" << endl;
                                continue;
                            } else if (choice < 5 && choice > s_MinShipPosition) {
                                CLEAR_CONSOLE;
                                shipOrientation = static_cast<Orientation>(choice);

                                switch (shipOrientation) {
                                    case UP_ORIENTATION:
                                        if ((static_cast<int>(yCoord) - 97) - ships[i].size < 0) {
                                            cout << "ERROR: Ship does not have enough space" << endl;
                                            errorOccurred = true;
                                            break;
                                        } else {
                                            for (int n{}; n < ships[i].size; n++) {
                                                if (player->map[(static_cast<int>(yCoord) - 97) - n][xCoord] == ShipPosition) {
                                                    shipInPosition = true;
                                                    break;
                                                }
                                            }
                                            if (not shipInPosition) {
                                                for (int n{}; n < ships[i].size; n++) {
                                                    player->map[(static_cast<int>(yCoord) - 97) - n][xCoord] = ShipPosition;
                                                }
                                                break;
                                            } else {
                                                cout << "ERROR: Stacking ships is not allowed" << endl;
                                                errorOccurred = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case LEFT_ORIENTATION:
                                        if (xCoord - ships[i].size < s_MinShipPosition) {
                                            cout << "ERROR: Ship does not have enough space" << endl;
                                            errorOccurred = true;
                                            break;
                                        } else {
                                            for (int n{}; n < ships[i].size; n++) {
                                                if (player->map[static_cast<int>(yCoord) - 97][xCoord - n] == ShipPosition) {
                                                    shipInPosition = true;
                                                    break;
                                                }
                                            }
                                            if (not shipInPosition) {
                                                for (int n{}; n < ships[i].size; n++) {
                                                    player->map[static_cast<int>(yCoord) - 97][xCoord - n] = ShipPosition;
                                                }
                                                break;
                                            } else {
                                                cout << "ERROR: Stacking ships is not allowed" << endl;
                                                errorOccurred = true;
                                                break;
                                            }
                                        }
                                        break;

                                    case RIGHT_ORIENTATION:
                                        if (xCoord + ships[i].size > s_BordWidth) {
                                            cout << "ERROR: Ship does not have enough space" << endl;
                                            errorOccurred = true;
                                            break;
                                        } else {
                                            for (int n{}; n < ships[i].size; n++) {
                                                if (player->map[static_cast<int>(yCoord) - 97][xCoord + n] == ShipPosition) {
                                                    shipInPosition = true;
                                                    break;
                                                }
                                            }
                                            if (not shipInPosition) {
                                                for (int n{}; n < ships[i].size; n++) {
                                                    player->map[static_cast<int>(yCoord) - 97][xCoord + n] = ShipPosition;
                                                }
                                                break;
                                            } else {
                                                cout << "ERROR: Stacking ships is not allowed" << endl;
                                                errorOccurred = true;
                                                break;
                                            }
                                        }
                                    case DOWN_ORIENTATION:
                                        if (static_cast<int>(yCoord - 97) + ships[i].size > s_BordHeight) {
                                            cout << "ERROR: Ship does not have enough space" << endl;
                                            errorOccurred = true;
                                            break;
                                        } else {
                                            for (int n{}; n < ships[i].size; n++) {
                                                if (player->map[(static_cast<int>(yCoord) - 97) + n][xCoord] == ShipPosition) {
                                                    shipInPosition = true;
                                                    break;
                                                }
                                            }
                                            if (not shipInPosition) {
                                                for (int n{}; n < ships[i].size; n++) {
                                                    player->map[(static_cast<int>(yCoord) - 97) + n][xCoord] = ShipPosition;
                                                }
                                                break;
                                            } else {
                                                cout << "ERROR: Stacking ships is not allowed" << endl;
                                                errorOccurred = true;
                                                break;
                                            }
                                        }
                                }
                                break;
                            } else {
                                CLEAR_CONSOLE;
                                cout << "ERROR: Invalid orientation" << endl;
                                errorOccurred = true;
                                break;
                            }
                        }
                    }
                    if (errorOccurred) {
                        continue;
                    } else break;
                }
            }
        }
    }
}