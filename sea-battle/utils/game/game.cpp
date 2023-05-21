#include "game.h"
#include "chrono"
#include "thread"

using namespace std;

bool isGameOver(Game::GameMap* player){
    for (int i{}; i < s_BordHeight; i++) {
        for (int j{}; j < s_BordWidth; j++) {
            if (player->map[i][j] == ShipPosition) {
                return false;
            }
        }
    }
    return true;
}

void autoShoot(Game::GameMap* player, Game::GameMap* enemyPLayer, int shootCoordinateX, char shootCoordinateY){
    this_thread::sleep_for(chrono::seconds(2));
    bool validShot = false;

    while (!validShot) {
        shootCoordinateX = rand() % s_BordWidth + 1;
        shootCoordinateY = rand() % s_BordHeight + 97;

        if (player->enemyMap[shootCoordinateY - s_MinYCoord][shootCoordinateX - s_MinXCoord] == Empty) {
            validShot = true;
        }
    }

    if (enemyPLayer->map[shootCoordinateY - s_MinYCoord][shootCoordinateX - s_MinXCoord] == ShipPosition) {
        cout << "Hit" << endl;
        player->enemyMap[shootCoordinateY - s_MinYCoord][shootCoordinateX - s_MinXCoord] = ShotOnTarget;
        enemyPLayer->map[shootCoordinateY - s_MinYCoord][shootCoordinateX - s_MinXCoord] = ShotOnTarget;
    } else {
        cout << "Miss" << endl;
        player->enemyMap[shootCoordinateY - s_MinYCoord][shootCoordinateX - s_MinXCoord] = MissedShot;
    }
}

void startGame(Game gameConfig, Game::GameMap* playerOne, Game::GameMap* playerTwo){
    char shootCoordinateY = 'a';
    int shootCoordinateX{};
    srand(time(NULL));

    CLEAR_CONSOLE;
    cout << "Game started" << endl;
    while (true) {
        if (isGameOver(playerOne)) {
            cout << "Player two won" << endl;
            break;
        }


        if (gameConfig.mode == 1) {
            cout << "Player one turn" << endl;
            autoShoot(playerOne, playerTwo, shootCoordinateX, shootCoordinateY);
            if (isGameOver(playerTwo)) {
                cout << "Player one won" << endl;
                break;
            }

        } else {
            cout << "Your turn" << endl;

            playerOne->displayMap(true);
            cout << "Enter Y coordinate to shoot: ";
            cin >> shootCoordinateY;
            cin.ignore(numeric_limits<streamsize>::max(), '\n');

            cout << "Enter X coordinate to shoot: ";
            cin >> shootCoordinateX;

            while (true) {
                if (cin.fail()) {
                    cin.clear();
                    cin.ignore(numeric_limits<streamsize>::max(), '\n');
                    cout << "Enter X coordinate to shoot: ";
                    cin >> shootCoordinateX;
                    continue;
                }
                else break;
            }

            while (true) {
                if (shootCoordinateX < s_MinXCoord || shootCoordinateX > s_BordWidth || shootCoordinateY < s_MinYCoord ||
                    shootCoordinateY > s_MaxYCoord) {
                    cout << "Invalid coordinate" << endl;
                    cout << "Invalid coordinate or already shot, please enter again Y: ";
                    cin >> shootCoordinateY;
                    cin.ignore(numeric_limits<streamsize>::max(), '\n');

                    cout << "Enter X coordinate to shoot: ";
                    cin >> shootCoordinateX;
                    if (cin.fail()) {
                        cin.clear();
                        cin.ignore(numeric_limits<streamsize>::max(), '\n');
                        cout << "Invalid coordinate" << endl;
                    }
                    continue;
                }

                else break;
            }

            // Check if the coordinate is a hit or miss
            if (playerTwo->map[shootCoordinateY - s_MinYCoord][shootCoordinateX - s_MinXCoord] == ShipPosition) {
                cout << "Hit" << endl;
                playerOne->enemyMap[shootCoordinateY - s_MinYCoord][shootCoordinateX - s_MinXCoord] = ShotOnTarget;
            } else {
                cout << "Miss" << endl;
                playerOne->enemyMap[shootCoordinateY - s_MinYCoord][shootCoordinateX - s_MinXCoord] = MissedShot;
            }
        }
        if (isGameOver(playerTwo)) {
            cout << "Player one won" << endl;
            break;
        }

        // Player two turn
        CLEAR_CONSOLE;
        cout << "Player two turn" << endl;
        autoShoot(playerTwo, playerOne, shootCoordinateX, shootCoordinateY);
        if (gameConfig.mode == 1) {
            playerOne->displayMap();
        }
    }
    cout << "Game ended" << endl;
}
