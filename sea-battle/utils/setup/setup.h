#ifndef SEA_BATTLE_SETUP_H
#define SEA_BATTLE_SETUP_H

#include "iostream"
#include "../game/gameConfig.h"
#include "../ship/ship.h"
#include "../util.h"
#include "../menu-util/menu-util.h"
#include <cstdlib>
#include <ctime>

enum Orientation {
    UP_ORIENTATION= 1,
    LEFT_ORIENTATION = 2,
    RIGHT_ORIENTATION = 3,
    DOWN_ORIENTATION = 4
};


static const uint8_t s_MinXCoord = 1;
static const uint8_t s_MinYCoord = 97;
static const uint8_t s_MaxYCoord = 96 + s_BordHeight;
static const uint8_t s_MinShipPosition = 0;
bool isAdjacentOccupied(Game::GameMap* player, int x, int y);
void gameSetup(Game &gameConfig);
void mapSetup(Game::GameMap* player, Ship* ships, int shipCount, bool autoSetupMap = false);

#endif
