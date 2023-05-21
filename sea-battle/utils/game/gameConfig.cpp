#include <iostream>
#include "gameConfig.h"

using namespace std;

void Game::GameMap::displayMap(bool showEnemy) const {
    cout << "Your map";
    if(showEnemy) {
        cout << "\t\t\t\t" << "Enemy Map";
    }
    cout << endl;

    cout << "  ";
    for (int n{}; n < s_BordWidth; n++) {
        cout << n+1 << "  ";
    }
    if(showEnemy){
        cout << "\t  ";
        for (int n{}; n < s_BordWidth; n++) {
            cout << n+1 << "  ";
        }
    }

    cout << endl;

    for (int i{}; i < s_BordHeight; i++) {
        // Improve readability on the letter display board
        char c = 'a';
        c += i;
        cout << c << " ";
        for (int j{}; j < s_BordWidth; j++) {
            if (map[i][j] == Empty) {
                cout << "\u2B1C" << "  ";
            }
            else if (map[i][j] == ShipPosition) {
                cout << "\u2B1B" << "  ";
            }
            else if (map[i][j] == ShotOnTarget) {
                cout << "\U0001F7E5" << " ";
            }
        }

        if (showEnemy) {
            cout << "\t";
            cout << c << " ";

            for (int j{}; j < s_BordWidth; j++) {
                if (enemyMap[i][j] == Empty) {
                    cout << "\u2B1C" << "  ";
                }
                else if (enemyMap[i][j] == ShipPosition) {
                    cout << "\u2B1B" << "  ";
                }
                else if (enemyMap[i][j] == ShotOnTarget) {
                    cout << "\U0001F7E5" << " ";
                }
                else if (enemyMap[i][j] == MissedShot) {
                    cout << "\u274C" << "  ";
                }
            }
        }
        cout << endl;
    }
}


Game::GameMap::GameMap() {
    for (int i{}; i < s_BordHeight; i++) {
        map[i] = new PositionState[s_BordWidth]{};
        enemyMap[i] = new PositionState[s_BordWidth]{};
    }
}


Game::GameMap::~GameMap() {
    for (int i = 0; i < s_BordHeight; ++i) {
        delete[] map[i];
        delete[] enemyMap[i];
    }
    delete[] map;
    delete[] enemyMap;
}