#ifndef SEA_BATTLE_GAME_H
#define SEA_BATTLE_GAME_H

#include "iostream"
#include "../game/gameConfig.h"
#include "../ship/ship.h"
#include "../util.h"
#include "../menu-util/menu-util.h"
#include "../setup/setup.h"
#include <cstdlib>
#include <ctime>

bool isGameOver(Game::GameMap* player);
void autoShoot(Game::GameMap* player, Game::GameMap* enemyPLayer, int shootCoordinateX, char shootCoordinateY);
void startGame(Game gameConfig, Game::GameMap* playerOne, Game::GameMap* playerTwo);

#endif
