#ifndef SEA_BATTLE_GAMECONFIG_H
#define SEA_BATTLE_GAMECONFIG_H

#include <iostream>
#include "../util.h"

static const uint8_t s_BordHeight = 10; // Creating only one instance of it across all instances of this game class (static)
static const uint8_t s_BordWidth = 10;

enum PositionState {
    Empty = 0,
    ShipPosition = 1,
    MissedShot = 2,
    ShotOnTarget = 4,
};

struct Game {
    bool mode{};
    bool setup{};
    bool difficulty{};

    struct GameMap {
        PositionState** map = new PositionState*[s_BordHeight]{};
        PositionState** enemyMap = new PositionState*[s_BordHeight]{};

        GameMap(); // Constructor
        ~GameMap(); // Destructor automatically deallocates memory

        void displayMap(bool showEnemy = true) const;
    };
};

#endif
