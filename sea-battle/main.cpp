#include "utils/ship/ship.h"
#include "utils/game/gameConfig.h"
#include "utils/setup/setup.h"
#include "utils/game/game.h"

int main() {
    const int shipCount = 5;

    Game gameConfig{};
    Ship ships[shipCount]{};

    auto* playerOne = new Game::GameMap();
    auto* playerTwo = new Game::GameMap();

    shipInit(ships);
    gameSetup(gameConfig);
    mapSetup(playerOne, ships, shipCount, gameConfig.setup);
    mapSetup(playerTwo, ships, shipCount, true);
    startGame(gameConfig, playerOne, playerTwo);

    delete playerOne;
    delete playerTwo;

    return 0;
}
